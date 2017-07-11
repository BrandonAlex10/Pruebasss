/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   CRISTIAN A. HERNANDEZ VILLO
'* FCH. CREACIÓN : 20/04/2017
**********************************************************/

namespace SGR.ViewModels.Presentation.VentaDia.Pages
{
    using ComputerSystems;
    using ComputerSystems.Loading;
    using ComputerSystems.MethodList;
    using ComputerSystems.WPF;
    using ComputerSystems.WPF.Acciones.Controles.ModalDialog;
    using ComputerSystems.WPF.Interfaces;
    using ComputerSystems.WPF.Notificaciones;
    using SGR.Models.Business;
    using SGR.Models.Entity;
    using SGR.Reports;
    using SGR.ViewModels.Method;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    public class VMSGR_ListadoVentaDia : CmpNavigationService, CmpINavigation
    {
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
                        CmpLoading = new CmpLoading(MethodLoadHeader, MethodLoadDetails);
                        CmpLoading.Exceptions = ((e) => { CmpMessageBox.Show(SGRMessage.ListadoVentaDia, e.Message, CmpButton.Aceptar); });
                        CmpLoading.LoadHeader();
                    }
                });
            }
        }

        private Frame _MyFrame;
        public Frame MyFrame
        {
            set
            {
                _MyFrame = value;
            }
        }

        #region COLECCION

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
        }

        private CmpObservableCollection<ESGR_ListadoVentaDia> _CollectionESGR_ListadoVentaDia;
        public CmpObservableCollection<ESGR_ListadoVentaDia> CollectionESGR_ListadoVentaDia
        {
            get
            {
                if (_CollectionESGR_ListadoVentaDia == null)
                    _CollectionESGR_ListadoVentaDia = new CmpObservableCollection<ESGR_ListadoVentaDia>();
                return _CollectionESGR_ListadoVentaDia;
            }
        }

        #endregion

        #region OBJ SECUNDARIO

        private DateTime _SelectedFechaInicio;
        public DateTime SelectedFechaInicio
        {
            get
            {
                return _SelectedFechaInicio;
            }
            set
            {
                _SelectedFechaInicio = value;
                if (value != null)
                    CmpLoading.LoadDetail();
                OnPropertyChanged("SelectedFechaInicio");
            }
        }

        private ESGR_Producto _ESGR_Producto;
        public ESGR_Producto ESGR_Producto
        {
            get
            {
                if (_ESGR_Producto == null)
                    _ESGR_Producto = new ESGR_Producto();
                return _ESGR_Producto;
            }
            set 
            {
                _ESGR_Producto = value;
                OnPropertyChanged("ESGR_Producto");
            }
        }

        private ESGR_Usuario _ESGR_Usuario;
        public ESGR_Usuario ESGR_Usuario
        {
            get
            {
                if (_ESGR_Usuario == null)
                    _ESGR_Usuario = new ESGR_Usuario();
                return _ESGR_Usuario;
            }
            set
            {
                _ESGR_Usuario = value;
                OnPropertyChanged("ESGR_Usuario");
            }
        }

        private DateTime _SelectedFechaFin;
        public DateTime SelectedFechaFin
        {
            get
            {
                return _SelectedFechaFin;
            }
            set
            {
                _SelectedFechaFin = value;
                if (value != null)
                    CmpLoading.LoadDetail();
                OnPropertyChanged("SelectedFechaFin");
            }
        }

        private ESGR_ProductoCategoria _SelectedESGR_ProductoCategoria;
        public ESGR_ProductoCategoria SelectedESGR_ProductoCategoria
        {
            get
            {
                if (_SelectedESGR_ProductoCategoria == null)
                    _SelectedESGR_ProductoCategoria = new ESGR_ProductoCategoria();
                return _SelectedESGR_ProductoCategoria;
            }
            set
            {
                _SelectedESGR_ProductoCategoria = value;
                if (value != null)
                {
                    MethodLoadSubCategoria(value);
                    ESGR_Producto.ESGR_ProductoSubCategoria.ESGR_ProductoCategoria = value;
                    CmpLoading.LoadDetail();
                }
                OnPropertyChanged("SelectedESGR_ProductoCategoria");
            }
        }

        private ESGR_ProductoSubCategoria _SelectedESGR_ProductoSubCategoria;
        public ESGR_ProductoSubCategoria SelectedESGR_ProductoSubCategoria
        {
            get
            {
                if (_SelectedESGR_ProductoSubCategoria == null)
                    _SelectedESGR_ProductoSubCategoria = new ESGR_ProductoSubCategoria();
                return _SelectedESGR_ProductoSubCategoria;
            }
            set
            {
                _SelectedESGR_ProductoSubCategoria = value;
                if (value != null)
                {
                    ESGR_Producto.ESGR_ProductoSubCategoria = value;
                    CmpLoading.LoadDetail();
                }
                OnPropertyChanged("SelectedESGR_ProductoSubCategoria");
            }
        }

        #endregion

        #region PROPERTY

        private string _PropertyTextProducto;
        public string PropertyTextProducto
        {
            get
            {
                return _PropertyTextProducto;
            }
            set
            {
                _PropertyTextProducto = value;
                if (value.Trim().Length == 0)
                    ESGR_Producto.IdProducto = 0;
                CmpLoading.LoadDetail();
                OnPropertyChanged("PropertyTextProducto");
            }
        }

        private string _PropertyTextUsuario;
        public string PropertyTextUsuario
        {
            get
            {
                return _PropertyTextUsuario;
            }
            set
            {
                _PropertyTextUsuario = value;
                if (value.Trim().Length == 0)
                    ESGR_Usuario.IdUsuario = 0;
                CmpLoading.LoadDetail();
                OnPropertyChanged("PropertyTextUsuario");
            }
        }

        #endregion

        #region ICOMMAND

        public ICommand IImprimir
        {
            get
            {
                return CmpICommand.GetICommand((T) =>
                {
                    string PathReport = "SGR.Reports.Files.RptListadoVentaDia.rdlc";
                    string[] Parametro;
                    Parametro = new string[]
                    {

                    };
                    MainReports ObjMainReport = new MainReports();
                    ObjMainReport.InitializeComponet(PathReport, "DtsListadoVenta", CollectionESGR_ListadoVentaDia, Parametro);
                    ObjMainReport.ShowDialog();

                });
            }
        }

        public ICommand IExportar
        {
            get
            {
                return CmpICommand.GetICommand((T) =>
                {
                    if (CollectionESGR_ListadoVentaDia.Count == 0)
                    {
                        CmpMessageBox.Show(SGRMessage.ListadoVentaDia, "No hay Datos que Exportar", CmpButton.Aceptar);
                        return;
                    }
                    try
                    {
                        var ListarExcel = CollectionESGR_ListadoVentaDia.Select(x => new
                        {
                            Fecha = x.Fecha.ToString("dd/MM/yyyy"),
                            Producto = x.Producto,
                            Cantidad = x.Cantidad,
                            Precio = x.Precio.ToString("N2"),
                            Descuento = x.Descuento.ToString("N2"),
                            Importe = x.Importe.ToString("N2"),
                            Mozo = x.Mozo
                        }).ToList();
                        ListarExcel.Export("Listado Venta", ExportType.Excel, (value) => CmpMessageBox.Show(SGRMessage.ListadoVentaDia, value, CmpButton.Aceptar));
                    }
                    catch (Exception ex)
                    {
                        CmpMessageBox.Show(SGRMessage.ListadoVentaDia, ex.Message, CmpButton.Aceptar);
                    }
                });
            }
        }

        public ICommand IBuscarProducto
        {
            get
            {
                return CmpICommand.GetICommand((T) =>
                {
                    var Result = CmpModalDialog.GetContent().ShowDialog("MPSGR_BuscadorProducto", ESGR_Producto);
                    if (Result != null)
                    {
                        ESGR_Producto = (ESGR_Producto)Result;
                        PropertyTextProducto = ESGR_Producto.Producto;
                        CmpLoading.LoadDetail();
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
                    Volver();
                });
            }
        }

        public ICommand IMozo
        {
            get
            {
                return CmpICommand.GetICommand((T) =>
                {
                    var Result = CmpModalDialog.GetContent().ShowDialog("MPSGR_BuscadorUsuario", ESGR_Producto);
                    if (Result != null)
                    {
                        ESGR_Usuario = (ESGR_Usuario)Result;
                        PropertyTextUsuario = ((ESGR_Usuario)Result).Nombres;
                        CmpLoading.LoadDetail();
                    }
                });
            }
        }

        #endregion

        #region METODOS

        private void MethodLoadHeader()
        {
            try
            {
                SelectedFechaFin = DateTime.Now;
                SelectedFechaInicio = DateTime.Now;
                CollectionESGR_ProductoCategoria.Source = new BSGR_ProductoCategoria().GetCollectionProductoCategoria();
                Application.Current.Dispatcher.Invoke(() =>
                {
                    CollectionESGR_ProductoCategoria.Add(new ESGR_ProductoCategoria() { IdCategoria = 0, Categoria = "TODOS" });
                    SelectedESGR_ProductoCategoria = CollectionESGR_ProductoCategoria.FirstOrDefault(x => x.IdCategoria == 0);
                });
            }
            catch (Exception ex)
            {
                CmpMessageBox.Show(SGRMessage.ListadoVentaDia, ex.Message, CmpButton.Aceptar);
            }
        }

        private void MethodLoadDetails()
        {
            try
            {
                CollectionESGR_ListadoVentaDia.Source = new BSGR_ListadoVentaDia().CollectionESGR_ListadoVentaDia(SelectedESGR_ProductoCategoria.IdCategoria, SelectedESGR_ProductoSubCategoria.IdSubCategoria, ESGR_Producto.IdProducto, SelectedFechaInicio, SelectedFechaFin, ESGR_Usuario.IdUsuario);
            }
            catch (Exception ex)
            {
                CmpMessageBox.Show(SGRMessage.ListadoVentaDia, ex.Message, CmpButton.Aceptar);
            }
        }

        private async void MethodLoadSubCategoria(ESGR_ProductoCategoria ESGR_ProductoCategoria)
        {
            await Task.Factory.StartNew(() =>
            {
                try
                {
                    CollectionESGR_ProductoSubCategoria.Source = new BSGR_ProductoSubCategoria().GetCollectionProductoSubCategoria(ESGR_ProductoCategoria);
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        CollectionESGR_ProductoSubCategoria.Add(new ESGR_ProductoSubCategoria() { IdSubCategoria = 0, SubCategoria = "TODOS" });
                    });
                    SelectedESGR_ProductoSubCategoria = CollectionESGR_ProductoSubCategoria.FirstOrDefault(x => x.IdSubCategoria == 0);
                }
                catch(Exception ex)
                {
                    CmpMessageBox.Show(SGRMessage.ListadoVentaDia, ex.Message, CmpButton.Aceptar);
                }
            });
        }

        #endregion
    }
}
