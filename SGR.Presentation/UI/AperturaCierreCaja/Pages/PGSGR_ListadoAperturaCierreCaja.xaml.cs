/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   ABEL QUISPE ORELLANA
'* FCH. CREACIÓN : 05/12/2016
**********************************************************/
namespace SGR.Presentation.UI.CierreCaja.Pages
{
    using ComputerSystems.WPF.Acciones.Controles.ModalDialog;
    using SGR.Presentation.UI.AperturaCierreCaja.ModalPage;
    using SGR.ViewModels.Presentation.AperturaCierreCaja.Pages;
    using System.Windows.Controls;
    public partial class PGSGR_ListadoCierreCaja : Page
    {
        public PGSGR_ListadoCierreCaja()
        {
            InitializeComponent();
            DataContext = new VMSGR_ListadoAperturaCierreCaja();
        }

        private void Page_Loaded_1(object sender, System.Windows.RoutedEventArgs e)
        {
            CmpModalDialog.Add(new MPSGR_AperturaCierreCaja());
        }
    }
}
