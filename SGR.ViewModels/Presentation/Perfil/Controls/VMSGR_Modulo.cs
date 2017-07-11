using ComputerSystems;
using SGR.Models.Entity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SGR.ViewModels.Presentation.Perfil.Controls
{
    public class VMSGR_Modulo : CmpNotifyPropertyChanged
    {
        private string _HeaderModulo;
        public string HeaderModulo
        {
            get
            {
                return _HeaderModulo;
            }
            set
            {
                _HeaderModulo = value;
                OnPropertyChanged("HeaderModulo");
            }
        }

        private int _IdModulo;
        public int IdModulo
        {
            get
            {
                return _IdModulo;
            }
            set
            {
                _IdModulo = value;
                MethodLoadFormulario();
                OnPropertyChanged("IdModulo");
            }
        }

        private bool Consulta { get; set; }
        private bool Nuevo { get; set; }
        private bool Editar { get; set; }
        private bool Eliminar { get; set; }

        public ICommand IsChecked
        {
            get
            {
                return CmpICommand.GetICommand((T) =>
                {
                    string strNombre = T.ToString();
                    if (strNombre == "Consulta")
                    {
                        Consulta = !Consulta;
                        foreach (var item in CollectionVMSGR_Formulario)
                            item.Consulta = Consulta;
                    }
                    else if (strNombre == "Nuevo")
                    {
                        Nuevo = !Nuevo;
                        foreach (var item in CollectionVMSGR_Formulario)
                            item.Nuevo = Nuevo;
                    }
                    else if (strNombre == "Editar")
                    {
                        Editar = !Editar;
                        foreach (var item in CollectionVMSGR_Formulario)
                            item.Editar = Editar;
                    }
                    else if (strNombre == "Eliminar")
                    {
                        Eliminar = !Eliminar;
                        foreach (var item in CollectionVMSGR_Formulario)
                            item.Eliminar = Eliminar;
                    }
                });
            }
        }

        private ObservableCollection<ESGR_Formulario> _CollectionESGR_FormularioAll;
        public ObservableCollection<ESGR_Formulario> CollectionESGR_FormularioAll
        {
            get
            {
                if (_CollectionESGR_FormularioAll == null)
                    _CollectionESGR_FormularioAll = new ObservableCollection<ESGR_Formulario>();
                return _CollectionESGR_FormularioAll;
            }
            set
            {
                _CollectionESGR_FormularioAll = value;
                OnPropertyChanged("CollectionESGR_FormularioAll");
            }
        }

        private CmpObservableCollection<VMSGR_Formulario> _CollectionVMSGR_Formulario;
        public CmpObservableCollection<VMSGR_Formulario> CollectionVMSGR_Formulario
        {
            get
            {
                if (_CollectionVMSGR_Formulario == null)
                    _CollectionVMSGR_Formulario = new CmpObservableCollection<VMSGR_Formulario>();
                return _CollectionVMSGR_Formulario;
            }
        }

        private async void MethodLoadFormulario()
        {
            await Task.Factory.StartNew(() =>
            {
                try
                {
                    if (CollectionESGR_FormularioAll.Count > 0)
                    {
                        CollectionESGR_FormularioAll.Where(x => x.ESGR_Modulo.IdModulo == IdModulo).ToList().ForEach(x =>
                        {
                            CollectionVMSGR_Formulario.Add(new VMSGR_Formulario()
                            {
                                CodFormulario = x.CodFormulario,
                                NombreFormulario = x.NombreFormulario,
                                Descripcion = x.Descripcion,
                                Consulta = x.Consulta,
                                Nuevo = x.Nuevo,
                                Editar = x.Editar,
                                Eliminar = x.Eliminar
                            });
                        });
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            });
        }
    }
}
