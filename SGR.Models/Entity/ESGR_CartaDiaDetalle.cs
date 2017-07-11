/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   ABEL QUISPE ORELLANA
'* FCH. CREACIÓN : 28/11/2016
**********************************************************/
namespace SGR.Models.Entity
{
    using ComputerSystems;
    using System;
    using System.Collections.Generic;
    using System.Windows;
    public class ESGR_CartaDiaDetalle : CmpNotifyPropertyChanged
    {
        public ESGR_CartaDia ESGR_CartaDia { get; set; }
        public int IdCartaDia { get; set; }
        public int Item { get; set; }
        public ESGR_Producto ESGR_Producto { get; set; }
        private Nullable<int> _Stock;
        public Nullable<int> Stock
        {
            get
            {
                return _Stock;
            }
            set
            {
                _Stock = value;
                OnPropertyChanged("Stock");
            }
        }
        public Nullable<int> TempStock { get; set; }
        public int TempCantidad { get; set; }
        private decimal _Precio;
        public decimal Precio
        {
            get
            {
                return _Precio;
            }
            set
            {
                _Precio = value;
                OnPropertyChanged("Precio");
            }
        }
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
        public bool _CartaDia;
        public bool CartaDia
        {
            get
            {
                return _CartaDia;
            }
            set
            {
                _CartaDia = value;
               
                OnPropertyChanged("CartaDia");
            }
        }
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
        public Nullable<bool> FlgEliminado { get; set; }
        public string Producto { get; set; }
        public string Categoria { get; set; }
        public string SubCategoria { get; set; }

    }
}
