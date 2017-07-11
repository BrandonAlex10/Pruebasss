/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   ABEL QUISPE ORELLANA
'* FCH. CREACIÓN : 05/12/2016
**********************************************************/
namespace SGR.Presentation.UI.Perfil.ModalPage
{
    using ComputerSystems.WPF.Acciones.Controles.ModalDialog;
    using SGR.ViewModels.Presentation.Perfil.ModalPage;

    public partial class MPSGR_Perfil : CmpModalPage
    {
        public MPSGR_Perfil()
        {
            InitializeComponent();
            DataContext = new VMSGR_Perfil();
        }
    }
}
