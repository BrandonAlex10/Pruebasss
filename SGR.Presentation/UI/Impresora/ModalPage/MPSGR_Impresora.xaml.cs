using ComputerSystems.WPF.Acciones.Controles.ModalDialog;
using SGR.ViewModels.Presentation.Impresora.ModalPage;

namespace SGR.Presentation.UI.Impresora.ModalPage
{
    /// <summary>
    /// Lógica de interacción para PGSGR_Impresora.xaml
    /// </summary>
    public partial class MPSGR_Impresora : CmpModalPage
    {
        public MPSGR_Impresora()
        {
            InitializeComponent();
            DataContext = new VMSGR_Impresora();
        }
    }
}
