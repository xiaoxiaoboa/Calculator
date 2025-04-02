using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Wpf.Ui.Controls;
using Button = Wpf.Ui.Controls.Button;

namespace Calculator.Views;

public partial class MyCalculatorKeyBoard : UserControl {
    public MyCalculatorKeyBoard() {
        InitializeComponent();
    }

    // 定义 Content 属性
    public string ButtonText{
        get => (string)GetValue(ButtonTextProperty);
        set => SetValue(ButtonTextProperty, value);
    }

    public ControlAppearance ButtonAppearance{
        get => (ControlAppearance)GetValue(ButtonAppearanceProperty);
        set => SetValue(ButtonAppearanceProperty, value);
    }

    public double ButtonFontSize{
        get => (double)GetValue(ButtonFontSizeProperty);
        set => SetValue(ButtonFontSizeProperty, value);
    }

    public ICommand ButtonCommand{
        get => (ICommand)GetValue(ButtonCommandProperty);
        set => SetValue(ButtonCommandProperty, value);
    }

    public object ButtonCommandParameter{
        get => GetValue(ButtonCommandProperty);
        set => SetValue(ButtonCommandProperty, value);
    }


    // 定义依赖项属性
    public static readonly DependencyProperty ButtonTextProperty =
        DependencyProperty.Register(nameof(ButtonText), typeof(object), typeof(MyCalculatorKeyBoard),
            new PropertyMetadata(string.Empty));

    public static readonly DependencyProperty ButtonAppearanceProperty =
        DependencyProperty.Register(nameof(ButtonAppearance), typeof(object), typeof(MyCalculatorKeyBoard),
            new PropertyMetadata(ControlAppearance.Secondary));

    public static readonly DependencyProperty ButtonFontSizeProperty =
        DependencyProperty.Register(nameof(ButtonFontSize), typeof(object), typeof(MyCalculatorKeyBoard),
            new PropertyMetadata(16));

    public static readonly DependencyProperty ButtonCommandProperty =
        DependencyProperty.Register(nameof(ButtonCommand), typeof(object), typeof(MyCalculatorKeyBoard),
            new PropertyMetadata(null));

    public static readonly DependencyProperty ButtonCommandParameterProperty =
        DependencyProperty.Register(nameof(ButtonCommandParameter), typeof(object), typeof(MyCalculatorKeyBoard),
            new PropertyMetadata(null));
}