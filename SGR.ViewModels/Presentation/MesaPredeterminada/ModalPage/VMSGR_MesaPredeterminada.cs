/*********************************************************
'* CREADO POR	  : COMPUTER SYSTEMS SOLUTION
 * ********************************************************
 * 
'* FCH. CREACIÓN  : 01/12/2016 [OSCAR HUAMAN CABRERA]
'* 
**********************************************************/

namespace SGR.ViewModels.Presentation.MesaPredeterminada.ModalPage
{
    using ComputerSystems;
    using ComputerSystems.WPF;
    using ComputerSystems.WPF.Acciones.Controles.ModalDialog;
    using ComputerSystems.WPF.Interfaces;
    using ComputerSystems.WPF.Notificaciones;
    using SGR.Models;
    using SGR.Models.Business;
    using SGR.Models.Entity;
    using SGR.ViewModels.Method;
    using SGR.ViewModels.Presentation;
    using System;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Input;

    public class VMSGR_MesaPredeterminada : CmpNavigationService, CmpIModalDialog
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
                        MesaPredeterminada = (int)value;
                });
            }
        }

        #region COLECCIONES
        #endregion

        #region OBJETOS SECUNDARIOS

        private int _MesaPredeterminada;
        public int MesaPredeterminada
        {
            get
            {
                return _MesaPredeterminada;
            }
            set
            {
                _MesaPredeterminada = value;
                OnPropertyChanged("MesaPredeterminada");
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
                    if (MesaPredeterminada == 0)
                    {
                        CmpMessageBox.Show(SGRMessage.AdministratorImpuesto, "Número de Mesas no puede ser igual a 0", CmpButton.Aceptar);
                        return;
                    }
                    await Task.Factory.StartNew(() =>
                    {
                        try
                        {
                            new BSGR_Configuracion().TransConfiguracion(new ESGR_Configuracion
                            {
                                IdConfig = "MPD",
                                NivelConfig = "BD",
                                Valor = MesaPredeterminada.ToString(),
                                Descripcion = "Mesas Predeterminada",
                                FlgEliminado = true
                            });
                            SGRVariables.MesaPredeterminada = MesaPredeterminada;
                            CmpMessageBox.Show(SGRMessage.AdministratorImpuesto, SGRMessageContent.ContentSaveOK, CmpButton.Aceptar, () =>
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
