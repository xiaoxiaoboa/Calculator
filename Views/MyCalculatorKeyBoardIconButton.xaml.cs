using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Calculator.Utils;
using Wpf.Ui.Controls;

namespace Calculator.Views;

public partial class MyCalculatorKeyBoardIconButton : MyBaseControl {
    public MyCalculatorKeyBoardIconButton() {
        InitializeComponent();
        SizeChanged += Window_SizeChanged;
    }
    
    
    
    private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
    {
        var scaleFactor = Math.Min(e.NewSize.Width / 50, e.NewSize.Height / 50);
        IconScale.ScaleX = scaleFactor;
        IconScale.ScaleY = scaleFactor;
    }

}