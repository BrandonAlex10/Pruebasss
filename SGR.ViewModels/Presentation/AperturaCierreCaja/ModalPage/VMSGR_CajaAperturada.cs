using ComputerSystems;
using ComputerSystems.WPF;
using ComputerSystems.WPF.Acciones.Controles.ModalDialog;
using ComputerSystems.WPF.Interfaces;
using ComputerSystems.WPF.Notificaciones;
using SGR.Models.Business;
using SGR.Models.Entity;
using SGR.ViewModels.Method;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SGR.ViewModels.Presentation.AperturaCierreCaja.ModalPage
{
    public class VMSGR_CajaAperturada : CmpNavigationService, CmpIModalDialog
    {
        public object Parameter
        {
            set
            {
                Application.Current.Dispatcher.Invoke(async () =>
                {
                    var result = await MTSGR_GetPrivilege.Process(this.GetType(), SGRMessage.TitleMessage, SGRMessageContent.GetAccesoRestringido);
                    if (result == null)
                        CmpModalDialog.GetContent().Close(null);
                    else
                        MethodLoadCaja();
                });
            }
        }

        #region OBJETOS SECUNDARIOS

        private ESGR_Caja _SelectedESGR_Caja;
        public ESGR_Caja SelectedESGR_Caja
        {
            get
            {
                return _SelectedESGR_Caja;
            }
            set
            {
                _SelectedESGR_Caja = value;
                OnPropertyChanged("SelectedESGR_Caja");
            }
        }

        #endregion

        #region COLECCION

        private CmpObservableCollection<ESGR_Caja> _CollectionESGR_Caja;
        public CmpObservableCollection<ESGR_Caja> CollectionESGR_Caja
        {
            get
            {
                if (_CollectionESGR_Caja == null)
                    _CollectionESGR_Caja = new CmpObservableCollection<ESGR_Caja>();
                return _CollectionESGR_Caja;
            }
        }

        #endregion

        #region ICOMMAND

        public ICommand IAceptar
        {
            get
            {
                return CmpICommand.GetICommand((T) =>
                {
                    CmpModalDialog.GetContent().Close(SelectedESGR_Caja);
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

        private async void MethodLoadCaja()
        {
            await Task.Factory.StartNew(() =>
            {
                try
                {
                    var CollectionCajaAperturada = new BSGR_AperturaCierreCaja().CollectionESGR_AperturaCierreCaja().Where(x => x.ESGR_Estado.CodEstado == "APTCJ").ToList();

                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        CollectionESGR_Caja.Clear();
                        CollectionCajaAperturada.ForEach(x =>
                        {
                            CollectionESGR_Caja.Add(x.ESGR_Caja);
                        });
                    });

                }
                catch (Exception ex)
                {
                    CmpMessageBox.Show(SGRMessage.AdministracionCaja, ex.Message, CmpButton.Aceptar);
                }
            });
        }

        #endregion
    }
}
