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

namespace SGR.ViewModels.Presentation.Impresora.ModalPage
{
    public class VMSGR_Impresora : CmpNavigationService, CmpIModalDialog
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
                    {
                        ImpresoraCaja = SGRVariables.ImpresoraCaja;
                        ImpresoraPedido = SGRVariables.ImpresoraPedido;
                        ImpresoraCopiaPedido = SGRVariables.ImpresoraCopiaPedido;
                    }
                });
            }
        }

        #region COLECCIONES
        
        #endregion

        #region OBJETOS SECUNDARIOS

        private string _ImpresoraPedido;
        public string ImpresoraPedido
        {
            get
            {
                return _ImpresoraPedido;
            }
            set
            {
                _ImpresoraPedido = value;
                OnPropertyChanged("ImpresoraPedido");
            }
        }

        private string _ImpresoraCopiaPedido;
        public string ImpresoraCopiaPedido
        {
            get
            {
                return _ImpresoraCopiaPedido;
            }
            set
            {
                _ImpresoraCopiaPedido = value;
                OnPropertyChanged("ImpresoraCopiaPedido");
            }
        }

        private string _ImpresoraCaja;
        public string ImpresoraCaja
        {
            get
            {
                return _ImpresoraCaja;
            }
            set
            {
                _ImpresoraCaja = value;
                OnPropertyChanged("ImpresoraCaja");
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
                    if (ImpresoraPedido.Trim().Length == 0)
                    {
                        CmpMessageBox.Show(SGRMessage.AdministratorImpuesto, "Ingrese una impresora", CmpButton.Aceptar);
                        return;
                    }
                    await Task.Factory.StartNew(() =>
                    {
                        try
                        {
                            new BSGR_Configuracion().TransConfiguracion(new ESGR_Configuracion
                            {
                                IdConfig = "IMP",
                                NivelConfig = "ST",
                                Valor = ImpresoraPedido,
                                Descripcion = "Impresora Pedido",
                                FlgEliminado = true
                            });

                            new BSGR_Configuracion().TransConfiguracion(new ESGR_Configuracion
                            {
                                IdConfig = "ICJ",
                                NivelConfig = "ST",
                                Valor = ImpresoraCaja,
                                Descripcion = "Impresora de Caja",
                                FlgEliminado = true
                            });

                            new BSGR_Configuracion().TransConfiguracion(new ESGR_Configuracion
                            {
                                IdConfig = "IPC",
                                NivelConfig = "ST",
                                Valor = ImpresoraCopiaPedido,
                                Descripcion = "Impresora de Pedido para Copia",
                                FlgEliminado = true
                            });

                            CmpMessageBox.Show(SGRMessage.AdministratorImpuesto, SGRMessageContent.ContentSaveOK, CmpButton.Aceptar, () =>
                            {
                                SGRVariables.ImpresoraCaja = ImpresoraCaja;
                                SGRVariables.ImpresoraPedido = ImpresoraPedido;
                                SGRVariables.ImpresoraCopiaPedido = ImpresoraCopiaPedido;
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
