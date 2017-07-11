/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   ABEL QUISPE ORELLANA
'* FCH. CREACIÓN : 23/12/2016
**********************************************************/
namespace SGR.Orders.UI.Login.Pages
{
    using ComputerSystems.WPF.Acciones.Controles.Menus;
    using SGR.Orders.UI.Index.Pages;
    using SGR.ViewModels.Presentation.Login;
    using System.Windows.Controls;

    public partial class PGSGR_Login : Page
    {
        public PGSGR_Login()
        {
            InitializeComponent();
        }

        private void Page_Loaded_1(object sender, System.Windows.RoutedEventArgs e)
        {
            CmpMenu.AddPage(new PGSGR_Index());
        }
    }
}
