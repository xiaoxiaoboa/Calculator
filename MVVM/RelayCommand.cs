using System.Windows.Input;

namespace Calculator.MVVM;

public class RelayCommand : ICommand
{
    private readonly Action<object?> _execute; // 执行方法

    public RelayCommand(Action<object?> execute)
    {
        _execute = execute ?? throw new ArgumentNullException(nameof(execute));
    }

    public event EventHandler? CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }

    public bool CanExecute(object? parameter) {
        return true;
    }

    public void Execute(object? parameter) {
        _execute(parameter);
    }
}