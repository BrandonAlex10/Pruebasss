/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   ABEL QUISPE ORELLANA
'* FCH. CREACIÓN : 03/12/2016
**********************************************************/
namespace SGR.ViewModels.Presentation.CartaDia.Controls
{
    using ComputerSystems;
    using ComputerSystems.WPF.Acciones.Controles.ModalDialog;
    using ComputerSystems.WPF.Interfaces;
    using ComputerSystems.WPF.Acciones.Controles.Menus;
    using ComputerSystems.WPF.Notificaciones;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media;
    using SGR.Models.Entity;
    using ComputerSystems.WPF;
    using SGR.Models.Business;
    using ComputerSystems.Loading;
    using SGR.Reports;
    using SGR.ViewModels.Method;

    public class VMSGR_ListadoCartaDia : CmpNavigationService, CmpINavigation
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
                        CmpLoading = new CmpLoading(MethodLoadDetails, MethodLoadCartaDia);
                        CmpLoading.Exceptions = ((e) => { CmpMessageBox.Show(SGRMessage.AdministratorDocumento, e.Message, CmpButton.Aceptar); });
                        CmpLoading.LoadHeader();
                    }
                });
                
            }
        }

        private CmpLoading CmpLoading { get; set; }

        #region OBJ SECUNDARIOS

        private DateTime _SelectedEGSR_Fecha;
        public DateTime SelectedESGR_Fecha
        {
            get
            {
                return _SelectedEGSR_Fecha;
            }
            set
            {
                _SelectedEGSR_Fecha = value;
                if (value != null)
                    MethodLoadFechaCartaDia();
                OnPropertyChanged("SelectedESGR_Fecha");
            }
        }

        #endregion

        #region COLECCIONES

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

        private CmpObservableCollection<ESGR_CartaDiaDetalle> _CollectionESGR_CartaDiaDetalleTemp;
        public CmpObservableCollection<ESGR_CartaDiaDetalle> CollectionESGR_CartaDiaDetalleTemp
        {
            get
            {
                if (_CollectionESGR_CartaDiaDetalleTemp == null)
                    _CollectionESGR_CartaDiaDetalleTemp = new CmpObservableCollection<ESGR_CartaDiaDetalle>();
                return _CollectionESGR_CartaDiaDetalleTemp;
            }
        }

        private CmpObservableCollection<ESGR_CartaDia> _CollectionESGR_CartaDia;
        public CmpObservableCollection<ESGR_CartaDia> CollectionESGR_CartaDia
        {
            get
            {
                if (_CollectionESGR_CartaDia == null)
                    _CollectionESGR_CartaDia = new CmpObservableCollection<ESGR_CartaDia>();
                return _CollectionESGR_CartaDia;
            }
        }

        #endregion

        #region PROPERTY

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

        #endregion

        #region ICOMMAND

        public ICommand IGuardar
        {
            get
            {
                return CmpICommand.GetICommand((T) =>
                {
                    CmpMessageBox.Proccess(SGRMessage.AdministratorCartaDia, SGRMessageContent.ProcesandoDatos, () =>
                    {
                        try
                        {
                            if (MethodValidaDatos()) return;
                            ESGR_CartaDia.Fecha = DateTime.Now;
                            ESGR_CartaDia.Opcion = "I";
                            ESGR_CartaDia.ESGR_EmpresaSucursal.IdEmpSucursal = 1;
                            ESGR_CartaDia.DetalleXML = DetalleXML();
                            new BSGR_CartaDia().TransCartaDia(ESGR_CartaDia);
                            CmpMessageBox.Show(SGRMessage.AdministratorCartaDia, SGRMessageContent.DatosProcesados, CmpButton.Aceptar);
                        }
                        catch (Exception ex)
                        {
                            CmpMessageBox.Show(SGRMessage.AdministratorCartaDia, ex.Message, CmpButton.Aceptar);
                        }
                    }, null);
                });
            }
        }

        public ICommand ISalir
        {
            get
            {
                return CmpICommand.GetICommand((S) =>
                {
                    Volver();
                });
            }
        }

        #endregion

        #region METODOS

        private void MethodLoadDetails()
        {
            try
            {
                CollectionESGR_Producto.Source = new CmpObservableCollection<ESGR_Producto>(new BSGR_Producto().GetCollectionProducto(new ESGR_ProductoSubCategoria(), "%", "PRODUCTO"));
                CollectionESGR_Producto.ToList().ForEach(x => MethodAddDetails(new ESGR_CartaDiaDetalle() { ESGR_Producto = x, Precio = Convert.ToDecimal(x.Precio) }));
            }
            catch (Exception ex)
            {
                CmpMessageBox.Show(SGRMessage.AdministratorCartaDia, ex.Message, CmpButton.Aceptar);
            }
        }

        private void MethodAddDetails(ESGR_CartaDiaDetalle ESGR_CartaDiaDetalle)
        {
            try
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
                    Application.Current.Dispatcher.Invoke(() =>
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
                        })
                    );
                }
                else
                {
                    //===================SI CATEGORÍA============================//
                    var FirstCategoria = CollectionVMSGR_CartaDiaDetalleCategoria.First(x => x.Categoria == NameCategoria);

                    //===================NO EXISTE SUB-CATEGORÍA============================//
                    var IsExistSubCategoria = FirstCategoria.CollectionVMSGR_CartaDiaDetalleSubCategoria.ToList().Exists(x => x.SubCategoria == NameSubCategoria);
                    if (!IsExistSubCategoria)
                    {
                        Application.Current.Dispatcher.Invoke(() =>
                            FirstCategoria.CollectionVMSGR_CartaDiaDetalleSubCategoria.Add(new VMSGR_CartaDiaDetalleSubCategoria()
                            {
                                SubCategoria = NameSubCategoria,
                                CollectionESGR_CartaDiaDetalle = new ObservableCollection<ESGR_CartaDiaDetalle>()
                                {
                                    ESGR_CartaDiaDetalle
                                }
                            })
                        );
                    }
                    else
                    {
                        //===================SI SUB-CATEGORÍA============================//
                        var FirstSubCategoria = FirstCategoria.CollectionVMSGR_CartaDiaDetalleSubCategoria.First(x => x.SubCategoria == NameSubCategoria);

                        //===================NO EXISTE ESGR_CartaDiaDetalle============================//
                        var IsExistProducto = FirstSubCategoria.CollectionESGR_CartaDiaDetalle.ToList().Exists(x => x.ESGR_Producto.IdProducto == ESGR_CartaDiaDetalle.ESGR_Producto.IdProducto);
                        if (!IsExistProducto)
                        {
                            Application.Current.Dispatcher.Invoke(() => FirstSubCategoria.CollectionESGR_CartaDiaDetalle.Add(ESGR_CartaDiaDetalle));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                CmpMessageBox.Show(SGRMessage.AdministratorCartaDia, ex.Message, CmpButton.Aceptar);
            }
        }

        private async void MethodLoadCartaDiaDetalle()
        {
            await Task.Factory.StartNew(() =>
             {
                 try
                 {
                     CollectionESGR_CartaDiaDetalle.Source = new BSGR_CartaDiaDetalle().GetCollectionCartaDiaDetalle(ESGR_CartaDia);
                     if (CollectionESGR_CartaDiaDetalle.Count > 0)
                     {
                         CollectionESGR_CartaDiaDetalle.ToList().ForEach(x => { MethodCompare(x); });
                     }
                     else
                         MethodCompare(new ESGR_CartaDiaDetalle());
                 }
                 catch (Exception ex)
                 {
                     CmpMessageBox.Show(SGRMessage.AdministratorCartaDia, ex.Message, CmpButton.Aceptar);
                 }
             });
        }

        private bool MethodValidaDatos()
        {
            var vrValidaDatos = false;
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
                        foreach (var vritems in items.CollectionESGR_CartaDiaDetalle)
                        {
                            if ((vritems.Cantidad == 0) && Convert.ToBoolean(vritems.ESGR_Producto.ESGR_ProductoSubCategoria.ESGR_ProductoCategoria.ValidaStock) && vritems.CartaDia)
                            {
                                CmpMessageBox.Show(SGRMessage.AdministratorCartaDia, "Dato Obligatorio: Cantidad", CmpButton.Aceptar);
                                vrValidaDatos = true;
                                break;
                            }
                            if (vritems.Precio == 0 && vritems.CartaDia)
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

        private async void MethodCompare(ESGR_CartaDiaDetalle ESGR_CartaDiaDetalle)
        {
            await Task.Factory.StartNew(() =>
            {
                try
                {
                    CollectionVMSGR_CartaDiaDetalleCategoria.ToList().ForEach(x =>
                    {
                        x.CollectionVMSGR_CartaDiaDetalleSubCategoria.ToList().ForEach(Z =>
                        {
                            Z.CollectionESGR_CartaDiaDetalle.ToList().ForEach(s =>
                            {
                                if (CollectionESGR_CartaDiaDetalle.Count() > 0)
                                {
                                    CollectionESGR_CartaDiaDetalle.ToList().ForEach(d =>
                                    {
                                        var Firts = Z.CollectionESGR_CartaDiaDetalle.ToList().Exists(r => r.ESGR_Producto.IdProducto == ESGR_CartaDiaDetalle.ESGR_Producto.IdProducto);
                                        if (Firts)
                                        {
                                            var TempProducto = Z.CollectionESGR_CartaDiaDetalle.FirstOrDefault(f => f.ESGR_Producto.IdProducto == ESGR_CartaDiaDetalle.ESGR_Producto.IdProducto);
                                            if (TempProducto == null) return;
                                            TempProducto.Precio = ESGR_CartaDiaDetalle.Precio;
                                            TempProducto.Observacion = ESGR_CartaDiaDetalle.Observacion;
                                            TempProducto.Cantidad = ESGR_CartaDiaDetalle.Cantidad;
                                            TempProducto.CartaDia = true;
                                        }
                                    });
                                }
                                else
                                    s.CartaDia = false;
                            });
                        });
                    });
                }
                catch (Exception ex)
                {
                    CmpMessageBox.Show(SGRMessage.AdministratorCartaDia, ex.Message, CmpButton.Aceptar);
                }
            });
        }

        private string DetalleXML()
        {
            string vrCadena = string.Empty;
            vrCadena = "<ROOT>";
            var Cont = 0;
            CollectionVMSGR_CartaDiaDetalleCategoria.ToList().ForEach(x =>
            {
                x.CollectionVMSGR_CartaDiaDetalleSubCategoria.ToList().ForEach(r =>
                {
                    r.CollectionESGR_CartaDiaDetalle.ToList().ForEach(f =>
                    {
                        if (f.CartaDia)
                        {
                            Cont++;
                            vrCadena += "<Listar";
                            vrCadena += " xIdCartaDia = \'" + f.IdCartaDia;
                            vrCadena += "\' xItem = \'" + Cont;
                            vrCadena += "\' xIdProducto = \'" + f.ESGR_Producto.IdProducto;
                            vrCadena += "\' xCantidad  = \'" + f.Cantidad;
                            vrCadena += "\' xPrecio = \'" + f.Precio;
                            vrCadena += "\' xStock   = \'" + f.Stock;
                            vrCadena += "\' xObservacion = \'" + f.Observacion;
                            vrCadena += "\'></Listar>";
                        }
                    });
                });
            });
            vrCadena += "</ROOT>";
            return vrCadena;
        }

        private CmpObservableCollection<ESGR_CartaDiaDetalle> MethodCartaDia()
        {
            var CollectionESGR_Carta = new CmpObservableCollection<ESGR_CartaDiaDetalle>();

            CollectionVMSGR_CartaDiaDetalleCategoria.ToList().ForEach(x =>
            {
                x.CollectionVMSGR_CartaDiaDetalleSubCategoria.ToList().ForEach(z =>
                {
                    z.CollectionESGR_CartaDiaDetalle.ToList().ForEach(s =>
                    {
                        if (s.CartaDia)
                        {
                            CollectionESGR_Carta.Add(new ESGR_CartaDiaDetalle()
                            {
                                Cantidad = s.Cantidad,
                                Precio = s.Precio,
                                Stock = s.Stock,
                                ESGR_Producto = s.ESGR_Producto,
                                Producto = s.ESGR_Producto.Producto,
                                SubCategoria = s.ESGR_Producto.ESGR_ProductoSubCategoria.SubCategoria,
                                Categoria = s.ESGR_Producto.ESGR_ProductoSubCategoria.ESGR_ProductoCategoria.Categoria
                            });
                        }
                    });
                });
            });
            return CollectionESGR_Carta;
        }

        private void MethodLoadCartaDia()
        {
            try
            {
                CollectionESGR_CartaDia.Source = new BSGR_CartaDia().CollectionCartaDia();
                ESGR_CartaDia = CollectionESGR_CartaDia.LastOrDefault();
                SelectedESGR_Fecha = Convert.ToDateTime((ESGR_CartaDia.Fecha) ?? DateTime.Now);
            }
            catch (Exception ex)
            {
                CmpMessageBox.Show(SGRMessage.AdministratorCartaDia, ex.Message, CmpButton.Aceptar);
            }
        }

        private async void MethodLoadFechaCartaDia()
        {
            await Task.Factory.StartNew(() =>
            {
                try
                {
                    var Fecha = DateTime.Now;
                    if (CollectionESGR_CartaDia.Count > 1)
                        Fecha = Convert.ToDateTime(CollectionESGR_CartaDia.LastOrDefault().Fecha);
                    ESGR_CartaDia = CollectionESGR_CartaDia.LastOrDefault(x => Convert.ToDateTime(x.Fecha).ToShortDateString() == SelectedESGR_Fecha.ToShortDateString());
                    if (Convert.ToDateTime(SelectedESGR_Fecha.ToShortDateString()) >= Convert.ToDateTime(Fecha) && ESGR_CartaDia.IdCartaDia == 0)
                        ESGR_CartaDia = CollectionESGR_CartaDia.LastOrDefault();
                    MethodLoadCartaDiaDetalle();
                }
                catch (Exception ex)
                {
                    CmpMessageBox.Show(SGRMessage.AdministratorCartaDia, ex.Message, CmpButton.Aceptar);
                }
            });
        }

        #endregion
    }
}
