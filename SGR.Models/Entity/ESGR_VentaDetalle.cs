/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   ABEL QUISPE ORELLANA
'* FCH. CREACIÓN : 28/11/2016
**********************************************************/
namespace SGR.Models.Entity
{
    using ComputerSystems;
    using System;
    public class ESGR_VentaDetalle : CmpNotifyPropertyChanged
    {
        public int Item { get; set; }
        private ESGR_VentaCuenta _ESGR_VentaCuenta;
        public ESGR_VentaCuenta ESGR_VentaCuenta
        {
            get
            {
                return _ESGR_VentaCuenta;
            }
            set
            {
                _ESGR_VentaCuenta = value;
                OnPropertyChanged("ESGR_VentaCuenta");
            }
        }
        public ESGR_Producto ESGR_Producto { get; set; }
        public int Cantidad { get; set; }
        private int _CantidadPagar;
        public int CantidadPagar
        {
            get
            {
                return _CantidadPagar;
            }
            set
            {
                _CantidadPagar = value;
                OnPropertyChanged("CantidadPagar");
            }
        }
        public decimal Precio { get; set; }
        public decimal Descuento { get; set; }
        private decimal _Importe;
        public decimal Importe
        {
            get
            {
                return _Importe;
            }
            set
            {
                _Importe = value;
                OnPropertyChanged("Importe");
            }
        }
    }
}
