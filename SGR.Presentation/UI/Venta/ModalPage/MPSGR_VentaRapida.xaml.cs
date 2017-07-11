using ComputerSystems.WPF.Acciones.Controles.ModalDialog;
using SGR.ViewModels.Presentation.Venta.ModalPage;

namespace SGR.Presentation.UI.Venta.ModalPage
{
    /// <summary>
    /// Lógica de interacción para MPSGR_VentaRapida.xaml
    /// </summary>
    public partial class MPSGR_VentaRapida : CmpModalPage
    {
        public MPSGR_VentaRapida()
        {
            InitializeComponent();
            DataContext = new VMSGR_VentaRapida();
        }
    }
}
