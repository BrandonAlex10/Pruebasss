/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   ABEL QUISPE ORELLANA
'* FCH. CREACIÓN : 05/12/2016
**********************************************************/
namespace SGR.Presentation.UI.ProductoCategoria.ModalPage
{
    using ComputerSystems.WPF.Acciones.Controles.ModalDialog;
    using SGR.ViewModels.Presentation.ProductoCategoria.ModalPage;

    public partial class MPSGR_ProductoCategoria : CmpModalPage
    {
        public MPSGR_ProductoCategoria()
        {
            InitializeComponent();
            DataContext = new VMSGR_ProductoCategoria();
        }
    }
}
