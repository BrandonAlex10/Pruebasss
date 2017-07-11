/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   ABEL QUISPE ORELLANA
'* FCH. CREACIÓN : 05/12/2016
**********************************************************/
namespace SGR.Presentation.UI.Producto.ModalPage
{
    using ComputerSystems.WPF.Acciones.Controles.ModalDialog;
    using SGR.ViewModels.Presentation.Producto.ModalPage;

    public partial class MPSGR_Producto : CmpModalPage
    {
        public MPSGR_Producto()
        {
            InitializeComponent();
            DataContext = new VMSGR_Producto();
        }
    }
}
