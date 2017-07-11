using ComputerSystems;

namespace SGR.Models.Entity
{
    public class ESGR_MovimientoCajaDetalle : CmpNotifyPropertyChanged
    {
        public ESGR_MovimientoCajaDetalle()
        {
        }
        public ESGR_MovimientoCaja ESGR_MovimientoCaja { get; set; }
        public int Item { get; set; }
        public int IdReferencia { get; set; }

        private string _ConceptoDescripcion;
        public string ConceptoDescripcion
        {
            get
            {
                return _ConceptoDescripcion;
            }
            set
            {
                _ConceptoDescripcion = value;
                OnPropertyChanged("ConceptoDescripcion");
            }
        }

        private decimal _Monto;
        public decimal Monto
        {
            get
            {
                return _Monto;
            }
            set
            {
                _Monto = value;
                OnPropertyChanged("Monto");
            }
        }
    }
}
