using ComputerSystems.WPF.Acciones.Controles.ModalDialog;
using SGR.ViewModels.Presentation.Notificaciones.ModalPage;

namespace SGR.Presentation.UI.Notificaciones.ModalPage
{
    /// <summary>
    /// Lógica de interacción para PGSGR_Impresora.xaml
    /// </summary>
    public partial class MPSGR_Notificaciones : CmpModalPage
    {
        public MPSGR_Notificaciones()
        {
            InitializeComponent();
            DataContext = new VMSGR_Notificaciones();
        }
    }
}
