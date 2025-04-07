using System.Globalization;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Calculator.Interfaces;
using Calculator.MVVM;
using Calculator.Utils;

namespace Calculator.ViewModel;

public class DisplayViewModel : ViewModelBase, IDisplayViewModel {
    #region 字段

    // 表达式显示
    private string _expression = string.Empty;

    // 输入栏显示
    private string _result = Constants.InitializationString;

    // 输入栏显示的缓存
    public string ResultCache{ get; set; } = Constants.InitializationString;

    // 显示最终计算结果
    private string _displayResult = Constants.InitializationString;


    // 控制字体
    private double _fontSize = 40; // 字体初始大小
    private double _maxWidth = 320; // 窗口初始大小，动态更新

    [Obsolete("已弃用", true)] private readonly double MinFontSize = 24; // 最小字体大小

    public int MaxDigitCount{ get; } = 16; // 最大数字位数

    #endregion

    #region 属性

    public string Result{
        get => _result;
        set {
            _result = value;
            DisplayResult = value;
        }
    }

    public string DisplayResult{
        get => _displayResult;
        private set {
            _displayResult = FormatNumber(value);

            OnPropertyChanged();

            // 该方法已经弃用
            // AdjustFontSize(); 
        }
    }

    public string Expression{
        get => _expression;
        set {
            _expression = value;
            OnPropertyChanged();

            // 该方法已经弃用
            // AdjustFontSize();
        }
    }

    [Obsolete("已弃用。", true)]
    public double FontSize{
        get => _fontSize;
        set {
            _fontSize = value;
            OnPropertyChanged();
        }
    }

    public double MaxWidth{
        get => _maxWidth;
        set {
            _maxWidth = value;
            OnPropertyChanged();

            // 该方法已经弃用
            // AdjustFontSize(); // 宽度变化时调整字体
        }
    }

    #endregion

    #region 方法

    // 格式化数字为带千位分隔符的字符串
    private string FormatNumber(string number) {
        // 检查是否包含小数点
        if (number.Contains(Constants.Dot)) {
            // 分割整数和小数部分
            string[] parts = number.Split(Constants.Dot);
            string integerPart = parts[0];
            string decimalPart = parts.Length > 1 ? parts[1] : "";

            // 尝试解析整数部分并添加千位分隔符
            if (decimal.TryParse(integerPart, NumberStyles.Any, CultureInfo.CurrentCulture, out decimal integerValue)) {
                string formattedInteger = integerValue.ToString("#,##0", CultureInfo.CurrentCulture);
                
                return $"{formattedInteger}.{decimalPart}";
            }

        }

        // 尝试解析数字
        if (decimal.TryParse(number, NumberStyles.Any, CultureInfo.CurrentCulture, out decimal value)) {
            // 如果成功解析，应用千位分隔符和小数格式

            return value.ToString("#,##0.##", CultureInfo.CurrentCulture);
        }

        // 如果解析失败（比如输入过程中），返回原始输入
        return number;
    }

    [Obsolete("此方法已弃用。", true)]
    // 调整字体大小
    private void AdjustFontSize() {
        var formattedText = new FormattedText(
            Result,
            CultureInfo.CurrentCulture,
            FlowDirection.LeftToRight,
            new Typeface("Segoe UI"),
            FontSize,
            Brushes.Black,
            96);


        // 文本长度达到限制，就缩小
        if (formattedText.Width > MaxWidth) {
            double scale = MaxWidth / formattedText.Width;
            FontSize = Math.Max(MinFontSize, FontSize * scale);
        }
        // 窗口宽度变大时，让字体逐渐变大
        else if (FontSize < 40 && formattedText.Width < MaxWidth * 0.8) {
            // 如果宽度减少后有余量，尝试恢复字体大小
            FontSize = Math.Min(40, MaxWidth / formattedText.Width * FontSize);
        }

        // Console.WriteLine($"scale:{MaxWidth / formattedText.Width}");

        CommandManager.InvalidateRequerySuggested();
    }

    #endregion
}