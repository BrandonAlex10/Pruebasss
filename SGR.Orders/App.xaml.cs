using ComputerSystems;
using ComputerSystems.WPF;
using SGR.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace SGR.Orders
{
    /// <summary>
    /// Lógica de interacción para App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            this.DispatcherUnhandledException += App_DispatcherUnhandledException;
        }

        void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            CmpMessageBox.Show("SGR", e.Exception.Message, CmpButton.Aceptar);
            e.Handled = true;
        }

        private void Application_Startup_1(object sender, StartupEventArgs e)
        {
            var Mensaje = "Usuario conectado: " + DateTime.Now + " - IP: " + CmpNetWorkInformation.GetIPAddress() + " - MAC: " + CmpNetWorkInformation.GetMacAddress();
            SGR.Models.SGRVariables.SaveLog(Mensaje);
            SGRInitialize.Component();
        }

        private void Application_Exit_1(object sender, ExitEventArgs e)
        {
            var Mensaje = "Usuario desconectado: " + DateTime.Now + " - IP: " + CmpNetWorkInformation.GetIPAddress() + " - MAC: " + CmpNetWorkInformation.GetMacAddress();
            SGR.Models.SGRVariables.SaveLog(Mensaje);
        }
    }
}
