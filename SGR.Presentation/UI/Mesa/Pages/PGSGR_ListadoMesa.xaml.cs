/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   ABEL QUISPE ORELLANA
'* FCH. CREACIÓN : 07/12/2016
**********************************************************/
namespace SGR.Presentation.UI.Mesa.Pages
{
    using ComputerSystems.WPF.Acciones.Controles.ModalDialog;
    using SGR.Presentation.UI.Mesa.ModalPage;
    using SGR.ViewModels.Presentation.Mesa.Pages;
    using System.Windows.Controls;

    public partial class PGSGR_ListadoMesa : Page
    {
        public PGSGR_ListadoMesa()
        {
            InitializeComponent();
            DataContext = new VMSGR_ListadoMesa();
            CmpModalDialog.Add(new MPSGR_Mesa());//SERÁ LLAMADO DESDE EL VM
        }
    }
}
