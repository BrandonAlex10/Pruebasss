/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   OSCAR HUAMAN CABRERA
'* FCH. CREACIÓN : 01/12/2016
'*********************************************************
'* FECHA MODIFIC. : 19/12/2016 [CRISTIAN HERNANDEZ VILLO]
'* MOTIVO MODIFIC.: Se agregó funcionalidad a los botones
**********************************************************/

namespace SGR.ViewModels.Presentation.Cliente.ModalPage
{
    using ComputerSystems;
    using ComputerSystems.Methods;
    using ComputerSystems.WPF;
    using ComputerSystems.WPF.Acciones.Controles.ModalDialog;
    using ComputerSystems.WPF.Interfaces;
    using ComputerSystems.WPF.Notificaciones;
    using SGR.Models.Business;
    using SGR.Models.Entity;
    using SGR.ViewModels.Method;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Net.NetworkInformation;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media;

    public class VMSGR_Cliente : CmpNavigationService, CmpIModalDialog
    {
        private ESGR_Cliente _ESGR_Cliente;
        public ESGR_Cliente ESGR_Cliente
        {
            get
            {
                if (_ESGR_Cliente == null)
                    _ESGR_Cliente = new ESGR_Cliente();
                return _ESGR_Cliente;
            }
            set
            {
                _ESGR_Cliente = value;
                OnPropertyChanged("ESGR_Cliente");
            }
        }

        public object Parameter
        {
            set
            {
                Application.Current.Dispatcher.Invoke(async () =>
                {
                    var result = await MTSGR_GetPrivilege.Process(this.GetType(), SGRMessage.TitleMessage, SGRMessageContent.GetAccesoRestringido);
                    if (result == null)
                        CmpModalDialog.GetContent().Close();
                    else if (value is ESGR_Cliente)
                    {
                        ESGR_Cliente = (ESGR_Cliente)value;
                        PropertyVisibility = Visibility.Visible;
                        PropertyEnabledActualizarCaptcha = true;
                        MethodLoadHeader();
                        PropertyEnabledActualizarCaptcha = NetworkInterface.GetIsNetworkAvailable();
                    }
                });
           }
        }

        #region COLECCIONES

        private CmpObservableCollection<ESGR_Item> _CollectionDocIdentidad;
        public CmpObservableCollection<ESGR_Item> CollectionDocIdentidad
        {
            get
            {
                if (_CollectionDocIdentidad == null)
                    _CollectionDocIdentidad = new CmpObservableCollection<ESGR_Item>();
                return _CollectionDocIdentidad;
            }
        }

        #endregion

        #region OBJ SECUNDARIOS

        private string _CodigoCaptcha;
        public string CodigoCaptcha
        {
            get
            {
                return _CodigoCaptcha;
            }
            set
            {
                _CodigoCaptcha = value;
                OnPropertyChanged("CodigoCaptcha");
            }
        }

        private ImageSource _PropertyImageContent;
        public ImageSource PropertyImageContent
        {
            get
            {
                return _PropertyImageContent;
            }
            set
            {
                _PropertyImageContent = value;
                OnPropertyChanged("PropertyImageContent");
            }
        }

        private ESGR_Item _SelectDocIdentidad;
        public ESGR_Item SelectDocIdentidad
        {
            get
            {
                return _SelectDocIdentidad;
            }
            set
            {
                _SelectDocIdentidad = value;
                if (NetworkInterface.GetIsNetworkAvailable())
                {
                    PropertyEnabledActualizarCaptcha = NetworkInterface.GetIsNetworkAvailable();
                    if (value != null)
                    {
                        PropertyVisibility = Visibility.Visible;
                        ESGR_Cliente.DocIdentidad = value.ValueMemberPath;
                        if (value.ValueMemberPath == "RUC")
                        {
                            MethodClearControl();
                            Application.Current.Dispatcher.InvokeAsync(async () =>
                            {
                                PropertyImageContent = await CmpConsultRUC.ReadCapcha();
                                PropertyVisibility = Visibility.Collapsed;
                            });
                        }
                        else
                        {
                            MethodClearControl();
                            Application.Current.Dispatcher.InvokeAsync(async () =>
                            {
                                PropertyImageContent = await CmpConsultDNI.ReadCapcha();
                                PropertyVisibility = Visibility.Collapsed;
                            });
                        }
                        //if (ESGR_Cliente.Opcion == null)
                        //    ESGR_Cliente.Opcion = "I";
                    }
                }
                OnPropertyChanged("SelectDocIdentidad");
            }
        }

        private ESGR_Item _SelectItemTelefono;
        public ESGR_Item SelectItemTelefono
        {
            get
            {
                return _SelectItemTelefono;
            }
            set
            {
                _SelectItemTelefono = value;
            }
        }

        #endregion

        #region PROPERTY

        private bool _PropertyEnabledActualizarCaptcha;
        public bool PropertyEnabledActualizarCaptcha
        {
            get
            {
                return _PropertyEnabledActualizarCaptcha;
            }
            set
            {
                _PropertyEnabledActualizarCaptcha = value;
                OnPropertyChanged("PropertyEnabledActualizarCaptcha");
            }
        }

        private bool _PropertyEnableDocIdentidad;
        public bool PropertyEnableDocIdentidad
        {
            get
            {
                return _PropertyEnableDocIdentidad;
            }
            set
            {
                _PropertyEnableDocIdentidad = value;
                OnPropertyChanged("PropertyEnableDocIdentidad");
            }
        }

        private Visibility _PropertyVisibility;
        public Visibility PropertyVisibility
        {
            get
            {
                return _PropertyVisibility;
            }
            set
            {
                _PropertyVisibility = value;
                OnPropertyChanged("PropertyVisibility");
            }
        }

        #endregion

        #region ICOMMAND

        public ICommand IUpdateCaptcha
        {
            get
            {
                return CmpICommand.GetICommand((U) =>
                {
                    PropertyVisibility = Visibility.Visible;
                    if (SelectDocIdentidad.ValueMemberPath == "RUC")
                    {
                        MethodClearControl();
                        Application.Current.Dispatcher.InvokeAsync(async () =>
                        {
                            PropertyImageContent = await CmpConsultRUC.ReadCapcha();
                            PropertyVisibility = Visibility.Collapsed;
                        });
                    }
                    else
                    {
                        MethodClearControl();
                        Application.Current.Dispatcher.InvokeAsync(async () =>
                        {
                            PropertyImageContent = await CmpConsultDNI.ReadCapcha();
                            PropertyVisibility = Visibility.Collapsed;
                        });
                    }
                });
            }
        }

        public ICommand IAgregarTelefono
        {
            get
            {
                return CmpICommand.GetICommand((B) =>
                {
                    var isExisteNullPhono = ESGR_Cliente.Telefono.ToList().Exists(x => x.ValueMemberPath.Trim().Length == 0);
                    if (!isExisteNullPhono)
                        ESGR_Cliente.Telefono.Add(new ESGR_Item());
                });
            }
        }

        public ICommand IQuitarTelefono
        {
            get
            {
                return CmpICommand.GetICommand((E) =>
                {
                    if (ESGR_Cliente.Telefono.Count() == 0)
                        return;
                    else
                        ESGR_Cliente.Telefono.Remove(SelectItemTelefono);
                });
            }
        }

        public ICommand IBuscar
        {
            get
            {
                return CmpICommand.GetICommand((B) =>
                {
                    if (!NetworkInterface.GetIsNetworkAvailable())
                        return;
                    else
                    {
                        if (SelectDocIdentidad.ValueMemberPath == "RUC")
                        {
                            if (ESGR_Cliente.NroDocIdentidad.Trim().Length != 11 && ESGR_Cliente.NroDocIdentidad.Trim().Length != 0)
                            {
                                CmpMessageBox.Show(SGRMessage.AdministratorCliente, "Número de Documento inválido", CmpButton.Aceptar);
                                return;
                            }
                            MethodConsultRUC();
                        }
                        else
                        {
                            if (ESGR_Cliente.NroDocIdentidad.Trim().Length != 8 && ESGR_Cliente.NroDocIdentidad.Trim().Length != 0)
                            {
                                CmpMessageBox.Show(SGRMessage.AdministratorCliente, "Número de Documento inválido", CmpButton.Aceptar);
                                return;
                            }
                            MethodConsultDNI();
                        }
                    }
                });
            }
        }

        public ICommand IGuardar
        {
            get
            {
                return CmpICommand.GetICommand(async(G) =>
                {
                    if (MethodValidarDatos())
                        return;
                    await Task.Factory.StartNew(()=>
                    {
                        try
                        {

                            if (ESGR_Cliente.Opcion == null)
                                ESGR_Cliente.Opcion = "I";
                            new BSGR_Cliente().TransCliente(ESGR_Cliente);
                            CmpMessageBox.Show(SGRMessage.AdministratorCliente, SGRMessageContent.ContentSaveOK, CmpButton.Aceptar, () =>
                            {
                                CmpModalDialog.GetContent().Close(ESGR_Cliente);
                            });
                        }
                        catch(Exception ex)
                        {
                            CmpMessageBox.Show(SGRMessage.AdministratorCliente,ex.Message,CmpButton.Aceptar);
                        }
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

        private async void  MethodLoadCaptchaDNI()
        {
            PropertyImageContent = await CmpConsultDNI.ReadCapcha();
            PropertyVisibility = Visibility.Collapsed;
        }

        private async void MethodLoadCaptchaRuc()
        {
            PropertyImageContent = await CmpConsultRUC.ReadCapcha();
            PropertyVisibility = Visibility.Hidden;
        }

        private void MethodLoadHeader()
        {
            CollectionDocIdentidad.Source = MethodLoadDocIdentidad();
            if (ESGR_Cliente.Opcion == "I" || ESGR_Cliente.Opcion == null)
            {
                SelectDocIdentidad = CollectionDocIdentidad.FirstOrDefault();
                PropertyEnableDocIdentidad = true;
            }
            else
            {
                SelectDocIdentidad = CollectionDocIdentidad.FirstOrDefault(x => x.ValueMemberPath == ESGR_Cliente.DocIdentidad);
                PropertyEnableDocIdentidad = false;
            }
        }

        private async void MethodConsultDNI()
        {
            ESGR_Cliente.Telefono.Clear();
            if (CodigoCaptcha.Trim().Length == 0)
            {
                CmpMessageBox.Show(SGRMessage.AdministratorCliente, "Campo Obligatorio: Codigo Captcha", CmpButton.Aceptar);
                return;
            }
            else if (ESGR_Cliente.NroDocIdentidad.Trim().Length == 0)
            {
                CmpMessageBox.Show(SGRMessage.AdministratorCliente, "Campo Obligatorio: Número de Documento", CmpButton.Aceptar);
                return;
            }
            else
            {
                try
                {
                    var instResult = await CmpConsultDNI.GetInfo(ESGR_Cliente.NroDocIdentidad, CodigoCaptcha);
                    switch (instResult.ResulState)
                    {
                        case CmpConsultDNI.Resul.Ok:
                            ESGR_Cliente.Cliente = instResult.Name + " " + instResult.PaternoSurnames + " " + instResult.MotherLastName;
                            break;
                        case CmpConsultDNI.Resul.NoResul:
                            ESGR_Cliente.NroDocIdentidad = string.Empty;
                            throw new Exception("No existe DNI");
                        case CmpConsultDNI.Resul.ErrorCapcha:
                            MethodLoadCaptchaDNI();
                            CodigoCaptcha = string.Empty;
                            throw new Exception("Ingrese Código de la imagen correctamente");
                        case CmpConsultDNI.Resul.Error:
                            MethodClearControl();
                            throw new Exception("Error desconocido");
                    }
                }
                catch (Exception ex)
                {
                    CmpMessageBox.Show(SGRMessage.AdministratorCliente, ex.Message, CmpButton.Aceptar);
                }
            }
        }

        private async void MethodConsultRUC()
        {
            ESGR_Cliente.Telefono.Clear();
            if (CodigoCaptcha.Trim().Length == 0)
            {
                CmpMessageBox.Show(SGRMessage.AdministratorCliente, "Campo Obligatorio: Codigo Captcha", CmpButton.Aceptar);
                return;
            }
            else if (ESGR_Cliente.NroDocIdentidad.Trim().Length == 0)
            {
                CmpMessageBox.Show(SGRMessage.AdministratorCliente, "Campo Obligatorio: Número de Documento", CmpButton.Aceptar);
                return;
            }
            else
            {
                var instResult = await CmpConsultRUC.GetInfo(ESGR_Cliente.NroDocIdentidad, CodigoCaptcha);
                ESGR_Cliente.Cliente = instResult.BusinessName;
                ESGR_Cliente.Direccion = instResult.Address;
                foreach (var item in instResult.ArrayPhone)
                {
                    if(item != "")
                        ESGR_Cliente.Telefono.Add(new ESGR_Item() { ValueMemberPath = item });
                }
            }
        }

        private void MethodClearControl()
        {
            if (ESGR_Cliente.Opcion == "I")
            {
                ESGR_Cliente.NroDocIdentidad = string.Empty;
                CodigoCaptcha = string.Empty;
                ESGR_Cliente.Cliente = string.Empty;
                ESGR_Cliente.Direccion = string.Empty;
            }
        }

        private bool MethodValidarDatos()
        {
            var vrValidaDatos = false;
            string OutMessageValid = string.Empty;
            if (ESGR_Cliente.Cliente == null || ESGR_Cliente.Cliente.Trim().Length == 0)
            {
                CmpMessageBox.Show(SGRMessage.AdministratorCliente,"Campo Obligatorio: Cliente",CmpButton.Aceptar);
                vrValidaDatos = true;
            }
            else if (ESGR_Cliente.NroDocIdentidad == null || ESGR_Cliente.NroDocIdentidad.Trim().Length == 0)
            {
                CmpMessageBox.Show(SGRMessage.AdministratorCliente, "Campo Obligatorio: Numero de Documento", CmpButton.Aceptar);
                vrValidaDatos = true;
            }
            else if (ESGR_Cliente.FechaNacimiento == null)
            {
                CmpMessageBox.Show(SGRMessage.AdministratorCliente, "Campo Obligatorio: Fecha Nacimiento", CmpButton.Aceptar);
                vrValidaDatos = true;
            }
            else if (ESGR_Cliente.Correo!=null)
            {
                if (!CmpMetodos.ValidateEmail(ESGR_Cliente.Correo, out OutMessageValid))
                {
                    CmpMessageBox.Show(SGRMessage.AdministratorCliente, OutMessageValid, CmpButton.Aceptar);
                    vrValidaDatos = true;
                }
            }
            else if (ESGR_Cliente.Direccion == null || ESGR_Cliente.Direccion.Trim().Length == 0)
            {
                CmpMessageBox.Show(SGRMessage.AdministratorCliente, "Campo Obligatorio: Direccion", CmpButton.Aceptar);
                vrValidaDatos = true;
            }
            return vrValidaDatos;
        }

        private CmpObservableCollection<ESGR_Item> MethodLoadDocIdentidad()
        {
            CmpObservableCollection<ESGR_Item> CollectionItemOpciones = new CmpObservableCollection<ESGR_Item>();
            CollectionItemOpciones.Add(new ESGR_Item { ValueMemberPath = "DNI", ValueValuePath = "DNI" });
            CollectionItemOpciones.Add(new ESGR_Item { ValueMemberPath = "RUC", ValueValuePath = "RUC" });
            return CollectionItemOpciones;
        }

        #endregion
    }
}
