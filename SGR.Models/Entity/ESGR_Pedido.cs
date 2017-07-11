/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   ABEL QUISPE ORELLANA
'* FCH. CREACIÓN : 28/11/2016
**********************************************************/
using ComputerSystems;
namespace SGR.Models.Entity
{
    public class ESGR_Pedido : CmpNotifyPropertyChanged
    {
        public ESGR_Pedido()
        {
            ESGR_Mesa = new ESGR_Mesa();
            ESGR_Estado = new ESGR_Estado();
        }
        public string Opcion { get; set; }
        public int IdPedido { get; set; }
        public ESGR_PedidoTipo ESGR_PedidoTipo { get; set; }
        public ESGR_EmpresaSucursal ESGR_EmpresaSucursal { get; set; }
        public ESGR_Usuario ESGR_Usuario { get; set; }
        private ESGR_Mesa _ESGR_Mesa;
        public ESGR_Mesa ESGR_Mesa
        {
            get
            {
                return _ESGR_Mesa;
            }
            set
            {
                _ESGR_Mesa = value;
                OnPropertyChanged("ESGR_Mesa");
            }
        }
        public ESGR_Estado ESGR_Estado { get; set; }
        private short _Cubierto;
        public short Cubierto
        {
            get
            {
                return _Cubierto;
            }
            set
            {
                _Cubierto = value;
                OnPropertyChanged("Cubierto");
            }
        }
        public string CadenaDetalleXML { get; set; }
        public string CadenaMesa { get; set; }
        private string _Identificador;
        public string Identificador
        {
            get
            {
                return _Identificador;
            }
            set
            {
                _Identificador = value;
                OnPropertyChanged("Identificador");
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
    }
}
