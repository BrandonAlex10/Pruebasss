using ComputerSystems.WPF.Acciones.Controles.ModalDialog;
using SGR.Presentation.UI.Producto.ModalPage;
using SGR.Presentation.UI.Usuario.ModalPage;
using SGR.ViewModels.Presentation.PedidoAnulado.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SGR.Presentation.UI.PedidoAnulado.Pages
{
    /// <summary>
    /// Lógica de interacción para PGSGR_ListadoPedidoAnulado.xaml
    /// </summary>
    public partial class PGSGR_ListadoPedidoAnulado : Page
    {
        public PGSGR_ListadoPedidoAnulado()
        {
            InitializeComponent();
            DataContext = new VMSGR_ListadoPedidoAnulado();
        }

        private void Page_Loaded_1(object sender, RoutedEventArgs e)
        {
            CmpModalDialog.Add(new MPSGR_BuscadorProducto());
            CmpModalDialog.Add(new MPSGR_BuscadorUsuario());
        }
    }
}
