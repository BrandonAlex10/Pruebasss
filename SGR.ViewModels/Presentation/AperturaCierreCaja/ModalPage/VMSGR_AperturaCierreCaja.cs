/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   OSCAR HUAMAN CABRERA
'* FCH. CREACIÓN : 02/06/2017
**********************************************************/

namespace SGR.ViewModels.Presentation.AperturaCierreCaja.ModalPage
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
    using SGR.ViewModels.AperturaCierreCaja.Models;
    using SGR.ViewModels.Method;
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Input;

    public class VMSGR_AperturaCierreCaja : CmpNavigationService, CmpIModalDialog
    {
        private ESGR_AperturaCierreCaja _ESGR_AperturaCierreCaja;
        public ESGR_AperturaCierreCaja ESGR_AperturaCierreCaja
        {
            get 
            {
                if (_ESGR_AperturaCierreCaja == null)
                    _ESGR_AperturaCierreCaja = new ESGR_AperturaCierreCaja();
                return _ESGR_AperturaCierreCaja;
            }
            set 
            {
                _ESGR_AperturaCierreCaja = value;
                OnPropertyChanged("ESGR_AperturaCaja");
            }
        }

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
                        if (value is ESGR_AperturaCierreCaja)
                        {
                            PropertyIsEnabledGuardar = result.Nuevo;
                            ESGR_AperturaCierreCaja = (ESGR_AperturaCierreCaja)value;
                            CmpLoading = new CmpLoading(MethodLoadHeader, MethodLoadDetail);
                            CmpLoading.Exceptions = ((e) => { CmpMessageBox.Show(SGRMessage.AdministracionAperturaCierreCaja, e.Message, CmpButton.Aceptar); });
                            CmpLoading.LoadHeader();
                            PropertyWidthModalPage = (ESGR_AperturaCierreCaja.ESGR_Estado.CodEstado == "APTCJ" || ESGR_AperturaCierreCaja.Opcion == "V") ? 850 : 550;
                        }
                    }
                });
            }
        }

        #region COLECCIONES

        private CmpObservableCollection<ESGR_Usuario> _CollectionESGR_UsuarioCajero;
        public CmpObservableCollection<ESGR_Usuario> CollectionESGR_UsuarioCajero
        {
            get
            {
                if (_CollectionESGR_UsuarioCajero == null)
                    _CollectionESGR_UsuarioCajero = new CmpObservableCollection<ESGR_Usuario>();
                return _CollectionESGR_UsuarioCajero;
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

        private CmpObservableCollection<MDSGR_AperturaCierreCajaDetalle> _CollectionMDSGR_AperturaCajaDetalle;
        public CmpObservableCollection<MDSGR_AperturaCierreCajaDetalle> ColletionMDSGR_AperturaCajaDetalle
        {
            get
            {
                if (_CollectionMDSGR_AperturaCajaDetalle == null)
                    _CollectionMDSGR_AperturaCajaDetalle = new CmpObservableCollection<MDSGR_AperturaCierreCajaDetalle>();
                return _CollectionMDSGR_AperturaCajaDetalle;
            }
        }

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

        private CmpObservableCollection<ESGR_Estado> _CollectionESGR_Estado;
        public CmpObservableCollection<ESGR_Estado> CollectionESGR_Estado
        {
            get
            {
                if (_CollectionESGR_Estado == null)
                    _CollectionESGR_Estado = new CmpObservableCollection<ESGR_Estado>();
                return _CollectionESGR_Estado;
            }
        }

        #endregion

        #region OBJ SECUNDARIO

        private ESGR_Usuario _SelectedESGR_UsuarioCajero;
        public ESGR_Usuario SelectedESGR_UsuarioCajero
        {
            get
            {
                return _SelectedESGR_UsuarioCajero;
            }
            set
            {
                _SelectedESGR_UsuarioCajero = value;
                if (value != null)
                ESGR_AperturaCierreCaja.ESGR_UsuarioCajero = value;
                OnPropertyChanged("SelectedESGR_UsuarioCajero");
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
                if (value != null)
                    ESGR_AperturaCierreCaja.ESGR_Motivo = value;
                OnPropertyChanged("SelectedESGR_Motivo");
            }
        }

        private DateTime _SelectedFecha;
        public DateTime SelectedFecha
        {
            get
            {
                return _SelectedFecha;
            }
            set
            {
                _SelectedFecha = value;
                if (ESGR_AperturaCierreCaja.ESGR_Estado.CodEstado == "CRDCJ")   //CodEstado CRDCJ = Cerrada
                    ESGR_AperturaCierreCaja.FechaApertura = value;
                else if (ESGR_AperturaCierreCaja.ESGR_Estado.CodEstado == "APTCJ")   //CodEstado CRDCJ = Cerrada
                    ESGR_AperturaCierreCaja.FechaCierre = value;
                OnPropertyChanged("SelectedFecha");
            }
        }

        private MDSGR_AperturaCierreCajaDetalle _SelectedMDSGR_AperturaCajaDetalle;
        public MDSGR_AperturaCierreCajaDetalle SelectedMDSGR_AperturaCajaDetalle
        {
            get
            {
                return _SelectedMDSGR_AperturaCajaDetalle;
            }
            set
            {
                _SelectedMDSGR_AperturaCajaDetalle = value;
                OnPropertyChanged("SelectedMDSGR_AperturaCajaDetalle");
            }
        }

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
                if (value != null)
                    ESGR_AperturaCierreCaja.ESGR_Caja = value;
                OnPropertyChanged("SelectedESGR_Caja");
            }
        }

        private ESGR_Estado _SelectedESGR_Estado;
        public ESGR_Estado SelectedESGR_Estado
        {
            get
            {
                return _SelectedESGR_Estado;
            }
            set
            {
                _SelectedESGR_Estado = value;
                if (value != null)
                    ESGR_AperturaCierreCaja.ESGR_Estado = value;
                OnPropertyChanged("SelectedESGR_Estado");
            }
        }

        private string _Glosa;
        public string Glosa
        {
            get
            {
                return _Glosa;
            }
            set
            {
                _Glosa = value;
                ESGR_AperturaCierreCaja.Glosa = value;
                OnPropertyChanged("Glosa");
            }
        }

        #endregion

        #region PROPERTYS

        private bool _PropertyIsEnabledMontoInicio;
        public bool PropertyIsEnabledMontoInicio
        {
            get
            {
                return _PropertyIsEnabledMontoInicio;
            }
            set
            {
                _PropertyIsEnabledMontoInicio = value;
                OnPropertyChanged("PropertyIsEnabledMontoInicio");
            }
        }

        private bool _PropertyIsEnabledMontoCierre;
        public bool PropertyIsEnabledMontoCierre
        {
            get
            {
                return _PropertyIsEnabledMontoCierre;
            }
            set
            {
                _PropertyIsEnabledMontoCierre = value;
                OnPropertyChanged("PropertyIsEnabledMontoCierre");
            }
        }

        private string _PropertyContetButtonAperCerrar;
        public string PropertyContetButtonAperCerrar
        {
            get
            {
                return _PropertyContetButtonAperCerrar;
            }
            set
            {
                _PropertyContetButtonAperCerrar = value;
                OnPropertyChanged("PropertyContetButtonAperCerrar");
            }
        }

        private bool _PropertyIsEnabledAgregar;
        public bool PropertyIsEnabledAgregar
        {
            get
            {
                return _PropertyIsEnabledAgregar;
            }
            set
            {
                _PropertyIsEnabledAgregar = value;
                OnPropertyChanged("PropertyIsEnabledAgregar");
            }
        }

        private bool _PropertyIsEnabledComboMoneda;
        public bool PropertyIsEnabledComboMoneda
        {
            get
            {
                return _PropertyIsEnabledComboMoneda;
            }
            set
            {
                _PropertyIsEnabledComboMoneda = value;
                OnPropertyChanged("PropertyIsEnabledComboMoneda");
            }
        }

        private bool _PropertyIsEnabledComboCajero;
        public bool PropertyIsEnabledComboCajero
        {
            get
            {
                return _PropertyIsEnabledComboCajero;
            }
            set
            {
                _PropertyIsEnabledComboCajero = value;
                OnPropertyChanged("PropertyIsEnabledComboCajero");
            }
        }

        private bool _PropertyIsEnabledComboCaja;
        public bool PropertyIsEnabledComboCaja
        {
            get
            {
                return _PropertyIsEnabledComboCaja;
            }
            set
            {
                _PropertyIsEnabledComboCaja = value;
                OnPropertyChanged("PropertyIsEnabledComboCaja");
            }
        }

        private bool _PropertyIsEnabledSelectedFecha;
        public bool PropertyIsEnabledSelectedFecha
        {
            get
            {
                return _PropertyIsEnabledSelectedFecha;
            }
            set
            {
                _PropertyIsEnabledSelectedFecha = value;
                OnPropertyChanged("PropertyIsEnabledSelectedFecha");
            }
        }

        private bool _PropertyIsEnabledGuardar;
        public bool PropertyIsEnabledGuardar
        {
            get
            {
                return _PropertyIsEnabledGuardar;
            }
            set
            {
                _PropertyIsEnabledGuardar = value;
                OnPropertyChanged("PropertyIsEnabledGuardar");
            }
        }

        private bool _PropertyIsReadOnlyMontoInicioCierre;
        public bool PropertyIsReadOnlyMontoInicioCierre
        {
            get
            {
                return _PropertyIsReadOnlyMontoInicioCierre;
            }
            set
            {
                _PropertyIsReadOnlyMontoInicioCierre = value;
                OnPropertyChanged("PropertyIsReadOnlyMontoInicio");
            }
        }

        private bool _PropertyIsReadOnlyGlosa;
        public bool PropertyIsReadOnlyGlosa
        {
            get
            {
                return _PropertyIsReadOnlyGlosa;
            }
            set
            {
                _PropertyIsReadOnlyGlosa = value;
                OnPropertyChanged("PropertyIsReadOnlyGlosa");
            }
        }

        private Visibility _PropertyVisibilityButtonAperCerrar;
        public Visibility PropertyVisibilityButtonAperCerrar
        {
            get
            {
                return _PropertyVisibilityButtonAperCerrar;
            }
            set
            {
                _PropertyVisibilityButtonAperCerrar = value;
                OnPropertyChanged("PropertyVisibilityButtonAperCerrar");
            }
        }

        private Visibility _PropertyVisibilityImprimir;
        public Visibility PropertyVisibilityImprimir
        {
            get
            {
                return _PropertyVisibilityImprimir;
            }
            set
            {
                _PropertyVisibilityImprimir = value;
                OnPropertyChanged("PropertyVisibilityImprimir");
            }
        }

        private int _PropertyWidthModalPage;
        public int PropertyWidthModalPage
        {
            get
            {
                return _PropertyWidthModalPage;
            }
            set
            {
                _PropertyWidthModalPage = value;
                OnPropertyChanged("PropertyWidthModalPage");
            }
        }

        private bool _PropertyVisibylityColumnMoneda;
        public bool PropertyVisibylityColumnMoneda
        {
            get
            {
                return _PropertyVisibylityColumnMoneda;
            }
            set
            {
                _PropertyVisibylityColumnMoneda = value;
                OnPropertyChanged("PropertyVisibylityColumnMoneda");
            }
        }

        private bool _PropertyVisibylityColumnMedioPago;
        public bool PropertyVisibylityColumnMedioPago
        {
            get
            {
                return _PropertyVisibylityColumnMedioPago;
            }
            set
            {
                _PropertyVisibylityColumnMedioPago = value;
                OnPropertyChanged("PropertyVisibylityColumnMedioPago");
            }
        }

        private bool _PropertyVisibylityColumnMontoInicio;
        public bool PropertyVisibylityColumnMontoInicio
        {
            get
            {
                return _PropertyVisibylityColumnMontoInicio;
            }
            set
            {
                _PropertyVisibylityColumnMontoInicio = value;
                OnPropertyChanged("PropertyVisibylityColumnMontoInicio");
            }
        }

        private bool _PropertyVisibylityColumnMontoSistema;
        public bool PropertyVisibylityColumnMontoSistema
        {
            get
            {
                return _PropertyVisibylityColumnMontoSistema;
            }
            set
            {
                _PropertyVisibylityColumnMontoSistema = value;
                OnPropertyChanged("PropertyVisibylityColumnMontoSistema");
            }
        }

        private bool _PropertyVisibylityColumnMontoCierre;
        public bool PropertyVisibylityColumnMontoCierre
        {
            get
            {
                return _PropertyVisibylityColumnMontoCierre;
            }
            set
            {
                _PropertyVisibylityColumnMontoCierre = value;
                OnPropertyChanged("PropertyVisibylityColumnMontoCierre");
            }
        }

        private bool _PropertyVisibylityColumnDiferExceso;
        public bool PropertyVisibylityColumnDiferExceso
        {
            get
            {
                return _PropertyVisibylityColumnDiferExceso;
            }
            set
            {
                _PropertyVisibylityColumnDiferExceso = value;
                OnPropertyChanged("PropertyVisibylityColumnDiferExceso");
            }
        }

        #endregion

        #region ICOMMAND

        public ICommand IUpdateMontoCierre
        {
            get
            {
                return CmpICommand.GetICommand((T) =>
                {
                    ColletionMDSGR_AperturaCajaDetalle.ToList().Where(x => x.Item == SelectedMDSGR_AperturaCajaDetalle.Item).ToList().ForEach(x => 
                    {
                        x.DiferExceso = x.Monto_Cierre - x.Monto_Sistema - x.Monto_Inicio;
                    });
                });
            }
        }

        public ICommand IAddRow
        {
            get
            {
                return CmpICommand.GetICommand((T) =>
                {
                    try
                    {
                        if (!ColletionMDSGR_AperturaCajaDetalle.ToList().Exists(x => x.Monto_Inicio == 0 || x.ESGR_Moneda.CodMoneda == null || x.ESGR_Moneda.CodMoneda.Trim().Length == 0))
                        {
                            var LastColletionMDSGR_AperturaCajaDetalle = ColletionMDSGR_AperturaCajaDetalle.LastOrDefault();
                            var CollectionESGR_Moneda = new CmpObservableCollection<ESGR_Moneda>(new BSGR_Moneda().GetCollectionMoneda());
                            var CollectionESGR_MedioPago = new CmpObservableCollection<ESGR_MedioPago>(new BSGR_MedioPago().GetCollectionMedioPago());
                            ColletionMDSGR_AperturaCajaDetalle.Add(new MDSGR_AperturaCierreCajaDetalle()
                            {
                                Item = ((LastColletionMDSGR_AperturaCajaDetalle == null) ? 1 : LastColletionMDSGR_AperturaCajaDetalle.Item + 1),
                                CollectionESGR_Moneda = CollectionESGR_Moneda,
                                CollectionESGR_MedioPago = CollectionESGR_MedioPago
                            });
                        }
                    }
                    catch (Exception ex)
                    {
                        CmpMessageBox.Show(SGRMessage.AdministracionAperturaCierreCaja, ex.Message, CmpButton.Aceptar);
                    }
                });
            }
        }

        public ICommand IDeleteRow
        {
            get
            {
                return CmpICommand.GetICommand((T) =>
                {
                    if (ESGR_AperturaCierreCaja.Opcion != "I") return;
                    ColletionMDSGR_AperturaCajaDetalle.Remove(SelectedMDSGR_AperturaCajaDetalle);
                    int vrItem = 1;
                    ColletionMDSGR_AperturaCajaDetalle.ToList().ForEach(x => x.Item = vrItem++);
                });
            }
        }

        public ICommand IGuardar
        {
            get
            {
                return CmpICommand.GetICommand(async(T) =>
                {
                    if (MethodValidaDatos()) return;
                    await Task.Factory.StartNew(() =>
                    {
                        try
                        {
                            ESGR_AperturaCierreCaja.DetalleXML = MethodPrepareDetalleXML();
                            if (ESGR_AperturaCierreCaja.ESGR_Estado.CodEstado == "APTCJ")
                                ESGR_AperturaCierreCaja.ESGR_Estado = CollectionESGR_Estado.FirstOrDefault(x => x.CodEstado == "CRDCJ");
                            else
                                ESGR_AperturaCierreCaja.ESGR_Estado = CollectionESGR_Estado.FirstOrDefault(x => x.CodEstado == "APTCJ");
                            ESGR_AperturaCierreCaja.ESGR_UsuarioApertura = SGRVariables.ESGR_Usuario;
                            new BSGR_AperturaCierreCaja().MethodTransaccionAperturaCaja(ESGR_AperturaCierreCaja);
                            if (ESGR_AperturaCierreCaja.Opcion == "I")
                                CmpMessageBox.Show(SGRMessage.AdministracionCaja, SGRMessageContent.ContentSaveOK, CmpButton.Aceptar,
                                () =>
                                {
                                    CmpModalDialog.GetContent().Close(null);
                                });
                            else
                                CmpMessageBox.Show(SGRMessage.AdministracionCaja, SGRMessageContent.ContentSaveOK + " ¿Desea imprimir el documento?", CmpButton.AceptarCancelar,
                                    /*ButtonAceptar */() => CmpMessageBox.Proccess(SGRMessage.AdministracionAperturaCierreCaja, "Cargando datos...", () => MethodImprimir("Caja Cerrada"), () => CmpModalDialog.GetContent().Close(null)),
                                    /*ButtonCancelar*/() => CmpModalDialog.GetContent().Close(null));
                        }
                        catch (Exception ex)
                        {
                            CmpMessageBox.Show(SGRMessage.AdministracionCaja, ex.Message, CmpButton.Aceptar);
                        }
                    });
                });
            }
        }

        public ICommand IImprimir
        {
            get
            {
                return CmpICommand.GetICommand((T) =>
                {
                    var vrTitulo = (ESGR_AperturaCierreCaja.ESGR_Estado.CodEstado == "CRDCJ") ? "Caja Cerrada" : "Caja Pre-Cerrada";
                    CmpMessageBox.Proccess(SGRMessage.AdministracionAperturaCierreCaja, "Cargando datos...", () => MethodImprimir(vrTitulo), () => CmpModalDialog.GetContent().Close(null));
                });
            }
        }

        public ICommand ISalir
        {
            get
            {
                return CmpICommand.GetICommand((T) =>
                {
                    CmpModalDialog.GetContent().Close(null);
                });
            }
        }

        #endregion

        #region METODOS

        private void MethodLoadHeader()
        {
            try
            {
                CollectionESGR_UsuarioCajero.Source = new CmpObservableCollection<ESGR_Usuario>(new BSGR_Usuario().GetCollectionUsuario().Where(x=>x.ESGR_Perfil.IdPerfil == 2));
                CollectionESGR_Caja.Source = new BSGR_Caja().CollectionESGR_Caja();
                CollectionESGR_Motivo.Source = new CmpObservableCollection<ESGR_Motivo>(new BSGR_Motivo().CollectionESGR_Motivo().Where(x => x.Modulo == "SGR_Caja"));
                CollectionESGR_Estado.Source = new BSGR_Estado().GetCollectionEstado("SGR_Caja");
                
                if (ESGR_AperturaCierreCaja.Opcion == "I")
                {
                    Application.Current.Dispatcher.Invoke(() => ColletionMDSGR_AperturaCajaDetalle.Clear());
                    SelectedESGR_Motivo = CollectionESGR_Motivo.FirstOrDefault(x => x.CodMotivo == "SLI");
                    CollectionESGR_Caja.Source = new CmpObservableCollection<ESGR_Caja>(CollectionESGR_Caja.Where(x => x.ESGR_Estado.CodEstado != "APTCJ" && x.FlgEliminado));
                    SelectedESGR_Caja = CollectionESGR_Caja.FirstOrDefault();
                    SelectedESGR_Estado = CollectionESGR_Estado.FirstOrDefault(x => x.CodEstado == "CRDCJ");
                    SelectedFecha = DateTime.Now;
                }
                else if (ESGR_AperturaCierreCaja.Opcion == "C" || ESGR_AperturaCierreCaja.Opcion == "V")
                {
                    SelectedESGR_Motivo = CollectionESGR_Motivo.FirstOrDefault(x => x.CodMotivo == ((ESGR_AperturaCierreCaja.Opcion == "C") ? "SLF" : ESGR_AperturaCierreCaja.ESGR_Motivo.CodMotivo));
                    SelectedESGR_Caja = CollectionESGR_Caja.FirstOrDefault(x => x.IdCaja == ESGR_AperturaCierreCaja.ESGR_Caja.IdCaja);
                    SelectedESGR_Estado = CollectionESGR_Estado.FirstOrDefault(x => x.CodEstado == ESGR_AperturaCierreCaja.ESGR_Estado.CodEstado);
                    SelectedESGR_UsuarioCajero = CollectionESGR_UsuarioCajero.FirstOrDefault(x => x.IdUsuario == ESGR_AperturaCierreCaja.ESGR_UsuarioCajero.IdUsuario);
                    SelectedFecha = (ESGR_AperturaCierreCaja.ESGR_Estado.CodEstado == "APTCJ") ? (ESGR_AperturaCierreCaja.FechaApertura.ToShortDateString() == DateTime.Now.ToShortDateString()) ? DateTime.Now : ESGR_AperturaCierreCaja.FechaApertura : ESGR_AperturaCierreCaja.FechaCierre;
                    Glosa = ESGR_AperturaCierreCaja.Glosa;
                }
                MethodValuesProperty();
            }
            catch (Exception ex)
            {
                CmpMessageBox.Show(SGRMessage.AdministracionAperturaCierreCaja, ex.Message, CmpButton.Aceptar);
            }
        }

        private void MethodLoadDetail()
        {
            try
            {
                if (ESGR_AperturaCierreCaja.Opcion != "I")
                {
                    var vrListDetalle = new BSGR_VentaDetalle().GetCollectionVentaDetalle(0, ESGR_AperturaCierreCaja.ESGR_Caja.IdCaja.ToString()).ToList();
                    var vrCollection_AperturaCierreCajaDetalle = new BSGR_AperturaCierreCaja().CollectionESGR_AperturaCierreCajaDetalle(ESGR_AperturaCierreCaja).ToList();
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        ColletionMDSGR_AperturaCajaDetalle.Clear();
                        var CollectionESGR_Moneda = new CmpObservableCollection<ESGR_Moneda>(new BSGR_Moneda().GetCollectionMoneda());
                        var CollectionESGR_MedioPago = new CmpObservableCollection<ESGR_MedioPago>(new BSGR_MedioPago().GetCollectionMedioPago());
                        vrCollection_AperturaCierreCajaDetalle.ForEach(x =>
                        {
                            var vrSelectMoneda = CollectionESGR_Moneda.FirstOrDefault(y => y.CodMoneda == x.ESGR_Moneda.CodMoneda);
                            var vrSelectMedioPago = CollectionESGR_MedioPago.FirstOrDefault(y => y.IdMedioPago == x.ESGR_MedioPago.IdMedioPago);
                            var vrDetalle = vrListDetalle.Where(z => z.ESGR_VentaCuenta.ESGR_MedioPago.IdMedioPago == x.ESGR_MedioPago.IdMedioPago && z.ESGR_VentaCuenta.ESGR_Moneda.CodMoneda == x.ESGR_Moneda.CodMoneda);
                            ColletionMDSGR_AperturaCajaDetalle.Add(new MDSGR_AperturaCierreCajaDetalle()
                            {
                                Item = x.Item,
                                CollectionESGR_Moneda = CollectionESGR_Moneda,
                                CollectionESGR_MedioPago = CollectionESGR_MedioPago,
                                SelectedESGR_Moneda = vrSelectMoneda,
                                SelectedESGR_MedioPago = vrSelectMedioPago,
                                ESGR_Moneda = x.ESGR_Moneda,
                                Monto_Inicio = x.Monto_Inicio,
                                Monto_Sistema = vrDetalle.Sum(y => y.Importe) - x.Monto_Inicio + vrDetalle.Sum(y => y.ESGR_VentaCuenta.Adicional) - vrDetalle.Sum(y => y.ESGR_VentaCuenta.Descuento),
                                Monto_Cierre = x.Monto_Cierre,
                                DiferExceso = x.DiferExceso
                            });
                        });
                    });
                }
            }
            catch (Exception ex)
            {
                CmpMessageBox.Show(SGRMessage.AdministracionAperturaCierreCaja, ex.Message, CmpButton.Aceptar);
            }
        }

        private void MethodValuesProperty()
        {
            //PropertyWidthModalPage = (ESGR_AperturaCierreCaja.ESGR_Estado.CodEstado == "APTCJ" || ESGR_AperturaCierreCaja.Opcion == "V") ? 850 : 550;
            PropertyIsEnabledComboMoneda = (ESGR_AperturaCierreCaja.Opcion == "I");
            PropertyIsEnabledComboCajero = (ESGR_AperturaCierreCaja.Opcion == "I");
            PropertyIsEnabledComboCaja = (ESGR_AperturaCierreCaja.Opcion == "I");
            PropertyIsEnabledAgregar = (ESGR_AperturaCierreCaja.Opcion == "I");
            PropertyVisibilityButtonAperCerrar = (ESGR_AperturaCierreCaja.Opcion == "V") ? Visibility.Collapsed : Visibility.Visible;
            PropertyIsReadOnlyMontoInicioCierre = (ESGR_AperturaCierreCaja.Opcion == "V");
            PropertyIsReadOnlyGlosa = (ESGR_AperturaCierreCaja.Opcion == "V");
            PropertyIsEnabledSelectedFecha = (ESGR_AperturaCierreCaja.Opcion != "V");
            PropertyIsEnabledMontoInicio = (ESGR_AperturaCierreCaja.ESGR_Estado.CodEstado == "CRDCJ" || ESGR_AperturaCierreCaja.Opcion == "V");
            PropertyIsEnabledMontoCierre = (ESGR_AperturaCierreCaja.ESGR_Estado.CodEstado == "APTCJ" || ESGR_AperturaCierreCaja.Opcion == "V");
            PropertyContetButtonAperCerrar = (ESGR_AperturaCierreCaja.ESGR_Estado.CodEstado == "CRDCJ") ? "Aperturar" : "Cerrar Caja";
            PropertyVisibilityImprimir = (ESGR_AperturaCierreCaja.ESGR_Estado.CodEstado == "APTCJ" || ESGR_AperturaCierreCaja.Opcion == "V") ? Visibility.Visible : Visibility.Collapsed;
            PropertyVisibylityColumnMoneda = true;
            PropertyVisibylityColumnMontoInicio = true;
            PropertyVisibylityColumnMedioPago = true;
            PropertyVisibylityColumnMontoSistema = (ESGR_AperturaCierreCaja.ESGR_Estado.CodEstado == "APTCJ" || ESGR_AperturaCierreCaja.Opcion == "V");
            PropertyVisibylityColumnMontoCierre = (ESGR_AperturaCierreCaja.ESGR_Estado.CodEstado == "APTCJ" || ESGR_AperturaCierreCaja.Opcion == "V");
            PropertyVisibylityColumnDiferExceso = (ESGR_AperturaCierreCaja.ESGR_Estado.CodEstado == "APTCJ" || ESGR_AperturaCierreCaja.Opcion == "V");
        }

        private void MethodImprimir(string Titulo)
        {
            try
            {
                var vrVentaMedioPago = new BSGR_VentaCuenta().GetReportVentaMedioPago(ESGR_AperturaCierreCaja.ESGR_Caja.IdCaja);
                var vrVentaCategoria = new BSGR_VentaDetalle().GetReportVentaCategoria(ESGR_AperturaCierreCaja.IdAperturaCierreCaja);
                var vrVentaPedidoTipo= new BSGR_VentaCuenta().GetReportVentaPedidoTipo(ESGR_AperturaCierreCaja.ESGR_Caja.IdCaja);
                var vrVentaFormaPago = new BSGR_VentaCuenta().GetReportVentaFormaPago(ESGR_AperturaCierreCaja.ESGR_Caja.IdCaja);
                var vrMovimientoCaja = new BSGR_MovimientoCaja().GetReportMovimientoCaja(ESGR_AperturaCierreCaja.ESGR_Caja.IdCaja);
                var vrTotalDescuentos= new BSGR_VentaCuenta().GetTotalAdicionalDescuentos(ESGR_AperturaCierreCaja.ESGR_Caja.IdCaja, "GetTotalDescuento");
                var vrTotalAdicional = new BSGR_VentaCuenta().GetTotalAdicionalDescuentos(ESGR_AperturaCierreCaja.ESGR_Caja.IdCaja, "GetTotalAdicional");
                var vrTotalPorEstado = new BSGR_Estado().GetReportCollectionEstado(ESGR_AperturaCierreCaja.ESGR_Caja.IdCaja);
                var vrTotalPorPedido = new BSGR_Venta().GetTotalPorPedido(ESGR_AperturaCierreCaja.ESGR_Caja.IdCaja);
                Thread.Sleep(2000);
                CrearTicket ticket = new CrearTicket();
                ticket.TextoCentro(SGRVariables.ESGR_Usuario.ESGR_Empresa.RazonSocial);
                ticket.TextoIzquierda("RUC: " + SGRVariables.ESGR_Usuario.ESGR_Empresa.Ruc);
                ticket.TextoIzquierda(SGRVariables.ESGR_Usuario.ESGR_Empresa.DireccionFiscal);
                ticket.TextoIzquierda(string.Empty);
                ticket.TextoCentro(Titulo.ToUpper());
                ticket.TextoIzquierda("Corte de " + ESGR_AperturaCierreCaja.ESGR_Caja.Descripcion);
                ticket.TextoIzquierda("DEL " + ESGR_AperturaCierreCaja.FechaApertura.ToString().ToUpper());
                ticket.TextoIzquierda(" AL " + ESGR_AperturaCierreCaja.FechaCierre.ToString().ToUpper());
                ticket.lineasIgual();
                ticket.TextoIzquierda(string.Empty);
                ticket.TextoCentro(ESGR_AperturaCierreCaja.ESGR_Caja.Descripcion);
                decimal dmlSaldoFinal = 0;
                vrVentaMedioPago.ToList().ForEach(x =>
                {
                    ticket.TextoTriple("+ " + x.ESGR_MedioPago.MedioPago, ": " + x.ESGR_Moneda.Simbolo, x.Total.ToString("N3"));
                    dmlSaldoFinal += x.Total;
                });
                vrMovimientoCaja.ToList().ForEach(x =>
                {
                    ticket.TextoTriple(((x.ESGR_Motivo.Campo.Trim() == "ING") ? "+ " : "- ") + x.ESGR_Motivo.Motivo, ": " + x.ESGR_Moneda.Simbolo, x.Total.ToString("N3"));
                    dmlSaldoFinal += ((x.ESGR_Motivo.Campo.Trim() == "ING") ? x.Total : (-1) * x.Total);
                });
                ticket.TextoExtremosAlineados(string.Empty, "________________");
                ticket.TextoTriple("=  SALDO FINAL", ": " + vrMovimientoCaja.FirstOrDefault().ESGR_Moneda.Simbolo, dmlSaldoFinal.ToString("N3"));
                ticket.TextoTriple("EFECTIVO FINAL", ": " + ColletionMDSGR_AperturaCajaDetalle.FirstOrDefault().SelectedESGR_Moneda.Simbolo, ColletionMDSGR_AperturaCajaDetalle.Sum(x => x.Monto_Cierre).ToString("N3"));
                ticket.TextoIzquierda(string.Empty);
                ticket.TextoCentro("FORMA DE PAGO VENTA");
                vrVentaFormaPago.ToList().ForEach(x => ticket.TextoTriple(x.ESGR_MedioPago.ESGR_FormaPago.FormaPago, ": " + x.ESGR_Moneda.Simbolo, x.Total.ToString("N3")));
                ticket.TextoIzquierda(string.Empty);
                ticket.lineasIgual();
                ticket.TextoCentro("VENTAS (INCLUYE IMPUESTO)");
                ticket.TextoCentro("POR CATEGORIA");
                vrVentaCategoria.ToList().ForEach(x =>
                {
                    ticket.TextoTriple(x.ESGR_Producto.ESGR_ProductoSubCategoria.ESGR_ProductoCategoria.Categoria, ": " + x.ESGR_VentaCuenta.ESGR_Moneda.Simbolo, x.Importe.ToString("N3"));
                });
                ticket.TextoIzquierda(string.Empty);
                ticket.TextoCentro("POR TIPO DE SERVICIO");
                decimal dmlSubTotal = 0;
                vrVentaPedidoTipo.ToList().ForEach(x =>
                {
                    ticket.TextoTriple(x.ESGR_Venta.ESGR_Pedido.ESGR_PedidoTipo.PedidoTipo, ": " + x.ESGR_Moneda.Simbolo, x.Total.ToString("N3"));
                    dmlSubTotal += x.Total;
                });
                ticket.TextoExtremosAlineados(string.Empty, "________________");
                string strSimbolo = vrVentaPedidoTipo.FirstOrDefault().ESGR_Moneda.Simbolo;
                ticket.TextoTriple(" SUBTOTAL", ": " + strSimbolo, dmlSubTotal.ToString("N3"));
                ticket.TextoTriple("+ADICIONAL", ": " + strSimbolo, vrTotalAdicional.ToString("N3"));
                ticket.TextoTriple("-DESCUENTOS", ": " + strSimbolo, vrTotalDescuentos.ToString("N3"));
                decimal dmlVentaNeta = (dmlSubTotal - vrTotalDescuentos + vrTotalAdicional);
                ticket.TextoTriple(" VENTA NETA", ": " + strSimbolo, dmlVentaNeta.ToString("N3"));
                ticket.TextoExtremosAlineados(string.Empty, "________________");
                dmlVentaNeta = dmlVentaNeta / ((SGRVariables.ESGR_Retencion.IGV / 100) + 1);
                ticket.TextoTriple("VENTA     (" + SGRVariables.ESGR_Retencion.IGV.ToString("N2") + "%)", ": " + strSimbolo, dmlVentaNeta.ToString("N3"));
                decimal dmlImpuesto = dmlVentaNeta * (SGRVariables.ESGR_Retencion.IGV / 100);
                ticket.TextoTriple("IMPUESTO  (" + SGRVariables.ESGR_Retencion.IGV.ToString("N2") + "%)", ": " + strSimbolo, dmlImpuesto.ToString("N3"));
                ticket.TextoIzquierda(string.Empty);
                ticket.TextoTriple("IMPUESTO TOTAL", ": " + strSimbolo, dmlImpuesto.ToString("N3"));
                ticket.TextoExtremosAlineados(string.Empty, "________________");
                ticket.TextoTriple("VENTA CON IMP.", ": " + strSimbolo, (dmlVentaNeta + dmlImpuesto).ToString("N3"));
                ticket.TextoExtremosAlineados(string.Empty, "================");
                ticket.TextoIzquierda(string.Empty);
                ticket.lineasIgual();
                #region OPCIONAL
                //ticket.TextoExtremosAliniados("CUENTAS NORMALES", ": 0");
                //ticket.TextoExtremosAliniados("CUENTAS CANCELADOS", ": 0");
                //ticket.TextoExtremosAliniados("CUENTAS CON DESCUENTO", ": 0");
                //ticket.TextoExtremosAliniados("CUENTAS CON CORTESIA", ": 0");
                //ticket.TextoExtremosAliniados("CUENTA PROMEDIO", ": S/. 0.000");
                //ticket.TextoExtremosAliniados("CONSUMO PROMEDIO", ": S/. 0.000");
                //ticket.TextoExtremosAliniados("COMENSALES", ": 0");
                //ticket.TextoExtremosAliniados("PROPINAS", ": S/. 0.000");
                //ticket.TextoExtremosAliniados("CARGOS", ": S/. 0.000");
                //ticket.TextoExtremosAliniados("DESCUENTO MONEDERO", ": S/. 0.000");
                //ticket.TextoExtremos("FOLIO INICIAL", "828");
                //ticket.TextoExtremos("FOLIO FINAL", "890");
                //ticket.TextoExtremosAliniados("CORTESIA ALIMENTOS", ": S/. 0.000");
                //ticket.TextoExtremosAliniados("CORTESIA BEBIDAS", ": S/. 0.000");
                //ticket.TextoExtremosAliniados("CORTESIA OTROS", ": S/. 0.000");
                //ticket.TextoExtremosAliniados(string.Empty, "________________");
                //ticket.TextoExtremosAliniados("TOTAL CORTESIAS", ": S/. 0.000");
                //ticket.TextoExtremosAliniados("DESCUENTO ALIMENTOS", ": S/. 0.000");
                //ticket.TextoExtremosAliniados("DESCUENTO BEBIDAS", ": S/. 0.000");
                //ticket.TextoExtremosAliniados("DESCUENTO OTROS", ": S/. 0.000");
                //ticket.TextoExtremosAliniados(string.Empty, "________________");
                //ticket.TextoExtremosAliniados("TOTAL DESCUENTOS", ": S/. 0.000");
                #endregion
                ticket.TextoCentro("REPORTE DE PEDIDOS");
                ticket.lineasAsteriscos();
                ticket.TextoTriple("FOLIO INICIAL", ": ", vrTotalPorPedido.FirstOrDefault().ESGR_Pedido.IdPedido.ToString("00000#"));
                ticket.TextoTriple("FOLIO FINAL", ": ", vrTotalPorPedido.LastOrDefault().ESGR_Pedido.IdPedido.ToString("00000#"));
                ticket.TextoTriple("COMENSALES APROX.", ": ", vrTotalPorPedido.Sum(x => x.ESGR_Pedido.Cubierto).ToString("00#"));
                vrTotalPorEstado.ToList().ForEach(x => ticket.TextoTriple(x.Estado, ": ", x.Total.ToString("00#")));
                ticket.TextoIzquierda(string.Empty);
                ticket.TextoCentro("DECLARACION DE CAJERO");
                decimal dmlTotalCajero = 0;
                ColletionMDSGR_AperturaCajaDetalle.FirstOrDefault().CollectionESGR_MedioPago.ToList().ForEach(x =>
                {
                    var dml = ColletionMDSGR_AperturaCajaDetalle.FirstOrDefault(y => y.SelectedESGR_MedioPago.IdMedioPago == x.IdMedioPago);
                    ticket.TextoTriple(x.MedioPago, ": " + strSimbolo, ((dml == null) ? "0.000" : dml.Monto_Cierre.ToString("N3")));
                    dmlTotalCajero += (dml == null) ? 0 : dml.Monto_Cierre;
                });
                ticket.TextoExtremosAlineados(string.Empty, "________________");
                ticket.TextoTriple("TOTAL", ": " + strSimbolo, dmlTotalCajero.ToString("N3"));
                ticket.TextoIzquierda(string.Empty);
                ticket.TextoIzquierda("SOBRANTE(+) O FALTANTE(-): ");
                decimal dmlCierre = dmlTotalCajero - (dmlVentaNeta + dmlImpuesto);
                ticket.TextoDerecha(strSimbolo + " " + ((dmlCierre < 0) ? string.Empty : "+") + dmlCierre.ToString("N3"));
                ticket.TextoIzquierda(string.Empty);
                ticket.TextoIzquierda(string.Empty);
                ticket.TextoExtremosAlineados("_________________", "_________________");
                ticket.TextoExtremosAlineados("     GERENTE",      "     CAJERO");
                ticket.TextoIzquierda(string.Empty);
                ticket.TextoIzquierda(string.Empty);
                ticket.TextoCentro(SGRMessage.TitleMessage.ToUpper());
                ticket.TextoIzquierda(string.Empty);
                //Application.Current.Dispatcher.Invoke(() =>
                //{
                    ticket.ImprimirTicket(SGRVariables.ImpresoraCaja);
                //});
            }
            catch (Exception)
            {
                throw;
            }
        }

        private bool MethodValidaDatos()
        {
            if (ESGR_AperturaCierreCaja.ESGR_UsuarioCajero == null)
            {
                CmpMessageBox.Show(SGRMessage.AdministracionAperturaCierreCaja, "Debe seleccionar Cajero para continuar.", CmpButton.Aceptar);
                return true;
            }
            else if (ESGR_AperturaCierreCaja.ESGR_Motivo == null || ESGR_AperturaCierreCaja.ESGR_Motivo.CodMotivo.Trim().Length == 0)
            {
                CmpMessageBox.Show(SGRMessage.AdministracionAperturaCierreCaja, "Debe seleccionar Motivo para continuar.", CmpButton.Aceptar);
                return true;
            }
            else if (ColletionMDSGR_AperturaCajaDetalle.Count == 0)
            {
                CmpMessageBox.Show(SGRMessage.AdministracionAperturaCierreCaja, "Ingrese un detalle como mínimo.", CmpButton.Aceptar);
                return true;
            }
            else if (ColletionMDSGR_AperturaCajaDetalle.Count > 0)
            {
                string Message = string.Empty;
                foreach (var item in ColletionMDSGR_AperturaCajaDetalle)
                {
                    if (item.Monto_Inicio < 0 || item.Monto_Inicio < 0)
                        Message = "Los montos no deben ser menor a 0";
                }

                if (Message.Trim().Length > 0)
                {
                    CmpMessageBox.Show(SGRMessage.AdministracionAperturaCierreCaja, Message, CmpButton.Aceptar);
                    return true;
                }
            }
            return false;
        }

        private string MethodPrepareDetalleXML()
        {
            string vrCadena = "<ROOT>";
            ColletionMDSGR_AperturaCajaDetalle.ToList().ForEach(x =>
            {
                vrCadena += "<Listar ";
                vrCadena += "xItem = \'" + x.Item;
                vrCadena += "\' xCodMoneda = \'" + x.ESGR_Moneda.CodMoneda;
                vrCadena += "\' xIdMedioPago = \'" + x.ESGR_MedioPago.IdMedioPago;
                vrCadena += "\' xMonto_Inicio = \'" + x.Monto_Inicio;
                vrCadena += "\' xMonto_Sistema = \'" + x.Monto_Sistema;
                vrCadena += "\' xMonto_Cierre = \'" + x.Monto_Cierre;
                vrCadena += "\' xDiferExceso = \'" + x.DiferExceso;
                vrCadena += "\'></Listar>";
            });
            vrCadena += "</ROOT>";
            return vrCadena;
        }

        #endregion
    }
}