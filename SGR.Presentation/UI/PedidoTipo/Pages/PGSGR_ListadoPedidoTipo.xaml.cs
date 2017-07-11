/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   ABEL QUISPE ORELLANA
'* FCH. CREACIÓN : 07/12/2016
**********************************************************/
namespace SGR.Presentation.UI.PedidoTipo.Pages
{
    using ComputerSystems.WPF.Acciones.Controles.Menus;
    using ComputerSystems.WPF.Acciones.Controles.ModalDialog;
    using SGR.Presentation.UI.PedidoTipo.ModalPage;
    using SGR.ViewModels.Presentation.PedidoTipo.Pages;
    using System.Windows.Controls;

    public partial class PGSGR_ListadoPedidoTipo : Page
    {
        public PGSGR_ListadoPedidoTipo()
        {
            InitializeComponent();
            DataContext = new VMSGR_ListadoPedidoTipo();
        }

        private void Page_Loaded_1(object sender, System.Windows.RoutedEventArgs e)
        {
            CmpModalDialog.Add(new MPSGR_PedidoTipo());//SERÁ LLAMADO DESDE EL VM
        }
    }
}
