using System;
using System.Globalization;
using System.Windows.Data;

namespace RumbleJungle.Converters
{
    public class HealthToAngleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (int)value > 0 ? 0 : 90;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (int)value == 0 ? 100 : 0;
        }
    }
}
