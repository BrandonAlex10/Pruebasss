using ComputerSystems.WPF.Acciones.Controles.ModalDialog;
using SGR.ViewModels.Presentation.Caja.ModalPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SGR.Presentation.UI.Caja.ModalPage
{
    /// <summary>
    /// Lógica de interacción para MPSGR_Caja.xaml
    /// </summary>
    public partial class MPSGR_Caja : CmpModalPage
    {
        public MPSGR_Caja()
        {
            InitializeComponent();
            DataContext = new VMSGR_Caja();
        }
    }
}
