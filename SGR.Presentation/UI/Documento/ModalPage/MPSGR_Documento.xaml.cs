/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   ABEL QUISPE ORELLANA
'* FCH. CREACIÓN : 05/12/2016
**********************************************************/
namespace SGR.Presentation.UI.Documento.ModalPage
{
    using ComputerSystems.WPF.Acciones.Controles.ModalDialog;
    using SGR.ViewModels.Presentation.Documento.ModalPage;

    public partial class MPSGR_Documento : CmpModalPage
    {
        public MPSGR_Documento()
        {
            InitializeComponent();
            DataContext = new VMSGR_Documento();
        }
    }
}
