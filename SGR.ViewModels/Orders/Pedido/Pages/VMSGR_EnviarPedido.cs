/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*********************************************************
'* 
'* FCH. CREACIÓN : 26/12/2016 [ABEL QUISPE ORELLANA]
'* 
'*********************************************************
'* 
**********************************************************/

namespace SGR.ViewModels.Orders.Pedido.Pages
{
    using ComputerSystems;
    using ComputerSystems.WPF.Acciones.Controles.ModalDialog;
    using ComputerSystems.WPF.Notificaciones;
    using System.Windows.Input;

    public class VMSGR_EnviarPedido : CmpNavigationService
    {        
        #region COLECCIONES

        #endregion

        #region PROPERTY

        #endregion

        #region ICOMMAND

        public ICommand IAgregarItem
        {
            get
            {
                return CmpICommand.GetICommand((T) =>
                {
                    CmpModalDialog.GetContent().ShowDialog("MPSGR_MesasDisponibles", null);
                });
            }
        }

        public ICommand IVolver
        {
            get
            {
                return CmpICommand.GetICommand((T) =>
                {
                    Volver();
                });
            }
        }

        #endregion

        #region METODOS

        #endregion
    }
}
