/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   OSCAR HUAMAN CABRERA
'* FCH. CREACIÓN : 01/12/2016
'*********************************************************
'* FECHA MODIFIC. : 15/12/2016 [CRISTIAN HERNANDEZ VILLO]
'* MOTIVO MODIFIC.: Se agregó funcionalidad a los botones
**********************************************************/

namespace SGR.ViewModels.Presentation.MesaArea.ModalPage
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
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Input;

    public class VMSGR_MesaArea :CmpNavigationService, CmpIModalDialog
    {
        private ESGR_MesaArea _ESGR_MesaArea;
        public  ESGR_MesaArea ESGR_MesaArea
        {
            get
            {
                return _ESGR_MesaArea;
            }
            set
            {
                _ESGR_MesaArea = value;
                OnPropertyChanged("ESGR_MesaArea");
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
                    else if (value is ESGR_MesaArea)
                        ESGR_MesaArea = (ESGR_MesaArea)value;
                });
            }
        }

        #region COLECCIONES
        #endregion

        #region PROPERTY
        #endregion

        #region ICOMMAND

        public ICommand IGuardar
        {
            get
            {
                return CmpICommand.GetICommand(async(N) =>
                 {
                    if (MethodValidarDatos())
                         return;
                    await Task.Factory.StartNew(() =>
                     {
                        try
                        {
                          new BSGR_MesaArea().TransMesaArea(ESGR_MesaArea);
                          CmpMessageBox.Show(SGRMessage.AdministratorMesaArea, SGRMessageContent.ContentSaveOK, CmpButton.Aceptar, () =>
                          {
                             CmpModalDialog.GetContent().Close();
                          });
                        }
                        catch(Exception ex)
                        {
                            CmpMessageBox.Show(SGRMessage.AdministratorMesaArea, ex.Message, CmpButton.Aceptar);
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

        private bool MethodValidarDatos()
        {
            var vrValidaDatos = false;
            if (ESGR_MesaArea.MesaArea == null || ESGR_MesaArea.MesaArea.Trim().Length == 0)
            {
                CmpMessageBox.Show(SGRMessage.AdministratorMesaArea, "Campo Obligatoria: Área de Mesa.", CmpButton.Aceptar);
                vrValidaDatos = true;
            }
            return vrValidaDatos;
        }

        #endregion
    }
}
