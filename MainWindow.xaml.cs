using System.Windows;
using System.Windows.Media;
using Calculator.Utils;
using Calculator.ViewModel;
using Wpf.Ui;
using Wpf.Ui.Appearance;
using Wpf.Ui.Controls;

namespace Calculator;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow {
    public MainWindow() {
        InitializeComponent();
        
        SystemThemeWatcher.Watch(this);


        // 已弃用
        // SizeChanged += MainWindow_SizeChanged;
        // Loaded += (s, e) => UpdateMaxWidth();
    }


    [Obsolete("此方法已弃用。", true)]
    private void MainWindow_SizeChanged(object sender, SizeChangedEventArgs e) {
        UpdateMaxWidth();
    }

    [Obsolete("此方法已弃用。", true)]
    private void UpdateMaxWidth() {
        if (DataContext is MainViewModel vm) {
            var converter = new WidthConverter();
            vm.DisplayViewModel.MaxWidth =
                (double)converter.Convert(DisplayGrid.ActualWidth, typeof(double), null, null);
        }
    }
}