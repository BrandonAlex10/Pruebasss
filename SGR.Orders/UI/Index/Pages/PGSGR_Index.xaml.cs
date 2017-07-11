/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
 * ********************************************************
 * 
'* FCH. CREACIÓN : 23/12/2016 [ABEL QUISPE ORELLANA]
 * 
**********************************************************/

namespace SGR.Orders.UI.Index.Pages
{
    using ComputerSystems.WPF.Acciones.Controles.Menus;
    using ComputerSystems.WPF.Acciones.Controles.ModalDialog;
    using SGR.Orders.UI.Mesa.ModalPage;
    using SGR.Orders.UI.Pedido.Pages;
    using System.Windows.Controls;

    public partial class PGSGR_Index : Page
    {
        public PGSGR_Index()
        {
            InitializeComponent();
            DataContext = new SGR.ViewModels.Orders.Index.Pages.VMSGR_Index();
        }

        private void Page_Loaded_1(object sender, System.Windows.RoutedEventArgs e)
        {
            CmpMenu.AddPage(new PGSGR_Pedido());
            CmpModalDialog.Add(new MPSGR_MesasDisponibles());
            CmpModalDialog.Add(new MPSGR_MesasAtendidas());
            CmpModalDialog.Add(new MPSGR_MesasCerrarAnular());
        }
    }
}
