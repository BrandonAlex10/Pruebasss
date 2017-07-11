/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   ABEL QUISPE ORELLANA
'* FCH. CREACIÓN : 26/12/2016
**********************************************************/
namespace SGR.Orders.Converters
{
    using System;
    using System.Globalization;
    using System.Windows.Data;
    using System.Windows.Media;

    public class ConvertToStatusImage : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string)
            {
                if (((string)value) == "ATPED") // ATENDIDO
                {
                    return "/SGR.Orders;component/Resource/Mesa/mesa_verde.png"; // (Verde)
                }
                if (((string)value) == "CTPED") // SOLICITÓ CUENTA
                {
                    return "/SGR.Orders;component/Resource/Mesa/mesa_amarillo.png"; // (Amarillo)
                }
                if (((string)value) == "PGPED") // PAGADO
                {
                    return "/SGR.Orders;component/Resource/Mesa/mesa_gris.png"; // (Blanco)
                }
                if (((string)value) == "RVPED") // RESERVADO
                {
                    return "/SGR.Orders;component/Resource/Mesa/mesa_roja.png"; // (Rojo)
                }

                return "/SGR.Orders;component/Resource/Mesa/mesa_gris.png"; // (Blanco)
            }
            else
                return "/SGR.Orders;component/Resource/Mesa/mesa_gris.png"; // (Blanco)
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
