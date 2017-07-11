using ComputerSystems.WPF.Acciones.Controles.ModalDialog;
using SGR.ViewModels.Presentation.MovimientoCaja.ModalPage;

namespace SGR.Presentation.UI.MovimientoCaja.ModalPage
{
    /// <summary>
    /// Lógica de interacción para PGSGR_MovimientoCaja.xaml
    /// </summary>
    public partial class MPSGR_MovimientoCaja : CmpModalPage
    {
        public MPSGR_MovimientoCaja()
        {
            InitializeComponent();
            DataContext = new VMSGR_MovimientoCaja();
        }
    }
}
