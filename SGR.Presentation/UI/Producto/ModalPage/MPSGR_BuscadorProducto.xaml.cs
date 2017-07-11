using ComputerSystems.WPF.Acciones.Controles.ModalDialog;
using SGR.ViewModels.Presentation.Producto.ModalPage;
using SGR.ViewModels.Presentation.Producto.Pages;

namespace SGR.Presentation.UI.Producto.ModalPage
{
    /// <summary>
    /// Lógica de interacción para PGSGR_BuscadorProducto.xaml
    /// </summary>
    public partial class MPSGR_BuscadorProducto : CmpModalPage
    {
        public MPSGR_BuscadorProducto()
        {
            InitializeComponent();
            DataContext = new VMSGR_BuscadorProducto();
        }
    }
}
