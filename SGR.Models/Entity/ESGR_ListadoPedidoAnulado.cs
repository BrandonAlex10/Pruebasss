/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   CRISTIAN A. HERNANDEZ VILLO
'* FCH. CREACIÓN : 27/06/2017
**********************************************************/

namespace SGR.Models.Entity
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ESGR_ListadoPedidoAnulado
    {
        public DateTime Fecha { get; set; }
        public string  Mozo { get; set; }
        public decimal Cantidad { get; set; }
        public decimal Precio { get; set; }
        public decimal Cubierto { get; set; }
        public string Mesa { get; set; }
        public string Producto { get; set; }
        public decimal Importe { get; set; }
    }
}
