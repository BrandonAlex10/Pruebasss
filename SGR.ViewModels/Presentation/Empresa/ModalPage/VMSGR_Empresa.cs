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
 * 
**********************************************************/

namespace SGR.ViewModels.Presentation.Empresa.ModalPage
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

    public class VMSGR_Empresa : CmpNavigationService, CmpIModalDialog
    {
        private ESGR_Empresa _ESGR_Empresa;
        public ESGR_Empresa ESGR_Empresa
        {
            get
            {
                if (_ESGR_Empresa == null)
                    _ESGR_Empresa = new ESGR_Empresa();
                return _ESGR_Empresa;
            }
            set
            {
                _ESGR_Empresa = value;
                OnPropertyChanged("ESGR_Empresa");
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
                    else if (value is ESGR_Empresa)
                    {
                        ESGR_Empresa = (ESGR_Empresa)value;
                    }
                });
            }
        }

        #region COLECCIONES
        #endregion

        #region OBJETOS SECUNDARIOS
        #endregion

        #region PROPERTY
        #endregion

        #region ICOMMAND

        public ICommand IGuardar
        {
            get
            {
                return CmpICommand.GetICommand(async(G) =>
                {
                    if (MethodValidarDatos())
                        return;

                    await Task.Factory.StartNew(() =>
                    {
                        try
                        {
                            new BSGR_Empresa().TransEmpresa(ESGR_Empresa);
                            CmpMessageBox.Show(SGRMessage.AdministratorEmpresa, SGRMessageContent.ContentSaveOK, CmpButton.Aceptar, () =>
                            {
                                CmpModalDialog.GetContent().Close();
                            });
                        }
                        catch (Exception ex)
                        {
                            CmpMessageBox.Show(SGRMessage.AdministratorEmpresa, ex.Message, CmpButton.Aceptar);
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
            bool vrValidaDato = false;
            if (ESGR_Empresa.RazonSocial == string.Empty || ESGR_Empresa.RazonSocial == null)
            {
                CmpMessageBox.Show(SGRMessage.AdministratorEmpresa, "Campo Obligatorio: Razón Social.", CmpButton.Aceptar);
                vrValidaDato = true;
            }
            else if (ESGR_Empresa.Ruc == string.Empty || ESGR_Empresa.Ruc == null)
            {
                CmpMessageBox.Show(SGRMessage.AdministratorEmpresa, "Campo Obligatorio: RUC de la empresa.", CmpButton.Aceptar);
                vrValidaDato = true;
            }
            else if (ESGR_Empresa.DireccionFiscal == string.Empty || ESGR_Empresa.DireccionFiscal == null)
            {
                CmpMessageBox.Show(SGRMessage.AdministratorEmpresa, "Campo Obligatorio: Direccion Fiscal.", CmpButton.Aceptar);
                vrValidaDato = true;
            }
            return vrValidaDato;
        }

        #endregion
    }
}
