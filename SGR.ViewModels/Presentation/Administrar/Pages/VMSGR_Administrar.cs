/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   ABEL QUISPE ORELLANA
'* FCH. CREACIÓN : 02/12/2016
**********************************************************/

namespace SGR.ViewModels.Presentation.Administrar.Pages
{
    using ComputerSystems;
    using ComputerSystems.WPF;
    using ComputerSystems.WPF.Acciones.Controles.ModalDialog;
    using ComputerSystems.WPF.Interfaces;
    using ComputerSystems.WPF.Acciones.Controles.Menus;
    using ComputerSystems.WPF.Notificaciones;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media;
    using SGR.Models;
    using SGR.ViewModels.Method;

    public class VMSGR_Administrar : CmpNavigationService, CmpINavigation
    {
        public object Parameter
        {
            set
            {
                Application.Current.Dispatcher.Invoke(async() =>
                {
                    var result = await MTSGR_GetPrivilege.Process(this.GetType(), SGRMessage.TitleMessage, SGRMessageContent.GetAccesoRestringido);
                    if (result == null)
                        Volver();
                });
            }
        }

        #region COLECCIONES
        #endregion

        #region PROPERTY
        #endregion

        #region ICOMMAND

        public ICommand IDocumento
        {
            get
            {
                return CmpICommand.GetICommand((T) =>
                {
                    var instPageMenu = CmpMenu.GetPage("PGSGR_ListadoDocumento");
                    new CmpNavigationService().ShowCleanWindow(instPageMenu, CmpViewNavigationTipeShow.ShowDialog, 500, 700);
                });
            }
        }

        public ICommand IMoneda
        {
            get
            {
                return CmpICommand.GetICommand((T) =>
                {
                    var instPageMenu = CmpMenu.GetPage("PGSGR_ListadoMoneda");
                    new CmpNavigationService().ShowCleanWindow(instPageMenu, CmpViewNavigationTipeShow.ShowDialog, 500, 700);
                });
            }
        }

        public ICommand IUsuarioEmpSucursal
        {
            get
            {
                return CmpICommand.GetICommand((T) =>
                {
                    var instPageMenu = CmpMenu.GetPage("PGSGR_UsuarioEmpSucursal");
                    new CmpNavigationService().ShowCleanWindow(instPageMenu, CmpViewNavigationTipeShow.ShowDialog, 300, 570);
                });
            }
        }

        public ICommand IMedioPago
        {
            get
            {
                return CmpICommand.GetICommand((T) =>
                {
                    var instPageMenu = CmpMenu.GetPage("PGSGR_ListadoMedioPago");
                    new CmpNavigationService().ShowCleanWindow(instPageMenu, CmpViewNavigationTipeShow.ShowDialog, 500, 700);
                });
            }
        }

        public ICommand IMesaArea
        {
            get
            {
                return CmpICommand.GetICommand((T) =>
                {
                    var instPageMenu = CmpMenu.GetPage("PGSGR_ListadoMesaArea");
                    new CmpNavigationService().ShowCleanWindow(instPageMenu, CmpViewNavigationTipeShow.ShowDialog, 500, 700);
                });
            }
        }

        public ICommand IMesa
        {
            get
            {
                return CmpICommand.GetICommand((T) =>
                {
                    var instPageMenu = CmpMenu.GetPage("PGSGR_ListadoMesa");
                    new CmpNavigationService().ShowCleanWindow(instPageMenu, CmpViewNavigationTipeShow.ShowDialog, 500, 700);
                });
            }
        }

        public ICommand IArea
        {
            get
            {
                return CmpICommand.GetICommand((T) =>
                {
                    var instPageMenu = CmpMenu.GetPage("PGSGR_ListadoArea");
                    new CmpNavigationService().ShowCleanWindow(instPageMenu, CmpViewNavigationTipeShow.ShowDialog, 500, 700);
                });
            }
        }

        public ICommand IImpuesto
        {
            get
            {
                return CmpICommand.GetICommand((T) =>
                {
                    CmpModalDialog.GetContent().ShowDialog("MPSGR_Impuesto", SGRVariables.ESGR_Retencion);
                });
            }
        }

        public ICommand IImpresora
        {
            get
            {
                return CmpICommand.GetICommand((T) =>
                {
                    CmpModalDialog.GetContent().ShowDialog("MPSGR_Impresora", SGRVariables.ImpresoraPedido);
                });
            }
        }

        public ICommand IPedidoTipo
        {
            get
            {
                return CmpICommand.GetICommand((T) =>
                {
                    var instPageMenu = CmpMenu.GetPage("PGSGR_ListadoPedidoTipo");
                    new CmpNavigationService().ShowCleanWindow(instPageMenu, CmpViewNavigationTipeShow.ShowDialog, 500, 700);
                });
            }
        }

        public ICommand ICaja
        {
            get
            {
                return CmpICommand.GetICommand((T) =>
                {
                    var instPageMenu = CmpMenu.GetPage("PGSGR_ListadoCaja");
                    new CmpNavigationService().ShowCleanWindow(instPageMenu, CmpViewNavigationTipeShow.ShowDialog, 500, 700);
                });
            }
        }

        public ICommand IMesaPredeterminada
        {
            get
            {
                return CmpICommand.GetICommand((T) =>
                {
                    CmpModalDialog.GetContent().ShowDialog("MPSGR_MesaPredeterminada", SGRVariables.MesaPredeterminada);
                });
            }
        }

        public ICommand INotificaciones
        {
            get
            {
                return CmpICommand.GetICommand((T) =>
                {
                    CmpModalDialog.GetContent().ShowDialog("MPSGR_Notificaciones", SGRVariables.ImpresoraPedido);
                });
            }
        }

        #endregion

        #region METODOS
        #endregion
    }
}
