/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   ABEL QUISPE ORELLANA
'* FCH. CREACIÓN : 03/12/2016
**********************************************************/

namespace SGR.ViewModels.Presentation.Moneda.Pages
{
    using ComputerSystems;
    using ComputerSystems.WPF;
    using ComputerSystems.WPF.Acciones.Controles.Menus;
    using ComputerSystems.WPF.Acciones.Controles.ModalDialog;
    using ComputerSystems.WPF.Interfaces;
    using ComputerSystems.WPF.Notificaciones;
    using SGR.Models.Business;
    using SGR.Models.Entity;
    using SGR.ViewModels.Method;
    using System;
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Input;

    public class VMSGR_ListadoMoneda : CmpNavigationService, CmpINavigation
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
                        MethodLoadDetail();
                    }
                });
            }
        }

        #region OBJETOS SECUNDARIOS

        private ESGR_Moneda _SelectESGR_Moneda;
        public ESGR_Moneda SelectESGR_Moneda
        {
            get
            {
                return _SelectESGR_Moneda;
            }
            set
            {
                _SelectESGR_Moneda = value;
            }
        }

        #endregion

        #region COLECCIONES

        private CmpObservableCollection<ESGR_Moneda> _CollectionESGR_Moneda;
        public CmpObservableCollection<ESGR_Moneda> CollectionESGR_Moneda
        {
            get
            {
                if (_CollectionESGR_Moneda == null)
                    _CollectionESGR_Moneda = new CmpObservableCollection<ESGR_Moneda>();
                return _CollectionESGR_Moneda;
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
                return CmpICommand.GetICommand((N) =>
                {
                    CmpModalDialog.GetContent().ShowDialog("MPSGR_Moneda", new ESGR_Moneda { Opcion = "I" });
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
                    if (SelectESGR_Moneda == null)
                    {
                        CmpMessageBox.Show(SGRMessage.AdministratorMoneda, SGRMessageContent.ContentSelectNull + "Editar.", CmpButton.Aceptar);
                        return;
                    }
                    SelectESGR_Moneda.Opcion = "U";
                    CmpModalDialog.GetContent().ShowDialog("MPSGR_Moneda", SelectESGR_Moneda);
                    MethodLoadDetail();
                });
            }
        }

        public ICommand IEliminar
        {
            get
            {
                return CmpICommand.GetICommand((D) =>
                {
                    if (SelectESGR_Moneda == null)
                    {
                        CmpMessageBox.Show(SGRMessage.AdministratorMoneda, SGRMessageContent.ContentSelectNull + "Eliminar.", CmpButton.Aceptar);
                        return;
                    }
                    CmpMessageBox.Show(SGRMessage.AdministratorMoneda, "¿Seguro que desea eliminar el registro?", CmpButton.AceptarCancelar, () =>
                    {
                        try
                        {
                            SelectESGR_Moneda.Opcion = "D";
                            new BSGR_Moneda().TransMoneda(SelectESGR_Moneda);
                            CmpMessageBox.Show(SGRMessage.AdministratorMoneda, SGRMessageContent.ContentDeleteOK, CmpButton.Aceptar);
                            MethodLoadDetail();
                        }
                        catch (Exception ex)
                        {
                            CmpMessageBox.Show(SGRMessage.AdministratorMoneda, ex.Message, CmpButton.Aceptar);
                        }
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

        public async void MethodLoadDetail()
        {
            await Task.Factory.StartNew(() =>
            {
                try
                {
                    CollectionESGR_Moneda.Source = new BSGR_Moneda().GetCollectionMoneda();
                }
                catch (Exception ex)
                {
                    CmpMessageBox.Show(SGRMessage.AdministratorMoneda, ex.Message, CmpButton.Aceptar);
                }
            });
        }

        #endregion
    }
}
