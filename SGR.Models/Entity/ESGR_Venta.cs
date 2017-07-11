/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   ABEL QUISPE ORELLANA
'* FCH. CREACIÓN : 28/11/2016
**********************************************************/
namespace SGR.Models.Entity
{
    using ComputerSystems;
    using System;

    public class ESGR_Venta : CmpNotifyPropertyChanged
    {
        public ESGR_Venta()
        {
        }
        public string Opcion { get; set; }
        public int IdVenta { get; set; }
        public ESGR_Caja ESGR_Caja { get; set; }
        public ESGR_Pedido ESGR_Pedido { get; set; }
        public ESGR_Estado ESGR_Estado { get; set; }
        public DateTime Fecha { get; set; }
        public string CuentasXML { get; set; }
        public string DetalleXML { get; set; }
    }
}
