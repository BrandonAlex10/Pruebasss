using ComputerSystems.WPF;
using SGR.Models;
using SGR.ViewModels;
using SGR.ViewModels.Presentation;
using System.Windows;
using System.Windows.Threading;
namespace SGR.Presentation
{
    public partial class App : Application
    {
        public App()
        {
            //CmpMessageBox.InitializeChanceException();
            this.DispatcherUnhandledException += App_DispatcherUnhandledException;
        }

        void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            //MessageBox.Show(e.Exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            CmpMessageBox.Show("SGR", e.Exception.Message, CmpButton.Aceptar);
            e.Handled = true;
        }

        private void Application_Startup_1(object sender, StartupEventArgs e)
        {
            SGRInitialize.Component();
            SGRVariables.ESGR_Usuario = new Models.Entity.ESGR_Usuario()
            {
                IdUsuario = 1,
                ESGR_Empresa = new Models.Entity.ESGR_Empresa()
                {
                    IdEmpresa = 1
                }
            };
        }
    }
}
