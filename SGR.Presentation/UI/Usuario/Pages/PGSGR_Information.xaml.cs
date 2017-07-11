/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				  ABEL QUISPE ORELLANA
'* FCH. CREACIÓN : 24/04/2017
**********************************************************/

namespace SGR.Presentation.UI.Usuario.Pages
{
    using ComputerSystems.WPF.Acciones.Controles.CropImage;
    using ComputerSystems.WPF.Acciones.Controles.ModalDialog;
    using SGR.ViewModels.Presentation.Usuario.Pages;
    using System.Windows;
    using System.Windows.Controls;

    public partial class PGSGR_Information : Page
    {
        public PGSGR_Information()
        {
            InitializeComponent();
            DataContext = new VMSGR_Information();
        }

        private void Page_Loaded_1(object sender, RoutedEventArgs e)
        {
            CmpModalDialog.Add(new CmpCropImage());
        }
    }
}
