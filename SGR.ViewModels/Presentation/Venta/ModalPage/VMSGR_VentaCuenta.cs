/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   ABEL QUISPE ORELLANA
'* FCH. CREACIÓN : 21/02/2017
**********************************************************/

namespace SGR.ViewModels.Presentation.Venta.ModalPage
{
    using ComputerSystems;
    using ComputerSystems.WPF;
    using ComputerSystems.WPF.Acciones.Controles.ModalDialog;
    using ComputerSystems.WPF.Interfaces;
    using Newtonsoft.Json;
    using SGR.Models;
    using SGR.Models.Business;
    using SGR.Models.Entity;
    using SGR.ViewModels.Method;
    using SGR.ViewModels.Presentation.Venta.Assistant;
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Input;

    public class VMSGR_VentaCuenta : CmpNotifyPropertyChanged, CmpIModalDialog
    {
        private ESGR_VentaCuenta _ESGR_VentaCuenta;
        public ESGR_VentaCuenta ESGR_VentaCuenta
        {
            get
            {
                if (_ESGR_VentaCuenta == null)
                    _ESGR_VentaCuenta = new ESGR_VentaCuenta();
                return _ESGR_VentaCuenta;
            }
            set
            {
                _ESGR_VentaCuenta = value;
                OnPropertyChanged("ESGR_VentaCuenta");
            }
        }

        public object Parameter
        {
            set
            {
                Application.Current.Dispatcher.Invoke(async() =>
                {
                    var result = await MTSGR_GetPrivilege.Process(this.GetType(), SGRMessage.TitleMessage, SGRMessageContent.GetAccesoRestringido);
                    if (result == null)
                        CmpModalDialog.GetContent().Close();
                    else
                        MethodSetParameter(value);
                });
            }
        }

        #region OBJ SECUNDARIO

        private ESGR_Documento _SelectedESGR_Documento;
        public ESGR_Documento SelectedESGR_Documento
        {
            get
            {
                return _SelectedESGR_Documento;
            }
            set
            {
                if (value != null)
                {
                    _SelectedESGR_Documento = value;
                    ESGR_VentaCuenta.ESGR_Documento = value;
                    if (value != null)
                        MethodLoadSerieNumero(value.CodDocumento);
                }
                OnPropertyChanged("SelectedESGR_Documento");
            }
        }

        private ESGR_MedioPago _SelectedESGR_MedioPago;
        public ESGR_MedioPago SelectedESGR_MedioPago
        {
            get
            {
                return _SelectedESGR_MedioPago;
            }
            set
            {
                if (value != null)
                {
                    _SelectedESGR_MedioPago = value;
                    ESGR_VentaCuenta.ESGR_MedioPago = value;
                }
                OnPropertyChanged("SelectedESGR_MedioPago");
            }
        }

        private ESGR_Cliente _SelectedESGR_Cliente;
        public ESGR_Cliente SelectedEGSR_Cliente
        {
            get
            {
                if (_SelectedESGR_Cliente == null)
                    _SelectedESGR_Cliente = new ESGR_Cliente();
                return _SelectedESGR_Cliente;
            }
            set
            {
                _SelectedESGR_Cliente = value;
                if (value != null)
                    ESGR_VentaCuenta.ESGR_Cliente = value;
                else
                    ESGR_VentaCuenta.ESGR_Cliente = null;
                OnPropertyChanged("SelectedEGSR_Cliente");
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
                if (value != null)
                {
                    _SelectedESGR_Moneda = value;
                    ESGR_VentaCuenta.ESGR_Moneda = value;
                    MethodLoadTipoCambio();
                }
                OnPropertyChanged("SelectedESGR_Moneda");
            }
        }

        private ASSGR_ValueComboBox _SelectedESGR_Estado;
        public ASSGR_ValueComboBox SelectedESGR_Estado
        {
            get
            {
                return _SelectedESGR_Estado;
            }
            set
            {
                if(value != null)
                _SelectedESGR_Estado = value;
                OnPropertyChanged("SelectedESGR_Estado");
            }
        }

        private ESGR_VentaDetalle _SelectedESGR_VentaDetalle;
        public ESGR_VentaDetalle SelectedESGR_VentaDetalle
        {
            get
            {
                return _SelectedESGR_VentaDetalle;
            }
            set
            {
                _SelectedESGR_VentaDetalle = value;
                OnPropertyChanged("SelectedESGR_VentaDetalle");
            }
        }

        #region CALCULOS TOTALES

        private decimal _Importe;
        public decimal Importe
        {
            get
            {
                return _Importe;
            }
            set
            {
                _Importe = value;
                OnPropertyChanged("Importe");
            }
        }

        private decimal _ImporteDscto;
        public decimal ImporteDscto
        {
            get
            {
                return _ImporteDscto;
            }
            set
            {
                _ImporteDscto = value;
                OnPropertyChanged("ImporteDscto");
            }
        }

        private decimal _Efectivo;
        public decimal Efectivo
        {
            get
            {
                return _Efectivo;
            }
            set
            {
                _Efectivo = value;
                OnPropertyChanged("Efectivo");
            }
        }

        private decimal _Vuelto;
        public decimal Vuelto
        {
            get
            {
                return _Vuelto;
            }
            set
            {
                _Vuelto = value;
                OnPropertyChanged("Vuelto");
            }
        }

        #endregion

        private string _NroDocIdentidad;
        public string NroDocIdentidad
        {
            get
            {
                if (_NroDocIdentidad == null)
                    _NroDocIdentidad = string.Empty;
                return _NroDocIdentidad;
            }
            set
            {
                _NroDocIdentidad = value;
                ESGR_VentaCuenta.ESGR_Cliente.NroDocIdentidad = value;
                OnPropertyChanged("NroDocIdentidad");
            }
        }

        #endregion

        #region COLECCIONES

        private CmpObservableCollection<ESGR_Documento> _CollectionESGR_DocumentoSerieNumero;
        public CmpObservableCollection<ESGR_Documento> CollectionESGR_DocumentoSerieNumero
        {
            get
            {
                if (_CollectionESGR_DocumentoSerieNumero == null)
                    _CollectionESGR_DocumentoSerieNumero = new CmpObservableCollection<ESGR_Documento>();
                return _CollectionESGR_DocumentoSerieNumero;
            }
        }

        private CmpObservableCollection<ESGR_MedioPago> _CollectionESGR_MedioPago;
        public CmpObservableCollection<ESGR_MedioPago> CollectionESGR_MedioPago
        {
            get
            {
                if (_CollectionESGR_MedioPago == null)
                    _CollectionESGR_MedioPago = new CmpObservableCollection<ESGR_MedioPago>();
                return _CollectionESGR_MedioPago;
            }
        }

        private CmpObservableCollection<ESGR_Cliente> _CollectionESGR_Cliente;
        public CmpObservableCollection<ESGR_Cliente> CollectionESGR_Cliente
        {
            get
            {
                if (_CollectionESGR_Cliente == null)
                    _CollectionESGR_Cliente = new CmpObservableCollection<ESGR_Cliente>();
                return _CollectionESGR_Cliente;
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

        private CmpObservableCollection<ESGR_Pedido> _CollectionESGR_Pedido;
        public CmpObservableCollection<ESGR_Pedido> CollectionESGR_Pedido
        {
            get
            {
                if (_CollectionESGR_Pedido == null)
                    _CollectionESGR_Pedido = new CmpObservableCollection<ESGR_Pedido>();
                return _CollectionESGR_Pedido;
            }
        }

        private CmpObservableCollection<ESGR_PedidoDetalle> _CollectionESGR_PedidoDetalle;
        public CmpObservableCollection<ESGR_PedidoDetalle> CollectionESGR_PedidoDetalle
        {
            get
            {
                if (_CollectionESGR_PedidoDetalle == null)
                    _CollectionESGR_PedidoDetalle = new CmpObservableCollection<ESGR_PedidoDetalle>();
                return _CollectionESGR_PedidoDetalle;
            }
        }

        private CmpObservableCollection<ESGR_VentaDetalle> _CollectionEGSR_VentaDetalle;
        public CmpObservableCollection<ESGR_VentaDetalle> CollectionESGR_VentaDetalle
        {
            get
            {
                if (_CollectionEGSR_VentaDetalle == null)
                    _CollectionEGSR_VentaDetalle = new CmpObservableCollection<ESGR_VentaDetalle>();
                return _CollectionEGSR_VentaDetalle;
            }
        }

        private CmpObservableCollection<ASSGR_ValueComboBox> _CollectionESGR_Estado;
        public CmpObservableCollection<ASSGR_ValueComboBox> CollectionESGR_Estado
        {
            get
            {
                if (_CollectionESGR_Estado == null)
                    _CollectionESGR_Estado = new CmpObservableCollection<ASSGR_ValueComboBox>();
                return _CollectionESGR_Estado;
            }
        }

        private CmpObservableCollection<ESGR_TipoCambio> _CollectionESGR_TipoCambio;
        public CmpObservableCollection<ESGR_TipoCambio> CollectionESGR_TipoCambio
        {
            get
            {
                if (_CollectionESGR_TipoCambio == null)
                    _CollectionESGR_TipoCambio = new CmpObservableCollection<ESGR_TipoCambio>();
                return _CollectionESGR_TipoCambio;
            }
        }

        private CmpObservableCollection<ESGR_VentaDetalle> _CollectionESGR_DetalleVenta;
        public CmpObservableCollection<ESGR_VentaDetalle> CollectionESGR_DetalleVenta
        {
            get
            {
                if (_CollectionESGR_DetalleVenta == null)
                    _CollectionESGR_DetalleVenta = new CmpObservableCollection<ESGR_VentaDetalle>();
                return _CollectionESGR_DetalleVenta;
            }
        }

        #endregion

        #region PROPERTY

        private string _PropertyFiltroPedido;
        public string PropertyFiltroPedido
        {
            get
            {
                return _PropertyFiltroPedido;
            }
            set
            {
                _PropertyFiltroPedido = value;
                OnPropertyChanged("PropertyFiltroPedido");
            }

        }

        private bool _PropertyEnableUpdate;
        public bool PropertyEnableUpdate
        {
            get
            {
                return _PropertyEnableUpdate;
            }
            set
            {
                _PropertyEnableUpdate = value;
                OnPropertyChanged("PropertyEnableUpdate");
            }
        }

        private bool _PropertyEnableUpdateAddOneCount;
        public bool PropertyEnableUpdateAddOneCount
        {
            get
            {
                return _PropertyEnableUpdateAddOneCount;
            }
            set
            {
                _PropertyEnableUpdateAddOneCount = value;
                OnPropertyChanged("PropertyEnableUpdateAddOneCount");
            }
        }

        private bool _PropertyEnabledColumnCantidadAPagar;
        public bool PropertyEnabledColumnCantidadAPagar
        {
            get
            {
                return _PropertyEnabledColumnCantidadAPagar;
            }
            set
            {
                _PropertyEnabledColumnCantidadAPagar = value;
                OnPropertyChanged("PropertyEnabledColumnCantidadAPagar");
            }
        }

        #endregion

        #region ICOMMAND

        public ICommand IRegistraCliente
        {
            get
            {
                return CmpICommand.GetICommand((T) =>
                {
                    try
                    {
                        var a = CmpModalDialog.GetContent().ShowDialog("MPSGR_Cliente", new ESGR_Cliente() { Opcion = null, NroDocIdentidad = NroDocIdentidad });
                    }
                    catch (Exception ex)
                    {
                        CmpMessageBox.Show(SGRMessage.AdministratorVenta, ex.Message, CmpButton.Aceptar);
                    }
                });
            }
        }

        public ICommand ICalcularTotales
        {
            get
            {
                return CmpICommand.GetICommand((T) =>
                {
                    MethodCalcularTotales();
                });
            }
        }

        public ICommand ICalcularDetalle
        {
            get
            {
                return CmpICommand.GetICommand((T) =>
                {
                    if (SelectedESGR_VentaDetalle == null) return;
                    if (SelectedESGR_VentaDetalle.CantidadPagar > SelectedESGR_VentaDetalle.Cantidad)
                        SelectedESGR_VentaDetalle.CantidadPagar = SelectedESGR_VentaDetalle.Cantidad;
                    SelectedESGR_VentaDetalle.Importe = SelectedESGR_VentaDetalle.ESGR_Producto.Precio * SelectedESGR_VentaDetalle.CantidadPagar;
                    MethodCalcularTotales();
                });
            }
        }

        public ICommand ICalcularVuelto
        {
            get
            {
                return CmpICommand.GetICommand((T) =>
                {
                    Vuelto = Efectivo - Importe;
                });
            }
        }

        public ICommand IAddOneCount
        {
            get
            {
                return CmpICommand.GetICommand((T) =>
                {
                    CollectionESGR_VentaDetalle.ToList().ForEach(x =>
                    {
                        x.CantidadPagar = x.Cantidad;
                        x.Importe = x.CantidadPagar * x.ESGR_Producto.Precio;
                    });
                    MethodCalcularTotales();
                });
            }
        }

        public ICommand IAceptar
        {
            get
            {
                return CmpICommand.GetICommand((T) =>
                {
                    if (MethodValidaDatos()) return;
                    ESGR_VentaCuenta.JsonDetalle = JsonConvert.SerializeObject(CollectionESGR_VentaDetalle);

                    if (SelectedESGR_Documento.CodDocumento == "BOL" && NroDocIdentidad.Trim().Length == 0 && (ESGR_VentaCuenta.ESGR_Cliente == null || ESGR_VentaCuenta.ESGR_Cliente.IdCliente == 0))
                        SelectedEGSR_Cliente = CollectionESGR_Cliente.FirstOrDefault(x => x.NroDocIdentidad == "00000000");
                        
                    string strJSON = JsonConvert.SerializeObject(ESGR_VentaCuenta);
                    object[] ArrayObject = new object[3];
                    ArrayObject[0] = strJSON;
                    CollectionESGR_VentaDetalle.ToList().ForEach(x => x.Cantidad = x.Cantidad - x.CantidadPagar);
                    if (CollectionESGR_VentaDetalle.ToList().Exists(x => x.Cantidad != 0))
                    {
                        CmpMessageBox.Show(SGRMessage.AdministratorVenta, "Aun hay platos por pagar.\n¿Desea agregarlo en otra cuenta?", CmpButton.AceptarCancelar, () =>
                        {
                            ArrayObject[1] = true;
                            CmpModalDialog.GetContent().Close(ArrayObject);
                        },
                        () =>
                        {
                            ArrayObject[1] = false;
                            CmpModalDialog.GetContent().Close(ArrayObject);
                        });
                    }
                    else
                    {
                        ArrayObject[1] = false;
                        CmpModalDialog.GetContent().Close(ArrayObject);
                    }
                });
            }
        }

        public ICommand IVolver
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

        private void MethodSetParameter(object value)
        {
            if (value is object[])
            {
                object[] Arreglo = (object[])value;

                #region REINICIO CALCULO

                Importe = 0;
                ImporteDscto = 0;
                Efectivo = 0;
                Vuelto = 0;

                #endregion

                if (Arreglo[0] is ESGR_Venta)
                    ESGR_VentaCuenta.ESGR_Venta = (ESGR_Venta)Arreglo[0];
                if (Arreglo[2] is CmpObservableCollection<ESGR_VentaDetalle>)
                {
                    ESGR_VentaCuenta.Opcion = "I";
                    CollectionESGR_PedidoDetalle.Source = (CmpObservableCollection<ESGR_PedidoDetalle>)Arreglo[1];
                    CollectionESGR_DetalleVenta.Source = (CmpObservableCollection<ESGR_VentaDetalle>)Arreglo[2];
                    ESGR_VentaCuenta.IdCuenta = (CollectionESGR_DetalleVenta.Count == 0) ? 1 : CollectionESGR_DetalleVenta.LastOrDefault().ESGR_VentaCuenta.IdCuenta + 1;
                    ESGR_VentaCuenta.Adicional = 0;
                    ESGR_VentaCuenta.Descuento = 0;
                    PropertyEnableUpdate = true;
                    PropertyEnableUpdateAddOneCount = true;
                    PropertyEnabledColumnCantidadAPagar = true;
                }
                else
                {
                    CollectionESGR_VentaDetalle.Source = (CmpObservableCollection<ESGR_VentaDetalle>)Arreglo[1];
                    CollectionESGR_VentaDetalle.OrderByDescending(x => x.Cantidad);
                    ESGR_VentaCuenta = (ESGR_VentaCuenta)Arreglo[2];
                    PropertyEnableUpdate = (ESGR_VentaCuenta.Opcion != "U");
                    PropertyEnableUpdateAddOneCount = false;
                    PropertyEnabledColumnCantidadAPagar = (ESGR_VentaCuenta.Opcion != "U");
                }
                MethodLoadHeader();
            }
        }

        private async void MethodLoadHeader()
        {
            await Task.Factory.StartNew(() =>
            {
                try
                {
                    ESGR_Cliente vrESGR_Cliente = new ESGR_Cliente();
                    if (ESGR_VentaCuenta.Opcion != "I")
                        vrESGR_Cliente = ESGR_VentaCuenta.ESGR_Cliente;
                    ObservableCollection<ASSGR_ValueComboBox> GetCollectionValueComboBox = new ObservableCollection<ASSGR_ValueComboBox>()
                    {
                        #region ADD VALUES
		                new ASSGR_ValueComboBox()
                        {
                            Codigo = "PAG",
                            Value = "PAGADO"
                        },
                        new ASSGR_ValueComboBox()
                        {
                            Codigo = "PED",
                            Value = "PENDIENTE"
                        }
	                    #endregion
                    };
                    CollectionESGR_MedioPago.Source = new BSGR_MedioPago().GetCollectionMedioPago();
                    CollectionESGR_Estado.Source = new CmpObservableCollection<ASSGR_ValueComboBox>(GetCollectionValueComboBox);
                    CollectionESGR_DocumentoSerieNumero.Source = new BSGR_Documento().GetCollectionDocumento();
                    CollectionESGR_Cliente.Source = new BSGR_Cliente().GetCollectionCliente("%");
                    CollectionESGR_Documento.Source = new CmpObservableCollection<ESGR_Documento>(new BSGR_Documento().GetCollectionDocumento().Where(x => x.ESGR_EmpresaSucursal.IdEmpSucursal == 1).AsEnumerable());
                    CollectionESGR_Moneda.Source = new BSGR_Moneda().GetCollectionMoneda();
                    
                    PropertyFiltroPedido = string.Empty;
                    if (ESGR_VentaCuenta.Opcion != "I")
                    {
                        SelectedESGR_Documento = CollectionESGR_Documento.FirstOrDefault(x => x.CodDocumento == ESGR_VentaCuenta.ESGR_Documento.CodDocumento);
                        SelectedESGR_MedioPago = CollectionESGR_MedioPago.FirstOrDefault(x => x.IdMedioPago == ESGR_VentaCuenta.ESGR_MedioPago.IdMedioPago);
                        SelectedESGR_Estado = CollectionESGR_Estado.First(x => x.Codigo == "PAG");
                        SelectedESGR_Moneda = CollectionESGR_Moneda.FirstOrDefault(x => x.CodMoneda == ESGR_VentaCuenta.ESGR_Moneda.CodMoneda);
                        if (vrESGR_Cliente.IdCliente != 0)
                            SelectedEGSR_Cliente = CollectionESGR_Cliente.FirstOrDefault(x => x.IdCliente == vrESGR_Cliente.IdCliente);
                        else
                        {
                            SelectedEGSR_Cliente = vrESGR_Cliente;
                            NroDocIdentidad = vrESGR_Cliente.NroDocIdentidad;
                        }
                    }
                    else
                    {
                        SelectedESGR_Documento = CollectionESGR_Documento.ToList().FirstOrDefault();
                        SelectedESGR_MedioPago = CollectionESGR_MedioPago.FirstOrDefault();
                        SelectedESGR_Estado = CollectionESGR_Estado.First(x => x.Codigo == "PED");
                        SelectedESGR_Moneda = CollectionESGR_Moneda.FirstOrDefault(x => x.CodMoneda == "SOL");
                        SelectedEGSR_Cliente = null;
                    }
                    MethodLoadPedido(ESGR_VentaCuenta.ESGR_Venta.ESGR_Pedido);
                }
                catch (Exception ex)
                {
                    CmpMessageBox.Show(SGRMessage.AdministratorVenta, ex.Message, CmpButton.Aceptar);
                }
            });
        }

        private async void MethodLoadSerieNumero(string CodDocumento)
        {
            await Task.Factory.StartNew(() =>
            {
                try
                {
                    if (ESGR_VentaCuenta.Opcion == "I")
                    {
                        CollectionESGR_DetalleVenta.OrderBy(x => x.ESGR_VentaCuenta.ESGR_Documento.CodDocumento);
                        var asd = CollectionESGR_DetalleVenta;
                        var vrDocumento = (CollectionESGR_DetalleVenta.Count > 0 ) ? CollectionESGR_DetalleVenta.LastOrDefault(x => x.ESGR_VentaCuenta.ESGR_Documento.CodDocumento == CodDocumento) : null;
                        if (vrDocumento != null)
                        {
                            string Correlativo = string.Empty;
                            ESGR_VentaCuenta.Serie = vrDocumento.ESGR_VentaCuenta.ESGR_Documento.Serie;
                            var strCorrelativo = (Convert.ToInt32(vrDocumento.ESGR_VentaCuenta.Numero) + 1).ToString();
                            for (int item = 0; item < (vrDocumento.ESGR_VentaCuenta.ESGR_Documento.Longitud - strCorrelativo.Length); item++)
                                Correlativo += "0";
                            ESGR_VentaCuenta.Numero = Correlativo + strCorrelativo;
                        }
                        else
                        {
                            CollectionESGR_DocumentoSerieNumero.Source = new BSGR_Documento().GetCollectionDocumento(CodDocumento);
                            var FirstDocumentoSerieNumero = CollectionESGR_DocumentoSerieNumero.FirstOrDefault();
                            ESGR_VentaCuenta.Serie = FirstDocumentoSerieNumero.Serie;
                            ESGR_VentaCuenta.Numero = FirstDocumentoSerieNumero.Correlativo.ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    CmpMessageBox.Show(SGRMessage.AdministratorVenta, ex.Message, CmpButton.Aceptar);
                }
            });
        }

        private async void MethodLoadPedido(ESGR_Pedido ESGR_Pedido)
        {
            await Task.Factory.StartNew(() =>
            {
                try
                {
                    ESGR_VentaCuenta.ESGR_Venta.ESGR_Pedido = ESGR_Pedido;
                    if (CollectionESGR_PedidoDetalle.Count == 0)
                        PropertyFiltroPedido = string.Empty;
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        if (ESGR_VentaCuenta.Opcion == "I")
                        {
                            CollectionESGR_VentaDetalle.Clear();
                            CollectionESGR_PedidoDetalle.ToList().ForEach(x =>
                            {
                                CollectionESGR_VentaDetalle.Add(new ESGR_VentaDetalle()
                                {
                                    Cantidad = x.Cantidad,
                                    ESGR_Producto = x.ESGR_Producto,
                                    ESGR_VentaCuenta = this.ESGR_VentaCuenta,
                                    Importe = 0
                                });
                            });
                        }
                    });
                    MethodCalcularTotales();
                }
                catch (Exception ex)
                {
                    CmpMessageBox.Show(SGRMessage.AdministratorVenta, ex.Message, CmpButton.Aceptar);
                }
            });
        }

        private async void MethodLoadTipoCambio()
        {
            await Task.Factory.StartNew(() =>
            {
                try
                {
                    if (!SelectedESGR_Moneda.Defecto)
                    {
                        var ESGR_TipoCambio = new BSGR_TipoCambio().GetCollectionTipoCambio().FirstOrDefault(x => x.FechaTcb == ESGR_VentaCuenta.Fecha && x.ESGR_Moneda.CodMoneda == SelectedESGR_Moneda.CodMoneda);
                        if (ESGR_TipoCambio == null)
                        {
                            ESGR_VentaCuenta.TipoCambio = 0;
                            throw new Exception("No se ha registrado el tipo de cambio de la moneda extranjera para el día " + DateTime.Now.ToShortDateString());
                        }
                        else
                            ESGR_VentaCuenta.TipoCambio = (decimal)ESGR_TipoCambio.SelRate;
                    }
                    else
                        ESGR_VentaCuenta.TipoCambio = 1;
                }
                catch (Exception ex)
                {
                    CmpMessageBox.Show(SGRMessage.AdministratorVenta, ex.Message, CmpButton.Aceptar);
                }
            });
        }

        private void MethodCalcularTotales()
        {
            try
            {
                decimal IGV = 0;
                IGV = SGRVariables.ESGR_Retencion.IGV / 100;
                var ImporteTemp = CollectionESGR_VentaDetalle.Sum(x => x.Importe);
                ESGR_VentaCuenta.ImporteIGV = Convert.ToDecimal(ImporteTemp * IGV);
                ESGR_VentaCuenta.Gravada = Convert.ToDecimal(ImporteTemp - ESGR_VentaCuenta.ImporteIGV);
                Importe = Convert.ToDecimal(ESGR_VentaCuenta.Adicional - ESGR_VentaCuenta.Descuento + ESGR_VentaCuenta.ImporteIGV + ESGR_VentaCuenta.Gravada);
            }
            catch (Exception ex)
            {
                CmpMessageBox.Show(SGRMessage.AdministratorVenta, ex.Message, CmpButton.Aceptar);
            }

        }

        private bool MethodValidaDatos()
        {
            if (CollectionESGR_VentaDetalle.Count(x => x.CantidadPagar == 0) == CollectionESGR_VentaDetalle.Count)
            {
                CmpMessageBox.Show(SGRMessage.AdministratorVenta, "No ha ingresado cantidad a pagar", CmpButton.Aceptar);
                return true;
            }

            #region VALIDA POR DOCUMENTO

            else if (SelectedESGR_Documento.CodDocumento == "FAC")
            {
                if (ESGR_VentaCuenta.ESGR_Cliente == null)
                {
                    CmpMessageBox.Show(SGRMessage.AdministratorVenta, "Seleccione un Cliente", CmpButton.Aceptar);
                    return true;
                }
                else if (NroDocIdentidad.Trim().Length != 11)
                {
                    CmpMessageBox.Show(SGRMessage.AdministratorVenta, "Ingrese Nro de Documento válido", CmpButton.Aceptar);
                    return true;
                }
            }
            else if (SelectedESGR_Documento.CodDocumento == "BOL")
            {
                if (NroDocIdentidad.Trim().Length != 0 && NroDocIdentidad.Trim().Length != 8)
                {
                    CmpMessageBox.Show(SGRMessage.AdministratorVenta, "Ingrese Nro de Documento válido", CmpButton.Aceptar);
                    return true;
                }
            }

            #endregion
            
            return false;
        }

        #endregion
    }
}