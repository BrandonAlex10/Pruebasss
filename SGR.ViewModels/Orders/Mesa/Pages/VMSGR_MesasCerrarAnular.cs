/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
 * ********************************************************
 * 
'* FCH. CREACIÓN : 01/03/2017 [OSCAR M. HUAMÁN CABRERA]
 * 
 * ********************************************************
 * se tiene que trabajar con CmpObservableCollection pero no carga la lista con nuevo FrameWork*
**********************************************************/

namespace SGR.ViewModels.Orders.Mesa.Pages
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
    using System.Windows;
    using System.Windows.Input;

    public class VMSGR_MesasCerrarAnular : CmpNavigationService, CmpIModalDialog
    {
        private ESGR_Mesa _ESGR_Mesa;
        public ESGR_Mesa ESGR_Mesa
        {
            get
            {
                return _ESGR_Mesa;
            }
            set
            {
                _ESGR_Mesa = value;
                OnPropertyChanged("ESGR_Mesa");
            }
        }

        public object Parameter
        {
            set
            {
                Application.Current.Dispatcher.Invoke(async() =>
                {
                    var result = await MTSGR_GetPrivilege.Process(this.GetType(), SGRMessage.TitleSGR, SGRMessageContent.GetAccesoRestringido);
                    if (result == null)
                        CmpModalDialog.GetContent().Close(null);
                    else if (value is ESGR_Mesa)
                    {
                        if (CollectionESGR_Mesa.Count > 0)
                            CollectionESGR_Mesa.Clear();
                        ESGR_Mesa = ((ESGR_Mesa)value);
                        PropertyIsCheckedAllTables = !(ESGR_Mesa.Opcion == "C");
                        CmpLoading = new CmpLoading(MethodLoadArea, MethodLoadMesa);
                        CmpLoading.Exceptions = ((e) => { CmpMessageBox.Show(SGRMessage.TitleSGR, e.Message, CmpButton.Aceptar); });
                        CmpLoading.LoadHeader();
                    }
                });
            }
        }

        #region COLECCIONES

        private CmpObservableCollection<ESGR_Mesa> _CollectionESGR_Mesa;
        public CmpObservableCollection<ESGR_Mesa> CollectionESGR_Mesa
        {
            get
            {
                if (_CollectionESGR_Mesa == null)
                    _CollectionESGR_Mesa = new CmpObservableCollection<ESGR_Mesa>();
                return _CollectionESGR_Mesa;
            }
        }

        private CmpObservableCollection<ESGR_MesaArea> _CollectionESGR_MesaArea;
        public CmpObservableCollection<ESGR_MesaArea> CollectionESGR_MesaArea
        {
            get
            {
                if (_CollectionESGR_MesaArea == null)
                    _CollectionESGR_MesaArea = new CmpObservableCollection<ESGR_MesaArea>();
                return _CollectionESGR_MesaArea;
            }
        }

        #endregion

        #region OBJECTOS SECUNDARIOS

        private CmpLoading CmpLoading { get; set; }

        private ESGR_Mesa _SelectESGR_Mesa;
        public ESGR_Mesa SelectESGR_Mesa
        {
            get
            {
                return _SelectESGR_Mesa;
            }
            set
            {
                _SelectESGR_Mesa = value;
                OnPropertyChanged("SelectESGR_Mesa");
            }
        }

        private ESGR_MesaArea _SelectedESGR_MesaArea;
        public ESGR_MesaArea SelectedESGR_MesaArea
        {
            get
            {
                return _SelectedESGR_MesaArea;
            }
            set
            {
                _SelectedESGR_MesaArea = value;
                CmpLoading.LoadDetail();
                OnPropertyChanged("SelectedESGR_MesaArea");
            }
        }

        private ESGR_Pedido _ESGR_Pedido;
        public ESGR_Pedido ESGR_Pedido
        {
            get
            {
                return _ESGR_Pedido;
            }
            set
            {
                _ESGR_Pedido = value;
                OnPropertyChanged("ESGR_Pedido");
            }
        }

        private string _Observacion;
        public string Observacion
        {
            get
            {
                if (_Observacion == null)
                    _Observacion = string.Empty;
                return _Observacion;
            }
            set
            {
                _Observacion = value;
                OnPropertyChanged("Observacion");
            }
        }

        #endregion

        #region PROPERTY

        private bool _PropertyMetroProgressBar;
        public bool PropertyMetroProgressBar
        {
            get 
            {
                return _PropertyMetroProgressBar;
            }
            set 
            {
                _PropertyMetroProgressBar = value;
                OnPropertyChanged("PropertyMetroProgressBar");
            }
        }

        private bool _PropertyIsCheckedAllTables;
        public bool PropertyIsCheckedAllTables
        {
            get
            {
                return _PropertyIsCheckedAllTables;
            }
            set
            {
                _PropertyIsCheckedAllTables = value;
                if (CmpLoading != null)
                    CmpLoading.LoadDetail();
                OnPropertyChanged("PropertyIsCheckedAllTables");
            }
        }

        private Visibility _PropertyVisibilityCheckBoxAllTables;
        public Visibility PropertyVisibilityCheckBoxAllTables
        {
            get
            {
                return _PropertyVisibilityCheckBoxAllTables;
            }
            set
            {
                _PropertyVisibilityCheckBoxAllTables = value;
                OnPropertyChanged("PropertyVisibilityCheckBoxAllTables");
            }
        }

        private Visibility _PropertyVisibilityAbrir;
        public Visibility PropertyVisibilityAbrir
        {
            get
            {
                return _PropertyVisibilityAbrir;
            }
            set
            {
                _PropertyVisibilityAbrir = value;
                OnPropertyChanged("PropertyVisibilityAbrir");
            }
        }

        private Visibility _PropertyVisibilityCerrar;
        public Visibility PropertyVisibilityCerrar
        {
            get
            {
                return _PropertyVisibilityCerrar;
            }
            set
            {
                _PropertyVisibilityCerrar = value;
                OnPropertyChanged("PropertyVisibilityCerrar");
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
                PropertyVisibilityObservacion = value;
                OnPropertyChanged("PropertyVisibilityAnular");
            }
        }

        private Visibility _PropertyVisibilityObservacion;
        public Visibility PropertyVisibilityObservacion
        {
            get
            {
                return _PropertyVisibilityObservacion;
            }
            set
            {
                _PropertyVisibilityObservacion = value;
                OnPropertyChanged("PropertyVisibilityObservacion");
            }
        }

        #endregion

        #region ICOMMAND

        public ICommand ICerrar
        {
            get
            {
                return CmpICommand.GetICommand((T) =>
                {
                    if (SelectESGR_Mesa == null)
                    {
                        CmpMessageBox.Show(SGRMessage.BusquedaMesaAtendido, SGRMessageContent.ContentSelecedtNull + "Imprimir el Pedido", CmpButton.Aceptar);
                        return;
                    }

                    string strMessage = string.Empty;
                    CmpMessageBox.Proccess(SGRMessage.TitlePedido, "Imprimiendo Cuenta, por favor espere...", () =>
                    {
                        try
                        {
                            MethodLoadPedido(SelectESGR_Mesa);
                            ESGR_Pedido.Opcion = "C";
                            new BSGR_Pedido().TransPedido(ESGR_Pedido);
                        }
                        catch (Exception ex)
                        {
                            strMessage = ex.Message;
                        }
                    },
                    () =>
                    {
                        if (strMessage.Trim().Length > 0)
                        {
                            CmpMessageBox.Show(SGRMessage.TitlePedido, strMessage, CmpButton.Aceptar, () => CmpModalDialog.GetContent().Close("return") );
                            return;
                        }
                        else
                        {
                            #region REALIZA IMPRESION
                            try
                            {
                                var CollectionMSGR_PedidoDetalle = new BSGR_PedidoDetalle().GetCollectionPedidoDetalle(ESGR_Pedido);
                                CrearTicket ticket = new CrearTicket();
                                ticket.TextoCentro(SGRVariables.ESGR_Usuario.ESGR_Empresa.RazonSocial);
                                ticket.TextoIzquierda("RUC:   " + SGRVariables.ESGR_Usuario.ESGR_Empresa.Ruc);
                                ticket.TextoIzquierda("DIREC: " + SGRVariables.ESGR_Usuario.ESGR_Empresa.DireccionFiscal);
                                ticket.TextoIzquierda("TELEF: " + SGRVariables.ESGR_Usuario.ESGR_Empresa.Telefono);
                                ticket.TextoIzquierda("");
                                ticket.lineasIgual();
                                ticket.TextoIzquierda("");
                                ticket.TextoDerecha("Ticket #001-" + ESGR_Pedido.IdPedido.ToString("00000#"));
                                ticket.TextoIzquierda(ESGR_Pedido.ESGR_Usuario.ESGR_Perfil.NombrePerfil + ": " + ESGR_Pedido.ESGR_Usuario.IdUsuario + " " + ESGR_Pedido.ESGR_Usuario.Nombres);
                                ticket.TextoExtremos("Fecha: " + DateTime.Now.ToShortDateString(), "Hora: " + DateTime.Now.ToShortTimeString());
                                ticket.TextoExtremos("Mesa: " + ESGR_Pedido.Identificador, "Personas: " + ESGR_Pedido.Cubierto);
                                ticket.TextoIzquierda("");
                                ticket.lineasIgual();
                                ticket.TextoIzquierda("");
                                //Articulos a vender.
                                ticket.EncabezadoVenta();//NOMBRE DEL ARTICULO, CANT, PRECIO, IMPORTE
                                ticket.lineasGuion();
                                foreach (var item in CollectionMSGR_PedidoDetalle)
                                    ticket.AgregaArticulo(item.ESGR_Producto.Producto, (int)item.Cantidad, (decimal)item.ESGR_Producto.Precio, ((int)item.Cantidad * (decimal)item.ESGR_Producto.Precio));
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
                                }

                                if (SGRVariables.ImpresoraCopiaPedido != null || SGRVariables.ImpresoraCopiaPedido.Trim().Length != 0)
                                    ticket.ImprimirTicketCopia(SGRVariables.ImpresoraCopiaPedido);

                                ticket.ImprimirTicket(SGRVariables.ImpresoraPedido);
                            }
                            catch (Exception ex)
                            {
                                CmpMessageBox.Show(SGRMessage.TitlePedido, ex.Message, CmpButton.Aceptar);
                            }
                            #endregion
                            Application.Current.Dispatcher.Invoke(() => CmpModalDialog.GetContent().Close("return"));//texto para retornar al Login
                        }
                    });
                });
            }
        }

        public ICommand IAnular
        {
            get
            {
                return CmpICommand.GetICommand((T) =>
                {
                    if (SelectESGR_Mesa == null)
                    {
                        CmpMessageBox.Show(SGRMessage.BusquedaMesaAtendido, SGRMessageContent.ContentSelecedtNull + "Anular el Pedido", CmpButton.Aceptar);
                        return;
                    }
                    else if (Observacion.Trim().Length < 10)
                    {
                        CmpMessageBox.Show(SGRMessage.BusquedaMesaAtendido, "Ingrese 10 caracteres en la Observación para proceder a Anular", CmpButton.Aceptar);
                        return;
                    }

                    try
                    {
                        MethodLoadPedido(SelectESGR_Mesa);
                        var CollectionMSGR_PedidoDetalle = new BSGR_PedidoDetalle().GetCollectionPedidoDetalle(ESGR_Pedido);
                        CmpMessageBox.Show(SGRMessage.TitlePedido, SGRMessageContent.ContentQuestionCancel, CmpButton.AceptarCancelar, () =>
                        {
                            try
                            {
                                ESGR_Pedido.Opcion = "D";
                                new BSGR_Pedido().TransPedido(ESGR_Pedido);

                                #region ENVIO IMPRESION
                                var vrListImpresora = CollectionMSGR_PedidoDetalle.Select(x => new { x.ESGR_Producto.ESGR_ProductoSubCategoria.ESGR_ProductoCategoria.Impresora }).Distinct().ToList();
                                foreach (var item in vrListImpresora)
                                {
                                    CrearTicket ticket = new CrearTicket();
                                    ticket.TextoCentro("== PEDIDO ANULADO ==");
                                    ticket.TextoExtremos("Mesa: " + ESGR_Pedido.Identificador, "Ticket #001-" + ESGR_Pedido.IdPedido.ToString("00000#"));
                                    ticket.TextoIzquierda(ESGR_Pedido.ESGR_Usuario.ESGR_Perfil.NombrePerfil + ": " + ESGR_Pedido.ESGR_Usuario.Nombres);
                                    ticket.TextoCentro("-- " + DateTime.Now.ToShortTimeString() + " --");
                                    ticket.ImprimirTicket(item.Impresora);
                                }
                                #endregion

                                CmpMessageBox.Show(SGRMessage.TitlePedido, SGRMessageContent.ContentCanceledOK, CmpButton.Aceptar, () =>
                                {
                                    Application.Current.Dispatcher.Invoke(() => CmpModalDialog.GetContent().Close("return"));   //texto para retornar al Login
                                });
                            }
                            catch (Exception)
                            {
                                throw;
                            }
                        });
                    }
                    catch (Exception ex)
                    {
                        CmpMessageBox.Show(SGRMessage.TitlePedido, ex.Message, CmpButton.Aceptar, () => CmpModalDialog.GetContent().Close("return")); //texto para retornar al Login
                    }
                });
            }
        }

        public ICommand IAbrir
        {
            get
            {
                return CmpICommand.GetICommand((T) =>
                {
                    if (SelectESGR_Mesa == null)
                    {
                        CmpMessageBox.Show(SGRMessage.BusquedaMesaAtendido, SGRMessageContent.ContentSelecedtNull + "Abrir el Pedido", CmpButton.Aceptar);
                        return;
                    }
                    else if (Observacion.Trim().Length < 10)
                    {
                        CmpMessageBox.Show(SGRMessage.BusquedaMesaAtendido, "Ingrese 10 caracteres en la Observación para proceder a Anular", CmpButton.Aceptar);
                        return;
                    }

                    SelectESGR_Mesa.Opcion = Observacion;
                    CmpModalDialog.GetContent().Close(SelectESGR_Mesa);
                });
            }
        }

        public ICommand IVolver
        {
            get
            {
                return CmpICommand.GetICommand((s) =>
                {
                    CmpModalDialog.GetContent().Close(null);
                });
            }
        }

        #endregion

        #region METODOS

        private void MethodLoadPedido(ESGR_Mesa ESGR_Mesa)
        {
            try
            {
                ESGR_Pedido = new BSGR_Pedido().GetCollectionPedido().ToList().FirstOrDefault(x => x.IdPedido == ESGR_Mesa.IdPedido);
                if (ESGR_Pedido == null)
                    throw new Exception("Pedido no Encontrado");
                ESGR_Pedido.Observacion = Observacion;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void MethodLoadArea()
        {
            PropertyVisibilityCheckBoxAllTables = ESGR_Mesa.Opcion == "C" ? Visibility.Visible : Visibility.Collapsed;
            PropertyVisibilityCerrar = ESGR_Mesa.Opcion == "C" ? Visibility.Visible : Visibility.Collapsed;
            PropertyVisibilityAnular = ESGR_Mesa.Opcion == "A" ? Visibility.Visible : Visibility.Collapsed;
            PropertyVisibilityAbrir = ESGR_Mesa.Opcion == "A" ? Visibility.Visible : Visibility.Collapsed;

            CollectionESGR_MesaArea.Source = new BSGR_MesaArea().GetCollectionMesaArea();
            SelectedESGR_MesaArea = CollectionESGR_MesaArea.FirstOrDefault();
        }

        private void MethodLoadMesa()
        {
            PropertyMetroProgressBar = true;

            var vrCollectionESGR_Mesa = new BSGR_Mesa().GetCollectionMesa(SelectedESGR_MesaArea,SGRVariables.ESGR_Usuario.IdUsuario).Where(x => x.ESGR_Estado.CodEstado == "ATPED" || x.ESGR_Estado.CodEstado == "CTPED").GroupBy(x => x.Identificador).Select(y => y.First());

            if (ESGR_Mesa.Opcion == "C")
            {
                if (!PropertyIsCheckedAllTables)
                    CollectionESGR_Mesa.Source = new CmpObservableCollection<ESGR_Mesa>(vrCollectionESGR_Mesa.Where(x => x.ESGR_Estado.CodEstado == "ATPED").ToList());
                else
                    CollectionESGR_Mesa.Source = new CmpObservableCollection<ESGR_Mesa>(vrCollectionESGR_Mesa);
            }
            else if(ESGR_Mesa.Opcion == "A")
                CollectionESGR_Mesa.Source = new CmpObservableCollection<ESGR_Mesa>(vrCollectionESGR_Mesa.Where(x => x.ESGR_Estado.CodEstado == "ATPED").ToList());

            PropertyMetroProgressBar = false;
        }

        #endregion
    }
}