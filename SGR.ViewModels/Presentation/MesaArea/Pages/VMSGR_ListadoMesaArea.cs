/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   ABEL QUISPE ORELLANA
'* FCH. CREACIÓN : 03/12/2016
'*********************************************************
'*  FECHA MODIFIC. : 15/12/2016 [CRISTIAN HERNANDEZ VILLO]
'* MOTIVO MODIFIC.: Se agregaron funcionalidad de los botones y carga de la Grilla
**********************************************************/

namespace SGR.ViewModels.Presentation.MesaArea.Pages
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
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    public class VMSGR_ListadoMesaArea : CmpNavigationService, CmpINavigation
    {
        public object Parameter
        {
            set
            {
                Application.Current.Dispatcher.Invoke(async () =>
                {
                    var result = await MTSGR_GetPrivilege.Process(this.GetType(), SGRMessage.TitleMessage, SGRMessageContent.GetAccesoRestringido);
                    if (result == null)
                        CloseCleanWindow();
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

        #region COLECCIONES

        private CmpObservableCollection<ESGR_MesaArea> _CollectionESGR_MesaArea;
        public CmpObservableCollection<ESGR_MesaArea> CollectionESGR_MesaArea
        {
            get
            {
                if (_CollectionESGR_MesaArea == null)
                    _CollectionESGR_MesaArea = new CmpObservableCollection<ESGR_MesaArea>();
                return _CollectionESGR_MesaArea;
            }
        }

        #endregion

        #region OBJ SECUNDARIO

        private ESGR_MesaArea _SelectESGR_MesaArea;
        public  ESGR_MesaArea SelectESGR_MesaArea
        {
            get
            {
                return _SelectESGR_MesaArea;
            }
            set
            {
                _SelectESGR_MesaArea = value;
                OnPropertyChanged("SelectESGR_MesaArea");
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
                    CmpModalDialog.GetContent().ShowDialog("MPSGR_MesaArea", new ESGR_MesaArea { Opcion = "I" });
                    MethodLoadDetail();
                });
            }
        }

        public ICommand IEditar
        {
            get
            {
                return CmpICommand.GetICommand((E) =>
                {
                   if (SelectESGR_MesaArea == null)
                   {
                       CmpMessageBox.Show(SGRMessage.AdministratorMesaArea, SGRMessageContent.ContentSelectNull + "Editar.", CmpButton.Aceptar);
                      return;
                   }
                   SelectESGR_MesaArea.Opcion = "U";
                   CmpModalDialog.GetContent().ShowDialog("MPSGR_MesaArea", SelectESGR_MesaArea);
                   MethodLoadDetail();
               });
            }
        }

        public ICommand IEliminar
        {
            get
            {
                return CmpICommand.GetICommand(async(E) =>
                {
                  if (SelectESGR_MesaArea == null)
                  {
                      CmpMessageBox.Show(SGRMessage.AdministratorMesaArea, SGRMessageContent.ContentSelectNull + "Eliminar", CmpButton.Aceptar);
                     return;
                  }
                  await Task.Factory.StartNew(() =>
                  {

                      CmpMessageBox.Show(SGRMessage.AdministratorMesaArea, "¿Seguro que desea eliminar el registro?", CmpButton.AceptarCancelar, () =>
                       {
                           try
                            {
                              SelectESGR_MesaArea.Opcion = "D";
                              new BSGR_MesaArea().TransMesaArea(SelectESGR_MesaArea);
                              CmpMessageBox.Show(SGRMessage.AdministratorMesaArea, SGRMessageContent.ContentDeleteOK, CmpButton.Aceptar);
                              MethodLoadDetail();
                            }
                           catch (Exception ex)
                           {
                               CmpMessageBox.Show(SGRMessage.AdministratorMesaArea, ex.Message, CmpButton.Aceptar);
                           }
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
                    CloseCleanWindow();
                });
            }
        }

        #endregion

        #region METODOS

        public async void MethodLoadDetail()
        {
            await Task.Factory.StartNew(()=>
            {
                try
                {
                    CollectionESGR_MesaArea.Source = new BSGR_MesaArea().GetCollectionMesaArea();
                }
                catch(Exception ex)
                {
                    CmpMessageBox.Show(SGRMessage.AdministratorMesaArea, ex.Message, CmpButton.Aceptar);
                }
            });
            
        }

        #endregion
    }
}
