/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   ABEL QUISPE ORELLANA
'* FCH. CREACIÓN : 05/12/2016
**********************************************************/
namespace SGR.Presentation.UI.Moneda.ModalPage
{
    using ComputerSystems.WPF.Acciones.Controles.ModalDialog;
    using SGR.ViewModels.Presentation.Moneda.ModalPage;

    public partial class MPSGR_Moneda : CmpModalPage
    {
        public MPSGR_Moneda()
        {
            InitializeComponent();
            DataContext = new VMSGR_Moneda();
        }
    }
}
