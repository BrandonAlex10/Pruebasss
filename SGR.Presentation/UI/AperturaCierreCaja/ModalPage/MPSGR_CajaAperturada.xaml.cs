using ComputerSystems.WPF.Acciones.Controles.ModalDialog;
using SGR.ViewModels.Presentation.AperturaCierreCaja.ModalPage;
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

namespace SGR.Presentation.UI.AperturaCierreCaja.ModalPage
{
    /// <summary>
    /// Lógica de interacción para MPSGR_CajaAperturada.xaml
    /// </summary>
    public partial class MPSGR_CajaAperturada : CmpModalPage
    {
        public MPSGR_CajaAperturada()
        {
            InitializeComponent();
            DataContext = new VMSGR_CajaAperturada();
        }
    }
}
