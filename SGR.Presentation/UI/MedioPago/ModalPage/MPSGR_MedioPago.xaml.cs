/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   ABEL QUISPE ORELLANA
'* FCH. CREACIÓN : 05/12/2016
**********************************************************/
namespace SGR.Presentation.UI.MedioPago.ModalPage
{
    using ComputerSystems.WPF.Acciones.Controles.ModalDialog;
    using SGR.ViewModels.Presentation.MedioPago.ModalPage;

    public partial class MPSGR_MedioPago : CmpModalPage
    {
        public MPSGR_MedioPago()
        {
            InitializeComponent();
            DataContext = new VMSGR_MedioPago();
        }
    }
}
