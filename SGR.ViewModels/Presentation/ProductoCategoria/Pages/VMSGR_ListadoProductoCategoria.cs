/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   ABEL QUISPE ORELLANA
'* FCH. CREACIÓN : 03/12/2016
'*********************************************************
'* 
'* FECHA MODIFIC. : 13/12/2016 [OSCAR HUAMAN CABRERA]
'* MOTIVO MODIFIC.: Se agregaron funcionalidad de los botones y carga de la Grilla
**********************************************************/

namespace SGR.ViewModels.Presentation.ProductoCategoria.Pages
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

    public class VMSGR_ListadoProductoCategoria : CmpNavigationService, CmpINavigation
    {
        public object Parameter
        {
            set
            {
                Application.Current.Dispatcher.Invoke(async() =>
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

        #region OBJETOS SECUNDARIOS

        private ESGR_ProductoCategoria _SelectESGR_ProductoCategoria;
        public ESGR_ProductoCategoria SelectESGR_ProductoCategoria
        {
            get
            {
                return _SelectESGR_ProductoCategoria;
            }
            set
            {
                _SelectESGR_ProductoCategoria = value;
            }
        }

        #endregion

        #region COLECCIONES

        private CmpObservableCollection<ESGR_ProductoCategoria> _CollectionESGR_ProductoCategoria;
        public CmpObservableCollection<ESGR_ProductoCategoria> CollectionESGR_ProductoCategoria
        {
            get
            {
                if (_CollectionESGR_ProductoCategoria == null)
                    _CollectionESGR_ProductoCategoria = new CmpObservableCollection<ESGR_ProductoCategoria>();
                return _CollectionESGR_ProductoCategoria;
            }
        }

        #endregion

        #region PROPERTY

        private string _PropertyFiltrado;
        public string PropertyFiltrado
        {
            get
            {
                return _PropertyFiltrado;
            }
            set
            {
                _PropertyFiltrado = value;
                 MethodLoadDetails();
                OnPropertyChanged("PropertyFiltrado");
            }
        }

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
                        CmpModalDialog.GetContent().ShowDialog("MPSGR_ProductoCategoria", new ESGR_ProductoCategoria() { Opcion = "I" });
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
                    if (SelectESGR_ProductoCategoria == null)
                    {
                        CmpMessageBox.Show(SGRMessage.AdministratorProductoCategoria, SGRMessageContent.ContentSelectNull + "Editar.", CmpButton.Aceptar);
                        return;
                    }
                    SelectESGR_ProductoCategoria.Opcion = "U";
                    CmpModalDialog.GetContent().ShowDialog("MPSGR_ProductoCategoria", SelectESGR_ProductoCategoria);
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
                    if (SelectESGR_ProductoCategoria == null)
                    {
                        CmpMessageBox.Show(SGRMessage.AdministratorProductoCategoria, SGRMessageContent.ContentSelectNull + "Eliminar.", CmpButton.Aceptar);
                        return;
                    }
                    CmpMessageBox.Show(SGRMessage.AdministratorProductoCategoria, "¿Seguro que desea eliminar el registro?", CmpButton.AceptarCancelar, () =>
                    {
                        try
                        {
                            SelectESGR_ProductoCategoria.Opcion = "D";
                            new BSGR_ProductoCategoria().TransProductoCategoria(SelectESGR_ProductoCategoria);
                            CmpMessageBox.Show(SGRMessage.AdministratorProductoCategoria, SGRMessageContent.ContentDeleteOK, CmpButton.Aceptar);
                            MethodLoadDetails();
                        }
                        catch(Exception ex)
                        {
                            CmpMessageBox.Show(SGRMessage.AdministratorProductoCategoria, ex.Message, CmpButton.Aceptar);
                        }
                    });
                });
            }
        }

        public ICommand ISubCategoria
        {
            get
            {
                return CmpICommand.GetICommand((I) =>
                {
                    if (SelectESGR_ProductoCategoria == null)
                    {
                        CmpMessageBox.Show(SGRMessage.AdministratorProductoCategoria, SGRMessageContent.ContentSelectNull + "Agregar SubCategoría.", CmpButton.Aceptar);
                        return;
                    }
                    CmpModalDialog.GetContent().ShowDialog("MPSGR_ProductoSubCategoria", new ESGR_ProductoSubCategoria() { ESGR_ProductoCategoria = SelectESGR_ProductoCategoria });
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
                    Volver();
                });
            }
        }

        #endregion

        #region METODOS

        private async void MethodLoadDetails()
        {
            await Task.Factory.StartNew(() =>
            {
                try
                {
                    CollectionESGR_ProductoCategoria.Source = new BSGR_ProductoCategoria().GetCollectionProductoCategoria(PropertyFiltrado);
                }
                catch (Exception ex)
                {
                    CmpMessageBox.Show(SGRMessage.AdministratorArea, ex.Message, CmpButton.Aceptar);
                }
            });
        }

        #endregion
    }
}
