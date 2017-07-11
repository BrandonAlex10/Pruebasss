/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   ABEL QUISPE ORELLANA
'* FCH. CREACIÓN : 03/12/2016
'*********************************************************
'* FECHA MODIFIC. : 21/12/2016 [OSCAR HUAMÁN CABRERA]
'* MOTIVO MODIFIC.: Se agregó carga de Grilla y funcionalidad a los botones
**********************************************************/

namespace SGR.ViewModels.Presentation.Perfil.Pages
{
    using ComputerSystems;
    using ComputerSystems.WPF;
    using ComputerSystems.WPF.Acciones.Controles.Menus;
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
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media;

    public class VMSGR_ListadoPerfil : CmpNavigationService, CmpINavigation
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
                        MethodLoadDetail();
                    }
                });
            }
        }
        
        #region OBJETOS SECUNDARIOS

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
            }
        }

        #endregion

        #region COLECCIONES

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
                    CmpModalDialog.GetContent().ShowDialog("MPSGR_Perfil", new ESGR_Perfil { Opcion = "I" });
                    MethodLoadDetail();
                });
            }
        }

        public ICommand IEditar
        {
            get
            {
                return CmpICommand.GetICommand((T) =>
                {
                    if (SelectedESGR_Perfil == null)
                    {
                        CmpMessageBox.Show(SGRMessage.AdministratorPerfil, SGRMessageContent.ContentSelectNull + "Editar", CmpButton.Aceptar);
                        return;
                    }
                    SelectedESGR_Perfil.Opcion = "U";
                    CmpModalDialog.GetContent().ShowDialog("MPSGR_Perfil", SelectedESGR_Perfil);
                    MethodLoadDetail();
                });
            }
        }

        public ICommand IEliminar
        {
            get
            {
                return CmpICommand.GetICommand(async (T) =>
                {
                    if (SelectedESGR_Perfil == null)
                    {
                        CmpMessageBox.Show(SGRMessage.AdministratorPerfil, SGRMessageContent.ContentSelectNull + "Eliminar", CmpButton.Aceptar);
                        return;
                    }
                    await Task.Factory.StartNew(() =>
                    {
                        try
                        {
                            CmpMessageBox.Show(SGRMessage.AdministratorPerfil, "¿Seguro que desea eliminar el registro?", CmpButton.AceptarCancelar, () =>
                            {
                                SelectedESGR_Perfil.Opcion = "D";
                                new BSGR_Perfil().TransPerfil(SelectedESGR_Perfil);
                                CmpMessageBox.Show(SGRMessage.AdministratorPerfil, SGRMessageContent.ContentDeleteOK, CmpButton.Aceptar);
                                MethodLoadDetail();
                            });
                        }
                        catch (Exception ex)
                        {
                            CmpMessageBox.Show(SGRMessage.AdministratorPerfil, ex.Message, CmpButton.Aceptar);
                        }
                    });
                });
            }
        }

        public ICommand ISalir
        {
            get
            {
                return CmpICommand.GetICommand((V) =>
                {
                    Volver();
                });
            }
        }

        #endregion

        #region METODOS

        private async void MethodLoadDetail()
        {
            await Task.Factory.StartNew(() =>
            {
                try
                {
                    CollectionESGR_Perfil.Source = new BSGR_Perfil().GetCollectionPerfil();
                }
                catch (Exception ex)
                {
                    CmpMessageBox.Show(SGRMessage.AdministratorPerfil, ex.Message, CmpButton.Aceptar);
                }
            });
        }

        #endregion
    }
}
