using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace RumbleJungle.Converters
{
    public class StrengthToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (double)value == 1 ? Brushes.Blue : Brushes.Magenta;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (Brush)value == Brushes.Blue ? 1 : 1.3;
        }
    }
}
