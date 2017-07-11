using ComputerSystems.WPF.Acciones.Controles.ModalDialog;
using SGR.ViewModels.Presentation.Usuario.ModalPage;

namespace SGR.Presentation.UI.Usuario.ModalPage
{
    /// <summary>
    /// Lógica de interacción para MPSGR_BuscadorUsuario.xaml
    /// </summary>
    public partial class MPSGR_BuscadorUsuario : CmpModalPage
    {
        public MPSGR_BuscadorUsuario()
        {
            InitializeComponent();
            DataContext = new MVSGR_BuscadorUsuario();
        }
    }
}
