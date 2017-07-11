/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   OSCAR HUAMAN CABRERA
'* FCH. CREACIÓN : 05/12/2016
**********************************************************/
namespace SGR.Presentation.UI.Impuesto.ModalPage
{
    using ComputerSystems.WPF.Acciones.Controles.ModalDialog;
    using SGR.ViewModels.Presentation.Impuesto.ModalPage;

    public partial class MPSGR_Impuesto : CmpModalPage
    {
        public MPSGR_Impuesto()
        {
            InitializeComponent();
            DataContext = new VMSGR_Impuesto();
        }
    }
}
