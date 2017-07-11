/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   OSCAR HUAMAN CABRERA
'* FCH. CREACIÓN : 01/12/2016
**********************************************************/

namespace SGR.ViewModels.Presentation.EmpresaSucursal.ModalPage
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
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Input;

    public class VMSGR_EmpresaSucursal : CmpNavigationService, CmpIModalDialog
    {
        private ESGR_EmpresaSucursal _ESGR_EmpresaSucursal;
        public ESGR_EmpresaSucursal ESGR_EmpresaSucursal
        {
            get
            {
                if (_ESGR_EmpresaSucursal == null)
                    _ESGR_EmpresaSucursal = new ESGR_EmpresaSucursal();
                return _ESGR_EmpresaSucursal;
            }
            set
            {
                _ESGR_EmpresaSucursal = value;
                OnPropertyChanged("ESGR_EmpresaSucursal");
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
                        Volver();
                    else if (value is ESGR_EmpresaSucursal)
                    {
                        ESGR_EmpresaSucursal = (ESGR_EmpresaSucursal)value;
                        MethodLoadDetail();
                        PropertyIsEnabledEditar = true;
                    }
                });
            }
        }

        #region COLECCIONES

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

        #endregion

        #region OBJETOS SECUNDARIOS

        private ESGR_EmpresaSucursal _SelectESGR_EmpresaSucursal;
        public ESGR_EmpresaSucursal SelectESGR_EmpresaSucursal
        {
            get
            {
                return _SelectESGR_EmpresaSucursal;
            }
            set
            {
                _SelectESGR_EmpresaSucursal = value;
            }
        }

        #endregion

        #region PROPERTY

        private string _PropertySucursal;
        public string ProptertySucursal
        {
            get
            {
                return _PropertySucursal;
            }
            set
            {
                _PropertySucursal = value;
                ESGR_EmpresaSucursal.Sucursal = value;
                OnPropertyChanged("ProptertySucursal");
            }
        }

        private bool _PropertyIsCheckedPrincipal;
        public bool PropertyIsCheckedPrincipal
        {
            get
            {
                return _PropertyIsCheckedPrincipal;
            }
            set
            {
                _PropertyIsCheckedPrincipal = value;
                ESGR_EmpresaSucursal.Principal = value;
                OnPropertyChanged("PropertyIsCheckedPrincipal");
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

        #endregion

        #region ICOMMAND

        public ICommand IGuardar
        {
             get
             {
                return CmpICommand.GetICommand(async(I) =>
                {
                    if (ProptertySucursal == string.Empty)
                    {
                        CmpMessageBox.Show(SGRMessage.AdministratorEmpresaSucursal, "No hay datos que guardar", CmpButton.Aceptar);
                        return;
                    }
                    await Task.Factory.StartNew(() =>
                    {
                        try
                        {
                            ESGR_EmpresaSucursal.Opcion = "I";
                            if (SelectESGR_EmpresaSucursal != null)
                                if (SelectESGR_EmpresaSucursal.Opcion == "U")
                                {
                                    ESGR_EmpresaSucursal.Opcion = "U";
                                    ESGR_EmpresaSucursal.IdEmpSucursal = SelectESGR_EmpresaSucursal.IdEmpSucursal;
                                }
                            new BSGR_EmpresaSucursal().TransEmpresaSucursal(ESGR_EmpresaSucursal);
                            CmpMessageBox.Show(SGRMessage.AdministratorEmpresaSucursal, SGRMessageContent.ContentSaveOK, CmpButton.Aceptar, () =>
                            {
                                MethodLoadDetail();
                                ProptertySucursal = string.Empty;
                                PropertyIsEnabledEditar = true;
                            });
                        }
                        catch (Exception ex)
                        {
                            CmpMessageBox.Show(SGRMessage.AdministratorEmpresaSucursal, ex.Message, CmpButton.Aceptar);
                        }

                    });
                });
             }
        }

        public ICommand IEditar
        {
            get
            {
                return CmpICommand.GetICommand((U) =>
                {
                    if (SelectESGR_EmpresaSucursal == null)
                    {
                        CmpMessageBox.Show(SGRMessage.AdministratorEmpresaSucursal, SGRMessageContent.ContentSelectNull + "Editar", CmpButton.Aceptar);
                        return;
                    }
                    ProptertySucursal = SelectESGR_EmpresaSucursal.Sucursal;
                    PropertyIsCheckedPrincipal = (bool)SelectESGR_EmpresaSucursal.Principal;
                    SelectESGR_EmpresaSucursal.Opcion = "U";
                    PropertyIsEnabledEditar = false;
                });
            }
        }

        public ICommand ICancelar
        {
            get
            {
                return CmpICommand.GetICommand((C) =>
                {
                    try
                    {
                        SelectESGR_EmpresaSucursal.Opcion = string.Empty;
                        ProptertySucursal = string.Empty;
                        PropertyIsEnabledEditar = true;
                        PropertyIsCheckedPrincipal = false;
                    }catch(Exception) { }
                });
            }
        }

        public ICommand IEliminar
        {
            get
            {
                return CmpICommand.GetICommand(async(D) =>
                {
                    if (SelectESGR_EmpresaSucursal == null)
                    {
                        CmpMessageBox.Show(SGRMessage.AdministratorEmpresaSucursal, SGRMessageContent.ContentSelectNull + "Eliminar", CmpButton.Aceptar);
                        return;
                    }
                    SelectESGR_EmpresaSucursal.Opcion = "D";
                    await Task.Factory.StartNew(() =>
                    {
                        try
                        {
                            CmpMessageBox.Show(SGRMessage.AdministratorEmpresaSucursal, "¿Seguro que desea eliminar el registro?", CmpButton.Aceptar, () =>
                            {
                                new BSGR_EmpresaSucursal().TransEmpresaSucursal(SelectESGR_EmpresaSucursal);
                                CmpMessageBox.Show(SGRMessage.AdministratorEmpresaSucursal, SGRMessageContent.ContentDeleteOK, CmpButton.Aceptar);
                                MethodLoadDetail();
                                ProptertySucursal = string.Empty;
                            });
                        }
                        catch (Exception ex)
                        {
                            CmpMessageBox.Show(SGRMessage.AdministratorEmpresaSucursal, ex.Message, CmpButton.Aceptar);
                        }

                    });
                });
            }
        }

        public ICommand IVolver
        {
            get
            {
                return CmpICommand.GetICommand((R) =>
                {
                    CmpModalDialog.GetContent().Close();
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
                    PropertyIsCheckedPrincipal = false;
                    CollectionESGR_EmpresaSucursal.Source = new BSGR_EmpresaSucursal().GetCollectionEmpresaSucursal(ESGR_EmpresaSucursal.ESGR_Empresa);
                }
                catch (Exception ex)
                {
                    CmpMessageBox.Show(SGRMessage.AdministratorEmpresaSucursal, ex.Message, CmpButton.Aceptar);
                }   
            });
        }

        #endregion
    }
}
