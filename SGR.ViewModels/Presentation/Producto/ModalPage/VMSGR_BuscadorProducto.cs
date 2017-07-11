using ComputerSystems;
using ComputerSystems.WPF;
using ComputerSystems.WPF.Acciones.Controles.ModalDialog;
using ComputerSystems.WPF.Interfaces;
using ComputerSystems.WPF.Notificaciones;
using SGR.Models.Business;
using SGR.Models.Entity;
using SGR.ViewModels.Method;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SGR.ViewModels.Presentation.Producto.ModalPage
{
    public class VMSGR_BuscadorProducto : CmpNavigationService, CmpIModalDialog
    {
        private ESGR_Producto _ESGR_Producto;
        public ESGR_Producto ESGR_Producto
        {
            get
            {
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
                    if(result == null)
                        CmpModalDialog.GetContent().Close();
                    else if (value is ESGR_Producto)
                    {
                        ESGR_Producto = (ESGR_Producto)value;
                        MethodLoadDetails();
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

        #endregion

        #region OBJ SECUNDARIOS

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

        #endregion

        #region PROPERTY

        private string _PropertytextFiltro;
        public string PropertytextFiltro
        {
            get
            {
                return _PropertytextFiltro;
            }
            set
            {
                _PropertytextFiltro = value;
                if (value != null)
                    MethodLoadDetails();
                OnPropertyChanged("PropertytextFiltro");
            }
        }

        #endregion

        #region ICOMMAND

        public ICommand IProducto
        {
            get
            {
                return CmpICommand.GetICommand((T) =>
                {
                    CmpModalDialog.GetContent().Close(SelectedESGR_Producto);
                });
            }
        }

        public ICommand ISalir
        {
            get
            {
                return CmpICommand.GetICommand((T) =>
                {
                    CmpModalDialog.GetContent().Close();
                });
            }
        }

        #endregion

        #region METODOS

        private async void MethodLoadDetails()
        {
            await Task.Factory.StartNew(() =>
            {
                try
                {
                    CollectionESGR_Producto.Source = new BSGR_Producto().GetCollectionProducto(ESGR_Producto.ESGR_ProductoSubCategoria);
                }
                catch (Exception ex)
                {
                    CmpMessageBox.Show(SGRMessage.ListadoVentaDia, ex.Message, CmpButton.Aceptar);
                }
            });
        }

        #endregion
    }
}
