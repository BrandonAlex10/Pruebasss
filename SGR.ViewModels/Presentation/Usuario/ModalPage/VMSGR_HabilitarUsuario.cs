using ComputerSystems;
using ComputerSystems.Loading;
using ComputerSystems.Methods;
using ComputerSystems.WPF;
using ComputerSystems.WPF.Acciones.Controles.ModalDialog;
using ComputerSystems.WPF.Interfaces;
using SGR.Models.Business;
using SGR.Models.Entity;
using SGR.ViewModels.Method;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SGR.ViewModels.Presentation.Usuario.ModalPage
{
    public class VMSGR_HabilitarUsuario : CmpNotifyPropertyChanged, CmpIModalDialog
    {
        public object Parameter
        {
            set
            {
                Application.Current.Dispatcher.Invoke(async() =>
                {
                    var result = await MTSGR_GetPrivilege.Process(this.GetType(), SGRMessage.TitleMessage, SGRMessageContent.GetAccesoRestringido);
                    if (result == null)
                        CmpModalDialog.GetContent().Close();
                    else if (value is CmpObservableCollection<ESGR_Usuario>)
                    {
                        PropertyIsEnabledNuevo = result.Nuevo;
                        CollectionESGR_Usuario.Source = (CmpObservableCollection<ESGR_Usuario>)value;
                    }
                });
            }
        }

        #region COLLECIONES

        private CmpObservableCollection<ESGR_Usuario> _CollectionESGR_Usuario;
        public CmpObservableCollection<ESGR_Usuario> CollectionESGR_Usuario
        {
            get
            {
                if (_CollectionESGR_Usuario == null)
                    _CollectionESGR_Usuario = new CmpObservableCollection<ESGR_Usuario>();
                return _CollectionESGR_Usuario;
            }
        }
    
        #endregion

        #region OBJ SECUNDARIOS

        private ESGR_Usuario _SelectESGR_Usuario;
        public ESGR_Usuario SelectESGR_Usuario
        {
            get
            {
                return _SelectESGR_Usuario;
            }
            set
            {
                _SelectESGR_Usuario = value;
                OnPropertyChanged("SelectESGR_Usuario");
            }
        }

        #endregion

        #region PROPERTYS

        private bool _PropertyIsEnabledNuevo;
        public bool PropertyIsEnabledNuevo
        {
            get
            {
                return _PropertyIsEnabledNuevo;
            }
            set
            {
                _PropertyIsEnabledNuevo = value;
                OnPropertyChanged("PropertyIsEnabledNuevo");
            }
        }

        #endregion

        #region ICOMMAND

        public ICommand IGuardar
        {
            get
            {
                return CmpICommand.GetICommand( (G) =>
                {
                    string strMessage = string.Empty;
                    if (MethodValidarDatos()) return;
                    ESGR_Usuario ESGR_Usuario = new ESGR_Usuario();
                    ESGR_Usuario.CadenaHabilitar = MethodLoadHabilitarXML();
                    CmpMessageBox.Proccess(SGRMessage.AdministratorUsuario, "Procesando datos, por favor espere...", () =>
                    {
                        try
                        {
                            ESGR_Usuario.Opcion = "H";
                            new BSGR_Usuario().TransUsuario(ESGR_Usuario);
                            ///Envia la nueva contraseña generada al correo del usuario
                            CollectionESGR_Usuario.ToList().ForEach((item) =>
                            {
                                if (item.Habilitar)
                                {
                                    CmpMail objMail = new CmpMail("mail.computersystems.com.pe", "notificaciones@computersystems.com.pe", "?du0@v31@LbE");
                                    objMail.ValorDelReceptor(item.Correo, "Registro de Usuario - SGR", MethodMessageContent(item));
                                    objMail.EnviarEmail();
                                }
                            });
                        }
                        catch (Exception ex)
                        {
                            strMessage = ex.Message;
                        }
                    },
                    () =>
                    {
                        if (strMessage.Trim().Length > 0)
                            CmpMessageBox.Show(SGRMessage.AdministratorUsuario, strMessage, CmpButton.Aceptar);
                        else
                            CmpMessageBox.Show(SGRMessage.AdministratorUsuario, SGRMessageContent.ContentSaveOK, CmpButton.Aceptar, () =>
                            {
                                CmpModalDialog.GetContent().Close();
                            });
                    });
                });
            }
        }

        public ICommand IVolver
        {
            get
            {
                return CmpICommand.GetICommand((V) =>
                {
                    CmpModalDialog.GetContent().Close();
                });
            }
        }

        #endregion

        #region METODOS

        public string MethodLoadHabilitarXML()
        {
            string vrCadena = string.Empty;
            vrCadena = "<ROOT>";
            CollectionESGR_Usuario.ToList().ForEach((f) =>
            {
                if (f.Habilitar)
                {
                    vrCadena += "<Listar ";
                    vrCadena += "xIdUsuario = \'" + f.IdUsuario;
                    vrCadena += "\' xContrasenia = \'" + CmpCifrarObjecto.Encriptar(f.Contrasenia = CmpMetodos.CreatePassword(10));
                    vrCadena += "\'></Listar>";
                }
            });
            vrCadena += "</ROOT>";
            return vrCadena;
        }

        private string MethodMessageContent(ESGR_Usuario ESGR_Usuario)
        {
            string vrContent = string.Empty;
            vrContent += "Hola <b>" + ESGR_Usuario.Nombres + "</b><br/><br/>";
            vrContent += "Su Usuario a sido restablecido satisfactoriamente en nuestra aplicación SGR - SISTEMA DE GESTIÓN PARA RESTAURANT. <br/><br/>";
            vrContent += "Sus datos de acceso son: <br/><br/>";
            vrContent += "<b>Usuario: </b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + ESGR_Usuario.Usuario + "<br/>";
            vrContent += "<b>Contraseña:</b>&nbsp;" + ESGR_Usuario.Contrasenia + "<br/>";
            vrContent += (ESGR_Usuario.Nick == null || ESGR_Usuario.Nick.Trim().Length == 0) ? "<br/>" : "<b>Nick:</b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + ESGR_Usuario.Nick + "<br/>";
            vrContent += "<br/><b> - - - - - - - - - </b><br/>";
            vrContent += "<b>Mensaje enviado desde la aplicación: SGR.</b><br/><br/>";
            vrContent += "<b>Computer Systems Solution</b><br/>";
            vrContent += "<a href=\"http://www.computersystems.com.pe/\"><b>www.computersystems.com.pe</b></a>";
            return vrContent;
        }

        public bool MethodValidarDatos()
        {
            var vrValidar = false;
            if (SelectESGR_Usuario == null )
            {
                CmpMessageBox.Show(SGRMessage.AdministratorUsuario,SGRMessageContent.ContentSelectNull+ " Guardar",CmpButton.Aceptar);
                vrValidar=true;
            }
            else if ( SelectESGR_Usuario.Habilitar == false)
            {
                vrValidar = true;
            }
            return vrValidar;
        }

        #endregion
    }
}
