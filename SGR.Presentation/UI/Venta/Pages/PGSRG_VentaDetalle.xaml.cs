/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   ABEL QUISPE ORELLANA
'* FCH. CREACIÓN : 21/02/2017
**********************************************************/
namespace SGR.Presentation.UI.Venta.Pages
{
    using SGR.ViewModels.Presentation.Venta.ModalPage;

    public partial class PGSRG_VentaDetalle 
    {
        public PGSRG_VentaDetalle()
        {
            InitializeComponent();
            DataContext = new VMSGR_VentaCuenta();
        }
    }
}
