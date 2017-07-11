using ComputerSystems;

namespace SGR.Models.Entity
{
    public class ESGR_AperturaCierreCajaDetalle : CmpNotifyPropertyChanged
    {
        public ESGR_AperturaCierreCajaDetalle()
        {
        }
        private int _Item;
        public int Item
        {
            get
            {
                return _Item;
            }
            set
            {
                _Item = value;
                OnPropertyChanged("Item");
            }
        }

        private ESGR_AperturaCierreCaja _ESGR_AperturaCierreCaja;
        public ESGR_AperturaCierreCaja ESGR_AperturaCierreCaja
        {
            get
            {
                return _ESGR_AperturaCierreCaja;
            }
            set
            {
                _ESGR_AperturaCierreCaja = value;
                OnPropertyChanged("ESGR_AperturaCierreCaja");
            }
        }

        private ESGR_Moneda _ESGR_Moneda;
        public ESGR_Moneda ESGR_Moneda
        {
            get
            {
                if (_ESGR_Moneda == null)
                    _ESGR_Moneda = new ESGR_Moneda();
                return _ESGR_Moneda;
            }
            set
            {
                _ESGR_Moneda = value;
                OnPropertyChanged("ESGR_Moneda");
            }
        }

        private ESGR_MedioPago _ESGR_MedioPago;
        public ESGR_MedioPago ESGR_MedioPago
        {
            get
            {
                if (_ESGR_MedioPago == null)
                    _ESGR_MedioPago = new ESGR_MedioPago();
                return _ESGR_MedioPago;
            }
            set
            {
                _ESGR_MedioPago = value;
                OnPropertyChanged("ESGR_MedioPago");
            }
        }

        private decimal _Monto_Inicio;
        public decimal Monto_Inicio
        {
            get
            {
                return _Monto_Inicio;
            }
            set
            {
                _Monto_Inicio = value;
                OnPropertyChanged("Monto_Inicio");
            }
        }

        private decimal _Monto_Sistema;
        public decimal Monto_Sistema
        {
            get
            {
                return _Monto_Sistema;
            }
            set
            {
                _Monto_Sistema = value;
                OnPropertyChanged("Monto_Sistema");
            }
        }

        private decimal _Monto_Cierre;
        public decimal Monto_Cierre
        {
            get
            {
                return _Monto_Cierre;
            }
            set
            {
                _Monto_Cierre = value;
                OnPropertyChanged("Monto_Cierre");
            }
        }

        private decimal _DiferExceso;
        public decimal DiferExceso
        {
            get
            {
                return _DiferExceso;
            }
            set
            {
                _DiferExceso = value;
                OnPropertyChanged("DiferExceso");
            }
        }
    }
}
