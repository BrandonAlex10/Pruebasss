/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   ABEL QUISPE ORELLANA
'* FCH. CREACIÓN : 03/12/2016
**********************************************************/

namespace SGR.ViewModels.Presentation.MedioPago.Pages
{
    using ComputerSystems;
    using ComputerSystems.WPF;
    using ComputerSystems.WPF.Acciones.Controles.ModalDialog;
    using ComputerSystems.WPF.Interfaces;
    using ComputerSystems.WPF.Notificaciones;
    using SGR.Models.Business;
    using SGR.Models.Entity;
    using SGR.ViewModels.Method;
    using System;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Input;

    public class VMSGR_ListadoMedioPago : CmpNavigationService, CmpINavigation
    {
        public VMSGR_ListadoMedioPago()
        {
            MethodLoadDetails();
        }

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
                        MethodLoadDetails();
                    }
                });
            }
        }

        #region OBJETOS SECUNDARIOS

        private ESGR_MedioPago _SelectESGR_MedioPago;
        public ESGR_MedioPago SelectESGR_MedioPago
        {
            get
            {
                return _SelectESGR_MedioPago;
            }
            set
            {
                _SelectESGR_MedioPago = value;
                OnPropertyChanged("SelectESGR_MedioPago");
            }
        }

        #endregion

        #region COLECCIONES

        private CmpObservableCollection<ESGR_MedioPago> _CollectionESGR_MedioPago;
        public CmpObservableCollection<ESGR_MedioPago> CollectionESGR_MedioPago
        {
            get
            {
                if (_CollectionESGR_MedioPago == null)
                    _CollectionESGR_MedioPago = new CmpObservableCollection<ESGR_MedioPago>();
                return _CollectionESGR_MedioPago;
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
                return CmpICommand.GetICommand((I) =>
                {
                    CmpModalDialog.GetContent().ShowDialog("MPSGR_MedioPago", new ESGR_MedioPago() { Opcion = "I" });
                    MethodLoadDetails();
                });
            }
        }
        
        public ICommand IEditar
        {
            get
            {
                return CmpICommand.GetICommand((U) =>
                {
                    if (SelectESGR_MedioPago == null)
                    {
                        CmpMessageBox.Show(SGRMessage.AdministratorMedioPago, SGRMessageContent.ContentSelectNull + "Editar.", CmpButton.Aceptar);
                        return;
                    }
                    SelectESGR_MedioPago.Opcion = "U";
                    CmpModalDialog.GetContent().ShowDialog("MPSGR_MedioPago", SelectESGR_MedioPago);
                    MethodLoadDetails();
                });
            }
        }

        public ICommand IEliminar
        {
            get
            {
                return CmpICommand.GetICommand((D) =>
                {
                    if (SelectESGR_MedioPago == null)
                    {
                        CmpMessageBox.Show(SGRMessage.AdministratorMedioPago, SGRMessageContent.ContentSelectNull + "Eliminar.", CmpButton.Aceptar);
                        return;
                    }
                    
                    CmpMessageBox.Show(SGRMessage.AdministratorMedioPago, "¿Seguro que desea eliminar el registro?", CmpButton.AceptarCancelar, () =>
                    {
                        try
                        {
                            SelectESGR_MedioPago.Opcion = "D";
                            new BSGR_MedioPago().TransMedioPago(SelectESGR_MedioPago);
                            CmpMessageBox.Show(SGRMessage.AdministratorMedioPago, SGRMessageContent.ContentDeleteOK, CmpButton.Aceptar);
                            MethodLoadDetails();
                        }
                        catch (Exception ex)
                        {
                            CmpMessageBox.Show(SGRMessage.AdministratorMedioPago, ex.Message, CmpButton.Aceptar);
                        }
                    });
                });
            }
        }

        public ICommand ISalir
        {
            get
            {
                return CmpICommand.GetICommand((D) =>
                {
                    CloseCleanWindow();
                });
            }
        }

        #endregion

        #region METODOS

        public async void MethodLoadDetails()
        {
            await Task.Factory.StartNew(() =>
            {
                try
                {
                    CollectionESGR_MedioPago.Source = new BSGR_MedioPago().GetCollectionMedioPago();
                }
                catch (Exception ex)
                {
                    CmpMessageBox.Show(SGRMessage.AdministratorMedioPago, ex.Message, CmpButton.Aceptar);
                }
            });
        }

        #endregion

    }
}
