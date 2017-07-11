/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   ABEL QUISPE ORELLANA
'* FCH. CREACIÓN : 07/12/2016
**********************************************************/
namespace SGR.Presentation.UI.PedidoTipo.ModalPage
{
    using ComputerSystems.WPF.Acciones.Controles.ModalDialog;
    using SGR.ViewModels.Presentation.PedidoTipo.ModalPage;

    public partial class MPSGR_PedidoTipo : CmpModalPage
    {
        public MPSGR_PedidoTipo()
        {
            InitializeComponent();
            DataContext = new VMSGR_PedidoTipo();
        }
    }
}
