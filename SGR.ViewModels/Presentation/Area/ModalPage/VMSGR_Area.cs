/*********************************************************
'* CREADO POR	  : COMPUTER SYSTEMS SOLUTION
 * ********************************************************
 * 
'* FCH. CREACIÓN  : 01/12/2016 [OSCAR HUAMAN CABRERA]
'* 
 * ********************************************************
 * 
'* FECHA MODIFIC. : 12/12/2016 [OSCAR HUAMAN CABRERA]
'* MOTIVO MODIFIC.: Se agregaron validaciones para el guardado
**********************************************************/

namespace SGR.ViewModels.Presentation.Area.ModalPage
{
    using ComputerSystems;
    using ComputerSystems.WPF;
    using ComputerSystems.WPF.Acciones.Controles.ModalDialog;
    using ComputerSystems.WPF.Interfaces;
    using ComputerSystems.WPF.Notificaciones;
    using SGR.Models.Business;
    using SGR.Models.Entity;
    using SGR.ViewModels.Method;
    using SGR.ViewModels.Presentation;
    using System;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Input;

    public class VMSGR_Area : CmpNavigationService, CmpIModalDialog
    {
        private ESGR_Area _ESGR_Area;
        public ESGR_Area ESGR_Area
        {
            get
            {
                return _ESGR_Area;
            }
            set
            {
                _ESGR_Area = value;
                OnPropertyChanged("ESGR_Area");
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
                        CmpModalDialog.GetContent().Close();

                });
                if (value is ESGR_Area)
                {
                    ESGR_Area = (ESGR_Area)value;
                }
            }
        }

        #region COLECCIONES
        #endregion

        #region OBJETOS SECUNDARIOS
        #endregion

        #region ICOMMAND

        public ICommand IGuardar
        {
            get
            {
                return CmpICommand.GetICommand(async(T) =>
                {
                    if (MethodValidarDatos())
                    {
                        return;
                    }
                    await Task.Factory.StartNew(() =>
                    {
                        try
                        {
                            new BSGR_Area().TransArea(ESGR_Area);
                            CmpMessageBox.Show(SGRMessage.AdministratorArea, SGRMessageContent.ContentSaveOK, CmpButton.Aceptar, () =>
                            {
                                CmpModalDialog.GetContent().Close();
                            });
                        }
                        catch (Exception ex)
                        {
                            CmpMessageBox.Show(SGRMessage.AdministratorArea, ex.Message, CmpButton.Aceptar);
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

        private bool MethodValidarDatos()
        {
            bool vrValidaDato = false;
            if ( ESGR_Area.Area == null ||ESGR_Area.Area == string.Empty )
            {
                CmpMessageBox.Show(SGRMessage.AdministratorArea, "Campo Obligatorio: Área.", CmpButton.Aceptar);
                vrValidaDato = true;
            }
            return vrValidaDato;
        }

        #endregion
    }
}
