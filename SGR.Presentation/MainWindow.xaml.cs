/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   ABEL QUISPE ORELLANA
'* FCH. CREACIÓN : 28/11/2016
**********************************************************/
namespace SGR.Presentation
{
    using ComputerSystems.WPF;
    using ComputerSystems.WPF.Acciones.Controles.CropImage;
    using ComputerSystems.WPF.Acciones.Controles.Menus;
    using ComputerSystems.WPF.Acciones.Controles.ModalDialog;
    using ComputerSystems.WPF.Notificaciones;
    using SGR.Models;
    using SGR.Presentation.UI.AperturaCierreCaja.ModalPage;
    using SGR.Presentation.UI.Index.Pages;
    using SGR.Presentation.UI.Usuario.Pages;
    using SGR.ViewModels.Presentation;
    using SGR.ViewModels.Presentation.Index.Pages;
    using System.Globalization;
    using System.Windows;

    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new VMMainWindow();
            ContentCmpModalDialog.SetParent(ModalDialogParent);
        }

        private void Main_Closing_1(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                SGRVariables.ESGR_Usuario.Opcion = "N";
                new SGR.Models.Business.BSGR_Usuario().TransUsuario(SGRVariables.ESGR_Usuario);
            }
            catch (System.Exception) { }
            //System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
            Application.Current.Shutdown();
        }

        private void Main_ContentRendered_1(object sender, System.EventArgs e)
        {
            CmpModalDialog.Add(new MPSGR_CajaAperturada());
            lblEmpresa.Text = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(SGRVariables.ESGR_Usuario.ESGR_Empresa.RazonSocial.ToLower());
            lblUsuario.Text = SGRVariables.ESGR_Usuario.ESGR_Perfil.NombrePerfil.ToUpper() + ": " + CultureInfo.CurrentCulture.TextInfo.ToTitleCase(SGRVariables.ESGR_Usuario.NombresApellidos.ToLower()) + "     ";
            new CmpNavigationService().Ir(new PGSGR_Index(), MyFrameBody);
        }

    }
}
