using ComputerSystems.WPF.Acciones.Controles.ModalDialog;
using SGR.ViewModels.Presentation.AperturaCierreCaja.ModalPage;

namespace SGR.Presentation.UI.AperturaCierreCaja.ModalPage
{
    /// <summary>
    /// Lógica de interacción para MPSGR_AperturaCierreCaja.xaml
    /// </summary>
    public partial class MPSGR_AperturaCierreCaja : CmpModalPage
    {
        public MPSGR_AperturaCierreCaja()
        {
            InitializeComponent();
            DataContext = new VMSGR_AperturaCierreCaja();
        }
    }
}
