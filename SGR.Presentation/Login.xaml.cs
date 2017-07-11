/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   ABEL QUISPE ORELLANA
'* FCH. CREACIÓN : 28/11/2016
**********************************************************/
namespace SGR.Presentation
{
    using ComputerSystems.WPF;
    using SGR.ViewModels.Presentation;
    using SGR.ViewModels.Presentation.Login;
    using System.Windows.Controls;

    public partial class Login 
    {
        public Login()
        {
            InitializeComponent();
            DataContext = new VMLogin() { MainWindow = new MainWindow() };
        }

    }
}
