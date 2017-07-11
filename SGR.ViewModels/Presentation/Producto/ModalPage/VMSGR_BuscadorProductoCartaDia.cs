using ComputerSystems;
using ComputerSystems.Loading;
using ComputerSystems.WPF;
using ComputerSystems.WPF.Acciones.Controles.ModalDialog;
using ComputerSystems.WPF.Interfaces;
using ComputerSystems.WPF.Notificaciones;
using SGR.Models.Business;
using SGR.Models.Entity;
using SGR.ViewModels.Method;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SGR.ViewModels.Presentation.Producto.ModalPage
{
    public class VMSGR_BuscadorProductoCartaDia : CmpNavigationService, CmpIModalDialog
    {
        public object Parameter
        {
            set
            {
                Application.Current.Dispatcher.Invoke(async() =>
                {
                    var result = await MTSGR_GetPrivilege.Process(this.GetType(), SGRMessage.TitleMessage, SGRMessageContent.GetAccesoRestringido);
                    if(result == null)
                        CmpModalDialog.GetContent().Close();
                    else if (value is CmpObservableCollection<ESGR_PedidoDetalle>)
                    {
                        CollectionESGR_Detalle.Source = new CmpObservableCollection<ESGR_PedidoDetalle>((ObservableCollection<ESGR_PedidoDetalle>)value);
                        MethodLoadCategoria();
                    }
                });
            }
        }

        #region COLECCIONES

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

        private CmpObservableCollection<ESGR_PedidoDetalle> _CollectionESGR_Detalle;
        public CmpObservableCollection<ESGR_PedidoDetalle> CollectionESGR_Detalle
        {
            get
            {
                if (_CollectionESGR_Detalle == null)
                    _CollectionESGR_Detalle = new CmpObservableCollection<ESGR_PedidoDetalle>();
                return _CollectionESGR_Detalle;
            }
        }

        #endregion

        #region OBJ SECUNDARIOS

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
                    MethodLoadSubCategoría(value);
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
                    MethodLoadDetails(value);
                OnPropertyChanged("SelectedESGR_ProductoSubCategoria");
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

        #endregion

        #region PROPERTY

        

        #endregion

        #region ICOMMAND

        public ICommand IAgregar
        {
            get
            {
                return CmpICommand.GetICommand((T) =>
                {
                    SelectedESGR_PedidoDetalle.Cantidad = SelectedESGR_PedidoDetalle.CantidadAux;
                    SelectedESGR_PedidoDetalle.Importe = (SelectedESGR_PedidoDetalle.Cantidad * SelectedESGR_PedidoDetalle.ESGR_Producto.Precio);
                    CmpModalDialog.GetContent().Close(SelectedESGR_PedidoDetalle);
                });
            }
        }

        public ICommand ISalir
        {
            get
            {
                return CmpICommand.GetICommand((T) =>
                {
                    CmpModalDialog.GetContent().Close(CollectionESGR_Detalle);
                });
            }
        }

        #endregion

        #region METODOS

        private async void MethodLoadCategoria()
        {
            await Task.Factory.StartNew(() =>
            {
                CollectionESGR_ProductoCategoria.Source = new BSGR_ProductoCategoria().GetCollectionProductoCategoriaCartaDia();
                SelectedESGR_ProductoCategoria = CollectionESGR_ProductoCategoria.FirstOrDefault();
            });
        }

        private async void MethodLoadSubCategoría(ESGR_ProductoCategoria ESGR_ProductoCategoria)
        {
            await Task.Factory.StartNew(() =>
            {
                CollectionESGR_ProductoSubCategoria.Source = new BSGR_ProductoSubCategoria().GetCollectionProductoSubCategoriaCartaDia(ESGR_ProductoCategoria);
                SelectedESGR_ProductoSubCategoria = CollectionESGR_ProductoSubCategoria.FirstOrDefault();
            });
        }

        private async void MethodLoadDetails(ESGR_ProductoSubCategoria ESGR_ProductoSubCategoria)
        {
            await Task.Factory.StartNew(() =>
            {
                CollectionESGR_Producto.Source = new BSGR_Producto().GetCollectionProductoCartaDia(ESGR_ProductoSubCategoria);
                Application.Current.Dispatcher.Invoke(() =>
                {
                    CollectionESGR_PedidoDetalle.Clear();
                    CollectionESGR_Producto.ToList().ForEach(x =>
                    {
                        var vrDetalle = CollectionESGR_Detalle.FirstOrDefault(y => y.ESGR_Producto.IdProducto == x.IdProducto);
                        if (vrDetalle != null)
                            CollectionESGR_PedidoDetalle.Add(vrDetalle);
                        else
                            CollectionESGR_PedidoDetalle.Add(new ESGR_PedidoDetalle() { ESGR_Producto = x, Cantidad = 0, CantidadAux = 0, Enviado = false });
                    });
                });
            });
        }

        #endregion
    }
}
