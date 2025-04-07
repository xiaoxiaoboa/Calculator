using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Wpf.Ui.Controls;

namespace Calculator.Utils;

public abstract partial class MyBaseControl : UserControl {


    // 定义 Content 属性
    public object ButtonContent{
        get => GetValue(ButtonContentProperty);
        set => SetValue(ButtonContentProperty, value);
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
    
    public IconElement? ButtonIcon{
        get => (IconElement)GetValue(ButtonIconProperty);
        set => SetValue(ButtonIconProperty, value);
    }
    
    
    // 定义依赖项属性
    public static readonly DependencyProperty ButtonContentProperty =
        DependencyProperty.Register(nameof(ButtonContent), typeof(object), typeof(MyBaseControl),
            new PropertyMetadata(null));
    
    public static readonly DependencyProperty ButtonAppearanceProperty =
        DependencyProperty.Register(nameof(ButtonAppearance), typeof(object), typeof(MyBaseControl),
            new PropertyMetadata(ControlAppearance.Secondary));
    
    public static readonly DependencyProperty ButtonFontSizeProperty =
        DependencyProperty.Register(nameof(ButtonFontSize), typeof(object), typeof(MyBaseControl),
            new PropertyMetadata(16.0));
    
    public static readonly DependencyProperty ButtonCommandProperty =
        DependencyProperty.Register(nameof(ButtonCommand), typeof(object), typeof(MyBaseControl),
            new PropertyMetadata(null));
    
    public static readonly DependencyProperty ButtonCommandParameterProperty =
        DependencyProperty.Register(nameof(ButtonCommandParameter), typeof(object), typeof(MyBaseControl),
            new PropertyMetadata(null));
    
    public static readonly DependencyProperty ButtonIconProperty =
        DependencyProperty.Register(nameof(ButtonIcon), typeof(object), typeof(MyBaseControl),
            new PropertyMetadata(null));
}