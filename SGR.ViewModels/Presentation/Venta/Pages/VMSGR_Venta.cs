/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   OSCAR HUAMAN CABRERA
'* FCH. CREACIÓN : 01/12/2016
**********************************************************/

namespace SGR.ViewModels.Presentation.Venta.Pages
{
    using ComputerSystems;
    using ComputerSystems.WPF;
    using ComputerSystems.WPF.Acciones.Controles.ModalDialog;
    using ComputerSystems.WPF.Interfaces;
    using ComputerSystems.WPF.Notificaciones;
    using Newtonsoft.Json;
    using SGR.Models;
    using SGR.Models.Business;
    using SGR.Models.Entity;
    using SGR.ViewModels.Method;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Input;

    public class VMSGR_Venta : CmpNavigationService, CmpINavigation
    {
        private ESGR_Venta _ESGR_Venta;
        public ESGR_Venta ESGR_Venta
        {
            get
            {
                if (_ESGR_Venta == null)
                    _ESGR_Venta = new ESGR_Venta();
                return _ESGR_Venta;
            }
            set
            {
                _ESGR_Venta = value;
                OnPropertyChanged("ESGR_Venta");
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
                        Volver();
                    else if (value is ESGR_Venta)
                    {
                        ESGR_Venta = (ESGR_Venta)value;
                        MethodClear();
                        if (ESGR_Venta.Opcion == "I")
                        {
                            PropertyEnableUpdate = true;
                            PropertyIsEnabledButtonSeleccionarMesa = true;
                            PropertyIsEnabledButtonVentasRapidas = true;
                            PropertyIsEnabledButtonReservarMesa = true;
                            PropertyIsEnabledButtonAgregarProducto = false;
                            MethodCalcular();
                        }
                        else
                        {
                            MethodLoadPedido();
                            MethodLoadDetalle();
                            PropertyIsEnabledButtonAgregarProducto = false;
                        }
                    }
                });
            }
        }

        #region OBJ SECUNDARIO

        private decimal _SumaTotalPedido;
        public decimal SumaTotalPedido
        {
            get
            {
                return _SumaTotalPedido;
            }
            set
            {
                _SumaTotalPedido = value;
                OnPropertyChanged("SumaTotalPedido");
            }
        }

        private decimal _SumaTotalDetalle;
        public decimal SumaTotalDetalle
        {
            get
            {
                return _SumaTotalDetalle;
            }
            set
            {
                _SumaTotalDetalle = value;
                OnPropertyChanged("SumaTotalDetalle");
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

        private string OpcionGuardado;

        private string Observacion;

        #endregion

        #region COLECCIONES

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

        private bool _PropertyIsEnabledButtonSeleccionarMesa;
        public bool PropertyIsEnabledButtonSeleccionarMesa
        {
            get
            {
                return _PropertyIsEnabledButtonSeleccionarMesa;
            }
            set
            {
                _PropertyIsEnabledButtonSeleccionarMesa = value;
                OnPropertyChanged("PropertyIsEnabledButtonSeleccionarMesa");
            }
        }

        private bool _PropertyIsEnabledButtonVentasRapidas;
        public bool PropertyIsEnabledButtonVentasRapidas
        {
            get
            {
                return _PropertyIsEnabledButtonVentasRapidas;
            }
            set
            {
                _PropertyIsEnabledButtonVentasRapidas = value;
                OnPropertyChanged("PropertyIsEnabledButtonVentasRapidas");
            }
        }

        private bool _PropertyIsEnabledButtonReservarMesa;
        public bool PropertyIsEnabledButtonReservarMesa
        {
            get
            {
                return _PropertyIsEnabledButtonReservarMesa;
                //return false;
            }
            set
            {
                _PropertyIsEnabledButtonReservarMesa = value;
                OnPropertyChanged("PropertyIsEnabledButtonReservarMesa");
            }
        }

        private bool _PropertyIsEnabledButtonAgregarProducto;
        public bool PropertyIsEnabledButtonAgregarProducto
        {
            get
            {
                return _PropertyIsEnabledButtonAgregarProducto;
            }
            set
            {
                _PropertyIsEnabledButtonAgregarProducto = value;
                OnPropertyChanged("PropertyIsEnabledButtonAgregarProducto");
            }
        }

        #endregion

        #region ICOMMAND

        public ICommand ISeleccionarMesa
        {
            get
            {
                return CmpICommand.GetICommand(async(T) =>
                {
                    ESGR_Mesa result = (ESGR_Mesa)CmpModalDialog.GetContent().ShowDialog("MPSGR_MesasAtendidas", new ESGR_Mesa() { Opcion = "P" });
                    await Task.Factory.StartNew(() =>
                    {
                        if (result != null && result.IdPedido != 0)
                        {
                            try
                            {
                                ESGR_Venta.ESGR_Pedido = new BSGR_Pedido().GetCollectionPedido().ToList().FirstOrDefault(x => x.IdPedido == ((ESGR_Mesa)result).IdPedido);
                                MethodLoadPedido();
                                PropertyIsEnabledButtonVentasRapidas = false;
                                PropertyIsEnabledButtonReservarMesa = false;
                                PropertyIsEnabledButtonAgregarProducto = (ESGR_Venta.ESGR_Pedido.ESGR_Estado.CodEstado == "ATPED");
                            }
                            catch (Exception ex)
                            {
                                CmpMessageBox.Show(SGRMessage.AdministratorVenta, ex.Message, CmpButton.Aceptar);
                            }
                            OpcionGuardado = "SM";
                        }
                    });
                });
            }
        }

        public ICommand IVentaRapida
        {
            get
            {
                return CmpICommand.GetICommand((T) =>
                {
                    var Result = CmpModalDialog.GetContent().ShowDialog("MPSGR_VentaRapida", CollectionESGR_PedidoDetalle);
                    if (Result is object[])
                    {
                        var result = (object[])Result;

                        if (result[0] is CmpObservableCollection<ESGR_PedidoDetalle>)
                        {
                            CollectionESGR_PedidoDetalle.Source = (CmpObservableCollection<ESGR_PedidoDetalle>)result[0];
                            CollectionESGR_PedidoDetalle.ToList().ForEach(x => { x.Importe = x.Cantidad * x.ESGR_Producto.Precio; x.CantidadAux = x.Cantidad; });
                            MethodCalcular();
                            PropertyIsEnabledButtonSeleccionarMesa = false;
                            PropertyIsEnabledButtonReservarMesa = false;
                            OpcionGuardado = "VR";
                        }

                        if (result[1] is string)
                            Observacion = (string)result[1];
                    }
                });
            }
        }

        public ICommand IReservarMesa
        {
            get
            {
                return CmpICommand.GetICommand((T) =>
                {
                    CmpModalDialog.GetContent().ShowDialog("MPSGR_MesasDisponibles", new ESGR_Mesa() { Opcion = "R" });
                });
            }
        }

        public ICommand IAgregarProducto
        {
            get
            {
                return CmpICommand.GetICommand((T) =>
                {
                    var Temp = CollectionESGR_PedidoDetalle;
                    var result = CmpModalDialog.GetContent().ShowDialog("MPSGR_BuscadorProductoCartaDia", Temp);
                    if (result is ESGR_PedidoDetalle)
                    {
                        var vrPedidoDetalle = (ESGR_PedidoDetalle)result;
                        if (!CollectionESGR_PedidoDetalle.ToList().Exists(x => x.ESGR_Producto.IdProducto == vrPedidoDetalle.ESGR_Producto.IdProducto))
                            CollectionESGR_PedidoDetalle.Add(vrPedidoDetalle);
                        else
                        {
                            var FirstDetalle = CollectionESGR_PedidoDetalle.FirstOrDefault(x => x.ESGR_Producto.IdProducto == vrPedidoDetalle.ESGR_Producto.IdProducto);
                            if (FirstDetalle != null)
                            {
                                if (FirstDetalle.CantidadAux == 0)
                                    CollectionESGR_PedidoDetalle.Remove(FirstDetalle);
                                else
                                {
                                    FirstDetalle.Cantidad = vrPedidoDetalle.Cantidad;
                                    FirstDetalle.CantidadAux = vrPedidoDetalle.CantidadAux;
                                    FirstDetalle.Importe = vrPedidoDetalle.Cantidad * vrPedidoDetalle.ESGR_Producto.Precio;
                                    FirstDetalle.Enviado = false;
                                }
                            }
                        }
                    }
                    else if (result is CmpObservableCollection<ESGR_PedidoDetalle>)
                        CollectionESGR_PedidoDetalle.Source = new CmpObservableCollection<ESGR_PedidoDetalle>((System.Collections.ObjectModel.ObservableCollection<ESGR_PedidoDetalle>)result);
                });
            }
        }
        
        public ICommand IAddAccount
        {
            get
            {
                return CmpICommand.GetICommand((T) =>
                {
                    if (CollectionESGR_PedidoDetalle.Where(x => x.Cantidad == 0).ToList().Count == CollectionESGR_PedidoDetalle.Count)
                    {
                        CmpMessageBox.Show(SGRMessage.AdministratorVenta, "No hay pedido que dividir", CmpButton.Aceptar);
                        return;
                    }
                    bool estado = true;
                    while (estado)
                    {
                        object[] Parameter = new object[3];
                        Parameter[0] = ESGR_Venta;
                        Parameter[1] = CollectionESGR_PedidoDetalle;
                        Parameter[2] = CollectionESGR_VentaDetalle;

                        var Result = CmpModalDialog.GetContent().ShowDialog("MPSRG_VentaCuenta", Parameter);

                        object[] result = null;
                        if (Result is object[])
                            result = (object[])Result;

                        if (result != null && result[0] is string)
                        {
                            CollectionESGR_VentaDetalle.Add(new ESGR_VentaDetalle()
                            {
                                ESGR_VentaCuenta = JsonConvert.DeserializeObject<ESGR_VentaCuenta>((string)result[0]),
                            });
                            CollectionESGR_VentaDetalle.OrderByDescending(x => x.Item);
                            var vrLastESGR_VentaDetalle = CollectionESGR_VentaDetalle.LastOrDefault();
                            var vrLastESGR_VentaCuenta = vrLastESGR_VentaDetalle.ESGR_VentaCuenta;
                            vrLastESGR_VentaDetalle.Importe = vrLastESGR_VentaCuenta.ImporteIGV + vrLastESGR_VentaCuenta.Adicional - vrLastESGR_VentaCuenta.Descuento + vrLastESGR_VentaCuenta.Gravada;
                            var vrCollectionPedidoDetalle = JsonConvert.DeserializeObject<CmpObservableCollection<ESGR_VentaDetalle>>(vrLastESGR_VentaDetalle.ESGR_VentaCuenta.JsonDetalle);
                            vrCollectionPedidoDetalle.ToList().ForEach(x =>
                            {
                                var FirstPedido = CollectionESGR_PedidoDetalle.FirstOrDefault(y => y.ESGR_Producto.IdProducto == x.ESGR_Producto.IdProducto);
                                FirstPedido.Cantidad = x.Cantidad - x.CantidadPagar;
                                FirstPedido.Importe = FirstPedido.Cantidad * FirstPedido.ESGR_Producto.Precio;
                            });
                            MethodCalcular();
                            estado = (bool)result[1];
                        }
                        else
                            estado = false;
                    }

                    PropertyIsEnabledButtonReservarMesa = false;
                    PropertyIsEnabledButtonSeleccionarMesa = false;
                    PropertyIsEnabledButtonVentasRapidas = false;

                });
            }
        }

        public ICommand IEditAccount
        {
            get
            {
                return CmpICommand.GetICommand((T) =>
                {
                    #region LLENA PEDIDO DETALLE

                    if (SelectedESGR_VentaDetalle == null) return;
                    var vrCollectionESGR_VentaDetalle = JsonConvert.DeserializeObject<CmpObservableCollection<ESGR_VentaDetalle>>(SelectedESGR_VentaDetalle.ESGR_VentaCuenta.JsonDetalle);
                    vrCollectionESGR_VentaDetalle.ToList().ForEach(x =>
                    {
                        var FirstPedido = CollectionESGR_PedidoDetalle.FirstOrDefault(y => y.ESGR_Producto.IdProducto == x.ESGR_Producto.IdProducto);
                        if (FirstPedido != null)
                        {
                            x.Cantidad = FirstPedido.Cantidad + x.CantidadPagar;
                            FirstPedido.Importe = FirstPedido.Cantidad * FirstPedido.ESGR_Producto.Precio;
                        }
                    });

                    #endregion

                    object[] Parameter = new object[3];
                    Parameter[0] = SelectedESGR_VentaDetalle.ESGR_VentaCuenta.ESGR_Venta;
                    Parameter[1] = new CmpObservableCollection<ESGR_VentaDetalle>(vrCollectionESGR_VentaDetalle.Where(x => x.ESGR_VentaCuenta.IdCuenta == SelectedESGR_VentaDetalle.ESGR_VentaCuenta.IdCuenta).ToList());
                    SelectedESGR_VentaDetalle.ESGR_VentaCuenta.Opcion = (ESGR_Venta.Opcion == "I") ? "E" : "U";
                    Parameter[2] = SelectedESGR_VentaDetalle.ESGR_VentaCuenta;

                    var Result = CmpModalDialog.GetContent().ShowDialog("MPSRG_VentaCuenta", Parameter);
                    object[] result = (object[])Result;
                    if (result != null)
                    {
                        var vrFirstESGR_VentaDetalle = CollectionESGR_VentaDetalle.FirstOrDefault(x => x.ESGR_VentaCuenta.IdCuenta == SelectedESGR_VentaDetalle.ESGR_VentaCuenta.IdCuenta);
                        vrFirstESGR_VentaDetalle.ESGR_VentaCuenta.ESGR_Venta = ESGR_Venta;
                        vrFirstESGR_VentaDetalle.ESGR_VentaCuenta = JsonConvert.DeserializeObject<ESGR_VentaCuenta>((string)result[0]);

                        var vrLastESGR_VentaCuenta = vrFirstESGR_VentaDetalle.ESGR_VentaCuenta;
                        vrFirstESGR_VentaDetalle.Importe = vrLastESGR_VentaCuenta.ImporteIGV + vrLastESGR_VentaCuenta.Adicional - vrLastESGR_VentaCuenta.Descuento + vrLastESGR_VentaCuenta.Gravada;
                        var vrCollectionPedidoDetalle = JsonConvert.DeserializeObject<CmpObservableCollection<ESGR_VentaDetalle>>(vrFirstESGR_VentaDetalle.ESGR_VentaCuenta.JsonDetalle);
                        vrCollectionPedidoDetalle.ToList().ForEach(x =>
                        {
                            var FirstPedido = CollectionESGR_PedidoDetalle.FirstOrDefault(y => y.ESGR_Producto.IdProducto == x.ESGR_Producto.IdProducto);
                            if (FirstPedido != null)
                            {
                                FirstPedido.Cantidad = x.Cantidad - x.CantidadPagar;
                                FirstPedido.Importe = FirstPedido.Cantidad * FirstPedido.ESGR_Producto.Precio;
                            }
                        });
                        MethodCalcular();
                    }
                });
            }
        }

        public ICommand IQuitar
        {
            get
            {
                return CmpICommand.GetICommand((T) =>
                {
                    if (SelectedESGR_VentaDetalle == null || ESGR_Venta.Opcion != "I") return;

                    #region DEVUELVE CANTIDADES A PEDIDO

                    CmpMessageBox.Show(SGRMessage.AdministratorVenta, SGRMessageContent.QuestionDelete, CmpButton.AceptarCancelar, () =>
                    {
                        var vrCollectionESGR_VentaDetalle = JsonConvert.DeserializeObject<CmpObservableCollection<ESGR_VentaDetalle>>(SelectedESGR_VentaDetalle.ESGR_VentaCuenta.JsonDetalle);
                        vrCollectionESGR_VentaDetalle.ToList().ForEach(x =>
                        {
                            var FirstPedido = CollectionESGR_PedidoDetalle.FirstOrDefault(y => y.ESGR_Producto.IdProducto == x.ESGR_Producto.IdProducto);
                            if (FirstPedido != null)
                            {
                                FirstPedido.Cantidad += x.CantidadPagar;
                                FirstPedido.Importe = FirstPedido.Cantidad * FirstPedido.ESGR_Producto.Precio;
                            }
                        });
                        CollectionESGR_VentaDetalle.Remove(SelectedESGR_VentaDetalle);
                        MethodCalcular();
                    });

                    #endregion

                    PropertyIsEnabledButtonReservarMesa = (CollectionESGR_VentaDetalle.Count > 0) ? false : (OpcionGuardado == "RM");
                    PropertyIsEnabledButtonSeleccionarMesa = (CollectionESGR_VentaDetalle.Count > 0) ? false : (OpcionGuardado == "SM");
                    PropertyIsEnabledButtonVentasRapidas = (CollectionESGR_VentaDetalle.Count > 0) ? false : (OpcionGuardado == "VR");

                });
            }
        }

        public ICommand IGuardar
        {
            get
            {
                return CmpICommand.GetICommand((T) =>
                {
                    if(MethodValidaDatos()) return;
                    CmpMessageBox.Show(SGRMessage.AdministratorVenta, "¿Seguro que desea guardar los datos?", CmpButton.AceptarCancelar, () =>
                    {
                        if (OpcionGuardado == "SM")
                        {
                            #region SELECCIONAR MESA

                            CmpMessageBox.Proccess(SGRMessage.AdministratorVenta, SGRMessageContent.ProcesandoDatos, () =>
                            {
                                try
                                {
                                    if (ESGR_Venta.Opcion == "I" && ESGR_Venta.ESGR_Pedido.ESGR_Estado.CodEstado == "ATPED" && CollectionESGR_PedidoDetalle.Where(x => x.Enviado == false).ToList().Count > 0)
                                    {
                                        var vrPedido = ESGR_Venta.ESGR_Pedido;

                                        vrPedido.Opcion = "U";
                                        vrPedido.CadenaDetalleXML = MethodCadenaDetallePedidoXML();

                                        new BSGR_Pedido().TransPedido(vrPedido);
                                        ESGR_Venta.ESGR_Pedido = vrPedido;

                                        #region ENVIO IMPRESION
                                        var vrCollectionPedidoDetalle = new BSGR_PedidoDetalle().GetCollectionPedidoDetalle(vrPedido);

                                        foreach (var item in vrCollectionPedidoDetalle)
                                        {
                                            var vrFirst = CollectionESGR_PedidoDetalle.FirstOrDefault(x => x.ESGR_Producto.IdProducto == item.ESGR_Producto.IdProducto);
                                            item.Enviado = (vrFirst == null) ? false : vrFirst.Enviado;
                                            item.CantidadAux = (vrFirst == null) ? 0 : vrFirst.CantidadAux;
                                        }

                                        vrCollectionPedidoDetalle.Source = new CmpObservableCollection<ESGR_PedidoDetalle>(vrCollectionPedidoDetalle.Where(x => x.Enviado == false).ToList());

                                        var vrListImpresora = vrCollectionPedidoDetalle.Select(x => new { x.ESGR_Producto.ESGR_ProductoSubCategoria.ESGR_ProductoCategoria.Impresora }).Distinct().ToList();
                                        vrListImpresora.ForEach(item =>
                                        {
                                            CrearTicket ticket = new CrearTicket();
                                            ticket.TextoCentro("=:= AGREGADO =:=");
                                            ticket.TextoIzquierda(DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
                                            ticket.TextoExtremos("Mesa: " + vrPedido.Identificador, "Ticket #001-" + vrPedido.IdPedido.ToString("00000#"));
                                            ticket.TextoExtremos("Pers: " + vrPedido.Cubierto, SGRVariables.ESGR_Usuario.ESGR_Perfil.NombrePerfil + ": " + SGRVariables.ESGR_Usuario.Nombres);
                                            ticket.lineasIgual();
                                            foreach (var producto in vrCollectionPedidoDetalle.Where(x => x.ESGR_Producto.ESGR_ProductoSubCategoria.ESGR_ProductoCategoria.Impresora == item.Impresora))
                                            {
                                                if (!producto.Enviado)
                                                {
                                                    ticket.TextoIzquierda(((int)producto.CantidadAux).ToString("0#") + "  " + producto.ESGR_Producto.Producto);
                                                    producto.Enviado = true;
                                                    if (producto.Observacion != null && producto.Observacion.Trim().Length > 0)
                                                        ticket.TextoComentario(producto.Observacion);
                                                }
                                            }
                                            ticket.lineasIgual();
                                            ticket.ImprimirTicket(item.Impresora);
                                        });
                                        #endregion
                                    }
                                }
                                catch (Exception ex)
                                {
                                    CmpMessageBox.Show(SGRMessage.AdministratorVenta, ex.Message, CmpButton.Aceptar);
                                }
                                MethodGuardarVenta();
                            }, null);

                            #endregion
                        }
                        else if (OpcionGuardado == "VR")
                        {
                            #region REGISTRAR VENTA

                            CmpMessageBox.Proccess(SGRMessage.AdministratorVenta, SGRMessageContent.ProcesandoDatos, () =>
                            {
                                try
                                {
                                    if (ESGR_Venta.Opcion == "I")
                                    {
                                        ESGR_Pedido ESGR_Pedido = new ESGR_Pedido()
                                        {
                                            Opcion = "I",
                                            Identificador = "0",
                                            Cubierto = 0,
                                            ESGR_EmpresaSucursal = SGRVariables.ESGR_Usuario.ESGR_EmpresaSucursal,
                                            ESGR_Estado = new ESGR_Estado()
                                            {
                                                CodEstado = "CTPED"
                                            },
                                            ESGR_Mesa = new ESGR_Mesa(),
                                            ESGR_PedidoTipo = new ESGR_PedidoTipo()
                                            {
                                                IdPedidoTipo = 3,
                                            },
                                            Observacion = this.Observacion,
                                            ESGR_Usuario = SGRVariables.ESGR_Usuario,
                                            CadenaDetalleXML = MethodCadenaDetallePedidoXML()
                                        };
                                        new BSGR_Pedido().TransPedido(ESGR_Pedido);
                                        ESGR_Venta.ESGR_Pedido = ESGR_Pedido;

                                        #region ENVIO IMPRESION
                                        var vrCollectionPedidoDetalle = new BSGR_PedidoDetalle().GetCollectionPedidoDetalle(ESGR_Pedido);
                                        var vrListImpresora = vrCollectionPedidoDetalle.Select(x => new { x.ESGR_Producto.ESGR_ProductoSubCategoria.ESGR_ProductoCategoria.Impresora }).Distinct().ToList();
                                        foreach (var item in vrListImpresora)
                                        {
                                            CrearTicket ticket = new CrearTicket();
                                            ticket.TextoCentro("== VENTA RAPIDA ==");
                                            ticket.TextoIzquierda(DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
                                            ticket.TextoExtremos("Mesa: " + ESGR_Pedido.Identificador, "Ticket #001-" + ESGR_Pedido.IdPedido.ToString("00000#"));
                                            ticket.TextoExtremos("Pers: " + ESGR_Pedido.Cubierto, SGRVariables.ESGR_Usuario.ESGR_Perfil.NombrePerfil + ": " + SGRVariables.ESGR_Usuario.Nombres);
                                            ticket.TextoIzquierda("Observacion: " + ESGR_Pedido.Observacion);
                                            ticket.TextoIzquierda("");
                                            ticket.lineasIgual();
                                            ticket.TextoIzquierda("");
                                            foreach (var producto in vrCollectionPedidoDetalle.Where(x => x.ESGR_Producto.ESGR_ProductoSubCategoria.ESGR_ProductoCategoria.Impresora == item.Impresora))
                                            {
                                                ticket.TextoIzquierda(((int)producto.Cantidad).ToString("0#") + "  " + producto.ESGR_Producto.Producto);
                                                producto.Enviado = true;
                                                if (producto.Observacion != null && producto.Observacion.Trim().Length > 0)
                                                    ticket.TextoComentario(producto.Observacion);
                                            }
                                            ticket.TextoIzquierda("");
                                            ticket.lineasIgual();
                                            ticket.ImprimirTicket(item.Impresora);
                                        }
                                        #endregion
                                    }
                                    MethodGuardarVenta();
                                }
                                catch (Exception ex)
                                {
                                    CmpMessageBox.Show(SGRMessage.AdministratorVenta, ex.Message, CmpButton.Aceptar);
                                }
                            }, null);

                            #endregion
                        }
                        else if (OpcionGuardado == "MR")
                        {
                            #region MESA RESERVADA
                            
                            #endregion
                        }
                    });
                });
            }
        }

        public ICommand IVolver
        {
            get
            {
                return CmpICommand.GetICommand((T) =>
                {
                    Volver(null);
                });
            }
        }

        #endregion

        #region METODOS
        
        private async void MethodLoadPedido()
        {
            await Task.Factory.StartNew(() =>
            {
                try
                {
                    CollectionESGR_PedidoDetalle.Source = new BSGR_PedidoDetalle().GetCollectionPedidoDetalle(ESGR_Venta.ESGR_Pedido);
                    if (CollectionESGR_PedidoDetalle.Count == 0)
                        PropertyFiltroPedido = string.Empty;

                    if (ESGR_Venta.Opcion == "I")
                        CollectionESGR_PedidoDetalle.ToList().ForEach(x => { x.CantidadAux = x.Cantidad; x.Importe = x.Cantidad * x.ESGR_Producto.Precio; });
                    else
                        CollectionESGR_PedidoDetalle.ToList().ForEach(x => { x.Cantidad = 0; x.Importe = x.Cantidad * x.ESGR_Producto.Precio; });

                    MethodCalcular();
                }
                catch (Exception ex)
                {
                    CmpMessageBox.Show(SGRMessage.AdministratorVenta, ex.Message, CmpButton.Aceptar);
                }
            });
        }

        private async void MethodLoadDetalle()
        {
            await Task.Factory.StartNew(() =>
            {
                try
                {
                    var vrCollectionESGR_VentaCuenta = new BSGR_VentaCuenta().GetCollectionESGR_VentaCuenta(ESGR_Venta.IdVenta);
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        vrCollectionESGR_VentaCuenta.ToList().ForEach(x =>
                        {
                            var vrCollectionVentaDetalle = new BSGR_VentaDetalle().GetCollectionVentaDetalle(ESGR_Venta.IdVenta);
                            x.JsonDetalle = JsonConvert.SerializeObject(vrCollectionVentaDetalle);
                            decimal dmlImporte = vrCollectionVentaDetalle.Where(z => z.ESGR_VentaCuenta.IdCuenta == x.IdCuenta).Sum(y => y.Precio * y.CantidadPagar);
                            dmlImporte = (dmlImporte + x.Adicional - x.Descuento);
                            CollectionESGR_VentaDetalle.Add(new ESGR_VentaDetalle() { ESGR_VentaCuenta = x, Importe = dmlImporte });
                        });
                        MethodCalcular();
                    });
                }
                catch (Exception ex)
                {
                    CmpMessageBox.Show(SGRMessage.AdministratorVenta, ex.Message, CmpButton.Aceptar);
                }
            });
        }

        private async void MethodGuardarVenta()
        {
            await Task.Factory.StartNew(() =>
            {
                try
                {
                    ESGR_Venta.CuentasXML = MethodCuentasXML();
                    ESGR_Venta.DetalleXML = MethodDetalleXML();
                    new BSGR_Venta().TransVenta(ESGR_Venta);

                    if (OpcionGuardado == "VR")
                    {
                        var CollectionMSGR_PedidoDetalle = new BSGR_PedidoDetalle().GetCollectionPedidoDetalle(ESGR_Venta.ESGR_Pedido);
                        #region REALIZA IMPRESION

                        CrearTicket ticket = new CrearTicket();
                        ticket.TextoCentro(SGRVariables.ESGR_Usuario.ESGR_Empresa.RazonSocial);
                        ticket.TextoIzquierda("RUC:   " + SGRVariables.ESGR_Usuario.ESGR_Empresa.Ruc);
                        ticket.TextoIzquierda("DIREC: " + SGRVariables.ESGR_Usuario.ESGR_Empresa.DireccionFiscal);
                        ticket.TextoIzquierda("TELEF: " + SGRVariables.ESGR_Usuario.ESGR_Empresa.Telefono);
                        ticket.TextoIzquierda("");
                        ticket.lineasIgual();
                        ticket.TextoIzquierda("");
                        ticket.TextoDerecha("Ticket #001-" + ESGR_Venta.ESGR_Pedido.IdPedido.ToString("00000#"));
                        ticket.TextoIzquierda(SGRVariables.ESGR_Usuario.ESGR_Perfil.NombrePerfil + ": " + SGRVariables.ESGR_Usuario.IdUsuario + " " + SGRVariables.ESGR_Usuario.Nombres);
                        ticket.TextoExtremos("Fecha: " + DateTime.Now.ToShortDateString(), "Hora: " + DateTime.Now.ToShortTimeString());
                        ticket.TextoExtremos("Mesa: " + ESGR_Venta.ESGR_Pedido.Identificador, "Personas: " + ESGR_Venta.ESGR_Pedido.Cubierto);
                        ticket.TextoIzquierda("");
                        ticket.lineasIgual();
                        ticket.TextoIzquierda("");
                        //Articulos a vender.
                        ticket.EncabezadoVenta();//NOMBRE DEL ARTICULO, CANT, PRECIO, IMPORTE
                        ticket.lineasGuion();
                        foreach (var item in CollectionMSGR_PedidoDetalle)
                        {
                            ticket.AgregaArticulo(item.ESGR_Producto.Producto, (int)item.Cantidad, (decimal)item.ESGR_Producto.Precio, ((int)item.Cantidad * (decimal)item.ESGR_Producto.Precio));
                        }
                        ticket.lineasGuion();
                        decimal Total = CollectionMSGR_PedidoDetalle.Sum(x => ((int)x.Cantidad * (decimal)x.ESGR_Producto.Precio));
                        ticket.AgregarTotales("         TOTAL S/", Total);
                        ticket.TextoIzquierda("");
                        ticket.TextoIzquierda("Razon Social:");
                        ticket.lineasSubGuion();
                        ticket.lineasSubGuion();
                        ticket.TextoIzquierda("RUC:");
                        ticket.lineasSubGuion();
                        ticket.TextoIzquierda("Direccion:");
                        ticket.lineasSubGuion();
                        ticket.lineasSubGuion();
                        ticket.TextoIzquierda("");
                        ticket.TextoCentro("¡ GRACIAS POR SU PREFERENCIA !");
                        ticket.TextoIzquierda("ESTE NO ES UN COMPROBANTE FISCAL CANJEAR POR BOLETA O FACTURA");

                        if (SGRVariables.NotificacionPreCuenta != null && SGRVariables.NotificacionPreCuenta.Trim().Length != 0)
                        {
                            ticket.TextoIzquierda("");
                            ticket.lineasAsteriscos();
                            ticket.TextoIzquierda("");
                            ticket.TextoIzquierda(SGRVariables.NotificacionPreCuenta);
                            ticket.TextoIzquierda("");
                        }

                        ticket.ImprimirTicket(SGRVariables.ImpresoraCaja);

                        #endregion
                    }

                    CmpMessageBox.Show(SGRMessage.AdministratorVenta, SGRMessageContent.ContentSaveOK, CmpButton.Aceptar, () => Volver(null));
                }
                catch (Exception ex)
                {
                    CmpMessageBox.Show(SGRMessage.AdministratorVenta, ex.Message, CmpButton.Aceptar);
                }
            });
        }

        private void MethodClear()
        {
            CollectionESGR_PedidoDetalle.Clear();
            CollectionESGR_VentaDetalle.Clear();
        }

        private void MethodCalcular()
        {
            SumaTotalPedido = CollectionESGR_PedidoDetalle.Sum(x => x.ESGR_Producto.Precio * x.Cantidad);
            SumaTotalDetalle = CollectionESGR_VentaDetalle.Sum(x => x.Importe);
        }

        private string MethodCuentasXML()
        {
            string strCadena = "<ROOT>", strDocIdentidad = string.Empty;
            CollectionESGR_VentaDetalle.ToList().ForEach(x =>
            {
                switch (x.ESGR_VentaCuenta.ESGR_Cliente.NroDocIdentidad.Trim().Length)
                {
                    #region casos DocIdentidad

                    case 8:
                        strDocIdentidad = "DNI";
                        break;
                    case 11:
                        strDocIdentidad = "RUC";
                        break;

                    #endregion
                }
                strCadena += "<Listar ";
                strCadena += "xIdEmpSucursal = \'" + SGRVariables.ESGR_Usuario.ESGR_Empresa.IdEmpresa;
                strCadena += "\' xIdCuenta = \'" +      x.ESGR_VentaCuenta.IdCuenta;
                strCadena += "\' xIdCliente = \'" +     x.ESGR_VentaCuenta.ESGR_Cliente.IdCliente;
                strCadena += "\' xDocIdentidad = \'" +  strDocIdentidad;
                strCadena += "\' xNroIdentidad = \'" +  x.ESGR_VentaCuenta.ESGR_Cliente.NroDocIdentidad;
                strCadena += "\' xCliente = \'" +       x.ESGR_VentaCuenta.ESGR_Cliente.Cliente;
                strCadena += "\' xDireccion = \'" +     x.ESGR_VentaCuenta.ESGR_Cliente.Direccion;
                strCadena += "\' xIdMedioPago = \'" +   x.ESGR_VentaCuenta.ESGR_MedioPago.IdMedioPago;
                strCadena += "\' xCodMoneda = \'" +     x.ESGR_VentaCuenta.ESGR_Moneda.CodMoneda;
                strCadena += "\' xCodDocumento = \'" +  x.ESGR_VentaCuenta.ESGR_Documento.CodDocumento;
                strCadena += "\' xSerie = \'" +         x.ESGR_VentaCuenta.Serie;
                strCadena += "\' xNumero = \'" +        x.ESGR_VentaCuenta.Numero;
                strCadena += "\' xFecha = \'" +         x.ESGR_VentaCuenta.Fecha.ToString("yyyyMMdd");
                strCadena += "\' xGravada = \'" +       x.ESGR_VentaCuenta.Gravada;
                strCadena += "\' xIGV = \'" +           x.ESGR_VentaCuenta.IGV;
                strCadena += "\' xImporteIGV = \'" +    x.ESGR_VentaCuenta.ImporteIGV;
                strCadena += "\' xAdicional = \'" +     x.ESGR_VentaCuenta.Adicional;
                strCadena += "\' xDescuento = \'" +     x.ESGR_VentaCuenta.Descuento;
                strCadena += "\' xImporteDscto = \'" +  x.ESGR_VentaCuenta.ImporteDscto;
                strCadena += "\'></Listar>";
            });
            strCadena += "</ROOT>";
            return strCadena;
        }

        private string MethodDetalleXML()
        {
            string strCadena = "<ROOT>";
            CollectionESGR_VentaDetalle.ToList().ForEach(y =>
            {
                var vrCollectionVentaDetalle = JsonConvert.DeserializeObject<CmpObservableCollection<ESGR_VentaDetalle>>(y.ESGR_VentaCuenta.JsonDetalle);
                vrCollectionVentaDetalle.ToList().ForEach(x =>
                {
                    strCadena += "<Listar ";
                    strCadena +=    "xIdCuenta = \'" +  y.ESGR_VentaCuenta.IdCuenta;
                    strCadena += "\' xIdProducto = \'" +x.ESGR_Producto.IdProducto;
                    strCadena += "\' xCantidad = \'" +  x.CantidadPagar;
                    strCadena += "\' xPrecio = \'" +    x.ESGR_Producto.Precio;
                    strCadena += "\' xDescuento = \'" + x.Descuento;
                    strCadena += "\' xImporte = \'" +   x.Importe;
                    strCadena += "\'></Listar>";
                });
            });
            strCadena += "</ROOT>";
            return strCadena;
        }

        private string MethodCadenaDetallePedidoXML()
        {
            string strCadena = "<ROOT>";
            CollectionESGR_PedidoDetalle.ToList().ForEach((x) =>
            {
                strCadena += "<Listar ";
                strCadena += "xIdProducto = \'" + x.ESGR_Producto.IdProducto;
                strCadena += "\' xCantidad = \'" + x.CantidadAux;
                strCadena += "\' xCantidadMesa = \'" + 0;
                strCadena += "\' xCantidadLlevar = \'" + 0;
                strCadena += "\' xPrecio = \'" + x.ESGR_Producto.Precio;
                strCadena += "\' xObservacion = \'" + x.Observacion;
                strCadena += "\' xEnviado = \'" + "true";
                strCadena += "\'></Listar>";
            });
            strCadena += "</ROOT>";
            return strCadena;
        }

        private bool MethodValidaDatos()
        {
            if (CollectionESGR_VentaDetalle.Count() == 0)
            {
                CmpMessageBox.Show(SGRMessage.AdministratorVenta, "No hay detalle que guardar.", CmpButton.Aceptar);
                return true;
            }
            else if (CollectionESGR_PedidoDetalle.Count(x => x.Cantidad > 0) > 0)
            {
                CmpMessageBox.Show(SGRMessage.AdministratorVenta, "Aun hay platos pendientes de pago.", CmpButton.Aceptar);
                return true;
            }
            return false;
        }

        #endregion
    }
}
