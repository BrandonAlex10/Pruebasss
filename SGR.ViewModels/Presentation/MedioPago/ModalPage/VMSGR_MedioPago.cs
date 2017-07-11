/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   OSCAR HUAMAN CABRERA
'* FCH. CREACIÓN : 01/12/2016
**********************************************************/

namespace SGR.ViewModels.Presentation.MedioPago.ModalPage
{
    using ComputerSystems;
    using ComputerSystems.WPF;
    using ComputerSystems.WPF.Acciones.Controles.ModalDialog;
    using ComputerSystems.WPF.Interfaces;
    using SGR.Models.Business;
    using SGR.Models.Entity;
    using SGR.ViewModels.Method;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Input;

    public class VMSGR_MedioPago : CmpNotifyPropertyChanged, CmpIModalDialog
    {

        private ESGR_MedioPago _ESGR_MedioPago;
        public ESGR_MedioPago ESGR_MedioPago
        {
            get
            {
                return _ESGR_MedioPago;
            }
            set
            {
                _ESGR_MedioPago = value;
                OnPropertyChanged("ESGR_FormaPago");
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
                        CmpModalDialog.GetContent().Close();
                    else if (value is ESGR_MedioPago)
                    {
                        ESGR_MedioPago = (ESGR_MedioPago)value;
                        if (ESGR_MedioPago.Opcion == "I")
                            PropertyEnabledEditar = true;
                        else
                            PropertyEnabledEditar = false;
                        MethodLoadFormaPago();
                    }
                });
            }
        }

        #region OBJETOS SECUNDARIOS

        private ESGR_FormaPago _SelectedESGR_FormaPago;
        public ESGR_FormaPago SelectedESGR_FormaPago
        {
            get
            {
                return _SelectedESGR_FormaPago;
            }
            set
            {
                _SelectedESGR_FormaPago = value;

                if (value != null)
                {
                    ESGR_MedioPago.ESGR_FormaPago = value;
                    if (value.IdFormaPago == 1)
                        PropertyEnabledDiasCredito = false;
                    else
                        PropertyEnabledDiasCredito = true;
                }
                if (ESGR_MedioPago.Opcion == "I")
                {
                    ESGR_MedioPago.MedioPago = string.Empty;
                    ESGR_MedioPago.DiasCredito = 0;
                }
                OnPropertyChanged("SelectedESGR_FormaPago");
            }
        }

        #endregion

        #region COLECCIONES

        private CmpObservableCollection<ESGR_FormaPago> _CollectionESGR_FomaPago;
        public CmpObservableCollection<ESGR_FormaPago> CollectionESGR_FomaPago
        {
            get
            {
                if (_CollectionESGR_FomaPago == null)
                    _CollectionESGR_FomaPago = new CmpObservableCollection<ESGR_FormaPago>();
                return _CollectionESGR_FomaPago;
            }
        }

        #endregion

        #region PROPERTY

        private bool _PropertyEnabledDiasCredito;
        public bool PropertyEnabledDiasCredito
        {
            get
            {
                return _PropertyEnabledDiasCredito;
            }
            set
            {
                _PropertyEnabledDiasCredito = value;
                OnPropertyChanged();
            }
        }

        private bool _PropertyEnabledEditar;
        public bool PropertyEnabledEditar
        {
            get
            {
                return _PropertyEnabledEditar;
            }
            set
            {
                _PropertyEnabledEditar = value;
                OnPropertyChanged("PropertyEnabledEditar");
            }
        }

        #endregion

        #region ICOMMAND

        public ICommand IGuardar
        {
            get
            {
                return CmpICommand.GetICommand(async (G) =>
                {
                    if (MethodValidarDatos())
                        return;

                    await Task.Factory.StartNew(() =>
                    {
                        try
                        {
                            new BSGR_MedioPago().TransMedioPago(ESGR_MedioPago);
                            CmpMessageBox.Show(SGRMessage.AdministratorMedioPago, SGRMessageContent.ContentSaveOK, CmpButton.Aceptar, () =>
                            {
                                CmpModalDialog.GetContent().Close();
                            });
                        }
                        catch (Exception ex)
                        {
                            CmpMessageBox.Show(SGRMessage.AdministratorMedioPago, ex.Message, CmpButton.Aceptar);
                        }
                    });
                });
            }
        }

        public ICommand IVolver
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

        public bool MethodValidarDatos()
        {
            bool vrValidaDato = false;
            if (ESGR_MedioPago.MedioPago.Trim().Length == 0 || ESGR_MedioPago.MedioPago == null)
            {
                CmpMessageBox.Show(SGRMessage.AdministratorMedioPago, "Campo Obligatorio: Medio Pago.",CmpButton.Aceptar);
                vrValidaDato = true;
            }
            else if (ESGR_MedioPago.DiasCredito == 0 && ESGR_MedioPago.ESGR_FormaPago.IdFormaPago == 2)
            {
                CmpMessageBox.Show(SGRMessage.AdministratorMedioPago, "Campo Obligatorio: Días Crédito.", CmpButton.Aceptar);
                vrValidaDato = true;
            }
            return vrValidaDato;
        }

        public async void MethodLoadFormaPago()
        {
            await Task.Factory.StartNew(() =>
                {
                    try
                    {
                        CollectionESGR_FomaPago.Source = new BSGR_FormaPago().GetCollectionFormaPago();
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            if (ESGR_MedioPago.Opcion != "I")
                                SelectedESGR_FormaPago = CollectionESGR_FomaPago.FirstOrDefault(x => x.IdFormaPago == ESGR_MedioPago.ESGR_FormaPago.IdFormaPago);
                            else
                                SelectedESGR_FormaPago = CollectionESGR_FomaPago.FirstOrDefault();
                        });
                    }
                    catch (Exception ex)
                    {
                        CmpMessageBox.Show(SGRMessage.AdministratorMedioPago, ex.Message, CmpButton.Aceptar);
                    }
                });
        }

        #endregion
    }
}
