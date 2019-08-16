using RumbleJungle.Model;
using System;
using System.Windows.Data;
using System.Windows.Media;

namespace RumbleJungle.Converters
{
    class JungleObjectTypeToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return Brushes.Black;

            JungleObjectTypes jungleObjectType = (JungleObjectTypes)value;
            Brush result;
            if (jungleObjectType == JungleObjectTypes.Treasure)
            {
                result = Brushes.Brown;
            }
            else if (Configuration.Beasts.Contains(jungleObjectType))
            {
                result = Brushes.Red;
            }
            else if (Configuration.BadItems.Contains(jungleObjectType))
            {
                result = Brushes.LightBlue;
            }
            else if (Configuration.GoodItems.Contains(jungleObjectType))
            {
                result = Brushes.LightGreen;
            }
            else
            {
                result = Brushes.Black;
            }
            return result;
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
