/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
 * 
'* FCH. CREACIÓN : 28/11/2016 [ABEL QUISPE ORELLANA]
'* ********************************************************
'* 
'* FECHA MODIFIC. : 17/12/2016 [OSCAR HUAMAN CABRERA]
'* MOTIVO MODIFIC.: Se agregó funcionalidad de botones
**********************************************************/

namespace SGR.ViewModels.Presentation.PedidoTipo.ModalPage
{
    using ComputerSystems;
    using ComputerSystems.WPF;
    using ComputerSystems.WPF.Acciones.Controles.ModalDialog;
    using ComputerSystems.WPF.Interfaces;
    using SGR.Models.Business;
    using SGR.Models.Entity;
    using SGR.ViewModels.Method;
    using System;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Input;

    public class VMSGR_PedidoTipo : CmpNotifyPropertyChanged, CmpIModalDialog
    {
        private ESGR_PedidoTipo _ESGR_PedidoTipo;
        public ESGR_PedidoTipo ESGR_PedidoTipo
        {
            get
            {
                return _ESGR_PedidoTipo;
            }
            set
            {
                _ESGR_PedidoTipo = value;
                OnPropertyChanged("ESGR_PedidoTipo");
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
                    else if (value is ESGR_PedidoTipo)
                        ESGR_PedidoTipo = (ESGR_PedidoTipo)value;
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
                return CmpICommand.GetICommand(async(T) =>
                    {
                        if(MethodValidarDatos())
                        return;

                        await Task.Factory.StartNew(() =>
                        {
                            try
                            {
                                new BSGR_PedidoTipo().TransPedidoTipo(ESGR_PedidoTipo);
                                CmpMessageBox.Show(SGRMessage.AdministratorPedidoTipo, SGRMessageContent.ContentSaveOK, CmpButton.Aceptar, () =>
                                {
                                    CmpModalDialog.GetContent().Close();
                                });
                            }
                            catch (Exception ex)
                            {
                                CmpMessageBox.Show(SGRMessage.AdministratorPedidoTipo, ex.Message, CmpButton.Aceptar);
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
            if ( ESGR_PedidoTipo.PedidoTipo == null)
            {
                CmpMessageBox.Show(SGRMessage.AdministratorPedidoTipo, "Campo Obligatorio: Pedido Tipo.", CmpButton.Aceptar);
                vrValidaDato = true;
            }
            else if (ESGR_PedidoTipo.PedidoTipo.Trim().Length == 0)
            {
                CmpMessageBox.Show(SGRMessage.AdministratorPedidoTipo, "Campo Obligatorio: Pedido Tipo.", CmpButton.Aceptar);
                vrValidaDato = true;
            }
            return vrValidaDato;
        }

        #endregion
    }
}
