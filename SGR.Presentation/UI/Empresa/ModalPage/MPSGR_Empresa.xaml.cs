/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   ABEL QUISPE ORELLANA
'* FCH. CREACIÓN : 05/12/2016
**********************************************************/
namespace SGR.Presentation.UI.Empresa.ModalPage
{
    using ComputerSystems.WPF.Acciones.Controles.ModalDialog;
    using SGR.ViewModels.Presentation.Empresa.ModalPage;

    public partial class MPSGR_Empresa : CmpModalPage
    {
        public MPSGR_Empresa()
        {
            InitializeComponent();
            DataContext = new VMSGR_Empresa();
        }
    }
}
