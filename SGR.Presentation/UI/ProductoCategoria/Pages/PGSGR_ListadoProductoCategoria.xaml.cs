/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   ABEL QUISPE ORELLANA
'* FCH. CREACIÓN : 06/12/2016
**********************************************************/
namespace SGR.Presentation.UI.ProductoCategoria.Pages
{
    using ComputerSystems.WPF.Acciones.Controles.ModalDialog;
    using SGR.Presentation.UI.ProductoCategoria.ModalPage;
    using SGR.Presentation.UI.ProductoSubCategoria.ModalPage;
    using SGR.ViewModels.Presentation.ProductoCategoria.Pages;
    using System.Windows.Controls;

    public partial class PGSGR_ListadoProductoCategoria : Page
    {
        public PGSGR_ListadoProductoCategoria()
        {
            InitializeComponent();
            DataContext = new VMSGR_ListadoProductoCategoria();
            CmpModalDialog.Add(new MPSGR_ProductoCategoria());//SERÁ LLAMADO DESDE EL VM
            CmpModalDialog.Add(new MPSGR_ProductoSubCategoria());//SERÁ LLAMADO DESDE EL VM
        }
    }
}
