/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   ABEL QUISPE ORELLANA
'* FCH. CREACIÓN : 05/12/2016
**********************************************************/
namespace SGR.Presentation.UI.Administrar.Pages
{
    using ComputerSystems.WPF.Acciones.Controles.Menus;
    using ComputerSystems.WPF.Acciones.Controles.ModalDialog;
    using SGR.Presentation.UI.Area.Pages;
    using SGR.Presentation.UI.Caja.Pages;
    using SGR.Presentation.UI.Documento.Pages;
    using SGR.Presentation.UI.Impresora.ModalPage;
    using SGR.Presentation.UI.Impuesto.ModalPage;
    using SGR.Presentation.UI.MedioPago.Pages;
    using SGR.Presentation.UI.Mesa.Pages;
    using SGR.Presentation.UI.MesaArea.Pages;
    using SGR.Presentation.UI.MesaPredeterminada.ModalPage;
    using SGR.Presentation.UI.Moneda.Pages;
    using SGR.Presentation.UI.Notificaciones.ModalPage;
    using SGR.Presentation.UI.PedidoTipo.Pages;
    using SGR.Presentation.UI.UsuarioEmpresaSucursal.Pages;
    using SGR.ViewModels.Presentation.Administrar.Pages;
    using System.Windows.Controls;

    public partial class PGSGR_Administrar : Page
    {
        public PGSGR_Administrar()
        {
            InitializeComponent();
            DataContext = new VMSGR_Administrar();
        }

        private void Page_Loaded_1(object sender, System.Windows.RoutedEventArgs e)
        {

            CmpMenu.AddPage(new PGSGR_ListadoDocumento());

            CmpMenu.AddPage(new PGSGR_ListadoMoneda());//SERÁ LLAMADO DESDE EL VM

            CmpMenu.AddPage(new PGSGR_ListadoMedioPago());//SERÁ LLAMADO DESDE EL VM

            CmpMenu.AddPage(new PGSGR_ListadoMesaArea());//SERÁ LLAMADO DESDE EL VM

            CmpMenu.AddPage(new PGSGR_ListadoMesa());//SERÁ LLAMADO DESDE EL VM

            CmpMenu.AddPage(new PGSGR_ListadoArea());//SERÁ LLAMADO DESDE EL VM

            CmpMenu.AddPage(new PGSGR_ListadoPedidoTipo());//SERÁ LLAMADO DESDE EL VM

            CmpMenu.AddPage(new PGSGR_UsuarioEmpSucursal());//SERÁ LLAMADO DESDE EL VM

            CmpMenu.AddPage(new PGSGR_ListadoCaja()); // SERÁ LLAMADO DESDE EL VM

            CmpModalDialog.Add(new MPSGR_Impuesto());

            CmpModalDialog.Add(new MPSGR_MesaPredeterminada());

            CmpModalDialog.Add(new MPSGR_Notificaciones());

            CmpModalDialog.Add(new MPSGR_Impresora()); // SERÁ LLAMADO DESDE EL VM

        }
    }
}
