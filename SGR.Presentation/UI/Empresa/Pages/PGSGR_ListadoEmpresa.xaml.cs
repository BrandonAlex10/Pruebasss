/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   ABEL QUISPE ORELLANA
'* FCH. CREACIÓN : 05/12/2016
**********************************************************/
namespace SGR.Presentation.UI.Empresa.Pages
{
    using ComputerSystems.WPF.Acciones.Controles.ModalDialog;
    using SGR.Presentation.UI.Empresa.ModalPage;
    using SGR.Presentation.UI.EmpresaSucursal.ModalPage;
    using SGR.ViewModels.Presentation.Empresa.Pages;
    using System.Windows.Controls;

    public partial class PGSGR_ListadoEmpresa : Page
    {
        public PGSGR_ListadoEmpresa()
        {
            InitializeComponent();
            DataContext = new VMSGR_ListadoEmpresa();
        }

        private void Page_Loaded_1(object sender, System.Windows.RoutedEventArgs e)
        {
            CmpModalDialog.Add(new MPSGR_Empresa());//SERÁ LLAMADO DESDE EL VM
            CmpModalDialog.Add(new MPSGR_EmpresaSucursal());//SERÁ LLAMADO DESDE EL VM
        }
    }
}
