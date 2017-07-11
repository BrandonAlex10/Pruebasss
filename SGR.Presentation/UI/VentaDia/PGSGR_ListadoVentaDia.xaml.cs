using ComputerSystems.WPF.Acciones.Controles.ModalDialog;
using SGR.Presentation.UI.Producto.ModalPage;
using SGR.Presentation.UI.Usuario.ModalPage;
using SGR.ViewModels.Presentation.VentaDia.Pages;
using System.Windows;
using System.Windows.Controls;

namespace SGR.Presentation.UI.VentaDia
{
    /// <summary>
    /// Lógica de interacción para PGSGR_ListadoVentaDia.xaml
    /// </summary>
    public partial class PGSGR_ListadoVentaDia : Page
    {
        public PGSGR_ListadoVentaDia()
        {
            InitializeComponent();
            DataContext = new VMSGR_ListadoVentaDia();
        }

        private void Page_Loaded_1(object sender, RoutedEventArgs e)
        {
            CmpModalDialog.Add(new MPSGR_BuscadorProducto());
            CmpModalDialog.Add(new MPSGR_BuscadorUsuario());
        }
    }
}
