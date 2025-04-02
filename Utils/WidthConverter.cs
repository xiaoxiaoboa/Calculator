using System.Globalization;
using System.Windows.Data;

namespace Calculator.Utils;

public class WidthConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is double width)
        {
            return width - 85; // 减去边距，确保文本不会贴边
        }
        return 235; // 默认值
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}