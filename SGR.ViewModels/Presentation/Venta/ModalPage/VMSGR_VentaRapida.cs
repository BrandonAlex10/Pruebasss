using ComputerSystems;
using ComputerSystems.WPF;
using ComputerSystems.WPF.Acciones.Controles.ModalDialog;
using ComputerSystems.WPF.Interfaces;
using SGR.Models.Business;
using SGR.Models.Entity;
using SGR.ViewModels.Method;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SGR.ViewModels.Presentation.Venta.ModalPage
{
    public class VMSGR_VentaRapida : CmpNotifyPropertyChanged, CmpIModalDialog
    {
        public object Parameter
        {
            set
            {
                Application.Current.Dispatcher.Invoke(async() =>
                {
                    var result = await MTSGR_GetPrivilege.Process(this.GetType(), SGRMessage.TitleMessage, SGRMessageContent.GetAccesoRestringido);
                    if (result == null)
                        CmpModalDialog.GetContent().Close();
                    else if (value is CmpObservableCollection<ESGR_PedidoDetalle>)
                    {
                        CollectionESGR_PedidoDetalle.Source = (CmpObservableCollection<ESGR_PedidoDetalle>)value;
                        MethodLoadTipoCambio();
                        PropertyVisibilityTotalDolar = (ESGR_TipoCambio != null) ? Visibility.Visible : Visibility.Collapsed;
                        MethodLoadProductoCategoria();
                    }
                });
            }
        }

        #region OBJETOS SECUNDARIOS

        private ESGR_TipoCambio ESGR_TipoCambio { get; set; }

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
                    MethodLoadProductoSubCategoria(value);
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
                if (value != null)
                    MethodLoadProductos(value);
                OnPropertyChanged("SelectedESGR_ProductoSubCategoria");
            }
        }

        private ESGR_Producto _SelectedESGR_Producto;
        public ESGR_Producto SelectedESGR_Producto
        {
            get
            {
                return _SelectedESGR_Producto;
            }
            set
            {
                _SelectedESGR_Producto = value;
                OnPropertyChanged("SelectedESGR_Producto");
            }
        }

        private ESGR_PedidoDetalle _SelectedESGR_PedidoDetalle;
        public ESGR_PedidoDetalle SelectedESGR_PedidoDetalle
        {
            get
            {
                return _SelectedESGR_PedidoDetalle;
            }
            set
            {
                _SelectedESGR_PedidoDetalle = value;
                OnPropertyChanged("SelectedESGR_PedidoDetalle");
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

        private string _Observacion;
        public string Observacion
        {
            get
            {
                return _Observacion;
            }
            set
            {
                _Observacion = value;
                OnPropertyChanged("Observacion");
            }
        }

        #endregion

        #region COLLECIONES

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

        private CmpObservableCollection<ESGR_Producto> _CollectionESGR_Producto;
        public CmpObservableCollection<ESGR_Producto> CollectionESGR_Producto
        {
            get
            {
                if (_CollectionESGR_Producto == null)
                    _CollectionESGR_Producto = new CmpObservableCollection<ESGR_Producto>();
                return _CollectionESGR_Producto;
            }
            set
            {
                _CollectionESGR_Producto = value;
                OnPropertyChanged("CollectionESGR_Producto");
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

        #endregion

        #region PROPERTYS

        private Visibility _PropertyVisibilityTotalDolar;
        public Visibility PropertyVisibilityTotalDolar
        {
            get
            {
                return _PropertyVisibilityTotalDolar;
            }
            set
            {
                _PropertyVisibilityTotalDolar = value;
                OnPropertyChanged("PropertyVisibilityTotalDolar");
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

        #endregion

        #region ICOMMAND

        public ICommand IAgregar
        {
            get
            {
                return CmpICommand.GetICommand((T) =>
                {
                    if (CollectionESGR_PedidoDetalle.Count > 0 && CollectionESGR_PedidoDetalle.ToList().Exists(x => x.ESGR_Producto.IdProducto == SelectedESGR_Producto.IdProducto))
                        return;

                    CollectionESGR_PedidoDetalle.Add(new ESGR_PedidoDetalle()
                    {
                        ESGR_Producto = SelectedESGR_Producto
                    });
                });
            }
        }

        public ICommand IQuitar
        {
            get
            {
                return CmpICommand.GetICommand((T) =>
                {
                    CollectionESGR_PedidoDetalle.Remove(SelectedESGR_PedidoDetalle);
                    MethodCalcular();
                });
            }
        }

        public ICommand IUpdateCantidad
        {
            get
            {
                return CmpICommand.GetICommand((T) =>
                {
                    int Cantidad = 0;
                    try { Cantidad = Convert.ToInt32(T); }
                    catch (Exception) { SelectedESGR_PedidoDetalle.Cantidad = 0; }

                    if (Cantidad >= SelectedESGR_PedidoDetalle.ESGR_Producto.Stock && SelectedESGR_PedidoDetalle.ESGR_Producto.ESGR_ProductoSubCategoria.ESGR_ProductoCategoria.ValidaStock)
                    {
                        CmpMessageBox.Show(SGRMessage.AdministracionCaja, "Ha superado el límite de Stock", CmpButton.Aceptar, () =>
                        {
                            if (SelectedESGR_PedidoDetalle.ESGR_Producto.ESGR_ProductoSubCategoria.ESGR_ProductoCategoria.ValidaStock)
                                SelectedESGR_PedidoDetalle.Cantidad = 0;
                            MethodCalcular();
                        });
                        return;
                    }

                    MethodCalcular();
                });
            }
        }

        public ICommand IGuardar
        {
            get
            {
                return CmpICommand.GetICommand((T) =>
                {
                    if (MethodValidaDatos())
                        return;
                    CollectionESGR_PedidoDetalle.ToList().ForEach(x => { x.ESGR_Producto.Imagen = null; x.ESGR_Producto.ImageSource = null; });
                    object[] vrObject = new object[2];
                    vrObject[0] = CollectionESGR_PedidoDetalle;
                    vrObject[1] = Observacion;
                    CmpModalDialog.GetContent().Close(vrObject);
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

        private async void MethodLoadProductoCategoria()
        {
            await Task.Factory.StartNew(() =>
            {
                try
                {
                    CollectionESGR_ProductoCategoria.Source = new BSGR_ProductoCategoria().GetCollectionProductoCategoriaCartaDia();
                    SelectedESGR_ProductoCategoria = CollectionESGR_ProductoCategoria.FirstOrDefault();
                }
                catch (Exception ex)
                {
                    CmpMessageBox.Show(SGRMessage.AdministratorVenta, ex.Message, CmpButton.Aceptar);
                }
            });
        }

        private async void MethodLoadProductoSubCategoria(ESGR_ProductoCategoria ESGR_ProductoCategoria)
        {
            await Task.Factory.StartNew(() =>
            {
                try
                {
                    CollectionESGR_ProductoSubCategoria.Source = new BSGR_ProductoSubCategoria().GetCollectionProductoSubCategoriaCartaDia(ESGR_ProductoCategoria);
                    SelectedESGR_ProductoSubCategoria = CollectionESGR_ProductoSubCategoria.FirstOrDefault();
                    if (CollectionESGR_ProductoSubCategoria.Count == 1 && SelectedESGR_ProductoSubCategoria.SubCategoria == "NINGUNA")
                        PropertyVisibilitySubCategoria = Visibility.Collapsed;
                    else
                        PropertyVisibilitySubCategoria = Visibility.Visible;
                }
                catch (Exception ex)
                {
                    CmpMessageBox.Show(SGRMessage.AdministratorVenta, ex.Message, CmpButton.Aceptar);
                }
            });
        }

        private async void MethodLoadProductos(ESGR_ProductoSubCategoria ESGR_ProductoSubCategoria)
        {
            await Task.Factory.StartNew(() =>
            {
                try
                {
                    CollectionESGR_Producto.Source = new BSGR_Producto().GetCollectionProductoCartaDia(ESGR_ProductoSubCategoria);
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
                ESGR_TipoCambio = new BSGR_TipoCambio().GetCollectionTipoCambio().FirstOrDefault(x => x.ESGR_Moneda.CodMoneda == "USD" && x.FechaTcb.ToString("ddMMyyyy") == DateTime.Now.ToString("ddMMyyyy"));
            });
        }

        private bool MethodValidaDatos()
        {
            if(CollectionESGR_PedidoDetalle.Count == 0)
            {
                CmpMessageBox.Show(SGRMessage.AdministratorVenta, "El pedido no puede estar vacío", CmpButton.Aceptar);
                return true;
            }
            else if (CollectionESGR_PedidoDetalle.ToList().Exists(x => x.Cantidad == 0))
            {
                CmpMessageBox.Show(SGRMessage.AdministratorVenta, "No puede existir Producto con Cantidad 0", CmpButton.Aceptar);
                return true;
            }
            return false;
        }

        private void MethodCalcular()
        {
            Total = CollectionESGR_PedidoDetalle.Sum(x => x.ESGR_Producto.Precio * x.Cantidad);
            TotalDolar = (ESGR_TipoCambio != null) ? TotalDolar / (decimal)ESGR_TipoCambio.SelRate : 0;
        }

        #endregion
    }
}
