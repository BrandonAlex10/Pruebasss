/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
 * ********************************************************
 * 
'* FCH. CREACIÓN : 03/12/2016 [ABEL QUISPE ORELLANA]
 * 
'* ********************************************************
 * 
'* FCH. MODIFICACION : 10/12/2016 [OSCAR HUAMAN CABRERA]
'* MOTIVO: Se hizo cargar el listado, se dio funcionalidad a los botones 
 * 
**********************************************************/

namespace SGR.ViewModels.Presentation.TipoCambio.Pages
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

    public class VMSGR_ListadoTipoCambio : CmpNavigationService, CmpINavigation
    {
        public object Parameter
        {
            set
            {
                Application.Current.Dispatcher.Invoke(async () =>
                {
                    var result = await MTSGR_GetPrivilege.Process(this.GetType(), SGRMessage.TitleMessage, SGRMessageContent.GetAccesoRestringido);
                    if (result == null)
                        Volver();
                    else
                    {
                        PropertyIsEnabledNuevo = result.Nuevo;
                        PropertyIsEnabledEditar = result.Editar;
                        PropertyIsEnabledEliminar = result.Eliminar;
                        MethodLoadDetail();
                    }
                });
            }
        }

        #region COLECCIONES

        private CmpObservableCollection<ESGR_TipoCambio> _CollectionESGR_TipoCambio;
        public CmpObservableCollection<ESGR_TipoCambio> CollectionESGR_TipoCambio
        {
            get
            {
                if (_CollectionESGR_TipoCambio == null)
                    _CollectionESGR_TipoCambio = new CmpObservableCollection<ESGR_TipoCambio>();
                return _CollectionESGR_TipoCambio;
            }
        }

        #endregion

        #region OBJETOS SECUNDARIOS

        private ESGR_TipoCambio _SelectESGR_TipoCambio;
        public ESGR_TipoCambio SelectESGR_TipoCambio
        {
            get
            {
                return _SelectESGR_TipoCambio;
            }
            set
            {
                _SelectESGR_TipoCambio = value;
                OnPropertyChanged();
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
                    CmpModalDialog.GetContent().ShowDialog("MPSGR_TipoCambio", new ESGR_TipoCambio() { Opcion = "I" });
                    MethodLoadDetail();
                });
            }
        }

        public ICommand IEditar
        {
            get
            {
                return CmpICommand.GetICommand((U) =>
                {
                    if (SelectESGR_TipoCambio == null)
                    {
                        CmpMessageBox.Show(SGRMessage.AdministratorTipoCambio, SGRMessageContent.ContentSelectNull + "Editar.", CmpButton.Aceptar);
                        return;
                    }
                    SelectESGR_TipoCambio.Opcion = "U";
                    CmpModalDialog.GetContent().ShowDialog("MPSGR_TipoCambio", SelectESGR_TipoCambio);
                    MethodLoadDetail();
                });
            }
        }

        public ICommand IEliminar
        {
            get
            {
                return CmpICommand.GetICommand(async(U) =>
                {
                    if (SelectESGR_TipoCambio == null)
                    {
                        CmpMessageBox.Show(SGRMessage.AdministratorTipoCambio, SGRMessageContent.ContentSelectNull + "Eliminar.", CmpButton.Aceptar);
                        return;
                    }
                    await Task.Factory.StartNew(() =>
                    {
                        try
                        {
                            CmpMessageBox.Show(SGRMessage.AdministratorTipoCambio, "¿Seguro que desea eliminar el registro?", CmpButton.AceptarCancelar, () =>
                            {
                                SelectESGR_TipoCambio.Opcion = "D";
                                new BSGR_TipoCambio().TransTipoCambio(SelectESGR_TipoCambio);
                                CmpMessageBox.Show(SGRMessage.AdministratorTipoCambio, SGRMessageContent.ContentDeleteOK, CmpButton.Aceptar);
                                MethodLoadDetail();
                            });
                        }
                        catch (Exception ex)
                        {
                            CmpMessageBox.Show(SGRMessage.AdministratorTipoCambio, ex.Message, CmpButton.Aceptar);
                        }
                    });
                });
            }
        }

        public ICommand ISalir
        {
            get
            {
                return CmpICommand.GetICommand((s) =>
                {
                    Volver();
                });
            }
        }

        #endregion

        #region METODOS

        public async void MethodLoadDetail()
        {
            await Task.Factory.StartNew(() =>
            {
                try
                {
                    CollectionESGR_TipoCambio.Source = new BSGR_TipoCambio().GetCollectionTipoCambio();
                }
                catch (Exception ex)
                {
                    CmpMessageBox.Show(SGRMessage.AdministratorTipoCambio, ex.Message, CmpButton.Aceptar);
                }
            });
        }

        #endregion
    }
}
