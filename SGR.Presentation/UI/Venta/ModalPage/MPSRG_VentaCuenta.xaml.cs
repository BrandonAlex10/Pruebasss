/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   ABEL QUISPE ORELLANA
'* FCH. CREACIÓN : 21/02/2017
**********************************************************/
namespace SGR.Presentation.UI.Venta.ModalPage
{
    using ComputerSystems.WPF.Acciones.Controles.ModalDialog;
    using SGR.ViewModels.Presentation.Venta.ModalPage;

    public partial class MPSRG_VentaCuenta : CmpModalPage
    {
        public MPSRG_VentaCuenta()
        {
            InitializeComponent();
            DataContext = new VMSGR_VentaCuenta();
        }
    }
}
