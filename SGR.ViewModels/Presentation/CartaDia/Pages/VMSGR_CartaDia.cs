/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   ABEL QUISPE ORELLANA
'* FCH. CREACIÓN : 30/12/2016
**********************************************************
'* MODIFICADO POR : CRISTIAN A. HERNANDEZ VILLO
 * MOTIVO MODIFICA: SE DIO FUNCIONALIDAD A LOS BOTONES, GRILLAS, ETC
'* FCH. CREACIÓN  : 02/03/2017
**********************************************************/
namespace SGR.ViewModels.Presentation.CartaDia.Controls
{
    using ComputerSystems;
    using ComputerSystems.Loading;
    using ComputerSystems.WPF;
    using ComputerSystems.WPF.Interfaces;
    using ComputerSystems.WPF.Notificaciones;
    using SGR.Models.Business;
    using SGR.Models.Entity;
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Input;

    public class VMSGR_CartaDia : CmpNavigationService, CmpINavigation
    {
        private ESGR_CartaDia _ESGR_CartaDia;
        public ESGR_CartaDia ESGR_CartaDia
        {
            get
            {
                if (_ESGR_CartaDia == null)
                    _ESGR_CartaDia = new ESGR_CartaDia();
                return _ESGR_CartaDia;
            }
            set
            {
                _ESGR_CartaDia = value;
                OnPropertyChanged("ESGR_CartaDia");
            }
        }

        private CmpLoading CmpLoading { get; set; }

        public object Parameter
        {
            set
            {
                if (value is ESGR_CartaDia)
                {
                    ESGR_CartaDia = (ESGR_CartaDia)value;
                    if (ESGR_CartaDia.Opcion == "U")
                    {
                        MethodLoadDetails();
                        PropertyVisibility = true;
                    }
                    else
                        PropertyVisibility = false;
                    MethodLoadCategoria();
                    CollectionVMSGR_CartaDiaDetalleCategoria.Clear();
                    CollectionESGR_Producto.Clear();
                }
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
        
        private CmpObservableCollection<VMSGR_CartaDiaDetalleCategoria> _CollectionVMSGR_CartaDiaDetalleCategoria;
        public CmpObservableCollection<VMSGR_CartaDiaDetalleCategoria> CollectionVMSGR_CartaDiaDetalleCategoria
        {
            get 
            {
                if (_CollectionVMSGR_CartaDiaDetalleCategoria == null)
                    _CollectionVMSGR_CartaDiaDetalleCategoria = new CmpObservableCollection<VMSGR_CartaDiaDetalleCategoria>();
                return _CollectionVMSGR_CartaDiaDetalleCategoria;
            }
        }

        private CmpObservableCollection<ESGR_CartaDiaDetalle> _CollectionESGR_CartaDiaDetalle;
        public CmpObservableCollection<ESGR_CartaDiaDetalle> CollectionESGR_CartaDiaDetalle
        {
            get
            {
                if (_CollectionESGR_CartaDiaDetalle == null)
                    _CollectionESGR_CartaDiaDetalle = new CmpObservableCollection<ESGR_CartaDiaDetalle>();
                return _CollectionESGR_CartaDiaDetalle;
            }
        }

        #endregion

        #region OBJETOS SECUNDARIOS

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
                if (value != null)
                    SelectedESGR_Producto.IsCheck = ((SelectedESGR_Producto.IsCheck) ? false : true);
                _SelectedESGR_Producto = null;
                OnPropertyChanged("SelectedESGR_Producto");
            }
        }

        private ESGR_Producto _SelectESGR_Producto;
        public ESGR_Producto SelectESGR_Producto
        {
            get
            {
                return _SelectESGR_Producto;
            }
            set
            {
                _SelectESGR_Producto = value;
                OnPropertyChanged("SelectESGR_Producto");
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
                    MethodLoadSubCategoria(value);
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
                    MethodLoadProducto(value);
                OnPropertyChanged("SelectedESGR_ProductoSubCategoria");
            }
        }

        private DateTime _SelectFecha_CartaDia;
        public DateTime SelectFecha_CartaDia
        {
            get
            {
                return _SelectFecha_CartaDia;
            }
            set
            {
                _SelectFecha_CartaDia = value;
                OnPropertyChanged("SelectFecha_CartaDia");
            }
        }

        private ESGR_CartaDiaDetalle _SelectedEGSR_CartaDiaDetalle;
        public ESGR_CartaDiaDetalle SelectedESGR_CartaDiaDetalle
        {
            get
            {
                if (_SelectedEGSR_CartaDiaDetalle == null)
                    _SelectedEGSR_CartaDiaDetalle = new ESGR_CartaDiaDetalle();
                return _SelectedEGSR_CartaDiaDetalle;
            }
            set
            {
                _SelectedEGSR_CartaDiaDetalle = value;
                OnPropertyChanged("SelectedESGR_CartaDiaDetalle");
            }
        }

        #endregion

        #region ICOMMAND

        public ICommand IAniadir
        {
            get
            {
                return CmpICommand.GetICommand((T) =>
                {
                    var nCount = MethodContador();
                    foreach (var item in CollectionESGR_Producto)
                    {
                        if (item.IsCheck)
                        {
                            nCount++;
                            item.IsCheckeQuitar = false;
                            MethodAddDetails(new ESGR_CartaDiaDetalle() { ESGR_Producto = item, Item = nCount, Precio = item.Precio, Stock = 0, Cantidad = 0 });
                            
                        }
                    }
                       
                });
            }
        }

        public ICommand IAniadirTodo
        {
            get
            {
                return CmpICommand.GetICommand((T) =>
                {
                    var conta = MethodContador();
                    foreach (var item in CollectionESGR_Producto)
                    {
                        conta++;
                        item.IsCheckeQuitar = false;
                        MethodAddDetails(new ESGR_CartaDiaDetalle() { ESGR_Producto = item, Item = conta, Precio = item.Precio, Stock = 0, Cantidad = 0 });
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
                    MethodQuitar();
                });
            }
        }

        public ICommand IQuitarTodo
        {
            get
            {
                return CmpICommand.GetICommand((T) =>
                {
                    if (CollectionVMSGR_CartaDiaDetalleCategoria == null || CollectionVMSGR_CartaDiaDetalleCategoria.Count == 0) return;
                    if (ESGR_CartaDia.Opcion == "U")
                        MethodQuitar();
                    else
                        CollectionVMSGR_CartaDiaDetalleCategoria.Clear();
                });
            }
        }

        public ICommand IVolver
        {
            get
            {
                return CmpICommand.GetICommand((T) =>
                {
                    new CmpNavigationService().Volver();
                });
            }
        }

        public ICommand IGuardar
        {
            get
            {
                return CmpICommand.GetICommand((T) =>
                {
                    CmpMessageBox.Proccess(SGRMessage.AdministratorCartaDia,SGRMessageContent.ProcesandoDatos, ()=>
                    {
                        try
                        {
                            if (MethodValidaDatos()) return;
                            ESGR_CartaDia.Fecha = DateTime.Now;
                            ESGR_CartaDia.ESGR_EmpresaSucursal.IdEmpSucursal = 1;
                            ESGR_CartaDia.DetalleXML = DetalleXML();
                            new BSGR_CartaDia().TransCartaDia(ESGR_CartaDia);
                            CmpMessageBox.Show(SGRMessage.AdministratorCartaDia, SGRMessageContent.ContentSaveOK, CmpButton.Aceptar, () =>
                            {
                                Volver();
                            });
                        }
                        catch (Exception ex)
                        {
                            CmpMessageBox.Show(SGRMessage.AdministratorCartaDia, ex.Message, CmpButton.Aceptar);
                        }
                    },null);
                });
            }
        }

        public ICommand IUpdateStock
        {
            get 
            {
                return CmpICommand.GetICommand(async(T) =>
                {
                    await Task.Factory.StartNew(() =>
                    {
                        int CantPedido = 0;
                        if (ESGR_CartaDia.Opcion == "U")
                        {
                            if (SelectedESGR_CartaDiaDetalle.Stock > 0)
                            {
                                var vrProducto = SelectedESGR_CartaDiaDetalle;
                                CantPedido = ((vrProducto.TempCantidad > vrProducto.Cantidad) ? Convert.ToInt32((SelectedESGR_CartaDiaDetalle.Cantidad - (vrProducto.TempCantidad - vrProducto.TempStock))) : Convert.ToInt32((vrProducto.TempStock + (vrProducto.Cantidad - vrProducto.TempCantidad))));
                                if (CantPedido > 0)
                                    SelectedESGR_CartaDiaDetalle.Stock = CantPedido;
                                else
                                {
                                    SelectedESGR_CartaDiaDetalle.Cantidad = vrProducto.TempCantidad;
                                    SelectedESGR_CartaDiaDetalle.Stock = vrProducto.TempStock;
                                }
                            }
                            else
                                SelectedESGR_CartaDiaDetalle.Stock = SelectedESGR_CartaDiaDetalle.Cantidad;
                        }
                    });
                });
            }
        }

        #endregion

        #region PROPERTY

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

        private bool _PropertyEnableddActualizar;
        public bool PropertyEnableddActualizar
        {
            get 
            {
                return _PropertyEnableddActualizar;
            }
            set
            {
                _PropertyEnableddActualizar = value;
                OnPropertyChanged("PropertyEnableddActualizar");
            }
        }

        private string _PropertyFlitroText;
        public string PropertyFlitroText
        {
            get
            {
                return _PropertyFlitroText;
            }
            set
            {
                _PropertyFlitroText = value;
                if (value != null && value.Trim().Length > 0)
                    MethodLoadProducto(SelectedESGR_ProductoSubCategoria);
                OnPropertyChanged("PropertyFlitroText");
            }
        }

        private bool _PropertyVisibility;
        public bool PropertyVisibility
        {
            get
            {
                return _PropertyVisibility;
            }
            set
            {
                _PropertyVisibility = value;
                OnPropertyChanged("PropertyVisibility");
            }
        }

        private bool _PropertyVisibilitySubCategoria;
        public bool PropertyVisibilitySubCategoria
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

        #region METODOS

        private void MethodAddDetails(ESGR_CartaDiaDetalle ESGR_CartaDiaDetalle)
        {
            if (ESGR_CartaDiaDetalle == null) return;
            if (ESGR_CartaDiaDetalle.ESGR_Producto == null) return;
            if (ESGR_CartaDiaDetalle.ESGR_Producto.ESGR_ProductoSubCategoria == null) return;
            if (ESGR_CartaDiaDetalle.ESGR_Producto.ESGR_ProductoSubCategoria.ESGR_ProductoCategoria == null) return;

            var NameCategoria = ESGR_CartaDiaDetalle.ESGR_Producto.ESGR_ProductoSubCategoria.ESGR_ProductoCategoria.Categoria;
            var NameSubCategoria = ESGR_CartaDiaDetalle.ESGR_Producto.ESGR_ProductoSubCategoria.SubCategoria;

            //===================NO EXISTE CATEGORÍA============================//
            var IsExistCategoria = CollectionVMSGR_CartaDiaDetalleCategoria.ToList().Exists(x => x.Categoria == NameCategoria);
            if (!IsExistCategoria)
            {
                CollectionVMSGR_CartaDiaDetalleCategoria.Add(new VMSGR_CartaDiaDetalleCategoria()
                {
                    Categoria = NameCategoria,
                    CollectionVMSGR_CartaDiaDetalleSubCategoria = new ObservableCollection<VMSGR_CartaDiaDetalleSubCategoria>() 
                    {
                        new VMSGR_CartaDiaDetalleSubCategoria()
                        {
                            SubCategoria = NameSubCategoria,
                            CollectionESGR_CartaDiaDetalle = new ObservableCollection<ESGR_CartaDiaDetalle>()
                            {
                                ESGR_CartaDiaDetalle
                            }
                        }
                    }
                });
            }
            else
            {
                //===================SI CATEGORÍA============================//
                var FirstCategoria = CollectionVMSGR_CartaDiaDetalleCategoria.First(x => x.Categoria == NameCategoria);

                //===================NO EXISTE SUB-CATEGORÍA============================//
                var IsExistSubCategoria = FirstCategoria.CollectionVMSGR_CartaDiaDetalleSubCategoria.ToList().Exists(x => x.SubCategoria == NameSubCategoria);
                if (!IsExistSubCategoria)
                {
                    FirstCategoria.CollectionVMSGR_CartaDiaDetalleSubCategoria.Add(new VMSGR_CartaDiaDetalleSubCategoria()
                    {
                        SubCategoria = NameSubCategoria,
                        CollectionESGR_CartaDiaDetalle = new ObservableCollection<ESGR_CartaDiaDetalle>()
                        {
                            ESGR_CartaDiaDetalle
                        }
                    });
                }
                else
                {
                    //===================SI SUB-CATEGORÍA============================//
                    var FirstSubCategoria = FirstCategoria.CollectionVMSGR_CartaDiaDetalleSubCategoria.First(x => x.SubCategoria == NameSubCategoria);

                    //===================NO EXISTE ESGR_CartaDiaDetalle============================//
                    var IsExistProducto = FirstSubCategoria.CollectionESGR_CartaDiaDetalle.ToList().Exists(x => x.ESGR_Producto.IdProducto == ESGR_CartaDiaDetalle.ESGR_Producto.IdProducto);
                    if (!IsExistProducto)
                    {
                        FirstSubCategoria.CollectionESGR_CartaDiaDetalle.Add(ESGR_CartaDiaDetalle);
                    }
                }
            }
        }

        private async void MethodLoadCategoria()
        {
            await Task.Factory.StartNew(() =>
            {
                try
                {
                    PropertyMetroProgressBarCategoria = true;
                    PropertyFlitroText = string.Empty;
                    CollectionESGR_ProductoCategoria.Source = new BSGR_ProductoCategoria().GetCollectionProductoCategoria();
                    PropertyMetroProgressBarCategoria = false;
                    if (ESGR_CartaDia.Opcion == "I")
                    {
                        PropertyEnableddActualizar = true;
                        SelectFecha_CartaDia = DateTime.Now;
                    }
                    else
                    {
                        PropertyEnableddActualizar = false;
                        SelectFecha_CartaDia = Convert.ToDateTime(ESGR_CartaDia.Fecha);
                    }
                }
                catch (Exception ex)
                {
                    CmpMessageBox.Show(SGRMessage.AdministratorCartaDia, ex.Message, CmpButton.Aceptar);
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
                    CollectionESGR_ProductoSubCategoria.Source = new BSGR_ProductoSubCategoria().GetCollectionProductoSubCategoria(ESGR_ProductoCategoria);
                    PropertyMetroProgressBarSubCategoria = false;
                }
                catch (Exception ex)
                {
                    CmpMessageBox.Show(SGRMessage.AdministratorCartaDia, ex.Message, CmpButton.Aceptar);
                }
            });
        }

        private async void MethodLoadProducto(ESGR_ProductoSubCategoria ESGR_ProductoSubCategoria)
        {
            await Task.Factory.StartNew(() =>
            {
                try
                {
                    PropertyMetroProgressBarProducto = true;
                    if(PropertyFlitroText.Trim().Length > 0)
                        CollectionESGR_Producto.Source = new BSGR_Producto().GetCollectionProducto(ESGR_ProductoSubCategoria = null, PropertyFlitroText, "PRODUCTO");
                    else
                        CollectionESGR_Producto.Source = new BSGR_Producto().GetCollectionProducto(ESGR_ProductoSubCategoria);
                    PropertyMetroProgressBarProducto = false;
                }
                catch (Exception ex)
                {
                    CmpMessageBox.Show(SGRMessage.AdministratorCartaDia, ex.Message, CmpButton.Aceptar);
                }
            });
        }

        private async void MethodLoadDetails()
        {
            await Task.Factory.StartNew(() =>
            {
                CollectionESGR_CartaDiaDetalle.Source = new CmpObservableCollection<ESGR_CartaDiaDetalle>(new BSGR_CartaDiaDetalle().GetCollectionCartaDiaDetalle(ESGR_CartaDia));
                Application.Current.Dispatcher.Invoke(() =>
                {
                    CollectionESGR_CartaDiaDetalle.ToList().ForEach(x =>
                    {
                        MethodAddDetails(x);
                    });
                });

            });
        }

        private string DetalleXML()
        {
            string vrCadena = string.Empty;
            vrCadena = "<ROOT>";
            CollectionVMSGR_CartaDiaDetalleCategoria.ToList().ForEach(x =>
            {
                x.CollectionVMSGR_CartaDiaDetalleSubCategoria.ToList().ForEach(r =>
                {
                    r.CollectionESGR_CartaDiaDetalle.ToList().ForEach(f =>
                    {
                        vrCadena += "<Listar";
                        vrCadena += " xIdCartaDia = \'" + f.IdCartaDia;
                        vrCadena += "\' xItem = \'" + f.Item;
                        vrCadena += "\' xIdProducto = \'" + f.ESGR_Producto.IdProducto;
                        vrCadena += "\' xCantidad  = \'" + f.Cantidad;
                        vrCadena += "\' xPrecio = \'" + f.Precio;
                        vrCadena += "\' xStock   = \'" + f.Stock;
                        vrCadena += "\' xObservacion = \'" + f.Observacion;
                        vrCadena += "\'></Listar>";
                    });
                });
            });
            vrCadena += "</ROOT>";
            return vrCadena;
        }

        private void MethodQuitar()
        {
            var vrCollectionESGR_CartiaDiaDetalle = new CmpObservableCollection<ESGR_CartaDiaDetalle>();
            var vrCollectionESGR_VMSubCategoria = new CmpObservableCollection<VMSGR_CartaDiaDetalleSubCategoria>(); 
            var vrCollectionESGR_VMCategoria = new CmpObservableCollection<VMSGR_CartaDiaDetalleCategoria>();
            foreach (var item in CollectionVMSGR_CartaDiaDetalleCategoria)
            {
                foreach (var items in item.CollectionVMSGR_CartaDiaDetalleSubCategoria)
                {
                    vrCollectionESGR_CartiaDiaDetalle = new CmpObservableCollection<ESGR_CartaDiaDetalle>(items.CollectionESGR_CartaDiaDetalle);
                    foreach (ESGR_CartaDiaDetalle vrItems in vrCollectionESGR_CartiaDiaDetalle)
                    {
                        if (vrItems.Stock < vrItems.Cantidad && vrItems.ESGR_Producto.IsCheckeQuitar && ESGR_CartaDia.Opcion=="U")
                        {
                            CmpMessageBox.Show(SGRMessage.AdministratorCartaDia, "No se puede quitar algunos Producto debido a que ya existen Pedidos", CmpButton.Aceptar);
                            vrItems.ESGR_Producto.IsCheckeQuitar = false;
                        }
                        else
                        {
                            if (vrItems.ESGR_Producto.IsCheckeQuitar)
                                items.CollectionESGR_CartaDiaDetalle.Remove(vrItems);
                        }
                    }
                }
            }

            #region  QUITA CATEGORIA Y SUBCATEGORIA

            vrCollectionESGR_VMCategoria = new CmpObservableCollection<VMSGR_CartaDiaDetalleCategoria>(CollectionVMSGR_CartaDiaDetalleCategoria);
            foreach (var item in vrCollectionESGR_VMCategoria)
            {
                vrCollectionESGR_VMSubCategoria = new CmpObservableCollection<VMSGR_CartaDiaDetalleSubCategoria>(item.CollectionVMSGR_CartaDiaDetalleSubCategoria);
                foreach (VMSGR_CartaDiaDetalleSubCategoria items in vrCollectionESGR_VMSubCategoria)
                {
                    if (items.CollectionESGR_CartaDiaDetalle.Count() == 0)
                        item.CollectionVMSGR_CartaDiaDetalleSubCategoria.Remove(items);
                }

                if(item.CollectionVMSGR_CartaDiaDetalleSubCategoria.Count()==0)
                    CollectionVMSGR_CartaDiaDetalleCategoria.Remove(item);
            }
            MethodContador();
            #endregion
        }

        private bool MethodValidaDatos()
        {
            var  vrValidaDatos = false;
            if (CollectionVMSGR_CartaDiaDetalleCategoria == null || CollectionVMSGR_CartaDiaDetalleCategoria.Count == 0) vrValidaDatos = true;

            foreach (var item in CollectionVMSGR_CartaDiaDetalleCategoria)
            {
                foreach (var items in item.CollectionVMSGR_CartaDiaDetalleSubCategoria)
                {
                    if (items.CollectionESGR_CartaDiaDetalle == null || items.CollectionESGR_CartaDiaDetalle.Count == 0)
                    {
                        CmpMessageBox.Show(SGRMessage.AdministratorCartaDia, "Ingrese Producto para continuar con el Proceso", CmpButton.Aceptar);
                        vrValidaDatos = true;
                        break;
                    }
                    else
                    {
                        foreach(var vritems in items.CollectionESGR_CartaDiaDetalle)
                        {
                            if (Convert.ToBoolean(vritems.ESGR_Producto.ESGR_ProductoSubCategoria.ESGR_ProductoCategoria.ValidaStock))
                            {
                                CmpMessageBox.Show(SGRMessage.AdministratorCartaDia,"Dato Obligatorio: Cantidad",CmpButton.Aceptar);
                                vrValidaDatos = true;
                                break;
                            }
                            if (Convert.ToDecimal(vritems.Precio) == 0)
                            {
                                CmpMessageBox.Show(SGRMessage.AdministratorCartaDia, "Dato Obligatorio: Precio", CmpButton.Aceptar);
                                vrValidaDatos = true;
                                break;
                            }
                        }
                    }
                }
            }
            return vrValidaDatos;
        }

        private int MethodContador()
        {
            var nCount = 0;
            foreach (var item in CollectionVMSGR_CartaDiaDetalleCategoria)
            {
                foreach (var vritem in item.CollectionVMSGR_CartaDiaDetalleSubCategoria)
                {
                    foreach (var items in vritem.CollectionESGR_CartaDiaDetalle)
                    {
                        nCount++;
                        items.Item = nCount;
                    }
                }
            }
            return nCount;
        }

        #endregion
    }
}
