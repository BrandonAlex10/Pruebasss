/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   ABEL QUISPE ORELLANA
'* FCH. CREACIÓN : 05/12/2016
**********************************************************/
namespace SGR.Presentation.UI.Documento.Pages
{
    using ComputerSystems.WPF.Acciones.Controles.ModalDialog;
    using SGR.Presentation.UI.Documento.ModalPage;
    using SGR.ViewModels.Presentation.Documento.Pages;
    using System.Windows.Controls;

    public partial class PGSGR_ListadoDocumento : Page
    {
        public PGSGR_ListadoDocumento()
        {
            InitializeComponent();
            DataContext = new VMSGR_ListadoDocumento();
            CmpModalDialog.Add(new MPSGR_Documento());//SERÁ LLAMADO DESDE EL VM
        }
    }
}
