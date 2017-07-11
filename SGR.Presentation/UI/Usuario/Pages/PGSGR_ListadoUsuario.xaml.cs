/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   ABEL QUISPE ORELLANA
'* FCH. CREACIÓN : 05/12/2016
**********************************************************/
namespace SGR.Presentation.UI.Usuario.Pages
{
    using ComputerSystems.WPF.Acciones.Controles.ModalDialog;
    using SGR.Presentation.UI.Usuario.ModalPage;
    using SGR.ViewModels.Presentation.Usuario.Pages;
    using System.Windows.Controls;

    public partial class PGSGR_ListadoUsuario : Page
    {
        public PGSGR_ListadoUsuario()
        {
            InitializeComponent();
            DataContext = new VMSGR_ListadoUsuario();
        }

        private void Page_Loaded_1(object sender, System.Windows.RoutedEventArgs e)
        {
            CmpModalDialog.Add(new MPSGR_Usuario());
            CmpModalDialog.Add(new MPSGR_HabilitarUsuario());
        }
    }
}
