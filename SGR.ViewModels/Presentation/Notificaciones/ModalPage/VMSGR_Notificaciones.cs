using ComputerSystems;
using ComputerSystems.WPF;
using ComputerSystems.WPF.Acciones.Controles.ModalDialog;
using ComputerSystems.WPF.Interfaces;
using ComputerSystems.WPF.Notificaciones;
using SGR.Models;
using SGR.Models.Business;
using SGR.Models.Entity;
using SGR.ViewModels.Method;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SGR.ViewModels.Presentation.Notificaciones.ModalPage
{
    public class VMSGR_Notificaciones : CmpNavigationService, CmpIModalDialog
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
                        Notificacion = SGRVariables.NotificacionPreCuenta;
                });
            }
        }

        #region COLECCIONES
        
        #endregion

        #region OBJETOS SECUNDARIOS

        private string _Notificacion;
        public string Notificacion
        {
            get
            {
                return _Notificacion;
            }
            set
            {
                _Notificacion = value;
                OnPropertyChanged("Notificacion");
            }
        }

        #endregion

        #region ICOMMAND

        public ICommand IGuardar
        {
            get
            {
                return CmpICommand.GetICommand(async (T) =>
                {
                    await Task.Factory.StartNew(() =>
                    {
                        try
                        {
                            new BSGR_Configuracion().TransConfiguracion(new ESGR_Configuracion
                            {
                                IdConfig = "NTF",
                                NivelConfig = "ST",
                                Valor = Notificacion,
                                Descripcion = "Notificación de Pie de Pre Cuenta",
                                FlgEliminado = true
                            });

                            CmpMessageBox.Show(SGRMessage.AdministratorImpuesto, SGRMessageContent.ContentSaveOK, CmpButton.Aceptar, () =>
                            {
                                SGRVariables.NotificacionPreCuenta = Notificacion;
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
