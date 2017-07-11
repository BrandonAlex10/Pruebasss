using ComputerSystems.WPF.Acciones.Controles.ModalDialog;
using SGR.Presentation.UI.AperturaCierreCaja.ModalPage;
using SGR.Presentation.UI.Caja.ModalPage;
using SGR.ViewModels.Presentation.Caja.Pages;
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

namespace SGR.Presentation.UI.Caja.Pages
{
    /// <summary>
    /// Lógica de interacción para MPSGR_ListadoCaja.xaml
    /// </summary>
    public partial class PGSGR_ListadoCaja : Page
    {
        public PGSGR_ListadoCaja()
        {
            InitializeComponent();
            DataContext = new VMSGR_ListadoCaja();
        }

        private void Page_Loaded_1(object sender, RoutedEventArgs e)
        {
            CmpModalDialog.Add(new MPSGR_Caja());
            CmpModalDialog.Add(new MPSGR_AperturaCierreCaja());
        }
    }
}
