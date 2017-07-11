using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace SGR.Presentation.Converts
{
    public class ConvertVisibilityColumns : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var vrValue = (bool)value;
            if (vrValue == true)
                return Visibility.Visible;
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
