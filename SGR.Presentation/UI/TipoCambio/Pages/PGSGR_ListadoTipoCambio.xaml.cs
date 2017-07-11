/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   ABEL QUISPE ORELLANA
'* FCH. CREACIÓN : 05/12/2016
**********************************************************/
namespace SGR.Presentation.UI.TipoCambio.Pages
{
    using ComputerSystems.WPF.Acciones.Controles.ModalDialog;
    using SGR.Presentation.UI.TipoCambio.ModalPage;
    using SGR.ViewModels.Presentation.TipoCambio.Pages;
    using System.Windows.Controls;

    public partial class PGSGR_ListadoTipoCambio : Page
    {
        public PGSGR_ListadoTipoCambio()
        {
            InitializeComponent();
            DataContext = new VMSGR_ListadoTipoCambio();
            CmpModalDialog.Add(new MPSGR_TipoCambio());//SERÁ LLAMADO DESDE EL VM
        }
    }
}
