using ComputerSystems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGR.ViewModels.Presentation.Perfil.Controls
{
    public class VMSGR_Formulario : CmpNotifyPropertyChanged
    {
        private string _CodFormulario;
        public string CodFormulario
        {
            get
            {
                return _CodFormulario;
            }
            set
            {
                _CodFormulario = value;
                OnPropertyChanged("CodFormulario");
            }
        }

        private string _NombreFormulario;
        public string NombreFormulario
        {
            get
            {
                return _NombreFormulario;
            }
            set
            {
                _NombreFormulario = value;
                OnPropertyChanged("NombreFormulario");
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

        private bool _Consulta;
        public bool Consulta
        {
            get
            {
                return _Consulta;
            }
            set
            {
                _Consulta = value;
                OnPropertyChanged("Consulta");
            }
        }

        private bool _Nuevo;
        public bool Nuevo
        {
            get
            {
                return _Nuevo;
            }
            set
            {
                _Nuevo = value;
                OnPropertyChanged("Nuevo");
            }
        }

        private bool _Editar;
        public bool Editar
        {
            get
            {
                return _Editar;
            }
            set
            {
                _Editar = value;
                OnPropertyChanged("Editar");
            }
        }

        private bool _Eliminar;
        public bool Eliminar
        {
            get
            {
                return _Eliminar;
            }
            set
            {
                _Eliminar = value;
                OnPropertyChanged("Eliminar");
            }
        }
    }
}
