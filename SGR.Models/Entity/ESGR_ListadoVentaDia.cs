/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   CRISTIAN A. HERNANDEZ VILLO
'* FCH. CREACIÓN : 20/04/2017
**********************************************************/
namespace SGR.Models.Entity
{
    using System;

    public class ESGR_ListadoVentaDia
    {
        public DateTime Fecha { get; set; }
        public string Producto { get; set; }
        public decimal Cantidad { get; set; }
        public decimal Precio { get; set; }
        public decimal Descuento { get; set; }
        public decimal Importe { get; set; }
        public string Mozo { get; set; }
    }
}
