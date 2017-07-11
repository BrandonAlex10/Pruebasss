/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   ABEL QUISPE ORELLANA
'* FCH. CREACIÓN : 01/12/2016
'*********************************************************
'* 
'* FECHA MODIFIC. : 13/12/2016 [OSCAR HUAMAN CABRERA]
'* MOTIVO MODIFIC.: Se agregó funcionalidad a los botones y el cargado de la lista
**********************************************************/

namespace SGR.ViewModels.Presentation.ProductoSubCategoria.ModalPage
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

    public class VMSGR_ProductoSubCategoria : CmpNavigationService, CmpIModalDialog
    {
        private ESGR_ProductoSubCategoria _ESGR_ProductoSubCategoria;
        public ESGR_ProductoSubCategoria ESGR_ProductoSubCategoria
        {
            get
            {
                return _ESGR_ProductoSubCategoria;
            }
            set
            {
                _ESGR_ProductoSubCategoria = value;
                OnPropertyChanged("ESGR_ProductoSubCategoria");
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
                    else if (value is ESGR_ProductoSubCategoria)
                    {
                        PropertyIsEnabledEditar = result.Editar;
                        PropertyIsEnabledEliminar = result.Eliminar;
                        ESGR_ProductoSubCategoria = (ESGR_ProductoSubCategoria)value;
                        MethodLoadDetail();
                    }
                });
            }
        }

        #region OBJETOS SECUNDARIOS

        private ESGR_ProductoSubCategoria _SelectESGR_ProductoSubCategoria;
        public ESGR_ProductoSubCategoria SelectESGR_ProductoSubCategoria
        {
            get
            {
                return _SelectESGR_ProductoSubCategoria;
            }
            set
            {
                _SelectESGR_ProductoSubCategoria = value;
            }
        }

        #endregion

        #region COLECCIONES

        private CmpObservableCollection<ESGR_ProductoSubCategoria> _CollectionESGR_ProductoSubCategoria;
        public CmpObservableCollection<ESGR_ProductoSubCategoria> CollectionESGR_ProductoSubCategoria
        {
            get
            {
                if (_CollectionESGR_ProductoSubCategoria == null)
                    _CollectionESGR_ProductoSubCategoria = new CmpObservableCollection<ESGR_ProductoSubCategoria>();
                return _CollectionESGR_ProductoSubCategoria;
            }
        }

        #endregion

        #region PROPERTY

        private bool _PropertyEnabledEditar;
        public bool PropertyEnabledEditar
        {
            get
            {
                return _PropertyEnabledEditar;
            }
            set
            {
                _PropertyEnabledEditar = value;
                OnPropertyChanged();
            }
        }

        private bool _PropertyEnabledCancelar;
        public bool PropertyEnabledCancelar
        {
            get
            {
                return _PropertyEnabledCancelar;
            }
            set
            {
                _PropertyEnabledCancelar = value;
                OnPropertyChanged();
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

        public ICommand IGuardar
        {
            get
            {
                return CmpICommand.GetICommand(async(G) =>
                    {
                        if (MethodValidarDatos())
                            return;

                        await Task.Factory.StartNew(() =>
                        {
                            try
                            {
                                if (PropertyEnabledEditar)
                                    ESGR_ProductoSubCategoria.Opcion = "I";
                                else
                                    ESGR_ProductoSubCategoria.Opcion = "U";

                                new BSGR_ProductoSubCategoria().TransProductoSubCategoria(ESGR_ProductoSubCategoria);
                                CmpMessageBox.Show(SGRMessage.AdministratorProductoSubCategoria, SGRMessageContent.ContentSaveOK, CmpButton.Aceptar, () =>
                                {
                                    MethodLoadDetail();
                                });
                            }
                            catch (Exception ex)
                            {
                                CmpMessageBox.Show(SGRMessage.AdministratorProductoSubCategoria, ex.Message, CmpButton.Aceptar);
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
                    if (SelectESGR_ProductoSubCategoria == null)
                    {
                        CmpMessageBox.Show(SGRMessage.AdministratorProductoSubCategoria, SGRMessageContent.ContentSelectNull + "Editar.", CmpButton.Aceptar);
                        return;
                    }
                    ESGR_ProductoSubCategoria = SelectESGR_ProductoSubCategoria;
                    PropertyEnabledEditar = false;
                    PropertyEnabledCancelar = true;
                });
            }
        }

        public ICommand ICancelar
        {
            get
            {
                return CmpICommand.GetICommand((C) =>
                {
                    MethodLoadDetail();
                });
            }
        }

        public ICommand IEliminar
        {
            get
            {
                return CmpICommand.GetICommand(async(D) =>
                {
                    if (SelectESGR_ProductoSubCategoria == null)
                    {
                        CmpMessageBox.Show(SGRMessage.AdministratorProductoSubCategoria, SGRMessageContent.ContentSelectNull + "Eliminar", CmpButton.Aceptar);
                        return;
                    }
                    await Task.Factory.StartNew(() =>
                    {
                        try
                        {
                            CmpMessageBox.Show(SGRMessage.AdministratorProductoSubCategoria, "¿Seguro que desea eliminar el registro?", CmpButton.AceptarCancelar, () =>
                            {
                                SelectESGR_ProductoSubCategoria.Opcion = "D";
                                new BSGR_ProductoSubCategoria().TransProductoSubCategoria(SelectESGR_ProductoSubCategoria);
                                CmpMessageBox.Show(SGRMessage.AdministratorProductoSubCategoria, SGRMessageContent.ContentDeleteOK, CmpButton.Aceptar);
                                MethodLoadDetail();
                            });
                        }
                        catch (Exception ex)
                        {
                            CmpMessageBox.Show(SGRMessage.AdministratorProductoSubCategoria, ex.Message, CmpButton.Aceptar);
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

        private async void MethodLoadDetail()
        {
            await Task.Factory.StartNew(() =>
                {
                    try
                    {
                        CollectionESGR_ProductoSubCategoria.Source = new BSGR_ProductoSubCategoria().GetCollectionProductoSubCategoria(ESGR_ProductoSubCategoria.ESGR_ProductoCategoria);
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            PropertyEnabledEditar = true;
                            PropertyEnabledCancelar = false;
                            ESGR_ProductoSubCategoria.IdSubCategoria = 0;
                            ESGR_ProductoSubCategoria.SubCategoria = string.Empty;
                        });
                    }
                    catch (Exception ex)
                    {
                        CmpMessageBox.Show(SGRMessage.AdministratorProductoCategoria, ex.Message, CmpButton.Aceptar);
                    }
                });
            
        }

        private bool MethodValidarDatos()
        {
            bool vrValidaDato = false;
            if(ESGR_ProductoSubCategoria.SubCategoria == null || ESGR_ProductoSubCategoria.SubCategoria.Trim().Length == 0)
            {
                CmpMessageBox.Show(SGRMessage.AdministratorProductoSubCategoria, "Campo Obligatorio: Sub Categoría.", CmpButton.Aceptar);
                vrValidaDato = true;
            }
            return vrValidaDato;
        }

        #endregion
    }
}
