using System.Globalization;
using System.Windows.Data;

namespace Calculator.Utils;

[Obsolete("已弃用。", true)]
public class WidthConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo? culture)
    {
        if (value is double width)
        {
            return width - 104; // 减去边距，确保文本不会贴边
        }
        return 216; // 默认值
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}