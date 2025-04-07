namespace Calculator.Interfaces;

public interface IDisplayViewModel {
    string Expression{ get; set; }
    string Result{ get; set; }
    string ResultCache{ get; set; }
    string DisplayResult{ get; }

    double FontSize{ get; set; }
    double MaxWidth{ get; set; }
    
    int MaxDigitCount{ get; }
    
    
}