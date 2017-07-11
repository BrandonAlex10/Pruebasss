/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'* CREADO POR	 : ABEL QUISPE ORELLANA
'* FCH. CREACIÓN : 28/11/2016
**********************************************************/
namespace SGR.Presentation.Converts
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    public class ConvertVisibilityCommandClearText : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return false;
            var country = (string)value;
            return country.Length == 0 ? false : true;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
