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
using SGR.ViewModels.Presentation.MovimientoCaja.Assistant;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SGR.ViewModels.Presentation.MovimientoCaja.ModalPage
{
    public class VMSGR_MovimientoCaja : CmpNavigationService, CmpIModalDialog
    {
        private ESGR_MovimientoCaja _ESGR_MovimientoCaja;
        public ESGR_MovimientoCaja ESGR_MovimientoCaja
        {
            get
            {
                if (_ESGR_MovimientoCaja == null)
                    _ESGR_MovimientoCaja = new ESGR_MovimientoCaja();
                return _ESGR_MovimientoCaja;
            }
            set
            {
                _ESGR_MovimientoCaja = value;
                OnPropertyChanged("ESGR_MovimientoCaja");
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
                        CmpModalDialog.GetContent().Close(null);
                    else if (value is ESGR_MovimientoCaja)
                    {
                        CmpLoading = new CmpLoading(MethodLoadHeader, MethodLoadDetails);
                        CmpLoading.Exceptions = ((e) => CmpMessageBox.Show(SGRMessage.AdministracionMovimientoCaja, e.Message, CmpButton.Aceptar));
                        ESGR_MovimientoCaja = (ESGR_MovimientoCaja)value;
                        PropertyIsEnabledHeader = (ESGR_MovimientoCaja.Opcion == "I");
                        PropertyIsEnabledColumnMonto = (ESGR_MovimientoCaja.Opcion == "I");
                        PropertyIsEnabledButtonAgregarQuitar = (ESGR_MovimientoCaja.Opcion == "I");
                        CmpLoading.LoadHeader();
                    }
                });
            }
        }

        private CmpLoading CmpLoading { get; set; }

        #region COLECCIONES

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

        private CmpObservableCollection<ESGR_Documento> _CollectionESGR_Documento;
        public CmpObservableCollection<ESGR_Documento> CollectionESGR_Documento
        {
            get
            {
                if (_CollectionESGR_Documento == null)
                    _CollectionESGR_Documento = new CmpObservableCollection<ESGR_Documento>();
                return _CollectionESGR_Documento;
            }
        }

        private CmpObservableCollection<ESGR_EmpresaSucursal> _CollectionESGR_EmpresaSucursal;
        public CmpObservableCollection<ESGR_EmpresaSucursal> CollectionESGR_EmpresaSucursal
        {
            get
            {
                if (_CollectionESGR_EmpresaSucursal == null)
                    _CollectionESGR_EmpresaSucursal = new CmpObservableCollection<ESGR_EmpresaSucursal>();
                return _CollectionESGR_EmpresaSucursal;
            }
        }

        private CmpObservableCollection<ESGR_MovimientoCajaDetalle> _CollectionESGR_MovimientoCajaDetalle;
        public CmpObservableCollection<ESGR_MovimientoCajaDetalle> CollectionESGR_MovimientoCajaDetalle
        {
            get
            {
                if (_CollectionESGR_MovimientoCajaDetalle == null)
                    _CollectionESGR_MovimientoCajaDetalle = new CmpObservableCollection<ESGR_MovimientoCajaDetalle>();
                return _CollectionESGR_MovimientoCajaDetalle;
            }
        }

        private CmpObservableCollection<ASSGR_ValueComboBox> _CollectionASSGR_ValueComboBox;
        public CmpObservableCollection<ASSGR_ValueComboBox> CollectionASSGR_ValueComboBox
        {
            get
            {
                if (_CollectionASSGR_ValueComboBox == null)
                    _CollectionASSGR_ValueComboBox = new CmpObservableCollection<ASSGR_ValueComboBox>();
                return _CollectionASSGR_ValueComboBox;
            }
        }

        #endregion

        #region OBJ SECUNDARIOS

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
                {
                    ESGR_MovimientoCaja.ESGR_Motivo = value;
                    PropertyEnableCaja = ((value.CodMotivo == "CMP") ? false : true);
                }
                OnPropertyChanged("SelectedESGR_Motivo");
            }
        }

        private ESGR_Moneda _SelectedESGR_Moneda;
        public ESGR_Moneda SelectedESGR_Moneda
        {
            get
            {
                return _SelectedESGR_Moneda;
            }
            set
            {
                _SelectedESGR_Moneda = value;
                if (value != null)
                {
                    ESGR_MovimientoCaja.ESGR_Moneda = value;
                    SimboloMoneda = value.Simbolo;
                }
                OnPropertyChanged("SelectedESGR_Moneda");
            }
        }

        private ESGR_Documento _SelectedESGR_Documento;
        public ESGR_Documento SelectedESGR_Documento
        {
            get
            {
                return _SelectedESGR_Documento;
            }
            set
            {
                _SelectedESGR_Documento = value;
                if (value != null)
                    ESGR_MovimientoCaja.ESGR_Documento = value;
                OnPropertyChanged("SelectedESGR_Documento");
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
                if (value != null)
                    ESGR_MovimientoCaja.Fecha = value;
                OnPropertyChanged("SelectedFecha");
            }
        }

        private ESGR_MovimientoCajaDetalle _SelectedESGR_MovimientoCajaDetalle;
        public ESGR_MovimientoCajaDetalle SelectedESGR_MovimientoCajaDetalle
        {
            get
            {
                return _SelectedESGR_MovimientoCajaDetalle;
            }
            set
            {
                _SelectedESGR_MovimientoCajaDetalle = value;
                OnPropertyChanged("SelectedESGR_MovimientoCajaDetalle");
            }
        }

        private ESGR_EmpresaSucursal _SelectedESGR_EmpresaSucursal;
        public ESGR_EmpresaSucursal SelectedESGR_EmpresaSucursal
        {
            get
            {
                return _SelectedESGR_EmpresaSucursal;
            }
            set
            {
                _SelectedESGR_EmpresaSucursal = value;
                if (value != null)
                    ESGR_MovimientoCaja.ESGR_EmpresaSucursal = value;
                OnPropertyChanged("SelectedESGR_EmpresaSucursal");
            }
        }

        private ASSGR_ValueComboBox _SelectedASSGR_ValueComboBox;
        public ASSGR_ValueComboBox SelectedASSGR_ValueComboBox
        {
            get
            {
                return _SelectedASSGR_ValueComboBox;
            }
            set
            {
                _SelectedASSGR_ValueComboBox = value;
                if (value != null)
                {
                    ESGR_MovimientoCaja.CodOperacion = value.Codigo;
                    MethodLoadMotivo(value);
                }
                OnPropertyChanged("SelectedASSGR_ValueComboBox");
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
                if (value != null)
                    ESGR_MovimientoCaja.Glosa = value;
                OnPropertyChanged("Glosa");
            }
        }

        private string _SimboloMoneda;
        public string SimboloMoneda
        {
            get
            {
                return _SimboloMoneda;
            }
            set
            {
                _SimboloMoneda = value;
                OnPropertyChanged("SimboloMoneda");
            }
        }

        private decimal _Total;
        public decimal Total
        {
            get
            {
                return _Total;
            }
            set
            {
                _Total = value;
                OnPropertyChanged("Total");
            }
        }

        #endregion

        #region PROPERTYS

        private bool _PropertyEnabledCaja;
        public bool PropertyEnableCaja
        {
            get
            {
                return _PropertyEnabledCaja;
            }
            set
            {
                _PropertyEnabledCaja = value;
                OnPropertyChanged("PropertyEnableCaja");
            }
        }

        private bool _PropertyIsEnabledHeader;
        public bool PropertyIsEnabledHeader
        {
            get
            {
                return _PropertyIsEnabledHeader;
            }
            set
            {
                _PropertyIsEnabledHeader = value;
                OnPropertyChanged("PropertyIsEnabledHeader");
            }
        }

        private bool _PropertyIsEnabledColumnMonto;
        public bool PropertyIsEnabledColumnMonto
        {
            get
            {
                return _PropertyIsEnabledColumnMonto;
            }
            set
            {
                _PropertyIsEnabledColumnMonto = value;
                OnPropertyChanged("PropertyIsEnabledColumnMonto");
            }
        }

        private bool _PropertyIsEnabledButtonAgregarQuitar;
        public bool PropertyIsEnabledButtonAgregarQuitar
        {
            get
            {
                return _PropertyIsEnabledButtonAgregarQuitar;
            }
            set
            {
                _PropertyIsEnabledButtonAgregarQuitar = value;
                OnPropertyChanged("PropertyIsEnabledButtonAgregarQuitar");
            }
        }

        private decimal _PropertyTipoCambio;
        public decimal PropertyTipoCambio
        {
            get
            {
                return _PropertyTipoCambio;
            }
            set
            {
                _PropertyTipoCambio = value;
                OnPropertyChanged("PropertyTipoCambio");
            }
        }

        #endregion

        #region ICOMMAND

        public ICommand IAddItem
        {
            get
            {
                return CmpICommand.GetICommand((T) =>
                {
                    var LastDetails = CollectionESGR_MovimientoCajaDetalle.LastOrDefault();
                    if (LastDetails !=  null && (LastDetails.Monto == 0 || LastDetails.ConceptoDescripcion.Trim().Length == 0))
                        return;
                    CollectionESGR_MovimientoCajaDetalle.Add(new ESGR_MovimientoCajaDetalle());
                });
            }
        }

        public ICommand IRemove
        {
            get
            {
                return CmpICommand.GetICommand((T) =>
                {
                    if (CollectionESGR_MovimientoCajaDetalle.Count == 0) return;
                    if (SelectedESGR_MovimientoCajaDetalle == null)
                    {
                        CmpMessageBox.Show(SGRMessage.AdministracionMovimientoCaja, "Seleccione un registro para continuar con el proceso", CmpButton.Aceptar);
                        return;
                    }
                    CmpMessageBox.Show(SGRMessage.AdministracionMovimientoCaja, "Seguro que desea eliminar el registro", CmpButton.AceptarCancelar, () =>
                    {
                        CollectionESGR_MovimientoCajaDetalle.Remove(SelectedESGR_MovimientoCajaDetalle);
                    });
                });
            }
        }

        public ICommand IMontoUpdate
        {
            get
            {
                return CmpICommand.GetICommand((T) =>
                {
                    Total = CollectionESGR_MovimientoCajaDetalle.Sum(x => x.Monto);
                });
            }
        }

        public ICommand IGuardar
        {
            get
            {
                return CmpICommand.GetICommand((T) =>
                {
                    try
                    {
                        if (MethodValidaDatos()) return;
                        ESGR_MovimientoCaja.DetalleXML = MethodPrepareDocumentXML();
                        ESGR_MovimientoCaja.Total = CollectionESGR_MovimientoCajaDetalle.Sum(x => x.Monto);
                        new BSGR_MovimientoCaja().MethodTransaction(ESGR_MovimientoCaja);
                        CmpMessageBox.Show(SGRMessage.AdministracionMovimientoCaja, SGRMessageContent.ContentSaveOK, CmpButton.Aceptar, () =>
                        {
                            CmpModalDialog.GetContent().Close(null);
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
                    CmpModalDialog.GetContent().Close(null);
                });
            }
        }

        #endregion

        #region METODOS

        private void MethodLoadHeader()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                CollectionASSGR_ValueComboBox.Clear();
                CollectionASSGR_ValueComboBox.Add(new ASSGR_ValueComboBox() { Codigo = "ING", Value = "INGRESO" });
                CollectionASSGR_ValueComboBox.Add(new ASSGR_ValueComboBox() { Codigo = "SAL", Value = "SALIDA" });
            });
            CollectionESGR_Caja.Source = new CmpObservableCollection<ESGR_Caja>(new BSGR_Caja().CollectionESGR_Caja().Where(x => x.ESGR_Estado.CodEstado == "APTCJ"));
            CollectionESGR_Moneda.Source = new BSGR_Moneda().GetCollectionMoneda();
            CollectionESGR_Documento.Source = new BSGR_Documento().GetCollectionDocumento();
            CollectionESGR_EmpresaSucursal.Source = new BSGR_EmpresaSucursal().GetCollectionEmpresaSucursal(SGRVariables.ESGR_Usuario.ESGR_Empresa);
            PropertyTipoCambio = 1;
            if (ESGR_MovimientoCaja.Opcion == "I")
            {
                Glosa = string.Empty;
                SelectedESGR_Moneda = CollectionESGR_Moneda.FirstOrDefault(x => x.Defecto);
                ESGR_MovimientoCaja.ESGR_Documento = (SelectedESGR_Documento = CollectionESGR_Documento.FirstOrDefault(x => x.CodDocumento == "CAJ"));
                SelectedFecha = DateTime.Now;
                SelectedESGR_EmpresaSucursal = CollectionESGR_EmpresaSucursal.FirstOrDefault();
                SelectedASSGR_ValueComboBox = CollectionASSGR_ValueComboBox.FirstOrDefault();
                Application.Current.Dispatcher.Invoke(() =>
                {
                    if (CollectionESGR_MovimientoCajaDetalle.Count > 0)
                        CollectionESGR_MovimientoCajaDetalle.Clear();
                });
            }
            else
            {
                Glosa = ESGR_MovimientoCaja.Glosa;
                SelectedESGR_Documento = CollectionESGR_Documento.FirstOrDefault(x => x.CodDocumento == ESGR_MovimientoCaja.ESGR_Documento.CodDocumento);
                SelectedESGR_Moneda = CollectionESGR_Moneda.FirstOrDefault(x => x.CodMoneda == ESGR_MovimientoCaja.ESGR_Moneda.CodMoneda);
                SelectedESGR_EmpresaSucursal = CollectionESGR_EmpresaSucursal.FirstOrDefault(x => x.IdEmpSucursal == ESGR_MovimientoCaja.ESGR_EmpresaSucursal.IdEmpSucursal);
                SelectedASSGR_ValueComboBox = CollectionASSGR_ValueComboBox.FirstOrDefault(x => x.Codigo == ESGR_MovimientoCaja.CodOperacion);
                SelectedFecha = ESGR_MovimientoCaja.Fecha;
            }
        }

        private void MethodLoadDetails()
        {
            CollectionESGR_MovimientoCajaDetalle.Source = new BSGR_MovimientoCaja().CollectionESGR_MovimientoCajaDetalle(ESGR_MovimientoCaja);
        }

        private async void MethodLoadMotivo(ASSGR_ValueComboBox ASSGR_ValueComboBox)
        {
            await Task.Factory.StartNew(() =>
            {
                try
                {
                    CollectionESGR_Motivo.Source = new CmpObservableCollection<ESGR_Motivo>(new BSGR_Motivo().CollectionESGR_Motivo().Where(x => x.Modulo == "SGR_MovimientoCaja" && x.Campo == ASSGR_ValueComboBox.Codigo));
                    if(ESGR_MovimientoCaja.Opcion == "I")
                        SelectedESGR_Motivo = CollectionESGR_Motivo.FirstOrDefault();
                    else
                        SelectedESGR_Motivo = CollectionESGR_Motivo.FirstOrDefault(x => x.CodMotivo == ESGR_MovimientoCaja.ESGR_Motivo.CodMotivo);
                }
                catch (Exception ex)
                {
                    CmpMessageBox.Show(SGRMessage.AdministracionMovimientoCaja, ex.Message, CmpButton.Aceptar);
                }
            });
        }

        private string MethodPrepareDocumentXML()
        {
            var strCadena = "<ROOT>";
            CollectionESGR_MovimientoCajaDetalle.ToList().ForEach(x =>
            {
                strCadena += "<Listar ";
                strCadena += "xConceptoDescripcion = \'" + x.ConceptoDescripcion;
                strCadena += "\' xMonto = \'" + x.Monto;
                strCadena += "\'></Listar>";
            });
            strCadena += "</ROOT>";
            return strCadena;
        }

        private bool MethodValidaDatos()
        {
            if (ESGR_MovimientoCaja.ESGR_Estado != null && ESGR_MovimientoCaja.ESGR_Estado.CodEstado == "ANMOV")
            {
                CmpMessageBox.Show(SGRMessage.AdministracionMovimientoCaja, "No puede editar el Movimiento porque está anulado.", CmpButton.Aceptar);
                return true;
            }
            else if (ESGR_MovimientoCaja.ESGR_Moneda == null)
            {
                CmpMessageBox.Show(SGRMessage.AdministracionMovimientoCaja, "Datos Obligatorios: Moneda", CmpButton.Aceptar);
                return true;
            }
            else if (ESGR_MovimientoCaja.ESGR_Motivo == null)
            {
                CmpMessageBox.Show(SGRMessage.AdministracionMovimientoCaja, "Datos Obligatorios: Motivo", CmpButton.Aceptar);
                return true;
            }
            else if (ESGR_MovimientoCaja.Fecha == null)
            {
                CmpMessageBox.Show(SGRMessage.AdministracionMovimientoCaja, "Datos Obligatorios: Fecha", CmpButton.Aceptar);
                return true;
            }
            else if (ESGR_MovimientoCaja.ESGR_Documento.Correlativo == null)
            {
                CmpMessageBox.Show(SGRMessage.AdministracionMovimientoCaja, "Datos Obligatorios: Numero", CmpButton.Aceptar);
                return true;
            }
            else if (ESGR_MovimientoCaja.ESGR_Documento.Serie == null)
            {
                CmpMessageBox.Show(SGRMessage.AdministracionMovimientoCaja, "Datos Obligatorios: Serie", CmpButton.Aceptar);
                return true;
            }
            else if (CollectionESGR_MovimientoCajaDetalle.Count == 0)
            {
                CmpMessageBox.Show(SGRMessage.AdministracionMovimientoCaja, "Ingrese al menos un detalle.", CmpButton.Aceptar);
                return true;
            }
            else if (Glosa == null || Glosa.Trim().Length == 0)
            {
                CmpMessageBox.Show(SGRMessage.AdministracionMovimientoCaja, "Ingrese una glosa valida.", CmpButton.Aceptar);
                return true;
            }
            else if (CollectionESGR_MovimientoCajaDetalle.Count > 0)
            {
                string Message = string.Empty;
                CollectionESGR_MovimientoCajaDetalle.ToList().ForEach(x =>
                {
                    if (x.Monto == 0)
                        Message = "Datos Obligatorios: Monto";
                });
                if (Message.Trim().Length != 0)
                {
                    CmpMessageBox.Show(SGRMessage.AdministracionMovimientoCaja, Message, CmpButton.Aceptar);
                    return true;
                }
            }
            return false;
        }

        #endregion
    }
}
