/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   ABEL QUISPE ORELLANA
'* FCH. CREACIÓN : 07/12/2016
**********************************************************/
namespace SGR.Presentation.UI.Mesa.ModalPage
{
    using ComputerSystems.WPF.Acciones.Controles.ModalDialog;
    using SGR.ViewModels.Presentation.Mesa.ModalPage;

    public partial class MPSGR_Mesa : CmpModalPage
    {
        public MPSGR_Mesa()
        {
            InitializeComponent();
            DataContext = new VMSGR_Mesa();
        }
    }
}
