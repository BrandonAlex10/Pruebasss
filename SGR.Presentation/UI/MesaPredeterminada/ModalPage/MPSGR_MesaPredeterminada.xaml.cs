/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   OSCAR HUAMAN CABRERA
'* FCH. CREACIÓN : 05/12/2016
**********************************************************/
namespace SGR.Presentation.UI.MesaPredeterminada.ModalPage
{
    using ComputerSystems.WPF.Acciones.Controles.ModalDialog;
    using SGR.ViewModels.Presentation.MesaPredeterminada.ModalPage;

    public partial class MPSGR_MesaPredeterminada : CmpModalPage
    {
        public MPSGR_MesaPredeterminada()
        {
            InitializeComponent();
            DataContext = new VMSGR_MesaPredeterminada();
        }
    }
}
