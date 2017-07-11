/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   OSCAR HUAMAN CABRERA
'* FCH. CREACIÓN : 28/11/2016
'*********************************************************
'* FECHA MODIFIC. : 21/12/2016 [OSCAR HUAMÁN CABRERA]
'* MOTIVO MODIFIC.: Se agregó funcionalidad a los botones
**********************************************************/

namespace SGR.ViewModels.Presentation.Perfil.ModalPage
{
    using ComputerSystems;
    using ComputerSystems.WPF;
    using ComputerSystems.WPF.Acciones.Controles.ModalDialog;
    using ComputerSystems.WPF.Interfaces;
    using ComputerSystems.WPF.Notificaciones;
    using SGR.Models;
    using SGR.Models.Business;
    using SGR.Models.Entity;
    using SGR.ViewModels.Method;
    using SGR.ViewModels.Presentation.Perfil.Controls;
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

    public class VMSGR_Perfil : CmpNotifyPropertyChanged, CmpIModalDialog
    {
        private ESGR_Perfil _ESGR_Perfil;
        public ESGR_Perfil ESGR_Perfil
        {
            get
            {
                return _ESGR_Perfil;
            }
            set
            {
                _ESGR_Perfil = value;
                OnPropertyChanged("ESGR_Perfil");
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
                    else if (value is ESGR_Perfil)
                    {
                        ESGR_Perfil = (ESGR_Perfil)value;
                        LoadPermisoPerfil();
                    }
                });
            }
        }

        #region COLECCIONES

        private CmpObservableCollection<ESGR_Modulo> _CollectionESGR_Modulo;
        public CmpObservableCollection<ESGR_Modulo> CollectionESGR_Modulo
        {
            get
            {
                if (_CollectionESGR_Modulo == null)
                    _CollectionESGR_Modulo = new CmpObservableCollection<ESGR_Modulo>();
                return _CollectionESGR_Modulo;
            }
        }

        private CmpObservableCollection<VMSGR_Modulo> _CollectionVMSGR_Modulo;
        public CmpObservableCollection<VMSGR_Modulo> CollectionVMSGR_Modulo
        {
            get
            {
                if (_CollectionVMSGR_Modulo == null)
                    _CollectionVMSGR_Modulo = new CmpObservableCollection<VMSGR_Modulo>();
                return _CollectionVMSGR_Modulo;
            }
        }

        #endregion

        #region PROPERTY
        #endregion

        #region OBJ SECUNDARIOS
        #endregion

        #region ICOMMAND

        public ICommand IGuardar
        {
            get
            {
                return CmpICommand.GetICommand((G) =>
                {
                    try
                    {
                        if (MethodValidarDatos()) return;
                        ESGR_Perfil.XMLPermisoPerfil = XmlPermisoPerfil();
                        new BSGR_Perfil().TransPerfil(ESGR_Perfil);
                        CmpMessageBox.Show(SGRMessage.AdministratorPerfil, SGRMessageContent.ContentSaveOK, CmpButton.Aceptar, () =>
                        {
                            CmpModalDialog.GetContent().Close();
                        });
                    }
                    catch (Exception ex)
                    {
                        CmpMessageBox.Show(SGRMessage.AdministratorPerfil, ex.Message, CmpButton.Aceptar);
                    }
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

        private async void LoadPermisoPerfil()
        {
            var vrListFormulario = new List<ESGR_Formulario>();
            var vrListEPermisoPerfil = new List<ESGR_PermisoPerfil>();

            await Task.Factory.StartNew(
            () =>
            {
                try
                {
                    vrListFormulario = new BSGR_Formulario().ListGetAllFormulario();

                    if (ESGR_Perfil != null)
                    {
                        vrListEPermisoPerfil = new BSGR_PermisoPerfil().GetCollectionPermisoPerfil(ESGR_Perfil.IdPerfil).ToList();

                        ///Realizamos equivalencia 
                        foreach (var yy in vrListFormulario)
                        {
                            foreach (var ss in vrListEPermisoPerfil)
                            {
                                if (yy.CodFormulario == ss.ESGR_Formulario.CodFormulario)
                                {
                                    yy.Consulta = ss.Consulta;
                                    yy.Nuevo = ss.Nuevo;
                                    yy.Editar = ss.Editar;
                                    yy.Eliminar = ss.Eliminar;
                                }
                            }
                        }

                        ///Obtenemos solo los nombres de los módulos
                        var vrGetNombreModulo = vrListFormulario.Select(d => new { d.ESGR_Modulo.NombreModulo, d.ESGR_Modulo.IdModulo })
                                                .Distinct()
                                                .OrderByDescending(d => d.NombreModulo).ToList();

                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            CollectionVMSGR_Modulo.Clear();
                        });
                        ///Recorremos la lista de los nombres de Módulo
                        foreach (var tt in vrGetNombreModulo)
                        {
                            ///Listamos solo los formularios cuyo nombre sea igual al nombre del Módulo
                            var vrItemsFomulario = vrListFormulario.Where(x => x.ESGR_Modulo.NombreModulo == tt.NombreModulo).ToList();
                            Application.Current.Dispatcher.Invoke(() =>
                            {
                                CollectionVMSGR_Modulo.Add(new VMSGR_Modulo()
                                {
                                    HeaderModulo = tt.NombreModulo,
                                    CollectionESGR_FormularioAll = new ObservableCollection<ESGR_Formulario>(vrItemsFomulario),
                                    IdModulo = tt.IdModulo
                                });
                            });
                        }
                    }

                }
                catch (Exception ex)
                {
                    CmpMessageBox.Show(SGRMessage.TitleMessage, ex.Message, CmpButton.Aceptar);
                }
            });

        }

        private bool MethodValidarDatos()
        {
            var vrValidaDatos = false;
            if (ESGR_Perfil.NombrePerfil == null || ESGR_Perfil.NombrePerfil.Trim().Length == 0)
            {
                CmpMessageBox.Show(SGRMessage.AdministratorPerfil, "Campo Obligatorio: Nombre Perfil", CmpButton.Aceptar);
                vrValidaDatos = true;
            }
            else if (ESGR_Perfil.Descripcion == null || ESGR_Perfil.Descripcion.Trim().Length == 0)
            {
                CmpMessageBox.Show(SGRMessage.AdministratorPerfil, "Campo Obligatorio: Descripcion", CmpButton.Aceptar);
                vrValidaDatos = true;
            }
            return vrValidaDatos;
        }

        private string XmlPermisoPerfil()
        {
            var vrTempListFormulario = new List<VMSGR_Formulario>();

            foreach (var ff in CollectionVMSGR_Modulo)
            {
                ///Recorremos la lista de vrListEFormulario
                foreach (var NewItemsEFormulario in ff.CollectionVMSGR_Formulario)
                {
                    ///Si los atributos de la lista son true entonces se agrega el Items en una lista temporal   
                    vrTempListFormulario.Add(NewItemsEFormulario);
                }
            }

            string strCadXml = "";
            strCadXml = "<ROOT>";
            vrTempListFormulario.ForEach((VMSGR_Formulario f) =>
            {
                strCadXml += "<Listar xCodFormulario = \"" + f.CodFormulario +
                                "\" xConsulta = \"" + f.Consulta +
                                "\" xNuevo = \"" + f.Nuevo +
                                "\" xEditar = \"" + f.Editar +
                                "\" xEliminar = \"" + f.Eliminar +
                                "\" ></Listar>";
            });
            strCadXml += "</ROOT>";

            return strCadXml;
        }

        #endregion
    }
}
