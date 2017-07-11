/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   ABEL QUISPE ORELLANA
'* FCH. CREACIÓN : 03/12/2016
**********************************************************
'* MODIFICADO POR : COMPUTER SYSTEMS SOLUTION
'*				   CRISTIAN A. HERNANDEZ VILLO
'* FCH. CREACIÓN : 07/03/2017
**********************************************************/

namespace SGR.ViewModels.Presentation.Venta.Pages
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
    using System.Windows;
    using System.Windows.Input;

    public class VMSGR_ListadoVenta : CmpNavigationService, CmpINavigation
    {
        private CmpLoading CmpLoading { get; set; }

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
                        if (MethodValidaCaja()) return;
                        PropertyVisibilityFiltro = Visibility.Visible;
                        PropertyVisibilityFecha = Visibility.Collapsed;
                        CmpLoading = new CmpLoading(MethodLoadHeader, MethodLoadDetails);
                        CmpLoading.Exceptions = ((e) => { CmpMessageBox.Show(SGRMessage.AdministratorDocumento, e.Message, CmpButton.Aceptar); });
                        CmpLoading.LoadHeader();
                    }
                });
            }
        }

        #region COLECCIONES

        private CmpObservableCollection<ESGR_Venta> _CollectionESGR_Venta;
        public CmpObservableCollection<ESGR_Venta> CollectionESGR_Venta
        {
            get
            {
                if (_CollectionESGR_Venta == null)
                    _CollectionESGR_Venta = new CmpObservableCollection<ESGR_Venta>();
                return _CollectionESGR_Venta;
            }
        }

        private CmpObservableCollection<ESGR_Item> _CollectionESGR_Item;
        public CmpObservableCollection<ESGR_Item> CollectionEGSR_Item
        {
            get
            {
                if (_CollectionESGR_Item == null)
                    _CollectionESGR_Item = new CmpObservableCollection<ESGR_Item>();
                return _CollectionESGR_Item;
            }
        }

        #endregion

        #region OBJ SECUNDARIOS

        private ESGR_Venta _SelectedESGR_Venta;
        public ESGR_Venta SelectedESGR_Venta
        {
            get
            {
                return _SelectedESGR_Venta;
            }
            set
            {
                _SelectedESGR_Venta = value;
                OnPropertyChanged("SelectedESGR_Venta");
            }
        }

        private ESGR_Item _SelectedESGR_Item;
        public ESGR_Item SelectedESGR_Item
        {
            get
            {
                return _SelectedESGR_Item;
            }
            set
            {
                _SelectedESGR_Item = value;
                if (value != null)
                {
                    if (value.ValueValuePath == "Fecha")
                    {
                        PropertyVisibilityFecha = Visibility.Visible;
                        PropertyVisibilityFiltro = Visibility.Collapsed;
                    }
                    else
                    {
                        PropertyVisibilityFiltro = Visibility.Visible;
                        PropertyVisibilityFecha = Visibility.Collapsed;
                        PropertyTitleFiltro = ((value.ValueValuePath == "Cliente") ? "Filtro por [Razon Social / Ruc]" : "Filtrar por [Serie / Numero Documento]");
                    }
                    CmpLoading.LoadDetail();
                }
                OnPropertyChanged("SelectedESGR_Item");
            }

        }

        private DateTime _SelectedFechaIni;
        public DateTime SelectedFechaIni
        {
            get
            {
                return _SelectedFechaIni;
            }
            set
            {
                _SelectedFechaIni = value;
                if (value != null)
                    CmpLoading.LoadDetail();
                OnPropertyChanged("SelectedFechaIni");
            }
        }

        private DateTime _SelectedFechaFin;
        public DateTime SelectedFechaFin
        {
            get
            {
                return _SelectedFechaFin;
            }
            set 
            {
                _SelectedFechaFin = value;
                if (value != null)
                    CmpLoading.LoadDetail();
                OnPropertyChanged("SelectedFechaFin");
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

        #endregion

        #region PROPERTY

        private string _PropertyFiltroVenta;
        public string PropertyFiltroVenta
        {
            get
            {
                return _PropertyFiltroVenta;
            }
            set
            {
                _PropertyFiltroVenta = value;
                if (value != null)
                    CmpLoading.LoadDetail();
                OnPropertyChanged("PropertyFiltroVenta");
            }
        }

        private Visibility _PropertyVisibilityFiltro;
        public Visibility PropertyVisibilityFiltro
        {
            get
            {
                return _PropertyVisibilityFiltro;
            }
            set
            {
                _PropertyVisibilityFiltro = value;
                OnPropertyChanged("PropertyVisibilityFiltro");
            }
        }

        private Visibility _PropertyVisibilityFecha;
        public Visibility PropertyVisibilityFecha
        {
            get
            {
                return _PropertyVisibilityFecha;
            }
            set
            {
                _PropertyVisibilityFecha = value;
                OnPropertyChanged("PropertyVisibilityFecha");
            }
        }

        private string _PropertyTitleFiltro;
        public string PropertyTitleFiltro
        {
            get
            {
                return _PropertyTitleFiltro;
            }
            set
            {
                _PropertyTitleFiltro = value;
                OnPropertyChanged("PropertyTitleFiltro");
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
                    Ir(CmpMenu.GetPage("PGSRG_Venta"), new ESGR_Venta() { Opcion = "I", ESGR_Caja = ESGR_Caja });
                });
            }
        }

        public ICommand IEditar
        {
            get
            {
                return CmpICommand.GetICommand((T) =>
                {
                    if (SelectedESGR_Venta == null)
                    {
                        CmpMessageBox.Show(SGRMessage.AdministratorVenta,SGRMessageContent.ContentSelectNull + "Editar",CmpButton.Aceptar);
                        return;
                    }
                    SelectedESGR_Venta.Opcion = "U";
                    SelectedESGR_Venta.ESGR_Caja = ESGR_Caja;
                    Ir(CmpMenu.GetPage("PGSRG_Venta"), SelectedESGR_Venta);
                });
            }
        }

        public ICommand IEliminar
        {
            get
            {
                return CmpICommand.GetICommand((T) =>
                {
                    if (SelectedESGR_Venta == null)
                    {
                        CmpMessageBox.Show(SGRMessage.AdministratorVenta, SGRMessageContent.ContentSelectNull + "Eliminar", CmpButton.Aceptar);
                        return;
                    }
                    CmpMessageBox.Show(SGRMessage.AdministratorVenta, "¿Seguro que desea Eliminar el Registro. ?", CmpButton.AceptarCancelar, () =>
                    {
                        try
                        {
                            SelectedESGR_Venta.Opcion = "U";
                            new BSGR_Venta().TransVenta(SelectedESGR_Venta);
                            CmpMessageBox.Show(SGRMessage.AdministratorVenta, SGRMessageContent.QuestionDelete, CmpButton.Aceptar, () =>
                            {
                                MethodLoadDetails();
                            });
                        }
                        catch (Exception ex)
                        {
                            CmpMessageBox.Show(SGRMessage.AdministratorVenta, ex.Message, CmpButton.Aceptar);
                        }
                    });
                });
            }
        }

        public ICommand ISalir
        {
            get
            {
                return CmpICommand.GetICommand((T) =>
                {
                    new CmpNavigationService().Volver();
                });
            }
        }

        #endregion

        #region METODOS

        private void MethodLoadHeader()
        {
            try
            {
                CollectionEGSR_Item.Source = MethodLoaditem();
                SelectedESGR_Item = CollectionEGSR_Item.FirstOrDefault();
                if (SelectedFechaIni == new DateTime() || SelectedFechaFin == new DateTime())
                {
                    SelectedFechaIni = DateTime.Now;
                    SelectedFechaFin = DateTime.Now;
                }
                PropertyFiltroVenta = string.Empty;
            }
            catch (Exception ex)
            {
                CmpMessageBox.Show(SGRMessage.AdministratorVenta, ex.Message, CmpButton.Aceptar);
            }
        }

        private void MethodLoadDetails()
        {
            try
            {
                CollectionESGR_Venta.Source = new BSGR_Venta().GetCollectionVenta(SelectedESGR_Item.ValueMemberPath,SelectedFechaIni, SelectedFechaFin, PropertyFiltroVenta, ESGR_Caja);
            }
            catch (Exception ex)
            {
                CmpMessageBox.Show(SGRMessage.AdministratorVenta, ex.Message, CmpButton.Aceptar);
            }
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

        private CmpObservableCollection<ESGR_Item> MethodLoaditem()
        {
            CmpObservableCollection<ESGR_Item> CollectionItemOpciones = new CmpObservableCollection<ESGR_Item>();
            CollectionItemOpciones.Add(new ESGR_Item { ValueMemberPath = "Fecha", ValueValuePath = "Fecha" });
            return CollectionItemOpciones;
        }
        
        #endregion
    }
}
