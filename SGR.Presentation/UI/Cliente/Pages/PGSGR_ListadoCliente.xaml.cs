/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   ABEL QUISPE ORELLANA
'* FCH. CREACIÓN : 02/12/2016
**********************************************************/
namespace SGR.Presentation.UI.Cliente.Pages
{
    using ComputerSystems.WPF.Acciones.Controles.Menus;
    using ComputerSystems.WPF.Acciones.Controles.ModalDialog;
    using SGR.Presentation.UI.Cliente.ModalPage;
    using SGR.ViewModels.Presentation.Cliente.Pages;
    using System.Windows.Controls;

    public partial class PGSGR_ListadoCliente : Page
    {
        public PGSGR_ListadoCliente()
        {
            InitializeComponent();
            DataContext = new VMSGR_ListadoCliente();
            CmpModalDialog.Add(new MPSGR_Cliente());//SERÁ LLAMADO DESDE EL VM
        }
    }
}
