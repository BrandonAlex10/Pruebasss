/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   CRISTIAN A. HERNANDEZ VILLO
'* FCH. CREACIÓN : 04/05/2017
**********************************************************/

namespace SGR.Models.Entity
{
    using ComputerSystems;
    using System;

    public class ESGR_MovimientoCaja : CmpNotifyPropertyChanged
    {
        public ESGR_MovimientoCaja()
        {
            ESGR_Motivo = new ESGR_Motivo();
            ESGR_EmpresaSucursal = new ESGR_EmpresaSucursal();
        }
        public string Opcion { get; set; }
        public int IdMovimientoCaja { get; set; }
        public ESGR_EmpresaSucursal ESGR_EmpresaSucursal { get; set; }
        public ESGR_Usuario ESGR_Usuario { get; set; }
        public ESGR_Motivo ESGR_Motivo { get; set; }
        public ESGR_Estado ESGR_Estado { get; set; }
        private ESGR_Documento _ESGR_Documento;
        public ESGR_Documento ESGR_Documento
        {
            get
            {
                if (_ESGR_Documento == null)
                    _ESGR_Documento = new ESGR_Documento();
                return _ESGR_Documento;
            }
            set
            {
                _ESGR_Documento = value;
                OnPropertyChanged("ESGR_Documento");
            }
        }

        private ESGR_Caja _ESGR_Caja;
        public ESGR_Caja ESGR_Caja
        {
            get
            {
                return _ESGR_Caja;
            }
            set
            {
                _ESGR_Caja = value;
                OnPropertyChanged("ESGR_Caja");
            }
        }

        public string SerieNumero
        {
            get
            {
                return ESGR_Documento.Serie + "-" + ESGR_Documento.Correlativo;
            }
        }
        public ESGR_Moneda ESGR_Moneda { get; set; }
        public string Numero { get; set; }
        public DateTime Fecha { get; set; }
        public string CodOperacion { get; set; }
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
        public string Glosa { get; set; }
        public string DetalleXML { get; set; }
    }
}
