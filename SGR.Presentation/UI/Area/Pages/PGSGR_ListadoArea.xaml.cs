/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   OSCAR HUAMAN CABRERA
'* FCH. CREACIÓN : 05/12/2016
**********************************************************/
namespace SGR.Presentation.UI.Area.Pages
{
    using ComputerSystems.WPF.Acciones.Controles.ModalDialog;
    using SGR.Presentation.UI.Area.ModalPage;
    using SGR.ViewModels.Presentation.Area.Pages;
    using System.Windows.Controls;

    public partial class PGSGR_ListadoArea : Page
    {
        public PGSGR_ListadoArea()
        {
            InitializeComponent();
            DataContext = new VMSGR_ListadoArea();
        }

        private void Page_Loaded_1(object sender, System.Windows.RoutedEventArgs e)
        {
            CmpModalDialog.Add(new MPSGR_Area());//SERÁ LLAMADO DESDE EL VM
        }
    }
}
