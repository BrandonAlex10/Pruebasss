/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   ABEL QUISPE ORELLANA
'* FCH. CREACIÓN : 03/12/2016
'*********************************************************
'* FCH. MODIFICACION : 10/12/2016 [CRISTIAN HERNANDEZ VILLO]
'* MOTIVO: Se hizo cargar el listado y se dio funcionalidad a los botones
**********************************************************/
namespace SGR.ViewModels.Presentation.Producto.Pages
{
    using ComputerSystems;
    using ComputerSystems.Loading;
    using ComputerSystems.WPF;
    using ComputerSystems.WPF.Acciones.Controles.ModalDialog;
    using ComputerSystems.WPF.Interfaces;
    using ComputerSystems.WPF.Notificaciones;
    using SGR.Models.Business;
    using SGR.Models.Entity;
    using SGR.ViewModels.Method;
    using SGR.ViewModels.Presentation;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Input;

    public class VMSGR_ListadoProducto : CmpNavigationService, CmpINavigation
    {
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

        public object Parameter
        {
            set
            {
                Application.Current.Dispatcher.Invoke(async() =>
                {
                    var result = await MTSGR_GetPrivilege.Process(this.GetType(), SGRMessage.TitleMessage, SGRMessageContent.GetAccesoRestringido);
                    if (result == null)
                        Volver();
                    else
                    {
                        PropertyIsEnabledNuevo = result.Nuevo;
                        PropertyIsEnabledEditar = result.Editar;
                        PropertyIsEnabledEliminar = result.Eliminar;
                        CmpLoading = new CmpLoading(MethodLoadHeader, MethodLoadDetail);
                        CmpLoading.Exceptions = ((e) => { CmpMessageBox.Show(SGRMessage.AdministratorProducto, e.Message, CmpButton.Aceptar); });
                        CmpLoading.LoadHeader();
                    }
                });
            }
        }

        public CmpLoading CmpLoading { get; set; }

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

        private CmpObservableCollection<ESGR_Item> _CollectionItemOpcines;
        public CmpObservableCollection<ESGR_Item> CollectionItemOpciones
        {
            get
            {
                if (_CollectionItemOpcines == null)
                    _CollectionItemOpcines = new CmpObservableCollection<ESGR_Item>();
                return _CollectionItemOpcines;
            }
        }

        private CmpObservableCollection<ESGR_ProductoCategoria> _CollectionESGR_Categoria;
        public CmpObservableCollection<ESGR_ProductoCategoria> CollectionESGR_Categoria
        {
            get
            {
                if (_CollectionESGR_Categoria == null)
                    _CollectionESGR_Categoria = new CmpObservableCollection<ESGR_ProductoCategoria>();
                return _CollectionESGR_Categoria;
            }
        }

        private CmpObservableCollection<ESGR_ProductoSubCategoria> _CollectionESGR_SubCategoria;
        public CmpObservableCollection<ESGR_ProductoSubCategoria> Collection_SubCategoria
        {
            get
            {
                if (_CollectionESGR_SubCategoria == null)
                    _CollectionESGR_SubCategoria = new CmpObservableCollection<ESGR_ProductoSubCategoria>();
                return _CollectionESGR_SubCategoria;
            }
        }

        #endregion

        #region OBJ SECUNDARIOS

        private ESGR_Item _SelectItemOpciones;
        public  ESGR_Item SelectItemOpciones
        {
            get
            {
                if (_SelectItemOpciones == null)
                    _SelectItemOpciones = new ESGR_Item();
                return _SelectItemOpciones;
            }
            set
            {
                _SelectItemOpciones = value;
                if (value != null)
                {
                    if (value.ValueValuePath == "PRODUCTO")
                    {
                        PropertyVisibilityProducto = Visibility.Visible;
                        PropertyVisibilityCategoria = Visibility.Collapsed;
                        PropertyTitleFiltros = "Filtrar por Producto";
                        PropertyFiltroSubCategoria = "";
                    }
                    else if (value.ValueMemberPath == "CATEGORIA Y SUBCATEGORIA")
                    {
                        PropertyVisibilityProducto = Visibility.Collapsed;
                        PropertyVisibilityCategoria = Visibility.Visible;
                        PropertyTitleFiltros = "Filtrar por Categoria ";
                        PropertyFiltroSubCategoria = "Filtrar por  SubCategoria";
                        SelectESGR_ProductoCategoria = CollectionESGR_Categoria.FirstOrDefault(x => x.IdCategoria == 0);
                    }
                    CmpLoading.LoadDetail();
                }
                PropertyBuscador = string.Empty;
                OnPropertyChanged("SelectItemOpciones");
            }
        }

        private ESGR_ProductoCategoria _SelectESGR_ProductoCategoria;
        public ESGR_ProductoCategoria SelectESGR_ProductoCategoria
        {
            get
            {
                return _SelectESGR_ProductoCategoria;
            }
            set
            {
                _SelectESGR_ProductoCategoria = value;
                if (value != null)
                {
                    ESGR_Producto.ESGR_ProductoSubCategoria.ESGR_ProductoCategoria = value;
                    MethodLoadSubCategoria(value);
                    CmpLoading.LoadDetail();
                }
                OnPropertyChanged("SelectESGR_ProductoCategoria");
            }
        }

        private ESGR_ProductoSubCategoria _SelectESGR_ProductoSubCategoria;
        public ESGR_ProductoSubCategoria SelectESGR_ProductoSubCategoria
        {
            get
            {
                return _SelectESGR_ProductoSubCategoria;
            }
            set
            {
                _SelectESGR_ProductoSubCategoria = value;
                if (value != null)
                {
                    ESGR_Producto.ESGR_ProductoSubCategoria = value;
                    CmpLoading.LoadDetail();
                }
                OnPropertyChanged("SelectESGR_ProductoSubCategoria");
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

        #endregion

        #region PROPERTY

        private Visibility _PropertyVisibilityProducto;
        public Visibility PropertyVisibilityProducto
        {
            get
            {
                return _PropertyVisibilityProducto;
            }
            set
            {
                _PropertyVisibilityProducto = value;
                OnPropertyChanged("PropertyVisibilityProducto");
            }
        }

        private Visibility _PropertyVisibilityCategoria;
        public Visibility PropertyVisibilityCategoria
        {
            get
            {
                return _PropertyVisibilityCategoria;
            }
            set
            {
                _PropertyVisibilityCategoria = value;
                OnPropertyChanged("PropertyVisibilityCategoria");
            }
        }

        private string _PropertyTitleFiltros;
        public string PropertyTitleFiltros
        {
            get
            {
                return _PropertyTitleFiltros;
            }
            set
            {
                _PropertyTitleFiltros = value;
                OnPropertyChanged("PropertyTitleFiltros");
            }
        }

        private string _PropertyBuscador;
        public string PropertyBuscador
        {
            get
            {
                return _PropertyBuscador;
            }
            set
            {
                _PropertyBuscador = value;
                if (value != null)
                {
                    CmpLoading.LoadDetail();
                }
                OnPropertyChanged("PropertyBuscador");
            }
        }

        private string _PropertyFiltroSubCategoria;
        public string PropertyFiltroSubCategoria
        {
            get
            {
                return _PropertyFiltroSubCategoria;
            }
            set
            {
                _PropertyFiltroSubCategoria = value;
                OnPropertyChanged("PropertyFiltroSubCategoria");
            }
        }

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

        private bool _PropertyIsEnabledEditar;
        public bool PropertyIsEnabledEditar
        {
            get
            {
                return _PropertyIsEnabledEditar;
            }
            set
            {
                _PropertyIsEnabledEditar = value;
                OnPropertyChanged("PropertyIsEnabledEditar");
            }
        }

        private bool _PropertyIsEnabledEliminar;
        public bool PropertyIsEnabledEliminar
        {
            get
            {
                return _PropertyIsEnabledEliminar;
            }
            set
            {
                _PropertyIsEnabledEliminar = value;
                OnPropertyChanged("PropertyIsEnabledEliminar");
            }
        }

        #endregion

        #region ICOMMAND

        public ICommand INuevo
        {
            get
            {
                return CmpICommand.GetICommand((T) =>
                {
                    CmpModalDialog.GetContent().ShowDialog("MPSGR_Producto", new ESGR_Producto { Opcion = "I" });
                    CmpLoading.LoadDetail();
                });
            }
        }

        public ICommand IEditar
        {
            get
            {
                return CmpICommand.GetICommand((T) =>
                {
                    if (SelectESGR_Producto == null)
                    {
                        CmpMessageBox.Show(SGRMessage.AdministratorEmpresa, "Se debe de seleccionar un registro para proceder a editar", CmpButton.Aceptar);
                        return;
                    }
                    SelectESGR_Producto.Opcion = "U";
                    CmpModalDialog.GetContent().ShowDialog("MPSGR_Producto", SelectESGR_Producto);
                    CmpLoading.LoadDetail();
                });
            }
        }

        public ICommand IEliminar
        {
            get
            {
                return CmpICommand.GetICommand(async (T) =>
                {
                    if (SelectESGR_Producto == null)
                    {
                        CmpMessageBox.Show(SGRMessage.AdministratorProducto, SGRMessageContent.ContentSelectNull + "Eliminar", CmpButton.Aceptar);
                        return;
                    }
                    await Task.Factory.StartNew(() =>
                    {
                        try
                        {
                            CmpMessageBox.Show(SGRMessage.AdministratorProducto, "¿Seguro que desea eliminar el registro?", CmpButton.AceptarCancelar, () =>
                            {
                                SelectESGR_Producto.Opcion = "D";
                                new BSGR_Producto().TransProducto(SelectESGR_Producto);
                                CmpMessageBox.Show(SGRMessage.AdministratorProducto, SGRMessageContent.ContentDeleteOK, CmpButton.Aceptar);
                                CmpLoading.LoadDetail();
                            });
                        }
                        catch (Exception ex)
                        {
                            CmpMessageBox.Show(SGRMessage.AdministratorProducto, ex.Message, CmpButton.Aceptar);
                        }
                    });
                });
            }
        }

        public ICommand IVolver
        {
            get
            {
                return CmpICommand.GetICommand((V) =>
                {
                    Volver();
                });
            }
        }

        #endregion

        #region METODOS

        public void MethodLoadHeader()
        {
            try
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    CollectionItemOpciones.Source = GetOpciones();
                    PropertyVisibilityCategoria = Visibility.Collapsed;
                    CollectionESGR_Categoria.Source = new BSGR_ProductoCategoria().GetCollectionProductoCategoria();
                    CollectionESGR_Categoria.Add(new ESGR_ProductoCategoria
                    {
                        IdCategoria = 0,
                        Categoria = "TODOS"
                    });
                    SelectItemOpciones = CollectionItemOpciones.FirstOrDefault();
                    SelectESGR_ProductoCategoria = CollectionESGR_Categoria.FirstOrDefault(x => x.IdCategoria == 0);
                });
            }
            catch (Exception ex)
            {
                CmpMessageBox.Show(SGRMessage.AdministratorProducto, ex.Message, CmpButton.Aceptar);
            }
        }

        public void MethodLoadSubCategoria(ESGR_ProductoCategoria ESGR_ProductoCategoria)
        {
            try
            {
                Collection_SubCategoria.Source = new BSGR_ProductoSubCategoria().GetCollectionProductoSubCategoria(ESGR_ProductoCategoria);
                Collection_SubCategoria.Add(new ESGR_ProductoSubCategoria
                {
                    IdSubCategoria = 0,
                    SubCategoria = "TODOS",
                    ESGR_ProductoCategoria = ESGR_ProductoCategoria,
                });
                SelectESGR_ProductoSubCategoria = Collection_SubCategoria.FirstOrDefault(x => x.IdSubCategoria == 0);
            }
            catch (Exception ex)
            {
                CmpMessageBox.Show(SGRMessage.AdministratorProducto, ex.Message, CmpButton.Aceptar);
            }
        }

        public void MethodLoadDetail()
        {
            try
            {
                CollectionESGR_Producto.Source = new BSGR_Producto().GetCollectionProducto(ESGR_Producto.ESGR_ProductoSubCategoria, PropertyBuscador, SelectItemOpciones.ValueValuePath);
            }
            catch (Exception ex)
            {
                CmpMessageBox.Show(SGRMessage.AdministratorProducto, ex.Message, CmpButton.Aceptar);
            }
        }

        private CmpObservableCollection<ESGR_Item> GetOpciones()
        {
            CmpObservableCollection<ESGR_Item> CollectionItemOpciones = new CmpObservableCollection<ESGR_Item>();
            CollectionItemOpciones.Add(new ESGR_Item { ValueMemberPath = "PRODUCTO", ValueValuePath = "PRODUCTO" });
            CollectionItemOpciones.Add(new ESGR_Item { ValueMemberPath = "CATEGORIA Y SUBCATEGORIA", ValueValuePath = "CATEGORIAYSUB" });
            return CollectionItemOpciones;
        }

        #endregion

    }
}
