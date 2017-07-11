/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   ABEL QUISPE ORELLANA
'* FCH. CREACIÓN : 02/12/2016
'*********************************************************
'*  FECHA MODIFIC. : 15/12/2016 [CRISTIAN HERNANDEZ VILLO]
'* MOTIVO MODIFIC.: Se agregaron funcionalidad de los botones y carga de la Grilla
**********************************************************/

namespace SGR.ViewModels.Presentation.Cliente.Pages
{
    using ComputerSystems;
    using ComputerSystems.WPF.Acciones.Controles.ModalDialog;
    using ComputerSystems.WPF.Interfaces;
    using ComputerSystems.WPF.Notificaciones;
    using System;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using SGR.Models.Entity;
    using SGR.Models.Business;
    using ComputerSystems.WPF;
    using System.Windows;
    using SGR.ViewModels.Method;

    public class VMSGR_ListadoCliente : CmpNavigationService, CmpINavigation
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
                        MethodLoadDetails();
                    }
                });
            }
        }

        #region COLECCIONES

        private CmpObservableCollection<ESGR_Cliente> _CollectionESGR_Cliente;
        public  CmpObservableCollection<ESGR_Cliente> CollectionESGR_Cliente
        {
            get
            {
                if (_CollectionESGR_Cliente == null)
                    _CollectionESGR_Cliente = new CmpObservableCollection<ESGR_Cliente>();
                return _CollectionESGR_Cliente;
            }
        }
        
        #endregion

        #region OBJ SECUNDARIO

        private string _PropertyFiltro;
        public string PropertyFiltro
        {
            get
            {
                return _PropertyFiltro;
            }
            set 
            {
                _PropertyFiltro = value;
                MethodLoadDetails();
                OnPropertyChanged("PropertyFiltro");
            }

        }

        private ESGR_Cliente _SelectESGR_Cliente;
        public ESGR_Cliente SelectESGR_Cliente
        {
            get
            {
                return _SelectESGR_Cliente;
            }
            set
            {
                _SelectESGR_Cliente = value;
                OnPropertyChanged("SelectESGR_Cliente");
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

        public ICommand IEditar
        {
            get
            {
                return CmpICommand.GetICommand((E) =>
                {
                    if (SelectESGR_Cliente == null)
                    {
                        CmpMessageBox.Show(SGRMessage.AdministratorCliente, SGRMessageContent.ContentSelectNull + "Editar.", CmpButton.Aceptar);
                        return;
                    }
                    SelectESGR_Cliente.Opcion = "U";
                    CmpModalDialog.GetContent().ShowDialog("MPSGR_Cliente", SelectESGR_Cliente);
                    MethodLoadDetails();
                });
            }
        }

        public ICommand ISalir
        {
            get
            {
                return CmpICommand.GetICommand((S) =>
                {
                    new CmpNavigationService().Volver();
                });
            }
        }
        
        public ICommand INuevo
        {
          get
            {
                return CmpICommand.GetICommand((N)=>
                {
                    CmpModalDialog.GetContent().ShowDialog("MPSGR_Cliente", new ESGR_Cliente { Opcion = "I" });
                     MethodLoadDetails();
                });
            }
        }

        public ICommand IEliminar
        {
            get
            {
                return CmpICommand.GetICommand(async(E) =>
                {
                    if (SelectESGR_Cliente == null)
                    {
                        CmpMessageBox.Show(SGRMessage.AdministratorCliente, SGRMessageContent.ContentSelectNull + "Eliminar.", CmpButton.Aceptar);
                        return;
                    }
                    await Task.Factory.StartNew(() =>
                    {
                        try
                        {
                            CmpMessageBox.Show(SGRMessage.AdministratorCliente, "¿Seguro que desea eliminar el registro?", CmpButton.AceptarCancelar, () =>
                            {
                                SelectESGR_Cliente.Opcion = "D";
                                new BSGR_Cliente().TransCliente(SelectESGR_Cliente);
                                CmpMessageBox.Show(SGRMessage.AdministratorCliente, SGRMessageContent.ContentDeleteOK, CmpButton.Aceptar);
                                MethodLoadDetails();
                            });
                        }
                        catch (Exception ex)
                        {
                            CmpMessageBox.Show(SGRMessage.AdministratorProducto, ex.Message, CmpButton.Aceptar);
                        }
                    });
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
                    CollectionESGR_Cliente.Source = new BSGR_Cliente().GetCollectionCliente(PropertyFiltro);
                }
                catch (Exception ex)
                {
                    CmpMessageBox.Show(SGRMessage.AdministratorCliente, ex.Message, CmpButton.Aceptar);
                }
            });
        }

        #endregion
    }
}
