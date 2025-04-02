using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Calculator.MVVM;

namespace Calculator.ViewModel;

public class DisplayViewModel : ViewModelBase {
    private string _expression = "0";
    private double _fontSize = 40; // 字体初始大小
    private double _maxWidth = 320; // 窗口初始大小，动态更新
    private const double MinFontSize = 28; // 最小字体大小
    private const int MaxDigitCount = 16; // 最大数字位数

    public ICommand HandleInput{ get; }
    public ICommand HandleClear{ get; }


    public string Expression{
        get => _expression;
        set {
            _expression = _expression == "0" ? value : _expression + value;
            OnPropertyChanged();
            AdjustFontSize();
        }
    }

    public double FontSize{
        get => _fontSize;
        set {
            _fontSize = value;
            OnPropertyChanged(nameof(FontSize));
        }
    }

    public double MaxWidth{
        get => _maxWidth;
        set {
            _maxWidth = value;
            OnPropertyChanged(nameof(MaxWidth));
            AdjustFontSize(); // 宽度变化时调整字体
        }
    }


    #region 按钮事件

    public DisplayViewModel() {
        HandleInput = new RelayCommand(ExecuteInputNumber);
        HandleClear = new RelayCommand(ExecuteClear);
    }

    private void ExecuteInputNumber(object? parameter) {
        if (Expression.Length < MaxDigitCount) {
            Expression = parameter?.ToString() ?? "";
        }
    }

    private void ExecuteClear(object? parameter) {
        // 清空后恢复初始值
        // Expression = "0";
        _expression = "0";
        OnPropertyChanged(nameof(Expression));
    }

    #endregion


    private void AdjustFontSize() {
        var formattedText = new FormattedText(
            Expression,
            System.Globalization.CultureInfo.CurrentCulture,
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

        Console.WriteLine($"FontSize:{FontSize}");
        CommandManager.InvalidateRequerySuggested();
    }
}