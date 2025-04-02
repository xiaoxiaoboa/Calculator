using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Calculator.Utils;
using Calculator.ViewModel;
using Wpf.Ui.Appearance;

namespace Calculator;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow {
    public MainWindow() {
        InitializeComponent();
        SystemThemeWatcher.Watch(this);
        SizeChanged += MainWindow_SizeChanged;
        Loaded += (s, e) => UpdateMaxWidth();
    }

    private void MainWindow_SizeChanged(object sender, SizeChangedEventArgs e) {
        UpdateMaxWidth();
    }

    private void UpdateMaxWidth() {
        if (DataContext is MainViewModel vm) {
            var converter = new WidthConverter();
            vm.DisplayViewModel.MaxWidth =
                (double)converter.Convert(DisplayGrid.ActualWidth, typeof(double), null, null);
        }

       
    }
}