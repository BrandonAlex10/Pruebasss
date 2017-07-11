/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
 * ********************************************************
 * 
'* FCH. CREACIÓN : 03/12/2016 [ABEL QUISPE ORELLANA]
 * 
**********************************************************/

namespace SGR.Presentation.UI.Index.Pages
{
    using SGR.ViewModels.Presentation.Index.Pages;
    using System.Windows.Controls;

    public partial class PGSGR_Index : Page
    {
        public PGSGR_Index()
        {
            InitializeComponent();
            DataContext = new VMSGR_Index();
        }
    }
}
