/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   ABEL QUISPE ORELLANA
'* FCH. CREACIÓN : 05/12/2016
**********************************************************/
namespace SGR.Presentation.UI.MedioPago.Pages
{
    using ComputerSystems.WPF.Acciones.Controles.ModalDialog;
    using SGR.Presentation.UI.MedioPago.ModalPage;
    using SGR.ViewModels.Presentation.MedioPago.Pages;
    using System.Windows.Controls;

    public partial class PGSGR_ListadoMedioPago : Page
    {
        public PGSGR_ListadoMedioPago()
        {
            InitializeComponent();
            DataContext = new VMSGR_ListadoMedioPago();
        }

        private void Page_Loaded_1(object sender, System.Windows.RoutedEventArgs e)
        {
            CmpModalDialog.Add(new MPSGR_MedioPago());//SERÁ LLAMADO DESDE EL VM
        }
    }
}
