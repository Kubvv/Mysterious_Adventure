using RambleJungle.Model;
using RambleJungle.ViewModel;
using System;
using System.Windows.Data;
using System.Windows.Media;

namespace RambleJungle.Converters
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
                case Statuses.Shown:
                    result = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
                    break;
                case Statuses.Visible:
                    result = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
                    break;
                case Statuses.Visited:
                    result = new SolidColorBrush(Color.FromArgb(128, 0xFF, 0xFF, 0xFF));
                    break;
                case Statuses.Pointed:
                    if (jungleObject.JungleObjectType == JungleObjectType.Treasure)
                    {
                        result = Brushes.Gold;
                    }
                    else if (Config.Beasts.Contains(jungleObject.JungleObjectType))
                    {
                        result = Brushes.Red;
                    }
                    else if (Config.BadItems.Contains(jungleObject.JungleObjectType))
                    {
                        result = Brushes.LightBlue;
                    }
                    else if (Config.GoodItems.Contains(jungleObject.JungleObjectType))
                    {
                        result = Brushes.Purple;
                    }
                    else
                    {
                        result = Brushes.Green;
                    }
                    break;
                case Statuses.Marked:
                    result = new SolidColorBrush(Color.FromArgb(32, 0x78, 0x09, 0x0E));
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