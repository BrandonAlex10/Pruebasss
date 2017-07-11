using ComputerSystems.WPF.Acciones.Controles.ModalDialog;
using SGR.ViewModels.Presentation.Producto.ModalPage;

namespace SGR.Presentation.UI.Producto.ModalPage
{
    /// <summary>
    /// Lógica de interacción para PGSGR_BuscadorProducto.xaml
    /// </summary>
    public partial class MPSGR_BuscadorProductoCartaDia : CmpModalPage
    {
        public MPSGR_BuscadorProductoCartaDia()
        {
            InitializeComponent();
            DataContext = new VMSGR_BuscadorProductoCartaDia();
        }
    }
}
