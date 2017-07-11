/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   ABEL QUISPE ORELLANA
'* FCH. CREACIÓN : 01/12/2016
'*********************************************************
'*  FECHA MODIFIC. : 15/12/2016 [CRISTIAN HERNANDEZ VILLO]
'* MOTIVO MODIFIC.: Se agregaron funcionalidad de los botones y carga de la Grilla
**********************************************************/

namespace SGR.ViewModels.Presentation.Mesa.Pages
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

    public class VMSGR_ListadoMesa : CmpNavigationService, CmpINavigation
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

        #region COLECCIONES

        private CmpObservableCollection<ESGR_Mesa> _CollectionESGR_Mesa;
        public  CmpObservableCollection<ESGR_Mesa> ColletionESGR_Mesa
        {
            get
            {
                if (_CollectionESGR_Mesa == null)
                    _CollectionESGR_Mesa = new CmpObservableCollection<ESGR_Mesa>();
                return _CollectionESGR_Mesa;
            }
        }
        #endregion

        #region OBJ SECUNDARIOS

        private ESGR_Mesa _SelectESGR_Mesa;
        public ESGR_Mesa SelectESGR_Mesa
        {
            get
            {
                return _SelectESGR_Mesa;
            }
            set
            {
                _SelectESGR_Mesa = value;
                OnPropertyChanged("SelectESGR_Mesa");
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
                return CmpICommand.GetICommand((N)=>
                {
                    CmpModalDialog.GetContent().ShowDialog("MPSGR_Mesa", new ESGR_Mesa { Opcion = "I" });
                     MethodLoadDetail();
                });
            }
        }

        public ICommand IEditar
        {
            get
            {
                return CmpICommand.GetICommand((E) =>
                {
                    if (SelectESGR_Mesa == null)
                    {
                        CmpMessageBox.Show(SGRMessage.AdministratorMesa, SGRMessageContent.ContentSelectNull + "Editar.", CmpButton.Aceptar);
                        return;
                    }
                    SelectESGR_Mesa.Opcion = "U";
                    CmpModalDialog.GetContent().ShowDialog("MPSGR_Mesa", SelectESGR_Mesa);
                    MethodLoadDetail();
               });
            }
        }

        public ICommand IEliminar
        {
            get
            {
                return CmpICommand.GetICommand(async (E) =>
                {
                    if (SelectESGR_Mesa == null)
                    {
                        CmpMessageBox.Show(SGRMessage.AdministratorMesa, SGRMessageContent.ContentSelectNull + "Eliminar", CmpButton.Aceptar);
                        return;
                    }
                    await Task.Factory.StartNew(() =>
                    {
                      CmpMessageBox.Show(SGRMessage.AdministratorMesa, "¿Seguro que desea eliminar el registro?", CmpButton.AceptarCancelar, () =>
                      {
                        try
                        {
                            SelectESGR_Mesa.Opcion = "D";
                            new BSGR_Mesa().TransMesa(SelectESGR_Mesa);
                            CmpMessageBox.Show(SGRMessage.AdministratorMesa, SGRMessageContent.ContentDeleteOK, CmpButton.Aceptar);
                            MethodLoadDetail();
                        }
                        catch (Exception ex)
                        {
                           CmpMessageBox.Show(SGRMessage.AdministratorMesa, ex.Message, CmpButton.Aceptar);
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
                return CmpICommand.GetICommand((V) =>
                {
                    CloseCleanWindow();
                });
            }
        }

        #endregion

        #region METODOS

        private async void MethodLoadDetail()
        {
            await Task.Factory.StartNew(() =>
            {
                try
                {
                    ColletionESGR_Mesa.Source = new BSGR_Mesa().GetCollectionMesaMantenimiento();
                }
                catch (Exception ex)
                {
                    CmpMessageBox.Show(SGRMessage.AdministratorMesa, ex.Message, CmpButton.Aceptar);
                }
            });
        }

        #endregion
    }
}
