using ComputerSystems.WPF.Acciones.Controles.Menus;
using ComputerSystems.WPF.Acciones.Controles.ModalDialog;
using SGR.Presentation.UI.Producto.ModalPage;
using SGR.ViewModels.Presentation.Venta.Pages;
using System.Windows.Controls;

namespace SGR.Presentation.UI.Venta.Pages
{
    /// <summary>
    /// Lógica de interacción para PSGR_ListadoVenta.xaml
    /// </summary>
    public partial class PGSGR_ListadoVenta : Page
    {
        public PGSGR_ListadoVenta()
        {
            InitializeComponent();
            DataContext = new VMSGR_ListadoVenta();
        }

        private void Page_Loaded_1(object sender, System.Windows.RoutedEventArgs e)
        {
            CmpMenu.AddPage(new PGSRG_Venta());
            //CmpModalDialog.Add(new MPSGR_MesasAtendidas());
        }
    }
}
