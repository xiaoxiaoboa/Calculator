﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Calculator.Utils;
using Wpf.Ui.Controls;
using Button = Wpf.Ui.Controls.Button;

namespace Calculator.Views;

public partial class MyCalculatorKeyBoard : MyBaseControl {
    public MyCalculatorKeyBoard() {
        InitializeComponent();
        SizeChanged += Window_SizeChanged;
    }
    
    private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
    {
        var scaleFactor = Math.Min(e.NewSize.Width / 50, e.NewSize.Height / 50);
        TextScale.ScaleX = scaleFactor;
        TextScale.ScaleY = scaleFactor;
    }
}


