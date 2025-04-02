namespace Calculator.ViewModel;

public class MainViewModel
{
    public DisplayViewModel DisplayViewModel { get; }
    public CalculateViewModel CalculatorViewModel { get; }

    public MainViewModel()
    {
        DisplayViewModel = new DisplayViewModel();
        CalculatorViewModel = new CalculateViewModel(DisplayViewModel);
    }
}