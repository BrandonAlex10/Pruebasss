/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   ABEL QUISPE ORELLANA
'* FCH. CREACIÓN : 21/02/2017
**********************************************************/
namespace SGR.Presentation.UI.Venta.Pages
{
    using ComputerSystems.WPF.Acciones.Controles.ModalDialog;
    using SGR.Orders.UI.Mesa.ModalPage;
    using SGR.Presentation.UI.Producto.ModalPage;
    using SGR.Presentation.UI.Venta.ModalPage;
    using SGR.ViewModels.Presentation.Venta.Pages;
    using System.Windows.Controls;

    public partial class PGSRG_Venta : Page
    {
        public PGSRG_Venta()
        {
            InitializeComponent();
            DataContext = new VMSGR_Venta();
        }

        private void Page_Loaded_1(object sender, System.Windows.RoutedEventArgs e)
        {
            CmpModalDialog.Add(new MPSRG_VentaCuenta());
            CmpModalDialog.Add(new MPSGR_VentaRapida());
            CmpModalDialog.Add(new MPSGR_MesasDisponibles());
            CmpModalDialog.Add(new MPSGR_BuscadorProductoCartaDia());
            CmpModalDialog.Add(new MPSGR_MesasAtendidas());
        }
    }
}
