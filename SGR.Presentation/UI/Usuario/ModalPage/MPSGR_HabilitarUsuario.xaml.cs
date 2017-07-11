namespace SGR.Presentation.UI.Usuario.ModalPage
{
    using ComputerSystems.WPF.Acciones.Controles.ModalDialog;
    using SGR.ViewModels.Presentation.Usuario.ModalPage;
    using SGR.ViewModels.Presentation.Usuario.Pages;
    using System.Windows.Controls;

    public partial class MPSGR_HabilitarUsuario : CmpModalPage
    {
        public MPSGR_HabilitarUsuario()
        {
            InitializeComponent();
            DataContext = new VMSGR_HabilitarUsuario();
        }
    }
}
