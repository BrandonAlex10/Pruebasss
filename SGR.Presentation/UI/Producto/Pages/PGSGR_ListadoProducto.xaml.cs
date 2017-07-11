/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   ABEL QUISPE ORELLANA
'* FCH. CREACIÓN : 05/12/2016
**********************************************************/
namespace SGR.Presentation.UI.Producto.Pages
{
    using ComputerSystems.WPF.Acciones.Controles.ModalDialog;
    using SGR.Presentation.UI.Producto.ModalPage;
    using SGR.ViewModels.Presentation.Producto.Pages;
    using System.Windows.Controls;

    public partial class PGSGR_ListadoProducto : Page
    {
        public PGSGR_ListadoProducto()
        {
            InitializeComponent();
            DataContext = new VMSGR_ListadoProducto();
            CmpModalDialog.Add(new MPSGR_Producto());
        }
    }
}
