using System.Windows.Media;

namespace Calculator.Utils;

public struct Constants {
    // keyboard key
    public const int Number0 = 0;
    public const int Number1 = 1;
    public const int Number2 = 2;
    public const int Number3 = 3;
    public const int Number4 = 4;
    public const int Number5 = 5;
    public const int Number6 = 6;
    public const int Number7 = 7;
    public const int Number8 = 8;
    public const int Number9 = 9;
    public const char Addition = '+';
    public const char Subtractive = '-';
    public const char Multiplication = '\u00d7'; // ×
    public const char Division = '\u00f7'; // ÷
    public const char Percent = '\u0025'; // %
    public const string OneInX = "x\u207B\u00B9"; // x⁻¹
    public const string Square = "x\u00b2"; // x²
    public const string SquareRootSign = "\u221ax"; // √x
    public const char RadicalSign = '\u221a'; // √
    public const char Clear = 'C';
    public const char Dot = '.';
    public const char EqualSign = '=';
    public const string NegativeSign = "+/-";


    // color
    public static readonly Brush ExpressionLightColor;

    // other
    public const char LittleBlankSpace = '\u202F';
    public const string InitializationString = "0";
    public const char DefaultChar = '\0';


    static Constants() {
        var brush = new SolidColorBrush(Color.FromRgb(163, 163, 163));
        brush.Freeze();
        ExpressionLightColor = brush;
    }
}