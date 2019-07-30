using RumbleJungle.Model;
using RumbleJungle.ViewModel;
using System;
using System.Windows.Data;
using System.Windows.Media;

namespace RumbleJungle.Converters
{
    public class JungleObjectToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            JungleObjectViewModel jungleObject = (JungleObjectViewModel)value;
            if (jungleObject == null) return Brushes.Green;

            Brush result;
            switch (jungleObject.Status)
            {
                case Statuses.Hidden:
                    result = Brushes.Green;
                    break;
                case Statuses.Visible:
                    result = Brushes.White;
                    break;
                case Statuses.Visited:
                    result = Brushes.LightGray;
                    break;
                case Statuses.Pointed:
                    if (jungleObject.JungleObjectType == JungleObjectTypes.Treasure)
                    {
                        result = Brushes.Brown;
                    }
                    else if (Configuration.Beasts.Contains(jungleObject.JungleObjectType))
                    {
                        result = Brushes.Red;
                    }
                    else if (Configuration.BadItems.Contains(jungleObject.JungleObjectType))
                    {
                        result = Brushes.LightBlue;
                    }
                    else if (Configuration.GoodItems.Contains(jungleObject.JungleObjectType))
                    {
                        result = Brushes.LightGreen;
                    }
                    else
                    {
                        result = Brushes.Green;
                    }
                    break;
                case Statuses.Marked:
                    result = Brushes.Black;
                    break;
                default:
                    result = Brushes.Yellow;
                    break;
            }
            return result;
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}