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
    using System.Linq;
    using System.Windows;
    using System.Windows.Input;

    public class VMSGR_MesasAtendidas : CmpNavigationService, CmpIModalDialog
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
                        CmpModalDialog.GetContent().Close(new ESGR_Mesa());
                    else if (value is ESGR_Mesa)
                    {
                        if (CollectionESGR_Mesa.Count > 0)
                            CollectionESGR_Mesa.Clear();
                        ESGR_Mesa = ((ESGR_Mesa)value);
                        PropertyVisibilityCheckAllTables = Visibility.Collapsed;
                        AllTables = false;
                        CmpLoading = new CmpLoading(MethodLoadArea, MethodLoadMesa);
                        CmpLoading.Exceptions = ((e) => { CmpMessageBox.Show(SGRMessage.BusquedaMesaAtendido, e.Message, CmpButton.Aceptar); });
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

        private bool _AllTables;
        public bool AllTables
        {
            get
            {
                return _AllTables;
            }
            set
            {
                _AllTables = value;
                if (CmpLoading != null)
                    CmpLoading.LoadDetail();
                OnPropertyChanged("AllTables");
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

        private Visibility _PropertyVisibilityCheckAllTables;
        public Visibility PropertyVisibilityCheckAllTables
        {
            get
            {
                return _PropertyVisibilityCheckAllTables;
            }
            set
            {
                _PropertyVisibilityCheckAllTables = value;
                OnPropertyChanged("PropertyVisibilityCheckAllTables");
            }
        }

        #endregion

        #region ICOMMAND

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
                    CmpModalDialog.GetContent().Close(new ESGR_Mesa());
                });
            }
        }

        #endregion

        #region METODOS

        private void MethodLoadArea()
        {
            CollectionESGR_MesaArea.Source = new BSGR_MesaArea().GetCollectionMesaArea();
            SelectedESGR_MesaArea = CollectionESGR_MesaArea.FirstOrDefault();
        }

        private void MethodLoadMesa()
        {
            PropertyMetroProgressBar = true;

            var vrCollectionESGR_Mesa = new BSGR_Mesa().GetCollectionMesa(SelectedESGR_MesaArea, SGRVariables.ESGR_Usuario.IdUsuario).Where(x => x.ESGR_Estado.CodEstado == "ATPED" || x.ESGR_Estado.CodEstado == "CTPED" || x.ESGR_Estado.CodEstado == "RVPED").GroupBy(x => x.Identificador).Select(y => y.First());

            if (ESGR_Mesa.Opcion == "U")
                CollectionESGR_Mesa.Source = new CmpObservableCollection<ESGR_Mesa>(vrCollectionESGR_Mesa.Where(x => x.ESGR_Estado.CodEstado == "ATPED" || x.ESGR_Estado.CodEstado == "RVPED").ToList());

            if (ESGR_Mesa.Opcion == "P")
            {
                if (PropertyVisibilityCheckAllTables != Visibility.Visible)
                    PropertyVisibilityCheckAllTables = Visibility.Visible;

                if (AllTables)
                    CollectionESGR_Mesa.Source = new CmpObservableCollection<ESGR_Mesa>(vrCollectionESGR_Mesa.Where(x => x.ESGR_Estado.CodEstado != "RVPED"));
                else
                    CollectionESGR_Mesa.Source = new CmpObservableCollection<ESGR_Mesa>(vrCollectionESGR_Mesa.Where(x => x.ESGR_Estado.CodEstado == "CTPED").ToList());                    
            }

            PropertyMetroProgressBar = false;
        }

        #endregion
    }
}