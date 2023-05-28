namespace RambleJungle.Converters
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    public class IntToWeaponCountConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (int)value >= 0 ? ((int)value).ToString(CultureInfo.CurrentCulture) : "∞";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (string)value == "∞" ? -1 : int.Parse((string)value, CultureInfo.CurrentCulture);
        }
    }
}
