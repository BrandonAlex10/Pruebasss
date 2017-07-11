/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   ABEL QUISPE ORELLANA
'* FCH. CREACIÓN : 03/12/2016
'*********************************************************
'* FECHA MODIFIC. : 21/12/2016 [OSCAR HUAMÁN CABRERA]
'* MOTIVO MODIFIC.: Se agregó carga de Grilla y funcionalidad a los botones
**********************************************************/

namespace SGR.ViewModels.Presentation.Usuario.Pages
{
    using ComputerSystems;
    using ComputerSystems.WPF;
    using ComputerSystems.WPF.Acciones.Controles.ModalDialog;
    using ComputerSystems.WPF.Interfaces;
    using ComputerSystems.WPF.Notificaciones;
    using SGR.Models.Business;
    using SGR.Models.Entity;
    using SGR.ViewModels.Method;
    using System;
    using System.Linq;
    using System.Windows;
    using System.Windows.Input;

    public class VMSGR_ListadoUsuario : CmpNavigationService, CmpINavigation
    {
        public object Parameter
        {
            set
            {
                Application.Current.Dispatcher.Invoke(async () =>
                {
                    var result = await MTSGR_GetPrivilege.Process(this.GetType(), SGRMessage.TitleMessage, SGRMessageContent.GetAccesoRestringido);
                    if (result == null)
                        Volver();
                    else
                    {
                        PropertyIsEnabledNuevo = result.Nuevo;
                        PropertyIsEnabledEditar = result.Editar;
                        PropertyIsEnabledEliminar = result.Eliminar;
                        MethodLoadDetails();
                    }
                });
                
            }
        }

        #region OBJETOS SECUNDARIOS

        private ESGR_Usuario _SelectedESGR_Usuario;
        public ESGR_Usuario SelectedESGR_Usuario
        {
            get
            {
                return _SelectedESGR_Usuario;
            }
            set
            {
                _SelectedESGR_Usuario = value;
                OnPropertyChanged("SelectedESGR_Usuario");
            }
        }

        private string _Filtro;
        public string Filtro
        {
            get
            {
                return _Filtro;
            }
            set
            {
                _Filtro = value;
                if (value != null)
                    MethodLoadDetails(value);
                OnPropertyChanged("Filtro");
            }
        }

        #endregion

        #region COLECCIONES

        private CmpObservableCollection<ESGR_Usuario> _CollectionAuxESGR_Usuario;
        public CmpObservableCollection<ESGR_Usuario> CollectionAuxESGR_Usuario
        {
            get
            {
                if (_CollectionAuxESGR_Usuario == null)
                    _CollectionAuxESGR_Usuario = new CmpObservableCollection<ESGR_Usuario>();
                return _CollectionAuxESGR_Usuario;
            }
        }

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

        #region PROPERTY

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

        private bool _PropertyIsEnabledEditar;
        public bool PropertyIsEnabledEditar
        {
            get
            {
                return _PropertyIsEnabledEditar;
            }
            set
            {
                _PropertyIsEnabledEditar = value;
                OnPropertyChanged("PropertyIsEnabledEditar");
            }
        }

        private bool _PropertyIsEnabledEliminar;
        public bool PropertyIsEnabledEliminar
        {
            get
            {
                return _PropertyIsEnabledEliminar;
            }
            set
            {
                _PropertyIsEnabledEliminar = value;
                OnPropertyChanged("PropertyIsEnabledEliminar");
            }
        }

        #endregion

        #region ICOMMAND

        public ICommand INuevo
        {
            get
            {
                return CmpICommand.GetICommand((N) =>
                {
                    CmpModalDialog.GetContent().ShowDialog("MPSGR_Usuario", new ESGR_Usuario { Opcion = "I" });
                    MethodLoadDetails();
                });
            }
        }

        public ICommand IEditar
        {
            get
            {
                return CmpICommand.GetICommand((U) =>
                {
                    if (SelectedESGR_Usuario == null)
                    {
                        CmpMessageBox.Show(SGRMessage.AdministratorUsuario, SGRMessageContent.ContentSelectNull + "Editar.", CmpButton.Aceptar);
                        return;
                    }
                    SelectedESGR_Usuario.Opcion = "U";
                    CmpModalDialog.GetContent().ShowDialog("MPSGR_Usuario", SelectedESGR_Usuario);
                    MethodLoadDetails();
                });
            }
        }

        public ICommand IHabilitar
        {
            get
            {
                return CmpICommand.GetICommand((H) =>
                {
                    CmpModalDialog.GetContent().ShowDialog("MPSGR_HabilitarUsuario", new CmpObservableCollection<ESGR_Usuario>(CollectionAuxESGR_Usuario.Where(x => x.FlgEliminado == false)));
                    MethodLoadDetails();
                });
            }
        }

        public ICommand IEliminar
        {
            get
            {
                return CmpICommand.GetICommand((D) =>
                    {
                        if (SelectedESGR_Usuario == null)
                        {
                            CmpMessageBox.Show(SGRMessage.AdministratorUsuario, SGRMessageContent.ContentSelectNull + "Eliminar.", CmpButton.Aceptar);
                            return;
                        }
                        CmpMessageBox.Show(SGRMessage.AdministratorUsuario, "¿Seguro que desea eliminar el registro?", CmpButton.AceptarCancelar, () =>
                            {
                                try
                                {
                                    SelectedESGR_Usuario.Opcion = "D";
                                    new BSGR_Usuario().TransUsuario(SelectedESGR_Usuario);
                                    CmpMessageBox.Show(SGRMessage.AdministratorUsuario, SGRMessageContent.ContentDeleteOK, CmpButton.Aceptar);
                                    MethodLoadDetails();
                                }
                                catch (Exception ex)
                                {
                                    CmpMessageBox.Show(SGRMessage.AdministratorUsuario, ex.Message, CmpButton.Aceptar);
                                }
                            });
                    });
            }
        }

        public ICommand ISalir
        {
            get
            {
                return CmpICommand.GetICommand((S) =>
                {
                    Volver();
                });
            }
        }

        #endregion

        #region METODOS

        public void MethodLoadDetails(string Filtro = "%")
        {
            try
            {
                CollectionAuxESGR_Usuario.Source = new BSGR_Usuario().GetCollectionUsuario(Filtro);
                CollectionESGR_Usuario.Source = new CmpObservableCollection<ESGR_Usuario>(CollectionAuxESGR_Usuario.Where(x => x.FlgEliminado == true));
            }
            catch (Exception ex)
            {
                CmpMessageBox.Show(SGRMessage.AdministratorUsuario, ex.Message, CmpButton.Aceptar);
            }
        }

        #endregion
    }
}
