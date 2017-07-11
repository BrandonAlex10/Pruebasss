/*************************************************************
 * CREADO POR: COMPUTER SYSTEMS SOLUTION
 *             CRISTIAN HERNANDEZ VILLO
 * FECHA CREA: 08/05/2017 
 ************************************************************/

namespace SGR.ViewModels.Presentation.Movimiento_Caja.Pages
{
    using ComputerSystems;
    using ComputerSystems.Loading;
    using ComputerSystems.WPF;
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
    using System.Windows.Input;

    public class VMSGR_ListadoMovimientoCaja: CmpNavigationService, CmpINavigation
    {

        private CmpLoading CmpLoading { get; set; }

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
                        CmpLoading = new CmpLoading(MethodLoadHeader, MethodLoadDetails);
                        CmpLoading.Exceptions = ((e) => { CmpMessageBox.Show(SGRMessage.AdministracionMovimientoCaja, e.Message, CmpButton.Aceptar); });
                        CmpLoading.LoadHeader();
                    }
                });
            }
        }

        #region COLECCIONES

        private CmpObservableCollection<ESGR_MovimientoCaja> _CollectionEGSR_MovimientoCaja;
        public CmpObservableCollection<ESGR_MovimientoCaja> CollectionESGR_MovimientoCaja
        {
            get
            {
                if (_CollectionEGSR_MovimientoCaja == null)
                    _CollectionEGSR_MovimientoCaja = new CmpObservableCollection<ESGR_MovimientoCaja>();
                return _CollectionEGSR_MovimientoCaja;
            }
        }

        private CmpObservableCollection<ESGR_Motivo> _CollectionESGR_Motivo;
        public CmpObservableCollection<ESGR_Motivo> CollectionESGR_Motivo
        {
            get
            {
                if (_CollectionESGR_Motivo == null)
                    _CollectionESGR_Motivo = new CmpObservableCollection<ESGR_Motivo>();
                return _CollectionESGR_Motivo;
                    
            }
        }

        #endregion

        #region OBJ SECUNDARIOS

        private ESGR_MovimientoCaja _SelectedESGR_MovimientoCaja;
        public ESGR_MovimientoCaja SelectedESGR_MovimientoCaja
        {
            get
            {
                return _SelectedESGR_MovimientoCaja;
            }
            set
            {
                _SelectedESGR_MovimientoCaja = value;
                OnPropertyChanged("SelectedESGR_MovimientoCaja");
            }
        }

        private ESGR_Caja _ESGR_Caja;
        public ESGR_Caja ESGR_Caja
        {
            get
            {
                return _ESGR_Caja;
            }
            set
            {
                _ESGR_Caja = value;
                OnPropertyChanged("ESGR_Caja");
            }
        }

        private ESGR_Motivo _SelectedESGR_Motivo;
        public ESGR_Motivo SelectedESGR_Motivo
        {
            get
            {
                return _SelectedESGR_Motivo;
            }
            set
            {
                _SelectedESGR_Motivo = value;
                OnPropertyChanged("SelectedESGR_Motivo");
            }
        }

        private DateTime _SelectedFechaDesde;
        public DateTime SelectedFechaDesde
        {
            get
            {
                return _SelectedFechaDesde;
            }
            set
            {
                _SelectedFechaDesde = value;
                CmpLoading.LoadDetail();
                OnPropertyChanged("SelectedFechaDesde");
            }
        }

        private DateTime _SelectedFechaHasta;
        public DateTime SelectedFechaHasta
        {
            get
            {
                return _SelectedFechaHasta;
            }
            set
            {
                _SelectedFechaHasta = value;
                CmpLoading.LoadDetail();
                OnPropertyChanged("SelectedFechaHasta");
            }
        }

        #endregion

        #region PROPERTYS

        private bool _PropertyValidaFecha;
        public bool PropertyValidaFecha
        {
            get
            {
                return _PropertyValidaFecha;
            }
            set
            {
                _PropertyValidaFecha = value;
                CmpLoading.LoadDetail();
                OnPropertyChanged("PropertyValidaFecha");
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
                return CmpICommand.GetICommand((T) =>
                {
                    CmpModalDialog.GetContent().ShowDialog("MPSGR_MovimientoCaja", new ESGR_MovimientoCaja() { Opcion = "I", ESGR_Caja = ESGR_Caja });
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
                    try
                    {
                        if (SelectedESGR_MovimientoCaja == null)
                        {
                            CmpMessageBox.Show(SGRMessage.AdministracionMovimientoCaja, SGRMessageContent.ContentSelectNull + " Editar", CmpButton.Aceptar);
                            return;
                        }
                        SelectedESGR_MovimientoCaja.Opcion = "U";
                        CmpModalDialog.GetContent().ShowDialog("MPSGR_MovimientoCaja", SelectedESGR_MovimientoCaja);
                    }
                    catch(Exception ex)
                    {
                        CmpMessageBox.Show(SGRMessage.AdministracionMovimientoCaja, ex.Message, CmpButton.Aceptar);
                    }
                });
            }
        }

        public ICommand IEliminar
        {
            get
            {
                return CmpICommand.GetICommand((T) =>
                {
                    try
                    {
                        if (SelectedESGR_MovimientoCaja == null)
                        {
                            CmpMessageBox.Show(SGRMessage.AdministracionMovimientoCaja, SGRMessageContent.ContentSelectNull + " Eliminar", CmpButton.Aceptar);
                            return;
                        }
                        CmpMessageBox.Show(SGRMessage.AdministracionMovimientoCaja, "¿Seguro que desea eliminar el Registro?", CmpButton.AceptarCancelar, async() =>
                        {
                            await Task.Factory.StartNew(() =>
                            {
                                SelectedESGR_MovimientoCaja.Opcion = "D";
                                new BSGR_MovimientoCaja().MethodTransaction(SelectedESGR_MovimientoCaja);
                                CmpMessageBox.Show(SGRMessage.AdministracionMovimientoCaja, SGRMessageContent.ContentDeleteOK, CmpButton.Aceptar, () =>
                                {
                                    CmpLoading.LoadDetail();
                                });
                            });
                        });
                    }
                    catch(Exception ex)
                    {
                        CmpMessageBox.Show(SGRMessage.AdministracionMovimientoCaja, ex.Message, CmpButton.Aceptar);
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
                    Volver();
                });
            }
        }

        #endregion

        #region METODOS

        private void MethodLoadHeader()
        {
            CollectionESGR_Motivo.Source = new BSGR_Motivo().CollectionESGR_Motivo();
            Application.Current.Dispatcher.Invoke(() =>
            {
                CollectionESGR_Motivo.Add(new ESGR_Motivo() { CodMotivo = "TOD", Motivo = "TODOS" });
                if (MethodValidaCaja()) return;
            });

            SelectedFechaDesde = (SelectedFechaHasta = DateTime.Now);
            SelectedESGR_Motivo = CollectionESGR_Motivo.FirstOrDefault(x=>x.CodMotivo == "TOD");
            PropertyValidaFecha = true;
            CmpLoading.LoadDetail();
        }

        private void MethodLoadDetails()
        {
            if (SelectedESGR_Motivo != null)
                CollectionESGR_MovimientoCaja.Source = new BSGR_MovimientoCaja().CollectionESGR_MovimientoCaja(SelectedESGR_Motivo.CodMotivo, SelectedFechaDesde, SelectedFechaHasta, PropertyValidaFecha, ESGR_Caja);
        }

        private bool MethodValidaCaja()
        {
            ESGR_AperturaCierreCaja FirstAperuraCaja = null;
            string Message = string.Empty;
            try
            {
                if (SGRVariables.ESGR_Usuario.ESGR_Perfil.IdPerfil == 1)
                {
                    #region BUSCA CAJAS ABIERTAS PARA ADMINISTRADOR

                    var result = CmpModalDialog.GetContent().ShowDialog("MPSGR_CajaAperturada", null);
                    if (result != null)
                        FirstAperuraCaja = new ESGR_AperturaCierreCaja() { ESGR_Caja = (ESGR_Caja)result };
                    else
                    {
                        System.Threading.Thread.Sleep(100);
                        new CmpNavigationService().Volver();
                    }

                    #endregion
                }
                else
                {
                    #region VALIDA SI TIENE CAJA APERTURADA

                    var CollectionAperuraCaja = new BSGR_AperturaCierreCaja().CollectionESGR_AperturaCierreCaja().Where(x => x.ESGR_Estado.CodEstado == "APTCJ").ToList();
                    FirstAperuraCaja = CollectionAperuraCaja.FirstOrDefault(x => x.ESGR_UsuarioCajero.IdUsuario == SGRVariables.ESGR_Usuario.IdUsuario);
                    if (FirstAperuraCaja == null)
                        CmpMessageBox.Show(SGRMessage.AdministratorVenta, SGRVariables.ESGR_Usuario.Nombres + ", no tiene Caja Aperturada.", CmpButton.Aceptar, () => new CmpNavigationService().Volver());

                    #endregion
                }
                return (FirstAperuraCaja == null);
            }
            catch (Exception ex)
            {
                CmpMessageBox.Show(SGRMessage.AdministratorVenta, ex.Message, CmpButton.Aceptar);
                return true;
            }

            finally
            {
                if (FirstAperuraCaja != null)
                    ESGR_Caja = FirstAperuraCaja.ESGR_Caja;
            }
        }

        #endregion
    }
}
