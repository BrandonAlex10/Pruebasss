/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
 * ********************************************************
 * 
'* FCH. CREACIÓN : 26/12/2016 [ABEL QUISPE ORELLANA]
 * 
 * ********************************************************
 * 
**********************************************************/
namespace SGR.ViewModels.Orders.Pedido.Pages
{
    using ComputerSystems;
    using ComputerSystems.WPF;
    using ComputerSystems.WPF.Acciones.Controles.ModalDialog;
    using ComputerSystems.WPF.Interfaces;
    using ComputerSystems.WPF.Notificaciones;
    using SGR.Models.Business;
    using SGR.Models.Entity;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Input;
    using SGR.Models.Models;
    using SGR.ViewModels.Method;
    using SGR.Models;

    public class VMSGR_Pedido : CmpNavigationService, CmpINavigation
    {
        private ESGR_Pedido _ESGR_Pedido;
        public ESGR_Pedido ESGR_Pedido
        {
            get
            {
                if (_ESGR_Pedido == null)
                    _ESGR_Pedido = new ESGR_Pedido();
                return _ESGR_Pedido;
            }
            set
            {
                _ESGR_Pedido = value;
                OnPropertyChanged("ESGR_Pedido");
            }
        }

        public object Parameter
        {
            set
            {
                MethodLoadCategoria();
                MethodLoadPedidoTipo();
                PropertyVisibilitySubCategoria = Visibility.Collapsed;
                MethodCleaningOrder();
                if (value is object[])
                {
                    var vrValue = (object[])value;

                    if (vrValue[0] is ESGR_Pedido)
                        ESGR_Pedido = (ESGR_Pedido)vrValue[0];
                    if (vrValue[1] is CmpObservableCollection<ESGR_Mesa>)
                        CollectionSelectESGR_Mesa.Source = (CmpObservableCollection<ESGR_Mesa>)vrValue[1];

                    PropertyVisibilityImprimirCuenta = Visibility.Collapsed;
                    PropertyVisibilityAnular = Visibility.Visible;
                    PropertyVisbilityEnviarPedido = Visibility.Visible;
                    PropertyIsEnabledListProducto = true;
                    PropertyIsEnabledListPedidoDetalle = true;
                    PropertyIsEnabledCbxPedidoTipo = true;
                    PropertyTextAnularCancelar = "LIMPIAR PEDIDO";
                }
                else if (value is ESGR_Pedido)
                {
                    ESGR_Pedido = (ESGR_Pedido)value;
                    MethodLoadMesa(ESGR_Pedido);
                    MethodLoadDetails(ESGR_Pedido);
                    PropertyVisibilityImprimirCuenta = (ESGR_Pedido.Opcion == "C") ? Visibility.Visible : Visibility.Collapsed;
                    PropertyVisbilityEnviarPedido = (ESGR_Pedido.Opcion == "A") ? Visibility.Collapsed : Visibility.Visible;
                    PropertyIsEnabledListProducto = (ESGR_Pedido.Opcion != "A");
                    PropertyIsEnabledListPedidoDetalle = (ESGR_Pedido.Opcion != "A");
                    PropertyVisibilityAnular = (ESGR_Pedido.Opcion == "U") ? Visibility.Collapsed : Visibility.Visible;
                    PropertyIsEnabledCbxPedidoTipo = false;
                    PropertyTextAnularCancelar = "ANULAR PEDIDO";
                }
                MethodLoadTipoCambio();
            }
        }

        #region COLECCIONES

        private CmpObservableCollection<VMSGR_PedidoDetalle> _CollectionMSGR_PedidoDetalle;
        public CmpObservableCollection<VMSGR_PedidoDetalle> CollectionMSGR_PedidoDetalle
        {
            get
            {
                if (_CollectionMSGR_PedidoDetalle == null)
                    _CollectionMSGR_PedidoDetalle = new CmpObservableCollection<VMSGR_PedidoDetalle>();
                return _CollectionMSGR_PedidoDetalle;
            }
        }

        private CmpObservableCollection<ESGR_Producto> _CollectionESGR_Producto;
        public CmpObservableCollection<ESGR_Producto> CollectionESGR_Producto
        {
            get
            {
                if (_CollectionESGR_Producto == null)
                    _CollectionESGR_Producto = new CmpObservableCollection<ESGR_Producto>();
                return _CollectionESGR_Producto;
            }
        }

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

        private CmpObservableCollection<ESGR_ProductoSubCategoria> _CollectionESGR_ProductoSubCategoria;
        public CmpObservableCollection<ESGR_ProductoSubCategoria> CollectionESGR_ProductoSubCategoria
        {
            get
            {
                if (_CollectionESGR_ProductoSubCategoria == null)
                    _CollectionESGR_ProductoSubCategoria = new CmpObservableCollection<ESGR_ProductoSubCategoria>();
                return _CollectionESGR_ProductoSubCategoria;
            }
        }

        private CmpObservableCollection<ESGR_PedidoTipo> _CollectionESGR_PedidoTipo;
        public CmpObservableCollection<ESGR_PedidoTipo> CollectionESGR_PedidoTipo
        {
            get
            {
                if (_CollectionESGR_PedidoTipo == null)
                    _CollectionESGR_PedidoTipo = new CmpObservableCollection<ESGR_PedidoTipo>();
                return _CollectionESGR_PedidoTipo;
            }
        }

        private CmpObservableCollection<ESGR_Mesa> _CollectionSelectESGR_Mesa;
        public CmpObservableCollection<ESGR_Mesa> CollectionSelectESGR_Mesa
        {
            get
            {
                if (_CollectionSelectESGR_Mesa == null)
                    _CollectionSelectESGR_Mesa = new CmpObservableCollection<ESGR_Mesa>();
                return _CollectionSelectESGR_Mesa;
            }
        }

        private List<VMSGR_PedidoDetalle> ListVMSGR_PedidoDetalleDelete = new List<VMSGR_PedidoDetalle>();

        #endregion

        #region OBJETOS SECUNDARIOS

        private VMSGR_PedidoDetalle _SelectedMSGR_PedidoDetalle;
        public VMSGR_PedidoDetalle SelectedMSGR_PedidoDetalle
        {
            get
            {
                return _SelectedMSGR_PedidoDetalle;
            }
            set
            {
                _SelectedMSGR_PedidoDetalle = value;
                OnPropertyChanged("SelectedMSGR_PedidoDetalle");
            }
        }

        public ESGR_Producto SelectedESGR_Producto
        {
            set
            {
                MethodAddProduct(value);
                if (value != null && ListVMSGR_PedidoDetalleDelete.Exists(x => x.ESGR_Producto.IdProducto == value.IdProducto))
                    ListVMSGR_PedidoDetalleDelete.Remove(ListVMSGR_PedidoDetalleDelete.FirstOrDefault(x => x.ESGR_Producto.IdProducto == value.IdProducto));
                OnPropertyChanged("SelectedESGR_Producto");
            }
        }

        private ESGR_ProductoCategoria _SelectedESGR_ProductoCategoria;
        public ESGR_ProductoCategoria SelectedESGR_ProductoCategoria
        {
            get
            {
                return _SelectedESGR_ProductoCategoria;
            }
            set
            {
                _SelectedESGR_ProductoCategoria = value;
                if (value != null)
                {
                    MethodLoadSubCategoria(value);
                    CollectionESGR_Producto.Clear();
                    PropertyVisibilityStock = (value.ValidaStock) ? Visibility.Visible : Visibility.Collapsed;
                }
                OnPropertyChanged("SelectedESGR_ProductoCategoria");
            }
        }

        private ESGR_ProductoSubCategoria _SelectedESGR_ProductoSubCategoria;
        public ESGR_ProductoSubCategoria SelectedESGR_ProductoSubCategoria
        {
            get
            {
                return _SelectedESGR_ProductoSubCategoria;
            }
            set
            {
                _SelectedESGR_ProductoSubCategoria = value;
                FiltrarProducto = null;
                if (value != null)
                    MethodLoadProducto(value);
                OnPropertyChanged("SelectedESGR_ProductoSubCategoria");
            }
        }

        private ESGR_PedidoTipo _SelectedESGR_PedidoTipo;
        public ESGR_PedidoTipo SelectedESGR_PedidoTipo
        {
            get
            {
                return _SelectedESGR_PedidoTipo;
            }
            set
            {
                if (value != null)
                {
                    _SelectedESGR_PedidoTipo = value;
                    MethodChangedPedidoTipo(value);
                    ESGR_Pedido.ESGR_PedidoTipo = value;
                }
                OnPropertyChanged("SelectedESGR_PedidoTipo");
            }
        }

        private ESGR_TipoCambio _ESGR_TipoCambio;
        public ESGR_TipoCambio ESGR_TipoCambio
        {
            get
            {
                return _ESGR_TipoCambio;
            }
            set
            {
                _ESGR_TipoCambio = value;
                OnPropertyChanged("ESGR_TipoCambio");
            }
        }
        
        private string _FiltrarProducto;
        public string FiltrarProducto
        {
            get 
            {
                return _FiltrarProducto;
            }
            set 
            {
                if (value != null && value.Trim().Length == 0)
                    value = null;

                _FiltrarProducto = value;

                ESGR_ProductoSubCategoria ESGR_ProductoSubCategoria;
                if (value != null)
                    ESGR_ProductoSubCategoria = new ESGR_ProductoSubCategoria() { ESGR_ProductoCategoria = new ESGR_ProductoCategoria() { } };
                else
                    ESGR_ProductoSubCategoria = SelectedESGR_ProductoSubCategoria;

                MethodLoadProducto(ESGR_ProductoSubCategoria, value ?? "%");
                OnPropertyChanged("FiltrarProducto");
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

        private decimal _TotalDolar;
        public decimal TotalDolar
        {
            get
            {
                return _TotalDolar;
            }
            set
            {
                _TotalDolar = value;
                OnPropertyChanged("TotalDolar");
            }
        }

        #endregion

        #region PROPERTY

        private Visibility _PropertyVisibilityImprimirCuenta;
        public Visibility PropertyVisibilityImprimirCuenta
        {
            get
            {
                return _PropertyVisibilityImprimirCuenta;
            }
            set
            {
                _PropertyVisibilityImprimirCuenta = value;
                OnPropertyChanged("PropertyVisibilityImprimirCuenta");
            }
        }

        private Visibility _PropertyVisibilityProgressRing;
        public Visibility PropertyVisibilityProgressRing
        {
            get
            {
                return _PropertyVisibilityProgressRing;
            }
            set
            {
                _PropertyVisibilityProgressRing = value;
                OnPropertyChanged("PropertyVisibilityProgressRing");
            }
        }

        private bool _PropertyMetroProgressBarCategoria;
        public bool PropertyMetroProgressBarCategoria
        {
            get
            {
                return _PropertyMetroProgressBarCategoria;
            }
            set
            {
                _PropertyMetroProgressBarCategoria = value;
                OnPropertyChanged("PropertyMetroProgressBarCategoria");
            }
        }

        private bool _PropertyMetroProgressBarSubCategoria;
        public bool PropertyMetroProgressBarSubCategoria
        {
            get
            {
                return _PropertyMetroProgressBarSubCategoria;
            }
            set
            {
                _PropertyMetroProgressBarSubCategoria = value;
                OnPropertyChanged("PropertyMetroProgressBarSubCategoria");
            }
        }

        private Visibility _PropertyVisibilitySubCategoria;
        public Visibility PropertyVisibilitySubCategoria
        {
            get
            {
                return _PropertyVisibilitySubCategoria;
            }
            set
            {
                _PropertyVisibilitySubCategoria = value;
                OnPropertyChanged("PropertyVisibilitySubCategoria");
            }
        }

        private bool _PropertyMetroProgressBarProducto;
        public bool PropertyMetroProgressBarProducto
        {
            get
            {
                return _PropertyMetroProgressBarProducto;
            }
            set
            {
                _PropertyMetroProgressBarProducto = value;
                OnPropertyChanged("PropertyMetroProgressBarProducto");
            }
        }

        private Visibility _PropertyVisibilityCant;
        public Visibility PropertyVisibilityCant
        {
            get
            {
                return _PropertyVisibilityCant;
            }
            set
            {
                _PropertyVisibilityCant = value;
                OnPropertyChanged("PropertyVisibilityCant");
            }
        }

        private Visibility _PropertyVisibilityAnular;
        public Visibility PropertyVisibilityAnular
        {
            get
            {
                return _PropertyVisibilityAnular;
            }
            set
            {
                _PropertyVisibilityAnular = value;
                OnPropertyChanged("PropertyVisibilityAnular");
            }
        }

        private Visibility _PropertyVisbilityEnviarPedido;
        public Visibility PropertyVisbilityEnviarPedido
        {
            get
            {
                return _PropertyVisbilityEnviarPedido;
            }
            set
            {
                _PropertyVisbilityEnviarPedido = value;
                OnPropertyChanged("PropertyVisbilityEnviarPedido");
            }
        }

        private bool _PropertyIsEnabledListProducto;
        public bool PropertyIsEnabledListProducto
        {
            get
            {
                return _PropertyIsEnabledListProducto;
            }
            set
            {
                _PropertyIsEnabledListProducto = value;
                OnPropertyChanged("PropertyIsEnabledListProducto");
            }
        }

        private bool _PropertyIsEnabledListPedidoDetalle;
        public bool PropertyIsEnabledListPedidoDetalle
        {
            get
            {
                return _PropertyIsEnabledListPedidoDetalle;
            }
            set
            {
                _PropertyIsEnabledListPedidoDetalle = value;
                OnPropertyChanged("PropertyIsEnabledListPedidoDetalle");
            }
        }

        private bool _PropertyIsEnabledCbxPedidoTipo;
        public bool PropertyIsEnabledCbxPedidoTipo
        {
            get
            {
                return _PropertyIsEnabledCbxPedidoTipo;
            }
            set
            {
                _PropertyIsEnabledCbxPedidoTipo = value;
                OnPropertyChanged("PropertyIsEnabledCbxPedidoTipo");
            }
        }

        private string _PropertyTextAnularCancelar;
        public string PropertyTextAnularCancelar
        {
            get
            {
                return _PropertyTextAnularCancelar;
            }
            set
            {
                _PropertyTextAnularCancelar = value;
                OnPropertyChanged("PropertyTextAnularCancelar");
            }
        }

        private Visibility _PropertyVisibilityStock;
        public Visibility PropertyVisibilityStock
        {
            get
            {
                return _PropertyVisibilityStock;
            }
            set
            {
                _PropertyVisibilityStock = value;
                OnPropertyChanged("PropertyVisibilityStock");
            }
        }

        #endregion

        #region ICOMMAND

        public ICommand IEnviarPedido
        {
            get
            {
                return CmpICommand.GetICommand((T) =>
                    {
                        if (MethodValidarDatos())
                            return;

                        string Message = string.Empty;
                        CmpMessageBox.Proccess(SGRMessage.TitlePedido, "Procesando datos, por favor espere", () =>
                        {
                            try
                            {
                                ESGR_Pedido.ESGR_Mesa.DetalleMesaXML = MethodDetalleMesaXML();
                                ESGR_Pedido.CadenaDetalleXML = MethodDetallePedidoXML();
                                ESGR_Pedido.CadenaMesa = MethodCadenaMesa();
                                if (ESGR_Pedido.Opcion == "I" || ESGR_Pedido.Opcion == "U" || ESGR_Pedido.ESGR_Estado.CodEstado == "RVPED") ESGR_Pedido.ESGR_Estado.CodEstado = "ATPED";
                                new BSGR_Pedido().TransPedido(ESGR_Pedido);
                            }
                            catch (Exception ex)
                            {
                                Message = ex.Message;
                            }
                        },
                        () =>
                        {
                            if (Message.Trim().Length > 0)
                            {
                                CmpMessageBox.Show(SGRMessage.TitlePedido, Message, CmpButton.Aceptar);
                                return;
                            }
                            try
                            {
                                #region ENVIO IMPRESION
                                var vrCollectionMSGR_PedidoDetalle = new BSGR_PedidoDetalle().GetCollectionPedidoDetalle(ESGR_Pedido);

                                foreach (var item in vrCollectionMSGR_PedidoDetalle)
                                {
                                    var First = CollectionMSGR_PedidoDetalle.FirstOrDefault(x => x.ESGR_Producto.IdProducto == item.ESGR_Producto.IdProducto);
                                    if (First != null)
                                    {
                                        item.Enviado = First.Enviado;
                                        item.Observacion = First.Observacion;
                                    }
                                }

                                var vrListImpresora = vrCollectionMSGR_PedidoDetalle.Select(x => new { x.ESGR_Producto.ESGR_ProductoSubCategoria.ESGR_ProductoCategoria.Impresora }).Distinct().ToList();
                                foreach (var item in vrListImpresora)
                                {
                                    var ListCollectionPedidoDetalle = vrCollectionMSGR_PedidoDetalle.Where(x => x.ESGR_Producto.ESGR_ProductoSubCategoria.ESGR_ProductoCategoria.Impresora == item.Impresora && !x.Enviado).ToList();
                                    var ListCollectionPedidoDelete = ListVMSGR_PedidoDetalleDelete.Where(x => x.ESGR_Producto.ESGR_ProductoSubCategoria.ESGR_ProductoCategoria.Impresora == item.Impresora).ToList();
                                    CrearTicket ticket = new CrearTicket();
                                    if (ESGR_Pedido.Opcion == "I")
                                        ticket.TextoCentro("== PEDIDO NUEVO ==");
                                    else
                                        ticket.TextoCentro("== PEDIDO MODIFICADO ==");
                                    ticket.TextoIzquierda(DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
                                    ticket.TextoExtremos("Mesa: " + ESGR_Pedido.Identificador, "Ticket #001-" + ESGR_Pedido.IdPedido.ToString("00000#"));
                                    ticket.TextoExtremos("Pers: " + ESGR_Pedido.Cubierto, SGRVariables.ESGR_Usuario.ESGR_Perfil.NombrePerfil + ": " + SGRVariables.ESGR_Usuario.Nombres);
                                    ticket.lineasIgual();
                                    foreach (var producto in vrCollectionMSGR_PedidoDetalle.Where(x => x.ESGR_Producto.ESGR_ProductoSubCategoria.ESGR_ProductoCategoria.Impresora == item.Impresora))
                                    {
                                        if ((ESGR_Pedido.Opcion == "I" && !producto.Enviado) || (ESGR_Pedido.Opcion != "I" && producto.Enviado))
                                        {
                                            ticket.TextoIzquierda(((int)producto.Cantidad).ToString("0#") + "  " + producto.ESGR_Producto.Producto);
                                            producto.Enviado = true;
                                            if (producto.Observacion != null && producto.Observacion.Trim().Length > 0)
                                                ticket.TextoComentario(producto.Observacion);
                                        }
                                    }
                                    if (ESGR_Pedido.Opcion != "I")
                                    {
                                        if (ListCollectionPedidoDetalle.Count > 0)
                                        {
                                            ticket.lineasGuion();
                                            ticket.TextoCentro("** MODIFICADO **");
                                            ticket.TextoCentro("-- " + DateTime.Now.ToShortTimeString() + " --");
                                            foreach (var productoModificado in ListCollectionPedidoDetalle)
                                            {
                                                var FirstProducto = CollectionMSGR_PedidoDetalle.FirstOrDefault(x => x.ESGR_Producto.IdProducto == productoModificado.ESGR_Producto.IdProducto);
                                                var Cant = productoModificado.Cantidad - ((FirstProducto == null) ? 0 : FirstProducto.CantidadAux);

                                                ticket.TextoIzquierda(((Cant > 0) ? "+" : "") + Cant.ToString("0#") + "  " + productoModificado.ESGR_Producto.Producto);
                                                productoModificado.Enviado = true;
                                                if (productoModificado.Observacion != null && productoModificado.Observacion.Trim().Length > 0)
                                                    ticket.TextoComentario(productoModificado.Observacion);
                                            }
                                        }
                                        if (ListCollectionPedidoDelete.Count() > 0)
                                        {
                                            ticket.lineasGuion();
                                            ticket.TextoCentro("** ANULADO **");
                                            ticket.TextoCentro("-- " + DateTime.Now.ToShortTimeString() + " --");
                                            foreach (var DetailDelete in ListVMSGR_PedidoDetalleDelete.Where(x => x.ESGR_Producto.ESGR_ProductoSubCategoria.ESGR_ProductoCategoria.Impresora == item.Impresora))
                                            {
                                                ticket.TextoIzquierda(DetailDelete.ESGR_Producto.Producto);
                                            }
                                        }
                                    }
                                    ticket.lineasIgual();
                                    if (ESGR_Pedido.Opcion == "I" || (ESGR_Pedido.Opcion != "I" && (ListCollectionPedidoDetalle.Count > 0 || ListCollectionPedidoDelete.Count > 0)))
                                        ticket.ImprimirTicket(item.Impresora);
                                }
                                #endregion
                                CmpMessageBox.Show(SGRMessage.TitlePedido, SGRMessageContent.ContentSaveOK, CmpButton.Aceptar, () => Volver("return"));   //Se devuelve texto para regresar al Login
                            }
                            catch (Exception ex)
                            {
                                CmpMessageBox.Show(SGRMessage.TitlePedido, ex.Message, CmpButton.Aceptar, () => Volver("return"));
                            }
                        });
                    });
            }
        }

        public ICommand ICancelarPedido
        {
            get
            {
                return CmpICommand.GetICommand((T) =>
                {
                    if (ESGR_Pedido.Opcion == "I")
                        MethodCleaningOrder();
                    else if (ESGR_Pedido.ESGR_Estado.CodEstado == "CTPED")
                    {
                        CmpMessageBox.Show(SGRMessage.TitlePedido, "No puede anular el pedido.", CmpButton.Aceptar);
                        return;
                    }
                    else
                    {
                        try
                        {
                            CmpMessageBox.Show(SGRMessage.TitlePedido, SGRMessageContent.ContentQuestionCancel, CmpButton.AceptarCancelar, () =>
                            {
                                try
                                {
                                    ESGR_Pedido.Opcion = "D";
                                    new BSGR_Pedido().TransPedido(ESGR_Pedido);
                                    #region ENVIO IMPRESION
                                    var vrCollectionMSGR_PedidoDetalle = new BSGR_PedidoDetalle().GetCollectionPedidoDetalle(ESGR_Pedido);
                                    var vrListImpresora = vrCollectionMSGR_PedidoDetalle.Select(x => new { x.ESGR_Producto.ESGR_ProductoSubCategoria.ESGR_ProductoCategoria.Impresora }).Distinct().ToList();
                                    vrListImpresora.ToList().ForEach(item =>
                                    {
                                        CrearTicket ticket = new CrearTicket();
                                        ticket.TextoCentro("== PEDIDO ANULADO ==");
                                        ticket.TextoIzquierda("");
                                        ticket.TextoExtremos("Mesa: " + ESGR_Pedido.Identificador, "Ticket #001-" + ESGR_Pedido.IdPedido.ToString("00000#"));
                                        ticket.TextoIzquierda(ESGR_Pedido.ESGR_Usuario.ESGR_Perfil.NombrePerfil + ": " + ESGR_Pedido.ESGR_Usuario.IdUsuario + " " + ESGR_Pedido.ESGR_Usuario.Nombres);
                                        ticket.TextoCentro("-- " + DateTime.Now.ToShortTimeString() + " --");
                                        ticket.TextoIzquierda("");
                                        ticket.ImprimirTicket(item.Impresora);
                                    });
                                    #endregion
                                    CmpMessageBox.Show(SGRMessage.TitlePedido, SGRMessageContent.ContentCanceledOK, CmpButton.Aceptar, () => Volver("return")); //Se devuelve texto para regresar al Login
                                }
                                catch (Exception)
                                {
                                    throw;
                                }
                            });
                        }
                        catch (Exception ex)
                        {
                            CmpMessageBox.Show(SGRMessage.TitlePedido, ex.Message, CmpButton.Aceptar, () => Volver("return"));
                        }
                    }
                });
            }
        }

        public ICommand ISelectMesas
        {
            get
            {
                return CmpICommand.GetICommand((T) =>
                {
                    var result = CmpModalDialog.GetContent().ShowDialog("MPSGR_MesasDisponibles", CollectionSelectESGR_Mesa);
                    if (result is CmpObservableCollection<ESGR_Mesa>)
                    {
                        var vrIdentificador = string.Empty;
                        CollectionSelectESGR_Mesa.Source = (CmpObservableCollection<ESGR_Mesa>)result;
                        CollectionSelectESGR_Mesa.ToList().ForEach(x => vrIdentificador += " " + x.Mesa + " /");
                        ESGR_Pedido.Identificador = CollectionSelectESGR_Mesa.FirstOrDefault().Identificador;
                    }
                });
            }
        }

        public ICommand IAddQuantity
        {
            get
            {
                return CmpICommand.GetICommand((T) =>
                {
                    VMSGR_PedidoDetalle vrVMSGR_PedidoDetalle = ((VMSGR_PedidoDetalle)T);
                    CollectionMSGR_PedidoDetalle.FirstOrDefault(x => x.ESGR_Producto.IdProducto == vrVMSGR_PedidoDetalle.ESGR_Producto.IdProducto).IAddQuantity.Execute(T);
                    MethodCalcularCuenta();
                });
            }
        }

        public ICommand ISubtractQuantity
        {
            get
            {
                return CmpICommand.GetICommand((T) =>
                {
                    VMSGR_PedidoDetalle vrVMSGR_PedidoDetalle = ((VMSGR_PedidoDetalle)T);
                    CollectionMSGR_PedidoDetalle.FirstOrDefault(x => x.ESGR_Producto.IdProducto == vrVMSGR_PedidoDetalle.ESGR_Producto.IdProducto).ISubtractQuantity.Execute(T);
                    MethodCalcularCuenta();
                });
            }
        }

        public ICommand IDeleteDetail
        {
            get
            {
                return CmpICommand.GetICommand((T) =>
                {
                    ListVMSGR_PedidoDetalleDelete.Add((VMSGR_PedidoDetalle)T);
                    CollectionMSGR_PedidoDetalle.Remove((VMSGR_PedidoDetalle)T);
                    MethodCalcularCuenta();
                });
            }
        }

        public ICommand IImprimirCuenta
        {
            get
            {
                return CmpICommand.GetICommand((T) =>
                {
                    string strMessage = string.Empty;

                    CmpMessageBox.Proccess(SGRMessage.TitlePedido, "Imprimiendo Cuenta, por favor espere...", () =>
                    {
                        try
                        {
                            ESGR_Pedido.Opcion = "C";
                            new BSGR_Pedido().TransPedido(ESGR_Pedido);

                            #region REALIZA IMPRESION

                            CrearTicket ticket = new CrearTicket();
                            ticket.TextoCentro(SGRVariables.ESGR_Usuario.ESGR_Empresa.RazonSocial);
                            ticket.TextoIzquierda("RUC:   " + SGRVariables.ESGR_Usuario.ESGR_Empresa.Ruc);
                            ticket.TextoIzquierda("DIREC: " + SGRVariables.ESGR_Usuario.ESGR_Empresa.DireccionFiscal);
                            ticket.TextoIzquierda("TELEF: " + SGRVariables.ESGR_Usuario.ESGR_Empresa.Telefono);
                            ticket.TextoIzquierda("");
                            ticket.lineasIgual();

                            ticket.TextoDerecha("Ticket #001-" + ESGR_Pedido.IdPedido.ToString("0000000#"));
                            ticket.TextoIzquierda(SGRVariables.ESGR_Usuario.ESGR_Perfil.NombrePerfil + ": " + SGRVariables.ESGR_Usuario.IdUsuario + " " + SGRVariables.ESGR_Usuario.Nombres);
                            ticket.TextoExtremos("FECHA: " + DateTime.Now.ToShortDateString(), "HORA: " + DateTime.Now.ToShortTimeString());
                            ticket.TextoExtremos("Mesa: " + ESGR_Pedido.Identificador, "Personas: " + ESGR_Pedido.Cubierto);
                            ticket.TextoIzquierda("");
                            ticket.lineasIgual();

                            //Articulos a vender.
                            ticket.EncabezadoVenta();//NOMBRE DEL ARTICULO, CANT, PRECIO, IMPORTE
                            ticket.lineasGuion();
                            CollectionMSGR_PedidoDetalle.ToList().ForEach(item => ticket.AgregaArticulo(item.ESGR_Producto.Producto, (int)item.Cantidad, (decimal)item.ESGR_Producto.Precio, ((int)item.Cantidad * (decimal)item.ESGR_Producto.Precio)));
                            ticket.lineasGuion();
                            decimal Total = CollectionMSGR_PedidoDetalle.Sum(x => ((int)x.Cantidad * (decimal)x.ESGR_Producto.Precio));
                            ticket.AgregarTotales("         TOTAL......S/", Total);
                            ticket.TextoIzquierda("");
                            ticket.TextoIzquierda("Razón Social:");
                            ticket.lineasSubGuion();
                            ticket.lineasSubGuion();
                            ticket.TextoIzquierda("RUC:");
                            ticket.lineasSubGuion();
                            ticket.TextoIzquierda("Direccion:");
                            ticket.lineasSubGuion();
                            ticket.lineasSubGuion();
                            ticket.TextoIzquierda("");
                            ticket.TextoCentro("¡GRACIAS POR SU PREFERENCIA!");
                            ticket.TextoIzquierda("ESTE NO ES UN COMPROBANTE FISCAL");
                            ticket.TextoIzquierda("CANJEAR POR BOLETA O FACTURA");
                            ticket.ImprimirTicket(SGRVariables.ImpresoraPedido);

                            #endregion

                            System.Threading.Thread.Sleep(500);
                            Volver(null);
                        }
                        catch (Exception ex)
                        {
                            strMessage = ex.Message;
                        }
                    },
                    () =>
                    {
                        if (strMessage.Trim().Length > 0)
                            CmpMessageBox.Show(SGRMessage.TitlePedido, strMessage, CmpButton.Aceptar);
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
                    Volver();
                });
            }
        }

        #endregion

        #region METODOS

        private async void MethodLoadDetails(ESGR_Pedido ESGR_Pedido)
        {
            await Task.Factory.StartNew(() =>
            {
                try
                {
                    var vrCollectionMSGR_PedidoDetalle = new BSGR_PedidoDetalle().GetCollectionPedidoDetalle(ESGR_Pedido);
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        CollectionMSGR_PedidoDetalle.Clear();
                        foreach (var item in vrCollectionMSGR_PedidoDetalle)
                        {
                            CollectionMSGR_PedidoDetalle.Add(new VMSGR_PedidoDetalle()
                            {
                                ESGR_Producto = item.ESGR_Producto,
                                PrecioDolar = (ESGR_TipoCambio != null) ? (item.ESGR_Producto.Precio / (decimal)ESGR_TipoCambio.SelRate) : 0,
                                Cantidad = item.Cantidad,
                                CantidadAux = item.Cantidad,
                                CantidadMesa = item.CantidadMesa,
                                CantidadLlevar = item.CantidadLlevar,
                                Enviado = true
                            });
                        }
                        MethodCalcularCuenta();
                    });
                }
                catch (Exception ex)
                {
                    CmpMessageBox.Show(SGRMessage.TitlePedido, ex.Message, CmpButton.Aceptar);
                }
            });
        }

        private async void MethodLoadCategoria()
        {
            await Task.Factory.StartNew(() =>
            {
                try
                {
                    PropertyMetroProgressBarCategoria = true;
                    CollectionESGR_ProductoCategoria.Source = new BSGR_ProductoCategoria().GetCollectionProductoCategoriaCartaDia();
                    Application.Current.Dispatcher.Invoke(() => { CollectionESGR_ProductoSubCategoria.Clear(); SelectedESGR_ProductoCategoria = CollectionESGR_ProductoCategoria.FirstOrDefault(); });
                    if (CollectionESGR_ProductoCategoria.Count == 0)
                        CmpMessageBox.Show(SGRMessage.TitlePedido, "No se ha registrado Carta del Día para hoy " + DateTime.Now.ToLongDateString(), CmpButton.Aceptar, () =>
                        {
                            Volver();
                        });
                    PropertyMetroProgressBarCategoria = false;
                }
                catch (Exception ex)
                {
                    CmpMessageBox.Show(SGRMessage.BusquedaMesaDisponible, ex.Message, CmpButton.Aceptar);
                }
            });
        }

        private async void MethodLoadSubCategoria(ESGR_ProductoCategoria ESGR_ProductoCategoria)
        {
            await Task.Factory.StartNew(() =>
            {
                try
                {
                    PropertyMetroProgressBarSubCategoria = true;
                    CollectionESGR_ProductoSubCategoria.Source = new BSGR_ProductoSubCategoria().GetCollectionProductoSubCategoriaCartaDia(ESGR_ProductoCategoria);
                    SelectedESGR_ProductoSubCategoria = CollectionESGR_ProductoSubCategoria.FirstOrDefault();
                    if (SelectedESGR_ProductoSubCategoria.SubCategoria == "NINGUNA")
                        PropertyVisibilitySubCategoria = Visibility.Collapsed;
                    else
                        PropertyVisibilitySubCategoria = Visibility.Visible;
                    PropertyMetroProgressBarSubCategoria = false;
                }
                catch (Exception ex)
                {
                    CmpMessageBox.Show(SGRMessage.TitlePedido, ex.Message, CmpButton.Aceptar);
                }
            });
        }

        private async void MethodLoadProducto(ESGR_ProductoSubCategoria ESGR_ProductoSubCategoria, string Filtro = "%")
        {
            await Task.Factory.StartNew(() =>
            {
                try
                {
                    if (ESGR_ProductoSubCategoria == null)
                    {
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            CollectionESGR_Producto.Clear();
                        });
                        return;
                    }
                    PropertyMetroProgressBarProducto = true;
                    CollectionESGR_Producto.Source = new BSGR_Producto().GetCollectionProductoCartaDia(ESGR_ProductoSubCategoria, Filtro);
                    PropertyMetroProgressBarProducto = false;
                }
                catch (Exception ex)
                {
                    CmpMessageBox.Show(SGRMessage.TitlePedido, ex.Message, CmpButton.Aceptar);
                }
            });
        }

        private async void MethodLoadPedidoTipo()
        {
            await Task.Factory.StartNew(() =>
            {
                try
                {
                    CollectionESGR_PedidoTipo.Source = new BSGR_PedidoTipo().GetCollectionPedidoTipo();
                    //Thread.Sleep(1000);
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        if(ESGR_Pedido.Opcion == "I")
                            SelectedESGR_PedidoTipo = CollectionESGR_PedidoTipo.FirstOrDefault();
                        else
                            SelectedESGR_PedidoTipo = CollectionESGR_PedidoTipo.FirstOrDefault(x => x.IdPedidoTipo == ESGR_Pedido.ESGR_PedidoTipo.IdPedidoTipo);
                    });
                }
                catch (Exception ex)
                {
                    CmpMessageBox.Show(SGRMessage.TitlePedido, ex.Message, CmpButton.Aceptar);
                }
            });
        }

        private async void MethodCleaningOrder()
        {
            await Task.Factory.StartNew(() =>
            {
                try
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        CollectionMSGR_PedidoDetalle.Clear();
                        SelectedESGR_ProductoSubCategoria = null;
                        CollectionESGR_Producto.Clear();
                        ListVMSGR_PedidoDetalleDelete.Clear();
                    });
                    Total = 0;
                    var Opcion = ESGR_Pedido.Opcion;
                    if (Opcion == "I")
                    {
                        var NombreMesa = ESGR_Pedido.Identificador;
                        var NumPersonas = ESGR_Pedido.Cubierto;
                        ESGR_Pedido = new BSGR_Pedido().GetPedido();
                        ESGR_Pedido.Opcion = Opcion;
                        ESGR_Pedido.Identificador = NombreMesa;
                        ESGR_Pedido.Cubierto = NumPersonas;
                        ESGR_Pedido.ESGR_PedidoTipo = SelectedESGR_PedidoTipo;
                    }
                }
                catch (Exception ex)
                {
                    CmpMessageBox.Show(SGRMessage.TitlePedido, ex.Message, CmpButton.Aceptar);
                }
            });
        }

        private string MethodDetallePedidoXML()
        {
            string vrCadena = string.Empty;
            vrCadena = "<ROOT>";
            CollectionMSGR_PedidoDetalle.ToList().ForEach((x) =>
            {
                vrCadena += "<Listar ";
                vrCadena += "xIdProducto = \'" + x.ESGR_Producto.IdProducto;
                vrCadena += "\' xCantidad = \'" + x.Cantidad;
                vrCadena += "\' xCantidadMesa = \'" + x.CantidadMesa;
                vrCadena += "\' xCantidadLlevar = \'" + x.CantidadLlevar;
                vrCadena += "\' xPrecio = \'" + x.ESGR_Producto.Precio;
                vrCadena += "\' xObservacion = \'" + x.Observacion;
                vrCadena += "\' xEnviado = \'" + "true";
                vrCadena += "\'></Listar>";
            });
            vrCadena += "</ROOT>";
            return vrCadena;
        }

        private string MethodDetalleMesaXML()
        {
            string vrCadena = string.Empty;
            vrCadena = "<ROOT>";
            CollectionSelectESGR_Mesa.ToList().ForEach((x) =>
            {
                vrCadena += "<Listar ";
                vrCadena += "xIdMesa = \'" + x.IdMesa;
                vrCadena += "\'></Listar>";
            });
            vrCadena += "</ROOT>";
            return vrCadena;
        }

        private string MethodCadenaMesa()
        {
            string vrCadena = string.Empty;
            CollectionSelectESGR_Mesa.ToList().ForEach((x) =>
            {
                vrCadena += "|" + x.IdMesa;
            });
            return vrCadena;
        }

        private void MethodLoadMesa(ESGR_Pedido ESGR_Pedido)
        {
            string[] ParametersList = ESGR_Pedido.CadenaMesa.Split("|".ToCharArray());
            CollectionSelectESGR_Mesa.Clear();
            for (int x = 1; x < ParametersList.Count(); x++)
                CollectionSelectESGR_Mesa.Add(new ESGR_Mesa() { IdMesa = Convert.ToInt16(ParametersList[x]) });
        }

        private void MethodAddProduct(ESGR_Producto ESGR_Producto)
        {
            bool MsjMostrado = false;
            if (ESGR_Producto == null) return;
            if (ESGR_Producto.ESGR_ProductoSubCategoria.ESGR_ProductoCategoria.ValidaStock && ESGR_Producto.Stock <= 5 && ESGR_Producto.Stock != 0)
            {
                CmpMessageBox.Show(SGRMessage.TitlePedido, "Solo quedan " + ESGR_Producto.Stock + " unidades de " + ESGR_Producto.Producto, CmpButton.Aceptar);
                MsjMostrado = true;
            }

            if (!CollectionMSGR_PedidoDetalle.ToList().Exists(x => x.ESGR_Producto.IdProducto == ESGR_Producto.IdProducto) && ((ESGR_Producto.ESGR_ProductoSubCategoria.ESGR_ProductoCategoria.ValidaStock && ESGR_Producto.Stock > 1) || !ESGR_Producto.ESGR_ProductoSubCategoria.ESGR_ProductoCategoria.ValidaStock))
            {
                var FirstDelete = ListVMSGR_PedidoDetalleDelete.FirstOrDefault(x => x.ESGR_Producto.IdProducto == ESGR_Producto.IdProducto);
                if (FirstDelete != null)
                {
                    FirstDelete.Cantidad = 1;
                    FirstDelete.Enviado = (FirstDelete.Cantidad == FirstDelete.CantidadAux);
                    CollectionMSGR_PedidoDetalle.Add(FirstDelete);
                    ListVMSGR_PedidoDetalleDelete.Remove(FirstDelete);
                }
                else
                    CollectionMSGR_PedidoDetalle.Add(new VMSGR_PedidoDetalle()
                    {
                        ESGR_Producto = ESGR_Producto,
                        PrecioDolar = (ESGR_TipoCambio != null) ? ESGR_Producto.Precio / (decimal)ESGR_TipoCambio.SelRate : 0,
                        MsjMostrado = MsjMostrado,
                        Cantidad = 1
                    });
                MethodCalcularCuenta();
            }
            else if (ESGR_Producto.ESGR_ProductoSubCategoria.ESGR_ProductoCategoria.ValidaStock && ESGR_Producto.Stock < 1)
                CmpMessageBox.Show(SGRMessage.TitlePedido, "Stock insuficiente", CmpButton.Aceptar);
        }

        private void MethodCalcularCuenta()
        {
            Total = Convert.ToDecimal(CollectionMSGR_PedidoDetalle.Sum(x => (x.Cantidad * x.ESGR_Producto.Precio)));
            TotalDolar = Convert.ToDecimal(CollectionMSGR_PedidoDetalle.Sum(x => (x.Cantidad * x.PrecioDolar)));
        }

        private void MethodLoadTipoCambio()
        {
            var vrESGR_TipoCambio = new BSGR_TipoCambio().GetCollectionTipoCambio();
            ESGR_TipoCambio = vrESGR_TipoCambio.FirstOrDefault(x => x.ESGR_Moneda.CodMoneda == "USD" && x.FechaTcb.ToString("ddMMyyyy") == DateTime.Now.ToString("ddMMyyyy"));
        }

        private void MethodChangedPedidoTipo(ESGR_PedidoTipo ESGR_PedidoTipo)
        {
            if (ESGR_PedidoTipo.IdPedidoTipo == 1)
            {
                if (PropertyVisibilityCant != Visibility.Visible)
                    PropertyVisibilityCant = Visibility.Visible;
                foreach (var item in CollectionMSGR_PedidoDetalle)
                {
                    item.CantidadMesa = (int)item.Cantidad;
                    item.CantidadLlevar = 0;
                }
            }
            else if(ESGR_PedidoTipo.IdPedidoTipo == 2)
            {
                PropertyVisibilityCant = Visibility.Collapsed;
                foreach (var item in CollectionMSGR_PedidoDetalle)
                {
                    item.CantidadLlevar = 0;
                    item.CantidadMesa = 0;
                }
            }
            else if (ESGR_PedidoTipo.IdPedidoTipo == 3)
            {
                if (PropertyVisibilityCant != Visibility.Visible)
                    PropertyVisibilityCant = Visibility.Visible;
                foreach (var item in CollectionMSGR_PedidoDetalle)
                {
                    item.CantidadLlevar = (int)item.Cantidad;
                    item.CantidadMesa = 0;
                }
            }
        }

        private bool MethodValidarDatos()
        {
            if (CollectionMSGR_PedidoDetalle == null || CollectionMSGR_PedidoDetalle.Count == 0)
            {
                CmpMessageBox.Show(SGRMessage.TitlePedido, "Ingrese un Detalle para el Pedido.", CmpButton.Aceptar);
                return true;
            }else if(CollectionSelectESGR_Mesa == null || CollectionSelectESGR_Mesa.Count == 0)
            {
                CmpMessageBox.Show(SGRMessage.TitlePedido, "Seleccione al menos una Mesa.", CmpButton.Aceptar);
                return true;
            }
            else if (ESGR_Pedido.ESGR_PedidoTipo == null || ESGR_Pedido.ESGR_PedidoTipo.IdPedidoTipo == 0)
            {
                CmpMessageBox.Show(SGRMessage.TitlePedido, "Seleccione el Tipo de Pedido.", CmpButton.Aceptar);
                return true;
            }
            else if (ESGR_Pedido.Identificador == null || ESGR_Pedido.Identificador.Trim().Length == 0)
            {
                CmpMessageBox.Show(SGRMessage.TitlePedido, "Ingrese el Número de Mesa.", CmpButton.Aceptar);
                return true;
            }
            //else if (ESGR_Pedido.Cubierto == 0)
            //{
            //    CmpMessageBox.Show(SGRMessage.TitlePedido, "Ingrese el Número de Cubiertos.", CmpButton.Aceptar);
            //    return true;
            //}
            return false;
        }

        #endregion
    }
}