/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   OSCAR HUAMAN CABRERA
'* FCH. CREACIÓN : 01/12/2016
'*********************************************************
'* FECHA MODIFIC. : 15/12/2016 [CRISTIAN HERNANDEZ VILLO]
'* MOTIVO MODIFIC.: Se agregó funcionalidad a los botones
**********************************************************/

namespace SGR.ViewModels.Presentation.Mesa.ModalPage
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
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Input;

    public class VMSGR_Mesa : CmpNavigationService, CmpIModalDialog
    {
        private  ESGR_Mesa _ESGR_Mesa;
        public   ESGR_Mesa ESGR_Mesa
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
                Application.Current.Dispatcher.Invoke(async () =>
                {
                    var result = await MTSGR_GetPrivilege.Process(this.GetType(), SGRMessage.TitleMessage, SGRMessageContent.GetAccesoRestringido);
                    if (result == null)
                        CmpModalDialog.GetContent().Close();
                    else if (value is ESGR_Mesa)
                    {
                        ESGR_Mesa = (ESGR_Mesa)value;
                        MethodLoadMesaArea();
                        if (ESGR_Mesa.Opcion == "U")
                            PropertyEnableMesaArea = false;
                        else
                            PropertyEnableMesaArea = true;
                    }
                });
            }
        }

        #region COLECCIONES

        private CmpObservableCollection<ESGR_MesaArea> _CollectionMesaArea;
        public  CmpObservableCollection<ESGR_MesaArea> CollectionMesaArea
        {
            get
            {
                if (_CollectionMesaArea == null)
                    _CollectionMesaArea = new CmpObservableCollection<ESGR_MesaArea>();
                return _CollectionMesaArea;
            }
        }
         
        #endregion

        #region PROPERTY

        private ESGR_MesaArea _SelectESGR_MesaArea;
        public  ESGR_MesaArea SelectESGR_MesaArea
        {
            get
            {
                return _SelectESGR_MesaArea;
            }
            set
            {
                _SelectESGR_MesaArea = value;
                if(value!=null)
                 ESGR_Mesa.EESGR_MesaArea = value;
                OnPropertyChanged("SelectESGR_MesaArea");
            }
        }

        private bool _PropertyEnableMesaArea;
        public  bool PropertyEnableMesaArea
        {
            get
            {
                return _PropertyEnableMesaArea;
            }
            set
            {
                _PropertyEnableMesaArea = value;
                OnPropertyChanged("PropertyEnableMesaArea");
            }
        }

        #endregion

        #region ICOMMAND

        public ICommand IGuardar
        {
            get
            {
                return CmpICommand.GetICommand(async (N) =>
                {
                    if (MethodValidarDatos()) return;
                    await Task.Factory.StartNew(() =>
                    {
                         try
                         {
                            new BSGR_Mesa().TransMesa(ESGR_Mesa);
                            CmpMessageBox.Show(SGRMessage.AdministratorMesa, SGRMessageContent.ContentSaveOK, CmpButton.Aceptar, () =>
                            {
                                CmpModalDialog.GetContent().Close();
                            });
                         }
                         catch (Exception ex)
                         {
                            CmpMessageBox.Show(SGRMessage.AdministratorMesa,ex.Message,CmpButton.Aceptar);
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

        private async void MethodLoadMesaArea()
        {
            await Task.Factory.StartNew(() =>
            {
                try
                {
                    CollectionMesaArea.Source = new BSGR_MesaArea().GetCollectionMesaArea();
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        if (ESGR_Mesa.Opcion == "I")
                            SelectESGR_MesaArea = CollectionMesaArea.FirstOrDefault();
                        else
                            SelectESGR_MesaArea = CollectionMesaArea.FirstOrDefault(x => x.IdMesaArea == ESGR_Mesa.EESGR_MesaArea.IdMesaArea);
                    });
                }
                catch (Exception ex)
                {
                    CmpMessageBox.Show(SGRMessage.AdministratorMesa, ex.Message, CmpButton.Aceptar);
                }
            });
          
        }

        private bool MethodValidarDatos()
        {
            var vrValidaDatos = false;
            if ( ESGR_Mesa.Mesa == null || ESGR_Mesa.Mesa.Trim().Length == 0)
            {
                CmpMessageBox.Show(SGRMessage.AdministratorMesa,"Campo Obligatorio: Mesa",CmpButton.Aceptar);
                vrValidaDatos = true;
            }
         
            return vrValidaDatos;
        }

        #endregion
    }
}
