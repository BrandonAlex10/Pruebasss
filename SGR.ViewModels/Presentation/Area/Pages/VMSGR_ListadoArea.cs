/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   ABEL QUISPE ORELLANA
'* FCH. CREACIÓN : 03/12/2016
**********************************************************/

namespace SGR.ViewModels.Presentation.Area.Pages
{
    using ComputerSystems;
    using ComputerSystems.WPF;
    using ComputerSystems.WPF.Acciones.Controles.ModalDialog;
    using ComputerSystems.WPF.Interfaces;
    using ComputerSystems.WPF.Acciones.Controles.Menus;
    using ComputerSystems.WPF.Notificaciones;
    using SGR.Models.Business;
    using SGR.Models.Entity;
    using SGR.ViewModels.Presentation;
    using System;
    using System.Linq;
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using SGR.ViewModels.Method;

    public class VMSGR_ListadoArea : CmpNavigationService, CmpINavigation
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
                        MethodLoadDetails();
                    }
                });
            }
        }

        #region COLECCIONES

        private CmpObservableCollection<ESGR_Area> _CollectionESGR_Area;
        public CmpObservableCollection<ESGR_Area> CollectionESGR_Area
        {
            get
            {
                if (_CollectionESGR_Area == null)
                    _CollectionESGR_Area = new CmpObservableCollection<ESGR_Area>();
                return _CollectionESGR_Area;
            }
        }

        #endregion

        #region OBJETOS SECUNDARIOS

        private ESGR_Area _SelectESGR_Area;
        public ESGR_Area SelectESGR_Area
        {
            get
            {
                return _SelectESGR_Area;
            }
            set
            {
                _SelectESGR_Area = value;
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
                    CmpModalDialog.GetContent().ShowDialog("MPSGR_Area", new ESGR_Area() { Opcion = "I" });
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
                    if (SelectESGR_Area == null)
                    {
                        CmpMessageBox.Show(SGRMessage.AdministratorArea, SGRMessageContent.ContentSelectNull + "Editar.", CmpButton.Aceptar);
                        return;
                    }
                    SelectESGR_Area.Opcion = "U";
                    CmpModalDialog.GetContent().ShowDialog("MPSGR_Area", SelectESGR_Area);
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
                    if (SelectESGR_Area == null)
                    {
                        CmpMessageBox.Show(SGRMessage.AdministratorArea, SGRMessageContent.ContentSelectNull + "Eliminar.", CmpButton.Aceptar);
                        return;
                    }
                    try
                    {
                        CmpMessageBox.Show(SGRMessage.AdministratorArea, "¿Seguro que desea eliminar el registro?", CmpButton.AceptarCancelar, () =>
                        {
                            SelectESGR_Area.Opcion = "D";
                            new BSGR_Area().TransArea(SelectESGR_Area);
                            CmpMessageBox.Show(SGRMessage.AdministratorArea, SGRMessageContent.ContentDeleteOK, CmpButton.Aceptar);
                            MethodLoadDetails();
                        } );
                        
                    }
                    catch (Exception ex)
                    {
                        CmpMessageBox.Show(SGRMessage.AdministratorArea, ex.Message, CmpButton.Aceptar);
                    }
                });
            }
        }

        public ICommand ISalir
        {
            get
            {
                return CmpICommand.GetICommand((D) =>
                {
                    new CmpNavigationService().CloseCleanWindow();
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
                    CollectionESGR_Area.Source = new BSGR_Area().GetCollectionArea();
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
