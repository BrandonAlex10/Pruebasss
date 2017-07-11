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

namespace SGR.ViewModels.Presentation.Impuesto.ModalPage
{
    using ComputerSystems;
    using ComputerSystems.WPF;
    using ComputerSystems.WPF.Acciones.Controles.ModalDialog;
    using ComputerSystems.WPF.Interfaces;
    using ComputerSystems.WPF.Notificaciones;
    using Newtonsoft.Json;
    using SGR.Models;
    using SGR.Models.Business;
    using SGR.Models.Entity;
    using SGR.ViewModels.Method;
    using SGR.ViewModels.Presentation;
    using System;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Input;

    public class VMSGR_Impuesto : CmpNavigationService, CmpIModalDialog
    {
        public object Parameter
        {
            set
            {
                Application.Current.Dispatcher.Invoke(async () =>
                {
                    var result = await MTSGR_GetPrivilege.Process(this.GetType(), SGRMessage.TitleMessage, SGRMessageContent.GetAccesoRestringido);
                    if (result == null)
                        CmpModalDialog.GetContent().Close();
                    else
                        IGV = ((ESGR_Retencion)value).IGV;
                });
                
            }
        }

        #region COLECCIONES
        #endregion

        #region OBJETOS SECUNDARIOS

        private decimal _IGV;
        public decimal IGV
        {
            get
            {
                return decimal.Round(_IGV, 2);
            }
            set
            {
                _IGV = value;
                OnPropertyChanged("IGV");
            }
        }

        #endregion

        #region ICOMMAND

        public ICommand IGuardar
        {
            get
            {
                return CmpICommand.GetICommand(async(T) =>
                {
                    if (IGV == 0)
                    {
                        CmpMessageBox.Show(SGRMessage.AdministratorImpuesto, "IGV no puede ser igual a 0", CmpButton.Aceptar);
                        return;
                    }
                    await Task.Factory.StartNew(() =>
                    {
                        try
                        {
                            new BSGR_Configuracion().TransConfiguracion(new ESGR_Configuracion
                            {
                                IdConfig = "IGV",
                                NivelConfig = "ST",
                                Valor = JsonConvert.SerializeObject(IGV),
                                Descripcion = "IGV",
                                FlgEliminado = true
                            });

                            if (SGRVariables.ESGR_Retencion == null)
                                SGRVariables.ESGR_Retencion = new ESGR_Retencion();

                            SGRVariables.ESGR_Retencion.IGV = IGV;
                            CmpMessageBox.Show(SGRMessage.AdministratorImpuesto, SGRMessageContent.ContentSaveOK, CmpButton.Aceptar, () =>
                            {
                                new CmpNavigationService().CloseCleanWindow();
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
                return CmpICommand.GetICommand((V) =>
                {
                    CmpModalDialog.GetContent().Close();
                });
            }
        }

        #endregion

        #region METODOS

        #endregion
    }
}
