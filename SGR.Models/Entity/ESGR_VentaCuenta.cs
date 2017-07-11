using ComputerSystems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGR.Models.Entity
{
    public class ESGR_VentaCuenta : CmpNotifyPropertyChanged
    {
        public ESGR_VentaCuenta()
        {
            Fecha = DateTime.Now;
        }

        public string Opcion { get; set; }
        public int IdCuenta { get; set; }
        public ESGR_Venta ESGR_Venta { get; set; }
        private ESGR_Cliente _ESGR_Cliente;
        public ESGR_Cliente ESGR_Cliente
        {
            get
            {
                if (_ESGR_Cliente == null)
                    _ESGR_Cliente = new ESGR_Cliente();
                return _ESGR_Cliente;
            }
            set
            {
                _ESGR_Cliente = value;
                OnPropertyChanged("ESGR_Cliente");
            }
        }
        public ESGR_MedioPago ESGR_MedioPago { get; set; }
        public ESGR_Moneda ESGR_Moneda { get; set; }
        public ESGR_Documento ESGR_Documento { get; set; }
        public string _Serie;
        public string Serie
        {
            get
            {
                return _Serie;
            }
            set
            {
                _Serie = value;
                OnPropertyChanged("Serie");
            }
        }
        private string _Numero;
        public string Numero
        {
            get
            {
                return _Numero;
            }
            set
            {
                _Numero = value;
                OnPropertyChanged("Numero");
            }
        }
        public DateTime Fecha { get; set; }
        private decimal _TipoCambio;
        public decimal TipoCambio
        {
            get
            {
                return _TipoCambio;
            }
            set
            {
                _TipoCambio = value;
                OnPropertyChanged("TipoCambio");
            }
        }
        private decimal _Gravada;
        public decimal Gravada
        {
            get
            {
                return _Gravada;
            }
            set
            {
                _Gravada = value;
                OnPropertyChanged("Gravada");
            }
        }
        private decimal _IGV;
        public decimal IGV
        {
            get
            {
                return _IGV;
            }
            set
            {
                _IGV = value;
                OnPropertyChanged("IGV");
            }
        }
        private decimal _ImporteIGV;
        public decimal ImporteIGV
        {
            get
            {
                return _ImporteIGV;
            }
            set
            {
                _ImporteIGV = value;
                OnPropertyChanged("ImporteIGV");
            }
        }
        private decimal _Descuento;
        public decimal Descuento
        {
            get
            {
                return _Descuento;
            }
            set
            {
                _Descuento = value; 
                OnPropertyChanged("Descuento");
            }
        }
        private decimal _Adicional;
        public decimal Adicional
        {
            get
            {
                return _Adicional;
            }
            set
            {
                _Adicional = value;
                OnPropertyChanged("Adicional");
            }
        }
        private decimal _ImporteDscto;
        public decimal ImporteDscto
        {
            get
            {
                return _ImporteDscto;
            }
            set
            {
                _ImporteDscto = value;
                OnPropertyChanged("ImporteDscto");
            }
        }

        private decimal _Total;
        public decimal Total
        {
            get
            {
                return _Total;
            }
            set
            {
                _Total = value;
                OnPropertyChanged("Total");
            }
        }

        public string JsonDetalle { get; set; }
    }
}
