/******************************************************
 * CREADO POR : COMPUTER SYSTEM SOLUTION
 *              CRISTIAN HERNANDEZ VILLO
 * FECHA CREA : 27/06/2017
 ******************************************************/

namespace SGR.ViewModels.Presentation.PedidoAnulado.Pages
{
    using ComputerSystems;
    using ComputerSystems.Loading;
    using ComputerSystems.WPF.Interfaces;
    using ComputerSystems.WPF.Notificaciones;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using SGR.Models.Entity;
    using System.Windows.Input;
    using ComputerSystems.WPF;
    using SGR.Models.Business;
    using System.Windows;
    using ComputerSystems.WPF.Acciones.Controles.ModalDialog;
    using System.Windows.Controls;
    using SGR.ViewModels.Method;
    using SGR.Models;
    using ComputerSystems.MethodList;
    using SGR.Reports;

    public class VMSGR_ListadoPedidoAnulado : CmpNavigationService, CmpINavigation
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
                        SelectedFechaInicio = DateTime.Now;
                        SelectedFechaFin = DateTime.Now;
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

        #region OBJ SECUNDARIO

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
                OnPropertyChanged();
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
                    ESGR_Producto.ESGR_ProductoSubCategoria.ESGR_ProductoCategoria = value;
                    MethodLoadSubCategoria();
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
                return _SelectedESGR_ProductoSubCategoria;
            }
            set
            {
                _SelectedESGR_ProductoSubCategoria = value;
                if (value != null)
                {
                    ESGR_Producto.ESGR_ProductoSubCategoria = value;
                    ESGR_Producto.ESGR_ProductoSubCategoria.ESGR_ProductoCategoria = SelectedESGR_ProductoCategoria;
                    CmpLoading.LoadDetail();
                }
                OnPropertyChanged("SelectedESGR_ProductoSubCategoria");
            }
        }

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
                if(value != null)
                    CmpLoading.LoadDetail();
                OnPropertyChanged("SelectedFechaInicio");
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
                if(value != null)
                    CmpLoading.LoadDetail();
                OnPropertyChanged("SelectedFechaFin");
            }
        }


        #endregion

        #region COLLECTIONS

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

        private CmpObservableCollection<ESGR_ListadoPedidoAnulado> _CollectionESGR_ListadoPedidoAnulado;
        public CmpObservableCollection<ESGR_ListadoPedidoAnulado> CollectionESGR_ListadoPedidoAnulado
        {
            get
            {
                if (_CollectionESGR_ListadoPedidoAnulado == null)
                    _CollectionESGR_ListadoPedidoAnulado = new CmpObservableCollection<ESGR_ListadoPedidoAnulado>();
                return _CollectionESGR_ListadoPedidoAnulado;
            }
        }

        #endregion

        #region PROPERTYS

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

        private string _PropertyProfile;
        public string PropertyProfile
        {
            get
            {
                return _PropertyProfile;
            }
            set
            {
                _PropertyProfile = value;
                OnPropertyChanged("PropertyProfile");
            }
        }
        #endregion

        #region ICOMMAND

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

        public ICommand IExportar
        {
            get
            {
                return CmpICommand.GetICommand((T) =>
                {
                    if (CollectionESGR_ListadoPedidoAnulado.Count == 0)
                    {
                        CmpMessageBox.Show(SGRMessage.ListadoVentaDia, "No hay Datos que Exportar", CmpButton.Aceptar);
                        return;
                    }
                    try
                    {
                        var ListarExcel = CollectionESGR_ListadoPedidoAnulado.Select(x => new
                        {
                            Fecha = x.Fecha.ToString("dd/MM/yyyy"),
                            Producto = x.Producto,
                            Cantidad = x.Cantidad,
                            Precio = x.Precio.ToString("N2"),
                            Importe = x.Importe.ToString("N2"),
                            Mozo = x.Mozo
                        }).ToList();
                        ListarExcel.Export("Listado Pedido Anulado", ExportType.Excel, (value) => CmpMessageBox.Show(SGRMessage.ListadoVentaDia, value, CmpButton.Aceptar));
                    }
                    catch (Exception ex)
                    {
                        CmpMessageBox.Show(SGRMessage.ListadoVentaDia, ex.Message, CmpButton.Aceptar);
                    }
                });
            }
        }

        public ICommand IImprimir
        {
            get
            {
                return CmpICommand.GetICommand((T) =>
                {
                    string PathReport = "SGR.Reports.Files.RptListadoPedidoAnulado.rdlc";
                    string[] Parametro;
                    Parametro = new string[]
                    {

                    };
                    MainReports ObjMainReport = new MainReports();
                    ObjMainReport.InitializeComponet(PathReport, "DtsListadoPedidoAnulado", CollectionESGR_ListadoPedidoAnulado, Parametro);
                    ObjMainReport.ShowDialog();
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
                });
            }
        }

        #endregion

        #region METODOS

        private async void MethodLoadHeader()
        {
            try
            {
                await Task.Factory.StartNew(() =>
                {
                    CollectionESGR_ProductoCategoria.Source = new BSGR_ProductoCategoria().GetCollectionProductoCategoria();
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        CollectionESGR_ProductoCategoria.Add(new ESGR_ProductoCategoria() { IdCategoria = 0, Categoria = "TODOS" });
                        SelectedESGR_ProductoCategoria = CollectionESGR_ProductoCategoria.LastOrDefault();

                        PropertyProfile = SGRVariables.ESGR_Usuario.ESGR_Perfil.NombrePerfil;
                    });
                });
            }
            catch(Exception ex)
            {
                CmpMessageBox.Show(SGRMessage.ListadoPedidoAnulado, ex.Message, CmpButton.Aceptar);
            }

        }

        private async void MethodLoadSubCategoria()
        {
            try
            {
                await Task.Factory.StartNew(() =>
                {
                    CollectionESGR_ProductoSubCategoria.Source = new BSGR_ProductoSubCategoria().GetCollectionProductoSubCategoria(SelectedESGR_ProductoCategoria);
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        CollectionESGR_ProductoSubCategoria.Add(new ESGR_ProductoSubCategoria() { IdSubCategoria = 0, SubCategoria = "TODOS" });
                        SelectedESGR_ProductoSubCategoria = CollectionESGR_ProductoSubCategoria.LastOrDefault();
                    });
                });
            }
            catch(Exception ex)
            {
                CmpMessageBox.Show(SGRMessage.ListadoPedidoAnulado, ex.Message, CmpButton.Aceptar);
            }
        }

        private async void MethodLoadDetails()
        {
            try
            {
                await Task.Factory.StartNew(() =>
                {
                    if (SelectedESGR_ProductoCategoria == null)
                        SelectedESGR_ProductoCategoria = new ESGR_ProductoCategoria();
                    if (SelectedESGR_ProductoSubCategoria == null)
                        SelectedESGR_ProductoSubCategoria = new ESGR_ProductoSubCategoria();
                    CollectionESGR_ListadoPedidoAnulado.Source = new BSGR_ListadoPedidoAnulado().CollectionESGR_ListadoPedidoAnulado(SelectedESGR_ProductoCategoria.IdCategoria, SelectedESGR_ProductoSubCategoria.IdSubCategoria, ESGR_Producto.IdProducto, SelectedFechaInicio, SelectedFechaFin, ESGR_Usuario.IdUsuario);
                });
            }
            catch(Exception ex)
            {
                CmpMessageBox.Show(SGRMessage.ListadoPedidoAnulado, ex.Message, CmpButton.Aceptar);
            }
        }

        #endregion

    }
}
