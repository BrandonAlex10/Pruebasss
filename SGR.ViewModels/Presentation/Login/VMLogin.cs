/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   ABEL QUISPE ORELLANA
'* FCH. CREACIÓN : 28/11/2016
'* ********************************************************
'* 
'* FECHA MODIFIC. : 30/12/2016 [OSCAR HUAMAN CABRERA]
'* MOTIVO MODIFIC.: Se agregó funcionalidad al Login, Olvidé Contraseña
'*                  y Cambio de Contraseña
**********************************************************/
namespace SGR.ViewModels.Presentation.Login
{
    using ComputerSystems;
    using SGR.Models.Business;
    using SGR.Models.Entity;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Input;
    using ComputerSystems.WPF;
    using SGR.Models;

    public class VMLogin : CmpNotifyPropertyChanged
    {
        public VMLogin()
        {
            PropertyHora = "00";
            PropertyMinuto = "00";
            PropertySegundo = "00";
            CallBackTimer = new System.Timers.Timer();
            CallBackTimer.Elapsed += CallBackTimer_Elapsed;
            CallBackTimer.Interval = 1000;
            CallBackTimer.Start();

            PropertyVisibilityUsuario = Visibility.Visible;
            PropertyVisibilityCorreo = Visibility.Collapsed;
            PropertyVisibilityCambioContrasenia = Visibility.Collapsed;

            //Usuario = "DESARROLLADOR";
            //Contrasenia = "admin1";
        }

        #region OBJETOS SECUNDARIOS

        private ESGR_Usuario _ESGR_Usuario;
        public ESGR_Usuario ESGR_Usuario
        {
            get
            {
                return _ESGR_Usuario;
            }
            set
            {
                _ESGR_Usuario = value;
                OnPropertyChanged("ESGR_Usuario");
            }
        }

        private string _Usuario;
        public string Usuario
        {
            get
            {
                if (_Usuario == null)
                    _Usuario = string.Empty;
                return _Usuario;
            }
            set
            {
                _Usuario = value;
                OnPropertyChanged("Usuario");
            }
        }

        private string _Contrasenia;
        public string Contrasenia
        {
            get
            {
                if (_Contrasenia == null)
                    _Contrasenia = string.Empty;
                return _Contrasenia;
            }
            set
            {
                _Contrasenia = value;
                OnPropertyChanged("Contrasenia");
            }
        }

        private string _NuevaContrasenia;
        public string NuevaContrasenia
        {
            get
            {
                if (_NuevaContrasenia == null)
                    _NuevaContrasenia = string.Empty;
                return _NuevaContrasenia;
            }
            set
            {
                _NuevaContrasenia = value;
                OnPropertyChanged("NuevaContrasenia");
            }
        }

        private string _RepetirContrasenia;
        public string RepetirContrasenia
        {
            get
            {
                if (_RepetirContrasenia == null)
                    _RepetirContrasenia = string.Empty;
                return _RepetirContrasenia;
            }
            set
            {
                _RepetirContrasenia = value;
                OnPropertyChanged("CambiarContrasenia");
            }
        }

        private string _Correo;
        public string Correo
        {
            get
            {
                return _Correo;
            }
            set
            {
                _Correo = value;
                OnPropertyChanged("Correo");
            }
        }

        private string _Tittle;
        public string Tittle
        {
            get
            {
                return _Tittle;
            }
            set
            {
                _Tittle = value;
                OnPropertyChanged("Tittle");
            }
        }

        private Window _MainWindow;
        public Window MainWindow { set { _MainWindow = value; } }

        #endregion

        #region PROPERTY

        public string _PropertyHora;
        public string PropertyHora 
        {
            get 
            {
                return _PropertyHora;
            }
            set 
            {
                _PropertyHora = value;
                OnPropertyChanged("PropertyHora");
            }
        }

        public string _PropertyMinuto;
        public string PropertyMinuto
        {
            get
            {
                return _PropertyMinuto;
            }
            set
            {
                _PropertyMinuto = value;
                OnPropertyChanged("PropertyMinuto");
            }
        }

        public string _PropertySegundo;
        public string PropertySegundo
        {
            get
            {
                return _PropertySegundo;
            }
            set
            {
                _PropertySegundo = value;
                OnPropertyChanged("PropertySegundo");
            }
        }

        private string _PropertyMessage;
        public string PropertyMessage
        {
            get
            {
                return _PropertyMessage;
            }
            set
            {
                _PropertyMessage = value;
                OnPropertyChanged("PropertyMessage");
            }
        }

        private string _PropertyMessageProcess;
        public string PropertyMessageProcess
        {
            get
            {
                return _PropertyMessageProcess;
            }
            set
            {
                _PropertyMessageProcess = value;
                if (value.Trim().Length > 0)
                    PropertyIsIndeterminate = true;
                else
                    PropertyIsIndeterminate = false;
                OnPropertyChanged("PropertyMessageProcess");
            }
        }

        private bool _PropertyIsIndeterminate;
        public bool PropertyIsIndeterminate 
        {
            get 
            {
                return _PropertyIsIndeterminate;
            }
            set 
            {
                _PropertyIsIndeterminate = value;
                OnPropertyChanged("PropertyIsIndeterminate");
            }
        }

        private Visibility _PropertyIsVisibilityLogin;
        public Visibility PropertyIsVisibilityLogin
        {
            get
            {
                return _PropertyIsVisibilityLogin;
            }
            set
            {
                _PropertyIsVisibilityLogin = value;
                OnPropertyChanged("PropertyIsVisibilityLogin");
            }
        }

        private Visibility _PropertyVisibilityCorreo;
        public Visibility PropertyVisibilityCorreo
        {
            get
            {
                return _PropertyVisibilityCorreo;
            }
            set
            {
                _PropertyVisibilityCorreo = value;
                if (value == Visibility.Visible)
                    Tittle = "Enviar Contraseña";
                OnPropertyChanged("PropertyVisibilityCorreo");
            }
        }

        private Visibility _PropertyVisibilityUsuario;
        public Visibility PropertyVisibilityUsuario
        {
            get
            {
                return _PropertyVisibilityUsuario;
            }
            set
            {
                _PropertyVisibilityUsuario = value;
                PropertyVisibilityBtnIngresar = value;
                if (value == Visibility.Visible)
                    Tittle = "Inicio de Sesión de Usuario";
                OnPropertyChanged("PropertyVisibilityUsuario");
            }
        }

        private Visibility _PropertyVisibilityBtnIngresar;
        public Visibility PropertyVisibilityBtnIngresar
        {
            get
            {
                return _PropertyVisibilityBtnIngresar;
            }
            set
            {
                _PropertyVisibilityBtnIngresar = value;
                OnPropertyChanged("PropertyVisibilityBtnIngresar");
            }
        }

        private Visibility _PropertyVisibilityCambioContrasenia;
        public Visibility PropertyVisibilityCambioContrasenia
        {
            get
            {
                return _PropertyVisibilityCambioContrasenia;
            }
            set
            {
                _PropertyVisibilityCambioContrasenia = value;
                if (value == Visibility.Visible)
                    Tittle = "Cambio de Contraseña";
                OnPropertyChanged("PropertyVisibilityCambioContrasenia");
            }
        }

        private bool _PropertyIsCheckedOlvideContrasenia;
        public bool PropertyIsCheckedOlvideContrasenia
        {
            get
            {
                return _PropertyIsCheckedOlvideContrasenia;
            }
            set
            {
                _PropertyIsCheckedOlvideContrasenia = value;
                Usuario = string.Empty;
                Contrasenia = string.Empty;
                Correo = string.Empty;
                PropertyMessage = string.Empty;
                if (value)
                {
                    PropertyIsCheckedCambiarContrasenia = false;
                    PropertyVisibilityUsuario = Visibility.Collapsed;
                    PropertyVisibilityCorreo = Visibility.Visible;
                    PropertyVisibilityInformacion = Visibility.Collapsed;
                }
                else
                {
                    PropertyVisibilityUsuario = Visibility.Visible;
                    PropertyVisibilityCorreo = Visibility.Collapsed;
                    PropertyVisibilityInformacion = Visibility.Visible;
                }
                OnPropertyChanged("PropertyIsCheckedOlvideContrasenia");
            }
        }

        private bool _PropertyIsCheckedCambiarContrasenia;
        public bool PropertyIsCheckedCambiarContrasenia
        {
            get
            {
                return _PropertyIsCheckedCambiarContrasenia;
            }
            set
            {
                _PropertyIsCheckedCambiarContrasenia = value;
                Usuario = string.Empty;
                Contrasenia = string.Empty;
                NuevaContrasenia = string.Empty;
                RepetirContrasenia = string.Empty;
                PropertyMessage = string.Empty;
                if (value)
                {
                    PropertyIsCheckedOlvideContrasenia = false;
                    PropertyVisibilityCambioContrasenia = Visibility.Visible;
                    PropertyVisibilityBtnIngresar = Visibility.Collapsed;
                    PropertyVisibilityInformacion = Visibility.Collapsed;
                }
                else
                {
                    PropertyVisibilityUsuario = Visibility.Visible;
                    PropertyVisibilityCambioContrasenia = Visibility.Collapsed;
                    PropertyVisibilityInformacion = Visibility.Visible;
                }
                OnPropertyChanged("PropertyIsCheckedCambiarContrasenia");
            }
        }

        private Visibility _PropertyVisibilityInformacion;
        public Visibility PropertyVisibilityInformacion
        {
            get
            {
                return _PropertyVisibilityInformacion;
            }
            set
            {
                _PropertyVisibilityInformacion = value;
                OnPropertyChanged("PropertyVisibilityInformacion");
            }
        }

        #endregion

        #region ICOMMAND

        public ICommand ICambiarContrasenia
        {
            get
            {
                return CmpICommand.GetICommand(async(T) =>
                    {
                        await Task.Factory.StartNew(() =>
                            {
                                try
                                {
                                    PropertyMessageProcess = "Cargando Lista de Usuario";
                                    PropertyIsVisibilityLogin = Visibility.Collapsed;
                                    var ListSMD_Usuario = new BSGR_Usuario().GetCollectionUsuario().Where(x => x.FlgEliminado == true);
                                    ESGR_Usuario = ListSMD_Usuario.FirstOrDefault(x => x.Usuario == Usuario);
                                    //Task.Delay(1000);
                                    System.Threading.Thread.Sleep(500);
                                    if (ESGR_Usuario == null)
                                    {
                                        PropertyMessage = "Usuario Incorrecto.";
                                        PropertyMessageProcess = string.Empty;
                                        PropertyIsVisibilityLogin = Visibility.Visible;
                                        return;
                                    }

                                    if (Contrasenia == "123") Contrasenia = ESGR_Usuario.Contrasenia;   //AGREGADO PARA EVITAR EL REVISAR EL CORREO - ELIMINAR AL TERMINAR EL DESARROLLO
                                    
                                    if (ESGR_Usuario.Contrasenia != Contrasenia)
                                    {
                                        AddIntentos(ESGR_Usuario);
                                        PropertyMessage = "Contraseña Incorrecta.";
                                        PropertyMessageProcess = string.Empty;
                                        PropertyIsVisibilityLogin = Visibility.Visible;
                                        return;
                                    }

                                    if (NuevaContrasenia != RepetirContrasenia)
                                    {
                                        PropertyMessage = "Las Contraseñas no Coindicen.";
                                        PropertyMessageProcess = string.Empty;
                                        PropertyIsVisibilityLogin = Visibility.Visible;
                                        return;
                                    }

                                    if (NuevaContrasenia.Trim().Length < 6 || NuevaContrasenia.Trim().Length > 10)
                                    {
                                        PropertyMessage = "La Contraseña debe tener entre 6 y 10 carácteres.";
                                        PropertyMessageProcess = string.Empty;
                                        PropertyIsVisibilityLogin = Visibility.Visible;
                                        return;
                                    }

                                    if (ESGR_Usuario.Contrasenia == NuevaContrasenia)
                                    {
                                        PropertyMessage = "La Contraseña no debe ser igual a la anterior.";
                                        PropertyMessageProcess = string.Empty;
                                        PropertyIsVisibilityLogin = Visibility.Visible;
                                        return;
                                    }

                                    PropertyMessageProcess = "Guardando Nueva Contraseña";
                                    ESGR_Usuario.Opcion = "U";
                                    ESGR_Usuario.Contrasenia = NuevaContrasenia;
                                    new BSGR_Usuario().TransUsuario(ESGR_Usuario);
                                    System.Threading.Thread.Sleep(1000);
                                    PropertyMessageProcess = string.Empty;
                                    PropertyIsVisibilityLogin = Visibility.Visible;
                                    PropertyMessage = "Contraseña Guardada Éxitosamente.";
                                }
                                catch(Exception ex)
                                {
                                    PropertyIsVisibilityLogin = Visibility.Visible;
                                    PropertyMessageProcess = string.Empty;
                                    PropertyMessage = ex.Message;
                                }
                            });
                    });
            }
        }

        public ICommand IEnviar
        {
            get
            {
                return CmpICommand.GetICommand(async (T) =>
                {

                    if (Correo == null || Correo.Trim().Length == 0)
                        return;

                    await Task.Factory.StartNew(async () =>
                    {
                        try
                        {
                            PropertyIsVisibilityLogin = Visibility.Collapsed;
                            PropertyMessage = string.Empty;
                            PropertyMessageProcess = "Cargando lista de usuarios";
                            await Task.Delay(500);
                            var ListSMD_Usuario = new BSGR_Usuario().GetCollectionUsuario().Where(x => x.FlgEliminado == true);
                            ESGR_Usuario = ListSMD_Usuario.FirstOrDefault(x => x.Correo.ToLower() == Correo.ToLower());
                            if (ESGR_Usuario != null)
                            {
                                PropertyMessageProcess = "Enviando Correo";
                                CmpMail objMail = new CmpMail("mail.computersystems.com.pe", "notificaciones@computersystems.com.pe", "?du0@v31@LbE");
                                objMail.ValorDelReceptor(Correo, "Solicitud de Contraseña - SGR", MethodMessageContent(ESGR_Usuario.Contrasenia));
                                objMail.EnviarEmail();
                                PropertyMessage = "Contraseña enviada correctamente.";
                            }
                            else
                            {
                                PropertyMessage = "Correo incorrecto.";
                            }
                            PropertyIsVisibilityLogin = Visibility.Visible;
                            PropertyMessageProcess = string.Empty;
                        }
                        catch (Exception ex)
                        {
                            PropertyIsVisibilityLogin = Visibility.Visible;
                            PropertyMessageProcess = string.Empty;
                            PropertyMessage = ex.Message;
                        }
                    });
                });
            }
        }

        public ICommand IIngresar
        {
            get
            {
                return CmpICommand.GetICommand(async(T) =>
                {
                    //Application.Current.Dispatcher.Invoke(() =>
                    //{
                    //    var Main = new Window();
                    //    bool IsValid = true;
                    //    while (IsValid)
                    //    {
                    //        Main = Application.Current.Windows.Cast<Window>().SingleOrDefault(x => x.IsActive);
                    //        IsValid = !(Main != null);
                    //    }
                    //    if (_MainWindow != null)
                    //        _MainWindow.Show();
                    //    Main.Close();
                    //});
                    //return;
                    //Usuario = "ADMINISTRADOR";
                    //Contrasenia = "admin1";
                    //Usuario = "ALUIS";
                    //Contrasenia = "caja01";
                    if (PropertyVisibilityBtnIngresar == Visibility.Collapsed)
                        return;

                    if (Usuario.Trim().Length == 0 || Contrasenia.Trim().Length == 0)
                        return;

                    await Task.Factory.StartNew(async() =>
                    {
                        try
                        {

                            PropertyIsVisibilityLogin = Visibility.Collapsed;
                            PropertyMessage = string.Empty;
                            PropertyMessageProcess = "Cargando lista de usuarios...";
                            await Task.Delay(300);
                            var ListSMD_Usuario = new BSGR_Usuario().GetCollectionUsuario().Where(x => x.FlgEliminado == true && (x.ESGR_Perfil.IdPerfil == 1 || x.ESGR_Perfil.IdPerfil == 2 || x.ESGR_Perfil.IdPerfil == 5));
                            ESGR_Usuario = ListSMD_Usuario.FirstOrDefault(x => x.Usuario == Usuario);
                            if (ESGR_Usuario != null && ESGR_Usuario.FlgConectado)
                            {
                                PropertyMessage = "El Usuario se encuentra conectado en otro equipo";
                                PropertyIsVisibilityLogin = Visibility.Visible;
                                PropertyMessageProcess = string.Empty;
                                return;
                            }
                            if (ListSMD_Usuario.ToList().Exists(x => x.Usuario == Usuario && x.Contrasenia == Contrasenia))
                            {
                                PropertyMessageProcess = "Autenticando...";
                                ESGR_Usuario.Opcion = "R";
                                new BSGR_Usuario().TransUsuario(ESGR_Usuario);
                                SGRVariables.ESGR_Usuario = ESGR_Usuario;
                                Application.Current.Dispatcher.Invoke(() =>
                                {
                                    var Main = new Window();
                                    bool IsValid = true;
                                    while (IsValid)
                                    {
                                        Main = Application.Current.Windows.Cast<Window>().SingleOrDefault(x => x.IsActive);
                                        IsValid = !(Main != null);
                                    }
                                    if (_MainWindow != null)
                                        _MainWindow.Show();
                                    Main.Close();
                                    
                                });
                            }
                            else
                            {
                                if (ESGR_Usuario == null)
                                    PropertyMessage = "Usuario no encontrado";
                                else
                                {
                                    AddIntentos(ESGR_Usuario);
                                    PropertyMessage = "Contraseña incorrecta";
                                }
                            }
                            PropertyIsVisibilityLogin = Visibility.Visible;
                            PropertyMessageProcess = string.Empty;
                        }
                        catch (Exception ex)
                        {
                            PropertyIsVisibilityLogin = Visibility.Visible;
                            PropertyMessageProcess = string.Empty;
                            PropertyMessage = ex.Message;
                        }
                    });
                });
            }
        }

        public ICommand ICerrar
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

        #region MÉTODOS

        /// <summary>
        /// Suma mas (+1) fallido
        /// </summary>
        public static bool AddIntentos(ESGR_Usuario SGR_Usuario)
        {
            if (SGR_Usuario != null)
            {
                int cantIntentos = SGRVariables.ESGR_PoliticaSeguridad.MaximoIntento - (SGR_Usuario.Fallido + 1);
                if (cantIntentos == 0)
                {
                    try
                    {
                        ///Si el usuario supera la cantidad de intentos, LO BLOQUEAMOS
                        SGR_Usuario.Opcion = "B";
                        new BSGR_Usuario().TransUsuario(SGR_Usuario);
                        CmpMessageBox.Show(SGRMessage.TitleMessage, SGRMessageContent.LogeoSuperoElNumeroIntentos, CmpButton.Aceptar);
                        return false;
                    }
                    catch (Exception ex)
                    {
                        CmpMessageBox.Show(SGRMessage.TitleMessage, ex.Message, CmpButton.Aceptar);
                    }
                }
                else
                {
                    try
                    {
                        ///Guardamos mas intento fallido (+)
                        SGR_Usuario.Opcion = "F";
                        new BSGR_Usuario().TransUsuario(SGR_Usuario);
                    }
                    catch (Exception ex)
                    {
                        CmpMessageBox.Show(SGRMessage.TitleMessage, ex.Message, CmpButton.Aceptar);
                        return false;
                    }
                    if (cantIntentos == 1)
                    {
                        CmpMessageBox.Show(SGRMessage.TitleMessage, SGRMessageContent.LogeoLeQuedaUnIntento, CmpButton.Aceptar);
                        return false;
                    }
                    else if (cantIntentos > 1)
                    {
                        CmpMessageBox.Show(SGRMessage.TitleMessage, SGRMessageContent.LogeoLeQueda + cantIntentos + " intentos!!", CmpButton.Aceptar);
                        return false;
                    }
                }
            }
            return true;
        }

        private string MethodMessageContent(string Contrasenia)
        {
            string vrContent = string.Empty;
            vrContent += "Hola <b>" + ESGR_Usuario.Nombres + "</b><br/><br/>";
            vrContent += "Usted a solicitado el reenvío de su contraseña al SGR - SISTEMA DE GESTIÓN PARA RESTAURANT. <br/><br/>";
            vrContent += "Sus datos de acceso son: <br/><br/>";
            vrContent += "<b>Usuario: </b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + ESGR_Usuario.Usuario + "<br/>";
            vrContent += "<b>Contraseña:</b>&nbsp;" + Contrasenia + "<br/>";
            vrContent += (ESGR_Usuario.Nick == null || ESGR_Usuario.Nick.Trim().Length == 0) ? "<br/>" : "<b>Nick:</b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + ESGR_Usuario.Nick + "<br/>";
            vrContent += "<br/><b> - - - - - - - - - </b><br/>";
            vrContent += "<b>Mensaje enviado desde la aplicación: SGR.</b><br/><br/>";
            vrContent += "<b>Computer Systems Solution</b><br/>";
            vrContent += "<a href=\"http://www.computersystems.com.pe/\"><b>www.computersystems.com.pe</b></a>";
            return vrContent;
        }

        private System.Timers.Timer callBackTimer;
        public System.Timers.Timer CallBackTimer
        {
            get
            {
                return callBackTimer;
            }

            set
            {
                callBackTimer = value;
                OnPropertyChanged();
            }
        }
        private void CallBackTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                PropertyHora = e.SignalTime.ToString("hh");
                PropertyMinuto = e.SignalTime.ToString("mm");
                PropertySegundo = e.SignalTime.ToString("ss");
            }
            catch (Exception)
            {

            }
        }

        #endregion
    }
}