using ComputerSystems.WPF.Notificaciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComputerSystems.WPF.Interfaces;
using SGR.Models.Entity;
using ComputerSystems;
using ComputerSystems.WPF;
using SGR.Models.Business;
using System.Windows.Input;
using ComputerSystems.WPF.Acciones.Controles.ModalDialog;
using System.Windows;
using SGR.ViewModels.Method;

namespace SGR.ViewModels.Presentation.Caja.Pages
{
    public class VMSGR_ListadoCaja: CmpNavigationService, CmpINavigation
    {
        private ESGR_Caja _ESGR_Caja;
        public ESGR_Caja ESGR_Caja
        {
            get 
            {
                if (_ESGR_Caja == null)
                    _ESGR_Caja = new ESGR_Caja();
                return _ESGR_Caja;
            }
            set
            {
                _ESGR_Caja = value;
                OnPropertyChanged("ESGR_Caja");
            }
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

        #region COLECCIONES

        private CmpObservableCollection<ESGR_Caja> _CollectionESGR_Caja;
        public CmpObservableCollection<ESGR_Caja> CollectionESGR_Caja
        {
            get
            {
                if (_CollectionESGR_Caja == null)
                    _CollectionESGR_Caja = new CmpObservableCollection<ESGR_Caja>();
                return _CollectionESGR_Caja;
            }
        }

        #endregion

        #region OBJ SECUNDARIOS

        private ESGR_Caja _SelectedESGR_Caja;
        public ESGR_Caja SelectedESGR_Caja
        {
            get
            {
                return _SelectedESGR_Caja;
            }
            set
            {
                _SelectedESGR_Caja = value;
                OnPropertyChanged("SelectedESGR_Caja");
            }
        }

        #endregion

        #region PROPERTYS

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
                    CmpModalDialog.GetContent().ShowDialog("MPSGR_Caja", new ESGR_Caja() { Opcion = "I" });
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
                    if (SelectedESGR_Caja == null)
                    {
                        CmpMessageBox.Show(SGRMessage.AdministratorArea, SGRMessageContent.ContentSelectNull + "Editar.", CmpButton.Aceptar);
                        return;
                    }
                    SelectedESGR_Caja.Opcion = "U";
                    CmpModalDialog.GetContent().ShowDialog("MPSGR_Caja", SelectedESGR_Caja);
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
                    if (SelectedESGR_Caja == null)
                    {
                        CmpMessageBox.Show(SGRMessage.AdministratorArea, SGRMessageContent.ContentSelectNull + "Eliminar.", CmpButton.Aceptar);
                        return;
                    }
                    try
                    {
                        CmpMessageBox.Show(SGRMessage.AdministratorArea, "¿Seguro que desea eliminar el registro?", CmpButton.AceptarCancelar, () =>
                        {
                            SelectedESGR_Caja.Opcion = "D";
                            new BSGR_Caja().MethodTransaccionCaja(SelectedESGR_Caja);
                            CmpMessageBox.Show(SGRMessage.AdministratorArea, SGRMessageContent.ContentDeleteOK, CmpButton.Aceptar);
                            MethodLoadDetails();
                        });

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
                return CmpICommand.GetICommand((T) =>
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
                    CollectionESGR_Caja.Source = new BSGR_Caja().CollectionESGR_Caja();
                }
                catch (Exception ex)
                {
                    CmpMessageBox.Show(SGRMessage.AdministracionCaja, ex.Message, CmpButton.Aceptar);
                }
            });
        }

        #endregion

    }
}
