/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   ABEL QUISPE ORELLANA
'* FCH. CREACIÓN : 03/12/2016
**********************************************************/

namespace SGR.ViewModels.Presentation.Documento.Pages
{
    using ComputerSystems;
    using ComputerSystems.Loading;
    using ComputerSystems.WPF;
    using ComputerSystems.WPF.Acciones.Controles.Menus;
    using ComputerSystems.WPF.Acciones.Controles.ModalDialog;
    using ComputerSystems.WPF.Interfaces;
    using ComputerSystems.WPF.Notificaciones;
    using SGR.Models;
    using SGR.Models.Business;
    using SGR.Models.Entity;
    using SGR.ViewModels.Method;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    public class VMSGR_ListadoDocumento : CmpNavigationService, CmpINavigation
    {
        public object Parameter
        {
            set
            {
                Application.Current.Dispatcher.Invoke(async () =>
                {
                    var result = await MTSGR_GetPrivilege.Process(this.GetType(), SGRMessage.TitleMessage, SGRMessageContent.GetAccesoRestringido);
                    if (result == null)
                        CloseCleanWindow();
                    else
                    {
                        PropertyIsEnabledNuevo = result.Nuevo;
                        PropertyIsEnabledEditar = result.Editar;
                        PropertyIsEnabledEliminar = result.Eliminar;
                        CmpLoading = new CmpLoading(MethodLoadHeader, MethodLoadDetail);
                        CmpLoading.Exceptions = ((e) => { CmpMessageBox.Show(SGRMessage.AdministratorDocumento, e.Message, CmpButton.Aceptar); });
                        CmpLoading.LoadHeader();
                    }
                });
            }
        }

        public CmpLoading CmpLoading { get; set; }

        #region COLECCIONES

        private CmpObservableCollection<ESGR_Documento> _CollectionESGR_Documento;
        public CmpObservableCollection<ESGR_Documento> CollectionESGR_Documento
        {
            get
            {
                if (_CollectionESGR_Documento == null)
                    _CollectionESGR_Documento = new CmpObservableCollection<ESGR_Documento>();
                return _CollectionESGR_Documento;
            }
        }

        private CmpObservableCollection<ESGR_EmpresaSucursal> _CollectionESGR_EmpresaSucursal;
        public CmpObservableCollection<ESGR_EmpresaSucursal> CollectionESGR_EmpresaSucursal
        {
            get
            {
                if (_CollectionESGR_EmpresaSucursal == null)
                    _CollectionESGR_EmpresaSucursal = new CmpObservableCollection<ESGR_EmpresaSucursal>();
                return _CollectionESGR_EmpresaSucursal;
            }
        }

        #endregion

        #region OBJETOS SECUNDARIOS

        private ESGR_Documento _SelectESGR_Documento;
        public ESGR_Documento SelectESGR_Documento
        {
            get
            {
                return _SelectESGR_Documento;
            }
            set
            {
                _SelectESGR_Documento = value;
                OnPropertyChanged("SelectESGR_Documento");
            }
        }

        private ESGR_EmpresaSucursal _SelectESGR_EmpresaSucursal;
        public ESGR_EmpresaSucursal SelectESGR_EmpresaSucursal
        {
            get
            {
                return _SelectESGR_EmpresaSucursal;
            }
            set
            {
                if (CollectionESGR_Documento != null)
                    CollectionESGR_Documento.Clear();
                _SelectESGR_EmpresaSucursal = value;
                if(value != null)
                    CmpLoading.LoadDetail();
                OnPropertyChanged("SelectESGR_EmpresaSucursal");
            }
        }

        #endregion

        #region PROPERTY

        private bool _PropertyIsEnabledNuevo;
        public bool PropertyIsEnabledNuevo
        {
            get
            {
                return _PropertyIsEnabledNuevo;
            }
            set
            {
                _PropertyIsEnabledNuevo = value;
                OnPropertyChanged("PropertyIsEnabledNuevo");
            }
        }

        private bool _PropertyIsEnabledEditar;
        public bool PropertyIsEnabledEditar
        {
            get
            {
                return _PropertyIsEnabledEditar;
            }
            set
            {
                _PropertyIsEnabledEditar = value;
                OnPropertyChanged("PropertyIsEnabledEditar");
            }
        }

        private bool _PropertyIsEnabledEliminar;
        public bool PropertyIsEnabledEliminar
        {
            get
            {
                return _PropertyIsEnabledEliminar;
            }
            set
            {
                _PropertyIsEnabledEliminar = value;
                OnPropertyChanged("PropertyIsEnabledEliminar");
            }
        }

        #endregion

        #region ICOMMAND

        public ICommand INuevo
        {
            get
            {
                return CmpICommand.GetICommand((T) =>
                {
                    CmpModalDialog.GetContent().ShowDialog("MPSGR_Documento", new ESGR_Documento() { Opcion = "I" });
                    CmpLoading.LoadDetail();
                });
            }
        }

        public ICommand IEditar
        {
            get
            {
                return CmpICommand.GetICommand((T) =>
                {
                    if (SelectESGR_Documento == null)
                    {
                        CmpMessageBox.Show(SGRMessage.AdministratorDocumento, SGRMessageContent.ContentSelectNull + "Editar", CmpButton.Aceptar);
                        return;
                    }
                    SelectESGR_Documento.Opcion = "U";
                    CmpModalDialog.GetContent().ShowDialog("MPSGR_Documento", SelectESGR_Documento);
                    CmpLoading.LoadDetail();
                });
            }
        }

        public ICommand IEliminar
        {
            get
            {
                return CmpICommand.GetICommand(async(T) =>
                {
                    if (SelectESGR_Documento == null)
                    {
                        CmpMessageBox.Show(SGRMessage.AdministratorDocumento, SGRMessageContent.ContentSelectNull + "Eliminar", CmpButton.Aceptar);
                        return;
                    }
                    await Task.Factory.StartNew(() =>
                    {
                        CmpMessageBox.Show(SGRMessage.AdministratorDocumento, "¿Seguro que desea eliminar el registro?", CmpButton.AceptarCancelar, () =>
                        {
                            try
                            {
                                SelectESGR_Documento.Opcion = "D";
                                new BSGR_Documento().TransDocumento(SelectESGR_Documento);
                                CmpMessageBox.Show(SGRMessage.AdministratorDocumento, SGRMessageContent.ContentDeleteOK, CmpButton.Aceptar);
                                CmpLoading.LoadDetail();
                            }
                            catch (Exception ex)
                            {
                                CmpMessageBox.Show(SGRMessage.AdministratorDocumento, ex.Message, CmpButton.Aceptar);
                            }
                        });
                    });
                });
            }
        }

        public ICommand ISalir
        {
            get
            {
                return CmpICommand.GetICommand((S) =>
                {
                    new CmpNavigationService().CloseCleanWindow();
                });
            }
        }

        #endregion

        #region METODOS

        private void MethodLoadHeader()
        {
            CollectionESGR_EmpresaSucursal.Source = new BSGR_EmpresaSucursal().GetCollectionEmpresaSucursal(SGRVariables.ESGR_Usuario.ESGR_Empresa);
            Application.Current.Dispatcher.Invoke(() =>
            {
                SelectESGR_EmpresaSucursal = CollectionESGR_EmpresaSucursal.FirstOrDefault();
            });
        }

        private void MethodLoadDetail()
        {
            CollectionESGR_Documento.Source = new CmpObservableCollection<ESGR_Documento>(new BSGR_Documento().GetCollectionDocumento().Where(x => x.ESGR_EmpresaSucursal.IdEmpSucursal == ((SelectESGR_EmpresaSucursal == null) ? 0 : SelectESGR_EmpresaSucursal.IdEmpSucursal)));
        }

        #endregion
    }
}
