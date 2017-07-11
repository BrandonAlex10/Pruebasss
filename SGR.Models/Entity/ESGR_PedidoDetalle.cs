/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   ABEL QUISPE ORELLANA
'* FCH. CREACIÓN : 28/11/2016
**********************************************************/
namespace SGR.Models.Entity
{
    using ComputerSystems;
    using System;
    public class ESGR_PedidoDetalle : CmpNotifyPropertyChanged
    {
        public ESGR_Pedido ESGR_Pedido { get; set; }
        public ESGR_Producto ESGR_Producto { get; set; }
        private int _Cantidad;
        public int Cantidad
        {
            get
            {
                return _Cantidad;
            }
            set
            {
                _Cantidad = value;
                OnPropertyChanged("Cantidad");
            }
        }
        private int _CantidadAux;
        public int CantidadAux
        {
            get
            {
                return _CantidadAux;
            }
            set
            {
                _CantidadAux = value;
                OnPropertyChanged("CantidadAux");
            }
        }
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
        public int CantidadMesa { get; set; }
        public int CantidadLlevar { get; set; }
        public bool Enviado { get; set; }
        private string _Observacion;
        public string Observacion
        {
            get
            {
                return _Observacion;
            }
            set
            {
                _Observacion = value;
                OnPropertyChanged("Observacion");
            }
        }
    }
}
