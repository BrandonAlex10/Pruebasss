/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   ABEL QUISPE ORELLANA
'* FCH. CREACIÓN : 06/12/2016
**********************************************************/
namespace SGR.Presentation.UI.ProductoSubCategoria.ModalPage
{
    using ComputerSystems.WPF.Acciones.Controles.ModalDialog;
    using SGR.ViewModels.Presentation.ProductoSubCategoria.ModalPage;

    public partial class MPSGR_ProductoSubCategoria : CmpModalPage
    {
        public MPSGR_ProductoSubCategoria()
        {
            InitializeComponent();
            DataContext = new VMSGR_ProductoSubCategoria();
        }
    }
}
