/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   ABEL QUISPE ORELLANA
'* FCH. CREACIÓN : 05/12/2016
**********************************************************/
namespace SGR.Presentation.UI.TipoCambio.ModalPage
{
    using ComputerSystems.WPF.Acciones.Controles.ModalDialog;
    using SGR.ViewModels.Presentation.TipoCambio.ModalPage;

    public partial class MPSGR_TipoCambio : CmpModalPage
    {
        public MPSGR_TipoCambio()
        {
            InitializeComponent();
            DataContext = new VMSGR_TipoCambio();
        }
    }
}
