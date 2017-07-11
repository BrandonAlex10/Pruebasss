/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   ABEL QUISPE ORELLANA
'* FCH. CREACIÓN : 28/11/2016
**********************************************************/
namespace SGR.ViewModels.Orders.Login
{
    using ComputerSystems;
    using ComputerSystems.WPF;
    using ComputerSystems.WPF.Acciones.Controles.Menus;
    using ComputerSystems.WPF.Interfaces;
    using ComputerSystems.WPF.Notificaciones;
    using SGR.Models;
    using SGR.Models.Business;
    using System;
    using System.Linq;
    using System.Windows;
    using System.Windows.Input;

    public class VMLogin : CmpNavigationService, CmpINavigation
    {
        public object Parameter
        {
            set
            {
                SGRVariables.ESGR_Usuario = null;
                Nick = string.Empty;
            }
        }
                
        #region OBJETOS SECUNDARIOS

        private string _Nick;
        public string Nick
        {
            get
            {
                if (_Nick == null)
                    _Nick = string.Empty;
                return _Nick.Trim();
            }
            set
            {
                _Nick = value;
                MessageLogin = string.Empty;
                OnPropertyChanged("Nick");
            }
        }

        private string _MessageLogin;
        public string MessageLogin
        {
            get
            {
                return _MessageLogin;
            }
            set
            {
                _MessageLogin = value;
                OnPropertyChanged("MessageLogin");
            }
        }

        private Visibility _PropertyVisibilityMessageLogin;
        public Visibility PropertyVisibilityMessageLogin
        {
            get
            {
                return _PropertyVisibilityMessageLogin;
            }
            set
            {
                _PropertyVisibilityMessageLogin = value;
                OnPropertyChanged("PropertyVisibilityMessageLogin");
            }
        }

        #endregion

        #region ICOMMAND

        public ICommand IIngresar
        {
            get
            {
                return CmpICommand.GetICommand((T) =>
                {
                    try
                    {
                        PropertyVisibilityMessageLogin = Visibility.Collapsed;
                        var ListSMD_Usuario = new BSGR_Usuario().GetCollectionUsuario().Where(x => x.FlgEliminado == true && (x.ESGR_Perfil.IdPerfil == 3 || x.ESGR_Perfil.IdPerfil == 4 || x.ESGR_Perfil.IdPerfil == 5 || x.ESGR_Perfil.IdPerfil == 1));
                        if (ListSMD_Usuario.ToList().Exists(x => x.Nick == Nick && Nick.Length > 0))
                        {
                            SGRVariables.ESGR_Usuario = ListSMD_Usuario.First(x => x.Nick == Nick);
                            if (SGRVariables.ESGR_Usuario.FlgConectado)
                            {
                                MessageLogin = "Nick conectado en otro equipo.";
                                PropertyVisibilityMessageLogin = Visibility.Visible;
                                return;
                            }
                            try
                            {
                                SGRVariables.ESGR_Usuario.Opcion = "R";
                                new BSGR_Usuario().TransUsuario(SGRVariables.ESGR_Usuario);
                            }
                            catch (Exception) { throw; }
                            SGRVariables.SaveLog(Nick + ", inició sesión: " + DateTime.Now);
                            Ir(CmpMenu.GetPage("PGSGR_Index"), new SGRVariables());
                            SGRVariables.SaveLog(Nick + ", cerró sesión: " + DateTime.Now);
                        }
                        else
                        {
                            MessageLogin = "Nick no encontrado, intentelo nuevamente.";
                            PropertyVisibilityMessageLogin = Visibility.Visible;
                        }
                    }
                    catch (Exception ex)
                    {
                        SGRVariables.SaveLog(ex.Message);
                        CmpMessageBox.Show(SGRMessage.TitleSGR, ex.Message, CmpButton.Aceptar);
                    }
                });
            }
        }

        public ICommand ISalir
        {
            get
            {
                return CmpICommand.GetICommand((T) =>
                {
                    Environment.Exit(0);
                });
            }
        }

        #endregion
    }
}
