/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   ABEL QUISPE ORELLANA
'* FCH. CREACIÓN : 05/12/2016
**********************************************************/
namespace SGR.Presentation.UI.CartaDia.Pages
{
    using SGR.ViewModels.Presentation.CartaDia.Controls;
    using SGR.ViewModels.Presentation.Documento.Pages;
    using System.Windows.Controls;

    public partial class PGSGR_CartaDia: Page
    {
        public PGSGR_CartaDia()
        {
            InitializeComponent();
            DataContext = new VMSGR_CartaDia();
        }
    }
}
