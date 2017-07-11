/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   ABEL QUISPE ORELLANA
'* FCH. CREACIÓN : 05/12/2016
**********************************************************/
namespace SGR.Presentation.UI.Moneda.Pages
{
    using ComputerSystems.WPF.Acciones.Controles.ModalDialog;
    using SGR.Presentation.UI.Moneda.ModalPage;
    using SGR.ViewModels.Presentation.Moneda.Pages;
    using System.Windows.Controls;

    public partial class PGSGR_ListadoMoneda : Page
    {
        public PGSGR_ListadoMoneda()
        {
            InitializeComponent();
            DataContext = new VMSGR_ListadoMoneda();
        }

        private void Page_Loaded_1(object sender, System.Windows.RoutedEventArgs e)
        {
            CmpModalDialog.Add(new MPSGR_Moneda());//SERÁ LLAMADO DESDE EL VM
        }
    }
}
