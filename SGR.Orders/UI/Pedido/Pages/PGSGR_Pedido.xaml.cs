/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   ABEL QUISPE ORELLANA
'* FCH. CREACIÓN : 26/12/2016
**********************************************************/
namespace SGR.Orders.UI.Pedido.Pages
{
    using ComputerSystems.WPF.Acciones.Controles.ModalDialog;
    using SGR.Orders.UI.Mesa.ModalPage;
    using SGR.ViewModels.Orders.Pedido.Pages;
    using System.Windows.Controls;

    public partial class PGSGR_Pedido : Page
    {
        public PGSGR_Pedido()
        {
            InitializeComponent();
            DataContext = new VMSGR_Pedido();
        }

        private void Page_Loaded_1(object sender, System.Windows.RoutedEventArgs e)
        {
            CmpModalDialog.Add(new MPSGR_MesasDisponibles());
        }

    }
}
