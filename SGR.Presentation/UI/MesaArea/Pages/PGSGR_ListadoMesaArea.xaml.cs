/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   ABEL QUISPE ORELLANA
'* FCH. CREACIÓN : 07/12/2016
**********************************************************/
namespace SGR.Presentation.UI.MesaArea.Pages
{
    using ComputerSystems.WPF.Acciones.Controles.Menus;
    using ComputerSystems.WPF.Acciones.Controles.ModalDialog;
    using SGR.Presentation.UI.MesaArea.ModalPage;
    using SGR.ViewModels.Presentation.MesaArea.Pages;
    using System.Windows.Controls;

    public partial class PGSGR_ListadoMesaArea : Page
    {
        public PGSGR_ListadoMesaArea()
        {
            InitializeComponent();
            DataContext = new VMSGR_ListadoMesaArea();
            CmpModalDialog.Add(new MPSGR_MesaArea());//SERÁ LLAMADO DESDE EL VM
        }
    }
}
