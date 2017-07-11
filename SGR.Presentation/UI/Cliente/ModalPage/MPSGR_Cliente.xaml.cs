/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   ABEL QUISPE ORELLANA
'* FCH. CREACIÓN : 02/12/2016
**********************************************************/
namespace SGR.Presentation.UI.Cliente.ModalPage
{
    using ComputerSystems.WPF.Acciones.Controles.ModalDialog;
    using SGR.ViewModels.Presentation.Cliente.ModalPage;

    public partial class MPSGR_Cliente : CmpModalPage
    {
        public MPSGR_Cliente()
        {
            InitializeComponent();
            DataContext = new VMSGR_Cliente() { PropertyImageContent = ImagenCaptcha.Source }; 
        }
    }
}
