using ComputerSystems.WPF.Acciones.Controles.Menus;
using ComputerSystems.WPF.Acciones.Controles.ModalDialog;
using SGR.Presentation.UI.MovimientoCaja.ModalPage;
using SGR.ViewModels.Presentation.Movimiento_Caja.Pages;
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

namespace SGR.Presentation.UI.MovimientoCaja.Pages
{
    /// <summary>
    /// Lógica de interacción para PGSGR_ListadoMovimientoCaja.xaml
    /// </summary>
    public partial class PGSGR_ListadoMovimientoCaja : Page
    {
        public PGSGR_ListadoMovimientoCaja()
        {
            InitializeComponent();
            DataContext = new VMSGR_ListadoMovimientoCaja();
        }

        private void Page_Loaded_1(object sender, RoutedEventArgs e)
        {
            CmpModalDialog.Add(new MPSGR_MovimientoCaja());
        }
    }
}
