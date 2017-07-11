/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   OSCAR HUAMAN CABRERA
'* FCH. CREACIÓN : 05/12/2016
**********************************************************/
namespace SGR.Presentation.UI.Area.ModalPage
{
    using ComputerSystems.WPF.Acciones.Controles.ModalDialog;
    using SGR.ViewModels.Presentation.Area.ModalPage;

    public partial class MPSGR_Area : CmpModalPage
    {
        public MPSGR_Area()
        {
            InitializeComponent();
            DataContext = new VMSGR_Area();
        }
    }
}
