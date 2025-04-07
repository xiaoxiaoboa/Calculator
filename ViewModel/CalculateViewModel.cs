using System.ComponentModel;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Input;
using Calculator.Interfaces;
using Calculator.MVVM;
using Calculator.Utils;

namespace Calculator.ViewModel;

public class CalculateViewModel : ViewModelBase {
    #region 字段

    private readonly IDisplayViewModel _displayViewModel;

    // 是否已经输入操作符
    private bool _isHaveOperator;

    // 输入操作符
    private char _operator;

    // 标识是否输出第二个数字
    private bool IsNewInput{ get; set; } = true;

    private static readonly Dictionary<char, (Func<decimal, decimal?, decimal> Operation, char Symbol)>
        OperatorMap =
            new() {
                { Constants.Addition, ((n1, n2) => n1 + n2 ?? 0, Constants.Addition) },
                { Constants.Subtractive, ((n1, n2) => n1 - n2 ?? 0, Constants.Subtractive) },
                { Constants.Multiplication, ((n1, n2) => n1 * n2 ?? 1, Constants.Multiplication) }, // 数学乘号
                { Constants.Division, ((n1, n2) => n2 == null ? n1 : n1 / n2.Value, Constants.Division) }, // 数学除号
                { Constants.Percent, ((n, n2) => n * 0.01m, Constants.Percent) },
            };

    #endregion


    #region 构造函数

    public CalculateViewModel(DisplayViewModel displayViewModel) {
        _displayViewModel = displayViewModel;

        HandleInput = new RelayCommand(ExecuteInputNumber);
        HandleClear = new RelayCommand(ExecuteClear);
        HandleBackSpace = new RelayCommand(ExecuteBackSpace);
        HandleNegative = new RelayCommand(ExecuteNegative);
        HandleOperator = new RelayCommand(ExecuteOperator);
        HandleDecimalPoint = new RelayCommand(ExecuteDecimalPoint);
        HandleCalc = new RelayCommand(ExecuteCalc);
        HandlePercentCalc = new RelayCommand(ExecutePercentCalc);
        HandleSquareCalc = new RelayCommand(ExecuteSquareCalc);
        HandleSquareRootCalc = new RelayCommand(ExecuteSquareRootCalc);
        HandleOneInXCalc = new RelayCommand(ExecuteOneInXCalc);
    }

    #endregion

    #region 事件声明

    public ICommand HandleInput{ get; }
    public ICommand HandleClear{ get; }
    public ICommand HandleBackSpace{ get; }
    public ICommand HandleNegative{ get; }
    public ICommand HandleOperator{ get; }
    public ICommand HandleDecimalPoint{ get; }
    public ICommand HandleCalc{ get; }
    public ICommand HandlePercentCalc{ get; }

    public ICommand HandleSquareCalc{ get; }
    public ICommand HandleSquareRootCalc{ get; }
    public ICommand HandleOneInXCalc{ get; }

    #endregion

    #region 属性

    private char Operator{
        get => _operator;
        set {
            _operator = value;
            OnPropertyChanged();

            // 该方法已经弃用
            // AdjustFontSize();
        }
    }

    #endregion


    #region 按钮事件

    private void ExecuteInputNumber(object? parameter = null) {
        if (IsNewInput) {
            if (parameter?.ToString() == _displayViewModel.Result) return;
            _displayViewModel.Result = parameter?.ToString() ?? "";
            IsNewInput = false;
        }
        else if (_displayViewModel.Result.Length <= _displayViewModel.MaxDigitCount) {
            _displayViewModel.Result += parameter?.ToString() ?? "";
        }
    }

    private void ExecuteOperator(object? parameter = null) {
        var c = Convert.ToChar(parameter);
        Operator = c;
        _isHaveOperator = true;
        IsNewInput = true;
        _displayViewModel.ResultCache = _displayViewModel.Result;
        _displayViewModel.Expression = _displayViewModel.Result + Constants.LittleBlankSpace + Operator;
    }

    private void ExecuteClear(object? parameter = null) {
        // 清空后恢复初始值
        _displayViewModel.Result = Constants.InitializationString;
        _displayViewModel.Expression = string.Empty;
        Operator = Constants.DefaultChar;
        _isHaveOperator = false;
        IsNewInput = true;
    }

    private void ExecuteBackSpace(object? parameter = null) {
        if (_isHaveOperator) return;
        if (_displayViewModel.Result.Length > 2 ||
            (_displayViewModel.Result[0] != Constants.Subtractive &&
             _displayViewModel.Result.Length == 2)) // 如果长度大于1，删除最后一个字符
        {
            _displayViewModel.Result = _displayViewModel.Result.Remove(_displayViewModel.Result.Length - 1, 1);
        }
        else if (_displayViewModel.Result.Length == 2 && _displayViewModel.Result[0] == Constants.Subtractive ||
                 _displayViewModel.Result.Length < 2 &&
                 _displayViewModel.Result[0] != Convert.ToChar(Constants.InitializationString)) {
            _displayViewModel.Result = Constants.InitializationString;
        }
    }

    private void ExecuteNegative(object? parameter = null) {
        _displayViewModel.Result = !_displayViewModel.Result.Contains(Constants.Subtractive)
            ? _displayViewModel.Result.Insert(0, Constants.Subtractive.ToString())
            : _displayViewModel.Result.Remove(0, 1);
    }

    private void ExecuteDecimalPoint(object? parameter = null) {
        if (!_displayViewModel.Result.Contains(Constants.Dot)) {
            _displayViewModel.Result += parameter;
            IsNewInput = false;
        }
    }

    private void ExecutePercentCalc(object? parameter = null) {
        ExecuteOperator(parameter);
        ExecuteCalc();
    }

    private void ExecuteSquareCalc(object? parameter = null) {
        if (double.TryParse(_displayViewModel.Result, out double n)) {
            _displayViewModel.Result = Math.Pow(n, 2).ToString(CultureInfo.CurrentCulture);
            _displayViewModel.Expression = $"sqr({n})";

            IsNewInput = true;
        }
    }

    private void ExecuteSquareRootCalc(object? parameter = null) {
        if (double.TryParse(_displayViewModel.Result, out double n)) {
            _displayViewModel.Result = Math.Sqrt(n).ToString(CultureInfo.CurrentCulture);
            _displayViewModel.Expression = $"{Constants.RadicalSign}({n})";

            IsNewInput = true;
        }
    }

    private void ExecuteOneInXCalc(object? parameter = null) {
        if (double.TryParse(_displayViewModel.Result, out double n)) {
            _displayViewModel.Result = (1 / n).ToString(CultureInfo.CurrentCulture);
            _displayViewModel.Expression = $"1{Constants.LittleBlankSpace}/{Constants.LittleBlankSpace}({n})";

            IsNewInput = true;
        }
    }

    #endregion


    #region 计算事件

    public void ExecuteCalc(object? parameter = null) {
        IsNewInput = true;
        // 解析数字
        if (!decimal.TryParse(_displayViewModel.Result, out decimal resultNumber) ||
            !decimal.TryParse(_displayViewModel.ResultCache, out decimal cacheNumber)) {
            _displayViewModel.Result = "Error";
            _displayViewModel.Expression = "Invalid Input";
            return;
        }

        // 检查运算符是否有效
        if (!OperatorMap.TryGetValue(Operator, out var operationInfo)) {
            _displayViewModel.Result = "Invalid Operator";
            // Expression = "Invalid Operator";
            return;
        }

        try {
            // 构建表达式
            string formattedExpression;
            if (operationInfo.Symbol is Constants.Addition or Constants.Subtractive or Constants.Multiplication
                or Constants.Division) {
                formattedExpression =
                    $"{cacheNumber}{Constants.LittleBlankSpace}{operationInfo.Symbol}{Constants.LittleBlankSpace}{resultNumber}{Constants.LittleBlankSpace}=";
            }
            else {
                formattedExpression =
                    $"{cacheNumber}{Constants.LittleBlankSpace}{operationInfo.Symbol}{Constants.LittleBlankSpace}{Constants.LittleBlankSpace}=";
            }

            // 处理除以零
            if (Operator == Constants.Division && cacheNumber == 0) {
                _displayViewModel.Result = "Error";
                _displayViewModel.Expression = formattedExpression;
                return;
            }

            // 执行运算
            decimal calcResult = operationInfo.Operation(cacheNumber, resultNumber);
            
            _displayViewModel.Result = calcResult.ToString(CultureInfo.CurrentCulture);
            _displayViewModel.ResultCache = calcResult.ToString(CultureInfo.CurrentCulture);
            _displayViewModel.Expression = formattedExpression;
        }
        catch (Exception ex) {
            _displayViewModel.Result = "Error";
            _displayViewModel.Expression =
                $"{cacheNumber}{Constants.LittleBlankSpace}{operationInfo.Symbol}{Constants.LittleBlankSpace}{resultNumber}";
            System.Diagnostics.Debug.WriteLine($"Calculation error: {ex.Message}");
        }
    }

    #endregion
}