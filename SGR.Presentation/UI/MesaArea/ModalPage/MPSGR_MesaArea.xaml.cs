/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   ABEL QUISPE ORELLANA
'* FCH. CREACIÓN : 07/12/2016
**********************************************************/
namespace SGR.Presentation.UI.MesaArea.ModalPage
{
    using ComputerSystems.WPF.Acciones.Controles.ModalDialog;
    using SGR.ViewModels.Presentation.MesaArea.ModalPage;
    using SGR.ViewModels.Presentation.MesaArea.Pages;
    using System.Windows.Controls;

    public partial class MPSGR_MesaArea : CmpModalPage
    {
        public MPSGR_MesaArea()
        {
            InitializeComponent();
            DataContext = new VMSGR_MesaArea();
        }
    }
}
