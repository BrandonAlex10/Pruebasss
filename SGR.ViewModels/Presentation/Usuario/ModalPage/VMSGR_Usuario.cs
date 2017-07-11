/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   OSCAR HUAMAN CABRERA
'* FCH. CREACIÓN : 01/12/2016
'*********************************************************
'* FECHA MODIFIC. : 21/12/2016 [OSCAR HUAMÁN CABRERA]
'* MOTIVO MODIFIC.: Se agregó funcionalidad a los botones
**********************************************************/

namespace SGR.ViewModels.Presentation.Usuario.ModalPage
{
    using ComputerSystems;
    using ComputerSystems.Loading;
    using ComputerSystems.Methods;
    using ComputerSystems.WPF;
    using ComputerSystems.WPF.Acciones.Controles.ModalDialog;
    using ComputerSystems.WPF.Interfaces;
    using SGR.Models;
    using SGR.Models.Business;
    using SGR.Models.Entity;
    using SGR.ViewModels.Method;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Input;

    public class VMSGR_Usuario : CmpNotifyPropertyChanged, CmpIModalDialog
    {
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

        public CmpLoading CmpLoading { get; set; }

        public object Parameter
        {
            set
            {
                Application.Current.Dispatcher.Invoke(async() =>
                {
                    var result = await MTSGR_GetPrivilege.Process(this.GetType(), SGRMessage.TitleMessage, SGRMessageContent.GetAccesoRestringido);
                    if (result == null)
                        CmpModalDialog.GetContent().Close();
                    else if (value is ESGR_Usuario)
                    {
                        ESGR_Usuario = (ESGR_Usuario)value;
                        CmpLoading = new CmpLoading(MethodLoadEmpresaSucursal, MethodLoadComboDetail);
                        CmpLoading.Exceptions = ((e) => { CmpMessageBox.Show(SGRMessage.AdministratorProducto, e.Message, CmpButton.Aceptar); });
                        CmpLoading.LoadHeader();
                        MethodLoadPerfil();
                        if (ESGR_Usuario.Opcion != "I")
                        {
                            PropertyIsEnabled = false;
                            MethodLoadDetail();
                        }
                        else
                            PropertyIsEnabled = true;
                    }
                });
            }
        }

        #region OBJETOS SECUNDARIOS

        private ESGR_EmpresaSucursal _SelectedHeaderESGR_EmpresaSucursal;
        public ESGR_EmpresaSucursal SelectedHeaderESGR_EmpresaSucursal
        {
            get
            {
                return _SelectedHeaderESGR_EmpresaSucursal;
            }
            set
            {
                _SelectedHeaderESGR_EmpresaSucursal = value;
                if (value != null)
                {
                    ESGR_Usuario.ESGR_EmpresaSucursal = value;
                    if (!CollectionDataGridESGR_EmpresaSucursal.ToList().Exists(x => x.IdEmpSucursal == value.IdEmpSucursal))
                    {
                        CollectionDataGridESGR_EmpresaSucursal.Add(value);
                        CollectionESGR_EmpresaSucursal.Remove(CollectionDataGridESGR_EmpresaSucursal.FirstOrDefault(x => x.IdEmpSucursal == value.IdEmpSucursal));
                    }
                }
                OnPropertyChanged("SelectedHeaderESGR_EmpresaSucursal");
            }
        }

        private ESGR_EmpresaSucursal _SelectedESGR_EmpresaSucursal;
        public ESGR_EmpresaSucursal SelectedESGR_EmpresaSucursal
        {
            get
            {
                return _SelectedESGR_EmpresaSucursal;
            }
            set
            {
                _SelectedESGR_EmpresaSucursal = value;
                OnPropertyChanged("SelectedESGR_EmpresaSucursal");
            }
        }

        private ESGR_EmpresaSucursal _SelectedDataGridESGR_EmpresaSucursal;
        public ESGR_EmpresaSucursal SelectedDataGridESGR_EmpresaSucursal
        {
            get
            {
                return _SelectedDataGridESGR_EmpresaSucursal;
            }
            set
            {
                _SelectedDataGridESGR_EmpresaSucursal = value;
                OnPropertyChanged("SelectedDataGridESGR_EmpresaSucursal");
            }
        }

        private ESGR_Area _SelectedESGR_Area;
        public ESGR_Area SelectedESGR_Area
        {
            get
            {
                return _SelectedESGR_Area;
            }
            set
            {
                _SelectedESGR_Area = value;
                OnPropertyChanged("SelectedESGR_Area");
            }
        }

        private ESGR_Perfil _SelectedESGR_Perfil;
        public ESGR_Perfil SelectedESGR_Perfil
        {
            get
            {
                return _SelectedESGR_Perfil;
            }
            set
            {
                _SelectedESGR_Perfil = value;
                if (value != null)
                    ESGR_Usuario.ESGR_Perfil = value;
                OnPropertyChanged("SelectedESGR_Perfil");
            }
        }

        private ESGR_Area _SelectedDataGridESGR_Area;
        public ESGR_Area SelectedDataGridESGR_Area
        {
            get
            {
                return _SelectedDataGridESGR_Area;
            }
            set
            {
                _SelectedDataGridESGR_Area = value;
                OnPropertyChanged("SelectedDataGridESGR_Area");
            }
        }

        #endregion

        #region COLECCIONES

        private CmpObservableCollection<ESGR_EmpresaSucursal> _CollectionHeaderESGR_EmpresaSucursal;
        public CmpObservableCollection<ESGR_EmpresaSucursal> CollectionHeaderESGR_EmpresaSucursal
        {
            get
            {
                if (_CollectionHeaderESGR_EmpresaSucursal == null)
                    _CollectionHeaderESGR_EmpresaSucursal = new CmpObservableCollection<ESGR_EmpresaSucursal>();
                return _CollectionHeaderESGR_EmpresaSucursal;
            }
        }

        private CmpObservableCollection<ESGR_Perfil> _CollectionESGR_Perfil;
        public CmpObservableCollection<ESGR_Perfil> CollectionESGR_Perfil
        {
            get
            {
                if (_CollectionESGR_Perfil == null)
                    _CollectionESGR_Perfil = new CmpObservableCollection<ESGR_Perfil>();
                return _CollectionESGR_Perfil;
            }
        }

        private CmpObservableCollection<ESGR_EmpresaSucursal> _CollectionESGR_EmpresaSucursal;
        public CmpObservableCollection<ESGR_EmpresaSucursal> CollectionESGR_EmpresaSucursal
        {
            get
            {
                if (_CollectionESGR_EmpresaSucursal == null)
                    _CollectionESGR_EmpresaSucursal = new CmpObservableCollection<ESGR_EmpresaSucursal>();
                return _CollectionESGR_EmpresaSucursal;
            }
        }

        private CmpObservableCollection<ESGR_EmpresaSucursal> _CollectionDataGridESGR_EmpresaSucursal;
        public CmpObservableCollection<ESGR_EmpresaSucursal> CollectionDataGridESGR_EmpresaSucursal
        {
            get
            {
                if (_CollectionDataGridESGR_EmpresaSucursal == null)
                    _CollectionDataGridESGR_EmpresaSucursal = new CmpObservableCollection<ESGR_EmpresaSucursal>();
                return _CollectionDataGridESGR_EmpresaSucursal;
            }
        }

        private CmpObservableCollection<ESGR_Area> _CollectionESGR_Area;
        public CmpObservableCollection<ESGR_Area> CollectionESGR_Area
        {
            get
            {
                if (_CollectionESGR_Area == null)
                    _CollectionESGR_Area = new CmpObservableCollection<ESGR_Area>();
                return _CollectionESGR_Area;
            }
        }

        private CmpObservableCollection<ESGR_Area> _CollectionDataGridESGR_Area;
        public CmpObservableCollection<ESGR_Area> CollectionDataGridESGR_Area
        {
            get
            {
                if (_CollectionDataGridESGR_Area == null)
                    _CollectionDataGridESGR_Area = new CmpObservableCollection<ESGR_Area>();
                return _CollectionDataGridESGR_Area;
            }
        }

        #endregion

        #region PROPERTY

        private bool _PropertyIsEnabled;
        public bool PropertyIsEnabled
        {
            get
            {
                return _PropertyIsEnabled;
            }
            set
            {
                _PropertyIsEnabled = value;
                OnPropertyChanged("PropertyIsEnabled");
            }
        }

        #endregion

        #region ICOMMAND

        public ICommand IGuardar
        {
            get
            {
                return CmpICommand.GetICommand((G) =>
                {
                    if (MethodValidarDatos())
                        return;

                    string strMessage = string.Empty;

                    ESGR_Usuario.CadenaSucursalXML = MethodCadenaSucursalXML();
                    ESGR_Usuario.CadenaAreaXML = MethodCadenaAreaXML();
                    if (ESGR_Usuario.Opcion == "I") ESGR_Usuario.Contrasenia = CmpMetodos.CreatePassword(10);
                        
                    CmpMessageBox.Proccess(SGRMessage.AdministratorUsuario, "Procesando datos, por favor espere...", () =>
                    {
                        try
                        {
                            new BSGR_Usuario().TransUsuario(ESGR_Usuario);
                            if (ESGR_Usuario.Opcion == "I")
                            {
                                ///Envia la nueva contraseña generada al correo del usuario
                                CmpMail objMail = new CmpMail("mail.computersystems.com.pe", "notificaciones@computersystems.com.pe", "?du0@v31@LbE");
                                objMail.ValorDelReceptor(ESGR_Usuario.Correo, "Registro de Usuario - SGR", MethodMessageContent(ESGR_Usuario.Contrasenia));
                                objMail.EnviarEmail();
                            }
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

        public ICommand IAgregarEmpSucursal
        {
            get
            {
                return CmpICommand.GetICommand((A) =>
                {
                    if (SelectedESGR_EmpresaSucursal == null) return;
                    if (!CollectionDataGridESGR_EmpresaSucursal.ToList().Exists(x => x.IdEmpSucursal == SelectedESGR_EmpresaSucursal.IdEmpSucursal))
                        CollectionDataGridESGR_EmpresaSucursal.Add(SelectedESGR_EmpresaSucursal);
                    CollectionESGR_EmpresaSucursal.Remove(SelectedESGR_EmpresaSucursal);
                });
            }
        }

        public ICommand IEliminarEmpSucursal
        {
            get
            {
                return CmpICommand.GetICommand((A) =>
                {
                    CollectionESGR_EmpresaSucursal.Add(SelectedDataGridESGR_EmpresaSucursal);
                    CollectionDataGridESGR_EmpresaSucursal.Remove(SelectedDataGridESGR_EmpresaSucursal);
                });
            }
        }

        public ICommand IAgregarArea
        {
            get
            {
                return CmpICommand.GetICommand((A) =>
                {
                    if (SelectedESGR_Area == null) return;
                    CollectionDataGridESGR_Area.Add(SelectedESGR_Area);
                    CollectionESGR_Area.Remove(SelectedESGR_Area);
                });
            }
        }

        public ICommand IEliminarArea
        {
            get
            {
                return CmpICommand.GetICommand((A) =>
                {
                    CollectionESGR_Area.Add(SelectedDataGridESGR_Area);
                    CollectionDataGridESGR_Area.Remove(SelectedDataGridESGR_Area);
                });
            }
        }

        public ICommand IVolver
        {
            get
            {
                return CmpICommand.GetICommand((T) =>
                {
                    CmpModalDialog.GetContent().Close();
                });
            }
        }

        #endregion

        #region METODOS

        private async void MethodLoadEmpresaSucursal()
        {
            await Task.Factory.StartNew(() =>
            {
                try
                {
                    CollectionHeaderESGR_EmpresaSucursal.Source = new BSGR_EmpresaSucursal().GetCollectionEmpresaSucursal(SGRVariables.ESGR_Usuario.ESGR_Empresa);
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        if (ESGR_Usuario.Opcion == "I")
                            SelectedHeaderESGR_EmpresaSucursal = CollectionHeaderESGR_EmpresaSucursal.FirstOrDefault();
                        else
                            SelectedHeaderESGR_EmpresaSucursal = CollectionHeaderESGR_EmpresaSucursal.FirstOrDefault(x => x.IdEmpSucursal == ESGR_Usuario.ESGR_EmpresaSucursal.IdEmpSucursal);
                    });
                }
                catch (Exception ex)
                {
                    CmpMessageBox.Show(SGRMessage.AdministratorUsuario, ex.Message, CmpButton.Aceptar);
                }
            });
        }

        private async void MethodLoadPerfil()
        {
            await Task.Factory.StartNew(() =>
            {
                try
                {
                    CollectionESGR_Perfil.Source = new BSGR_Perfil().GetCollectionPerfil();
                    Application.Current.Dispatcher.Invoke(()=>
                    {
                        if (ESGR_Usuario.Opcion == "I")
                            SelectedESGR_Perfil = CollectionESGR_Perfil.FirstOrDefault();
                        else
                            SelectedESGR_Perfil = CollectionESGR_Perfil.FirstOrDefault(x => x.IdPerfil == ESGR_Usuario.ESGR_Perfil.IdPerfil);
                    });
                }
                catch (Exception ex)
                {
                    CmpMessageBox.Show(SGRMessage.AdministratorUsuario, ex.Message, CmpButton.Aceptar);
                }
            });
        }

        private async void MethodLoadComboDetail()
        {
            await Task.Factory.StartNew(() =>
            {
                try
                {
                    CollectionESGR_EmpresaSucursal.Source = new BSGR_EmpresaSucursal().GetCollectionEmpresaSucursal(SGRVariables.ESGR_Usuario.ESGR_Empresa);
                    CollectionESGR_Area.Source = new CmpObservableCollection<ESGR_Area>(new BSGR_Area().GetCollectionArea().Where(x => x.ESGR_Empresa.IdEmpresa == SGRVariables.ESGR_Usuario.ESGR_Empresa.IdEmpresa));
                }
                catch (Exception ex)
                {
                    CmpMessageBox.Show(SGRMessage.AdministratorUsuario, ex.Message, CmpButton.Aceptar);
                }
            });
        }
        
        private async void MethodLoadDetail()
        {
            await Task.Factory.StartNew(() =>
            {
                try
                {
                    CmpObservableCollection<ESGR_UsuarioEmpresaSucursal> vrCollectionUsuarioEmpresaSucursal = new CmpObservableCollection<ESGR_UsuarioEmpresaSucursal>(new BSGR_UsuarioEmpresaSucursal().GetCollectionUsuarioEmpresaSucursal().Where(x => x.ESGR_Usuario.IdUsuario == ESGR_Usuario.IdUsuario));
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        CollectionDataGridESGR_EmpresaSucursal.Clear();
                        vrCollectionUsuarioEmpresaSucursal.ToList().ForEach(x =>
                        {
                            if (CollectionESGR_EmpresaSucursal.ToList().Exists(y => y.IdEmpSucursal == x.ESGR_EmpresaSucursal.IdEmpSucursal))
                            {
                                ESGR_EmpresaSucursal Aux = CollectionESGR_EmpresaSucursal.FirstOrDefault(y => y.IdEmpSucursal == x.ESGR_EmpresaSucursal.IdEmpSucursal);
                                CollectionDataGridESGR_EmpresaSucursal.Add(Aux);
                                CollectionESGR_EmpresaSucursal.Remove(Aux);
                            }
                        });
                    });

                    CmpObservableCollection<ESGR_UsuarioArea> vrCollectionUsuarioArea = new CmpObservableCollection<ESGR_UsuarioArea>(new BSGR_UsuarioArea().GetCollectionUsuarioArea().Where(x => x.ESGR_Usuario.IdUsuario == ESGR_Usuario.IdUsuario));
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        CollectionDataGridESGR_Area.Clear();
                        vrCollectionUsuarioArea.ToList().ForEach(x =>
                        {
                            if (CollectionESGR_Area.ToList().Exists(y => y.IdArea == x.ESGR_Area.IdArea))
                            {
                                ESGR_Area Aux = CollectionESGR_Area.FirstOrDefault(y => y.IdArea == x.ESGR_Area.IdArea);
                                CollectionDataGridESGR_Area.Add(Aux);
                                CollectionESGR_Area.Remove(Aux);
                            }
                        });
                    });
                }
                catch (Exception ex)
                {
                    CmpMessageBox.Show(SGRMessage.AdministratorUsuario, ex.Message, CmpButton.Aceptar);
                }
            });
        }

        private string MethodCadenaSucursalXML()
        {
            string vrCadena = string.Empty;
            vrCadena = "<ROOT>";
            CollectionDataGridESGR_EmpresaSucursal.ToList().ForEach((f) =>
            {
                vrCadena += "<Listar ";
                vrCadena += "xIdEmpSucursal = \'" + f.IdEmpSucursal;
                vrCadena += "\'></Listar>";
            });

            vrCadena += "</ROOT>";
            return vrCadena;
        }

        private string MethodCadenaAreaXML()
        {
            string vrCadena = string.Empty;
            vrCadena = "<ROOT>";
            CollectionDataGridESGR_Area.ToList().ForEach((f) =>
            {
                vrCadena += "<Listar ";
                vrCadena += "xIdArea = \'" + f.IdArea;
                vrCadena += "\'></Listar>";
            });
            vrCadena += "</ROOT>";
            return vrCadena;
        }

        private string MethodMessageContent(string Contrasenia)
        {
            string vrContent = string.Empty;
                vrContent += "Hola <b>" + ESGR_Usuario.Nombres + "</b><br /><br />";
                vrContent += "Usted a sido registrado satisfactoriamente en nuestra aplicación SGR - SISTEMA DE GESTIÓN PARA RESTAURANT. <br /><br />";
                vrContent += "Sus datos de acceso generados son: <br /><br />";
                vrContent += "<b>Usuario: </b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + ESGR_Usuario.Usuario + "<br />";
                vrContent += "<b>Contraseña:</b>&nbsp;" + Contrasenia + "<br />";
                vrContent += (ESGR_Usuario.Nick == null || ESGR_Usuario.Nick.Trim().Length == 0) ? "<br />" : "<b>Nick:</b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + ESGR_Usuario.Nick + "<br />";
                vrContent += "<br /><b> - - - - - - - - - </b><br />";
                vrContent += "<b>Mensaje enviado desde la aplicación: SGR.</b><br /><br />";
                vrContent += "<b>Computer Systems Solution</b><br />";
                vrContent += "<a href=\"http://www.computersystems.com.pe/\"><b>www.computersystems.com.pe</b></a>";
            return vrContent;
        }

        private bool MethodValidarDatos()
        {
            string Message = string.Empty;
            if (ESGR_Usuario.ESGR_Perfil == null || ESGR_Usuario.ESGR_Perfil.IdPerfil == 0)
            {
                CmpMessageBox.Show(SGRMessage.AdministratorUsuario, "Campo Obligatorio: Tipo Usuario [Perfil].", CmpButton.Aceptar);
                return true;
            }
            else if(ESGR_Usuario.Nombres == null || ESGR_Usuario.Nombres.Trim().Length == 0)
            {
                CmpMessageBox.Show(SGRMessage.AdministratorUsuario, "Campo Obligatorio: Nombre de Usuario.", CmpButton.Aceptar);
                return true;
            }
            else if (ESGR_Usuario.Apellidos == null || ESGR_Usuario.Apellidos.Trim().Length == 0)
            {
                CmpMessageBox.Show(SGRMessage.AdministratorUsuario, "Campo Obligatorio: Apellido de Usuario.", CmpButton.Aceptar);
                return true;
            }
            else if (ESGR_Usuario.Correo == null || ESGR_Usuario.Correo.Trim().Length == 0)
            {
                CmpMessageBox.Show(SGRMessage.AdministratorUsuario, "Campo Obligatorio: Correo de Usuario.", CmpButton.Aceptar);
                return true;
            }
            else if (!CmpMetodos.ValidateEmail(ESGR_Usuario.Correo, out Message))
            {
                CmpMessageBox.Show(SGRMessage.AdministratorUsuario, Message, CmpButton.Aceptar);
                return true;
            }
            else if (ESGR_Usuario.Usuario == null || ESGR_Usuario.Usuario.Trim().Length == 0)
            {
                CmpMessageBox.Show(SGRMessage.AdministratorUsuario, "Campo Obligatorio: Usuario.", CmpButton.Aceptar);
                return true;
            }
            else if( CollectionDataGridESGR_EmpresaSucursal.Count == 0)
            {
                CmpMessageBox.Show(SGRMessage.AdministratorUsuario, "Ingrese al menos una Sucursal.", CmpButton.Aceptar);
                return true;
            }
            else if (CollectionDataGridESGR_Area.Count == 0)
            {
                CmpMessageBox.Show(SGRMessage.AdministratorUsuario, "Ingrese al menos una Área.", CmpButton.Aceptar);
                return true;
            }
            return false;
        }

        #endregion
    }
}
