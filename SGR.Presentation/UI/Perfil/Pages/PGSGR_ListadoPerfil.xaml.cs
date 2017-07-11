/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   ABEL QUISPE ORELLANA
'* FCH. CREACIÓN : 05/12/2016
**********************************************************/
namespace SGR.Presentation.UI.Perfil.Pages
{
    using ComputerSystems.WPF.Acciones.Controles.Menus;
    using ComputerSystems.WPF.Acciones.Controles.ModalDialog;
    using SGR.Presentation.UI.Perfil.ModalPage;
    using SGR.ViewModels.Presentation.Perfil.Pages;
    using System.Windows.Controls;

    public partial class PGSGR_ListadoPerfil : Page
    {
        public PGSGR_ListadoPerfil()
        {
            InitializeComponent();
            DataContext = new VMSGR_ListadoPerfil();
            CmpModalDialog.Add(new MPSGR_Perfil());
        }
    }
}
