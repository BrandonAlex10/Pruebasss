/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   ABEL QUISPE ORELLANA
'* FCH. CREACIÓN : 05/12/2016
**********************************************************/
namespace SGR.Presentation.UI.Usuario.ModalPage
{
    using ComputerSystems.WPF.Acciones.Controles.ModalDialog;
    using SGR.ViewModels.Presentation.Usuario.ModalPage;

    public partial class MPSGR_Usuario : CmpModalPage
    {
        public MPSGR_Usuario()
        {
            InitializeComponent();
            DataContext = new VMSGR_Usuario();
        }
    }
}
