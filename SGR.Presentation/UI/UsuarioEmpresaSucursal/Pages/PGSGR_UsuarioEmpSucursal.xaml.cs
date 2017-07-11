/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   ABEL QUISPE ORELLANA
'* FCH. CREACIÓN : 05/12/2016
**********************************************************/
namespace SGR.Presentation.UI.UsuarioEmpresaSucursal.Pages
{
    using SGR.ViewModels.Presentation.UsuarioEmpresaSucursal.Pages;
    using System.Windows.Controls;

    public partial class PGSGR_UsuarioEmpSucursal: Page
    {
        public PGSGR_UsuarioEmpSucursal()
        {
            InitializeComponent();
            DataContext = new VMSGR_UsuarioEmpSucursal();
        }
    }
}
