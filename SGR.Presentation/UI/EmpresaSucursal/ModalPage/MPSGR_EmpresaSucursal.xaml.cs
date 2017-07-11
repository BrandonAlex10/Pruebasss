/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   ABEL QUISPE ORELLANA
'* FCH. CREACIÓN : 05/12/2016
**********************************************************/
namespace SGR.Presentation.UI.EmpresaSucursal.ModalPage
{
    using ComputerSystems.WPF.Acciones.Controles.ModalDialog;
    using SGR.ViewModels.Presentation.EmpresaSucursal.ModalPage;

    public partial class MPSGR_EmpresaSucursal : CmpModalPage
    {
        public MPSGR_EmpresaSucursal()
        {
            InitializeComponent();
            DataContext = new VMSGR_EmpresaSucursal();
        }

    }
}
