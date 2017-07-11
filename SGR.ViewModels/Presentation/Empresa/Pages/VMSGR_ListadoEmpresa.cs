/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
 * ********************************************************
 * 
'* FCH. CREACIÓN : 03/12/2016 [ABEL QUISPE ORELLANA]
 * 
 * ********************************************************
 * 
**********************************************************/

namespace SGR.ViewModels.Presentation.Empresa.Pages
{
    using ComputerSystems;
    using ComputerSystems.WPF;
    using ComputerSystems.WPF.Acciones.Controles.ModalDialog;
    using ComputerSystems.WPF.Interfaces;
    using ComputerSystems.WPF.Acciones.Controles.Menus;
    using ComputerSystems.WPF.Notificaciones;
    using SGR.Models.Business;
    using SGR.Models.Entity;
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
    using SGR.ViewModels.Method;

    public class VMSGR_ListadoEmpresa : CmpNavigationService, CmpINavigation
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
                MethodLoadDetail(value);
                OnPropertyChanged();
            }
        }

        #endregion

        #region COLECCIONES

        private CmpObservableCollection<ESGR_Empresa> _CollectionESGR_Empresa;
        public CmpObservableCollection<ESGR_Empresa> CollectionESGR_Empresa 
        {
            get 
            {
                if (_CollectionESGR_Empresa == null)
                    _CollectionESGR_Empresa = new CmpObservableCollection<ESGR_Empresa>();
                return _CollectionESGR_Empresa;
            }
        }

        #endregion

        #region PROPERTY

        private ESGR_Empresa _SelectESGR_Empresa;
        public ESGR_Empresa SelectESGR_Empresa 
        {
            get 
            {
                return _SelectESGR_Empresa;
            }
            set 
            {
                _SelectESGR_Empresa = value;
                OnPropertyChanged("SelectESGR_Empresa");
            }
        }

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
                return CmpICommand.GetICommand((T) =>
                {
                    CmpModalDialog.GetContent().ShowDialog("MPSGR_Empresa", new ESGR_Empresa() { Opcion = "I" });
                    MethodLoadDetail();
                });
            }
        }

        public ICommand IEmpresaSucursal
        {
            get
            {
                return CmpICommand.GetICommand((T) =>
                {
                    if (SelectESGR_Empresa == null)
                    {
                        CmpMessageBox.Show(SGRMessage.AdministratorEmpresa, "Se debe de seleccionar un registro para acceder a Sucursal", CmpButton.Aceptar);
                        return;
                    }
                    CmpModalDialog.GetContent().ShowDialog("MPSGR_EmpresaSucursal", new ESGR_EmpresaSucursal() { ESGR_Empresa = SelectESGR_Empresa });
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
                    if (SelectESGR_Empresa == null)
                    {
                        CmpMessageBox.Show(SGRMessage.AdministratorEmpresa, SGRMessageContent.ContentSelectNull + "Editar", CmpButton.Aceptar);
                        return;
                    }
                    SelectESGR_Empresa.Opcion = "U";
                    CmpModalDialog.GetContent().ShowDialog("MPSGR_Empresa", SelectESGR_Empresa);
                    MethodLoadDetail();
                });
            }
        }

        public ICommand IEliminar
        {
            get
            {
                return CmpICommand.GetICommand(async(T) =>
                {
                    if (SelectESGR_Empresa == null)
                    {
                        CmpMessageBox.Show(SGRMessage.AdministratorEmpresa, SGRMessageContent.ContentSelectNull + "Eliminar", CmpButton.Aceptar);
                        return;
                    }
                    await Task.Factory.StartNew(() =>
                    {
                        try
                        {
                            CmpMessageBox.Show(SGRMessage.AdministratorEmpresa, "¿Seguro que desea eliminar el registro?", CmpButton.AceptarCancelar, () =>
                            {
                                SelectESGR_Empresa.Opcion = "D";
                                new BSGR_Empresa().TransEmpresa(SelectESGR_Empresa);
                                CmpMessageBox.Show(SGRMessage.AdministratorEmpresa, SGRMessageContent.ContentSaveOK, CmpButton.Aceptar);
                                MethodLoadDetail();
                            });
                        }
                        catch (Exception ex)
                        {
                            CmpMessageBox.Show(SGRMessage.AdministratorEmpresa, ex.Message, CmpButton.Aceptar);
                        }
                    });
                });
            }
        }

        public ICommand ISalir
        {
            get
            {
                return CmpICommand.GetICommand((s) =>
                {
                    Volver();
                });
            }
        }

        #endregion

        #region METODOS

        private async void MethodLoadDetail(string Filtro = "%") 
        {
            await Task.Factory.StartNew(() =>
            {
                try
                {
                    CollectionESGR_Empresa.Source = new BSGR_Empresa().GetCollectionEmpresa(Filtro);
                    PropertyIsEnabledNuevo = (PropertyIsEnabledNuevo) ? CollectionESGR_Empresa.Count == 0 : false;
                }
                catch (Exception ex)
                {
                    CmpMessageBox.Show(SGRMessage.AdministratorEmpresa, ex.Message, CmpButton.Aceptar);
                }
            });
        }

        #endregion
    }
}
