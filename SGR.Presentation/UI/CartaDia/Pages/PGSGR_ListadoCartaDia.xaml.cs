/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   ABEL QUISPE ORELLANA
'* FCH. CREACIÓN : 05/12/2016
**********************************************************/
namespace SGR.Presentation.UI.CartaDia.Pages
{
    using ComputerSystems.WPF.Acciones.Controles.Menus;
    using SGR.ViewModels.Presentation.CartaDia.Controls;
    using System.Windows.Controls;

    public partial class PGSGR_ListadoCartaDia : Page
    {
        public PGSGR_ListadoCartaDia()
        {
            InitializeComponent();
            DataContext = new VMSGR_ListadoCartaDia();
            CmpMenu.AddPage(new PGSGR_CartaDia());//SERÁ LLAMADO DESDE EL VM
        }
    }
}
