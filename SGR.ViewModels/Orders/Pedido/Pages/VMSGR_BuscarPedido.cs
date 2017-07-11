using ComputerSystems;
using ComputerSystems.WPF.Acciones.Controles.ModalDialog;
using ComputerSystems.WPF.Interfaces;
using ComputerSystems.WPF.Notificaciones;
using SGR.Models.Business;
using SGR.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SGR.ViewModels.Orders.Pedido.Pages
{
    public class VMSGR_BuscarPedido : CmpNavigationService, CmpIModalDialog
    {
        public object Parameter
        {
            set
            {
                if (value is ESGR_Pedido)
                {
                    MethodLoadDetails();
                }
            }
        }

        #region COLECCION

        private CmpObservableCollection<ESGR_Pedido> _CollectionESGR_Pedido;
        public CmpObservableCollection<ESGR_Pedido> CollectionESGR_Pedido
        {
            get
            {
                if (_CollectionESGR_Pedido == null)
                    _CollectionESGR_Pedido = new CmpObservableCollection<ESGR_Pedido>();
                return _CollectionESGR_Pedido;
            }
        }

        #endregion

        #region OBJ SECUNDARIOS

        private ESGR_Pedido _ESGR_Pedido;
        public ESGR_Pedido ESGR_Pedido
        {
            get
            {
                if (_ESGR_Pedido == null)
                    _ESGR_Pedido = new ESGR_Pedido();
                return _ESGR_Pedido;
            }
            set
            {
                _ESGR_Pedido = value;
                OnPropertyChanged("ESGR_Pedido");
            }
        }

        private ESGR_Pedido _SelectedPedido;
        public ESGR_Pedido SelectedPedido
        {
            get
            {
                return _SelectedPedido;
            }
            set
            {
                _SelectedPedido = value;
                OnPropertyChanged("SelectedPedido");
            }
        }

        #endregion

        #region ICOMMAND

        public ICommand ISelectItem
        {
            get
            {
                return CmpICommand.GetICommand((T) =>
                {
                    CmpModalDialog.GetContent().Close(SelectedPedido);
                });
            }
        }

        #endregion

        #region METODOS

        private async void MethodLoadDetails()
        {
            await Task.Factory.StartNew(()=>
            {
                CollectionESGR_Pedido.Source = new CmpObservableCollection<ESGR_Pedido>(new BSGR_Pedido().GetCollectionPedido().Where(x => x.ESGR_Estado.CodEstado == "CTPED"));
            });
        }

        #endregion
    }
}
