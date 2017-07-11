/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   ABEL QUISPE ORELLANA
'* FCH. CREACIÓN : 28/11/2016
**********************************************************/
namespace SGR.Orders
{
    using ComputerSystems.WPF.Notificaciones;
    using SGR.Orders.UI.Login.Pages;
    using System;
    using System.Windows;

    public partial class MainWindow 
    {
        public MainWindow()
        {
            InitializeComponent();
            ContentCmpModalDialog.SetParent(ModalDialogParent);
        }

        private void MetroWindow_ContentRendered_1(object sender, EventArgs e)
        {
            new CmpNavigationService().Ir(new PGSGR_Login(), MyFrameBody);
        }

        private void MetroWindow_Closing_1(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (SGR.Models.SGRVariables.ESGR_Usuario != null)
            {
                try
                {
                    SGR.Models.SGRVariables.ESGR_Usuario.Opcion = "N";
                    new SGR.Models.Business.BSGR_Usuario().TransUsuario(SGR.Models.SGRVariables.ESGR_Usuario);
                }
                catch (Exception) { }
            }
            Application.Current.Shutdown();
        }
    }
}
