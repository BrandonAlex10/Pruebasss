using ComputerSystems;

namespace SGR.Models.Entity
{
    public class ESGR_Caja : CmpNotifyPropertyChanged
    {
        public string Opcion { get; set; }
        public int IdCaja { get; set; }

        private ESGR_UsuarioEmpresaSucursal _ESGR_UsuarioEmpresaSucursal;
        public ESGR_UsuarioEmpresaSucursal ESGR_UsuarioEmpresaSucursal
        {
            get
            {
                if (_ESGR_UsuarioEmpresaSucursal == null)
                    _ESGR_UsuarioEmpresaSucursal = new ESGR_UsuarioEmpresaSucursal();
                return _ESGR_UsuarioEmpresaSucursal;
            }
            set
            {
                _ESGR_UsuarioEmpresaSucursal = value;
                OnPropertyChanged("ESGR_UsuarioEmpresaSucursal");
            }
        }

        private string _Descripcion;
        public string Descripcion
        {
            get
            {
                return _Descripcion;
            }
            set
            {
                _Descripcion = value;
                OnPropertyChanged("Descripcion");
            }
        }

        public ESGR_Estado ESGR_Estado { get; set; }

        private bool _FlgEliminado;
        public bool FlgEliminado
        {
            get
            {
                return _FlgEliminado;
            }
            set
            {
                _FlgEliminado = value;
                OnPropertyChanged("FlgEliminado");
            }
        }
    }
}
