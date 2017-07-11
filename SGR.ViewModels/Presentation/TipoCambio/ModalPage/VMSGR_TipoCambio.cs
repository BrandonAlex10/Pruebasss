/*********************************************************
'* CREADO POR	  : COMPUTER SYSTEMS SOLUTION
 * ********************************************************
 * 
'* FCH. CREACIÓN  : 01/12/2016 [OSCAR HUAMAN CABRERA]
 * 
 * ********************************************************
'* 
'* FCH. MODIFICACION : 12/12/2016 [OSCAR HUAMAN CABRERA]
'* MOTIVO: Se hizo cargar el listado, se dio funcionalidad a los botones
 * 
**********************************************************/

namespace SGR.ViewModels.Presentation.TipoCambio.ModalPage
{
    using ComputerSystems;
    using ComputerSystems.WPF;
    using ComputerSystems.WPF.Acciones.Controles.ModalDialog;
    using ComputerSystems.WPF.Interfaces;
    using ComputerSystems.WPF.Notificaciones;
    using SGR.Models.Business;
    using SGR.Models.Entity;
    using SGR.ViewModels.Method;
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

    public class VMSGR_TipoCambio : CmpNavigationService, CmpIModalDialog
    {
        private ESGR_TipoCambio _ESGR_TipoCambio;
        public ESGR_TipoCambio ESGR_TipoCambio
        {
            get
            {
                if (_ESGR_TipoCambio != null)
                    if(_ESGR_TipoCambio.FechaTcb == new DateTime())
                        _ESGR_TipoCambio.FechaTcb = DateTime.Now;
                return _ESGR_TipoCambio;
            }
            set
            {
                _ESGR_TipoCambio = value;
                OnPropertyChanged("ESGR_TipoCambio");
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
                    else if (value is ESGR_TipoCambio)
                    {
                        ESGR_TipoCambio = (ESGR_TipoCambio)value;
                        MethodLoadMoneda();
                    }
                });
            }
        }

        #region COLECCIONES

        private CmpObservableCollection<ESGR_Moneda> _CollectionESGR_Moneda;
        public CmpObservableCollection<ESGR_Moneda> CollectionESGR_Moneda
        {
            get
            {
                if (_CollectionESGR_Moneda == null)
                    _CollectionESGR_Moneda = new CmpObservableCollection<ESGR_Moneda>();
                return _CollectionESGR_Moneda;
            }
        }

        #endregion

        #region OBJETOS SECUNDARIOS
        private ESGR_Moneda _SelectedESGR_Moneda;
        public ESGR_Moneda SelectedESGR_Moneda
        {
            get
            {
                return _SelectedESGR_Moneda;
            }
            set
            {
                _SelectedESGR_Moneda = value;
                if (value != null)
                    ESGR_TipoCambio.ESGR_Moneda = value;
                OnPropertyChanged("SelectedESGR_Moneda");
            }
        }


        #endregion

        #region PROPERTY
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
                            new BSGR_TipoCambio().TransTipoCambio(ESGR_TipoCambio);
                            CmpMessageBox.Show(SGRMessage.AdministratorTipoCambio, SGRMessageContent.ContentSaveOK, CmpButton.Aceptar, () =>
                            {
                                CmpModalDialog.GetContent().Close();
                            });
                        }
                        catch (Exception ex)
                        {
                            CmpMessageBox.Show(SGRMessage.AdministratorTipoCambio, ex.Message, CmpButton.Aceptar);
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
                    CmpModalDialog.GetContent().Close();
                });
            }
        }

        #endregion

        #region METODOS

        public async void MethodLoadMoneda()
        {
            await Task.Factory.StartNew(() =>
            {
                try
                {
                    CollectionESGR_Moneda.Source = new BSGR_Moneda().GetCollectionMoneda();
                    var FirstMoneda = CollectionESGR_Moneda.ToList().FirstOrDefault(x => x.Defecto == true);
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        CollectionESGR_Moneda.Remove(FirstMoneda);
                    });
                    if (ESGR_TipoCambio.Opcion == "I")
                        SelectedESGR_Moneda = CollectionESGR_Moneda.ToList().FirstOrDefault();
                    else
                        SelectedESGR_Moneda = CollectionESGR_Moneda.ToList().FirstOrDefault(x => x.CodMoneda == ESGR_TipoCambio.ESGR_Moneda.CodMoneda);
                }
                catch (Exception ex)
                {
                    CmpMessageBox.Show(SGRMessage.AdministratorTipoCambio, ex.Message, CmpButton.Aceptar);
                }
            });
        }

        public bool MethodValidarDatos()
        {
            bool vrValidaDato = false;
            if (ESGR_TipoCambio.FechaTcb == new DateTime() || ESGR_TipoCambio.FechaTcb == null)
            {
                CmpMessageBox.Show(SGRMessage.AdministratorTipoCambio, "Campo Obligatorio: Fecha.", CmpButton.Aceptar);
                vrValidaDato = true;
            }
            else if (ESGR_TipoCambio.ESGR_Moneda.CodMoneda == string.Empty || ESGR_TipoCambio.ESGR_Moneda.CodMoneda == null)
            {
                CmpMessageBox.Show(SGRMessage.AdministratorTipoCambio, "Campo Obligatorio: Tipo Moneda.", CmpButton.Aceptar);
                vrValidaDato = true;
            }
            else if (ESGR_TipoCambio.BuyRate == 0 || ESGR_TipoCambio.BuyRate == null)
            {
                CmpMessageBox.Show(SGRMessage.AdministratorTipoCambio, "Campo Obligatorio: Precio Compra.", CmpButton.Aceptar);
                vrValidaDato = true;
            }
            else if (ESGR_TipoCambio.SelRate == 0 || ESGR_TipoCambio.SelRate == null)
            {
                CmpMessageBox.Show(SGRMessage.AdministratorTipoCambio, "Campo Obligatorio: Precio Venta.", CmpButton.Aceptar);
                vrValidaDato = true;
            }
            return vrValidaDato;
        }

        #endregion
    }
}
