/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
 * 
'* FCH. CREACIÓN : 07/12/2016 [ABEL QUISPE ORELLANA]
'* ********************************************************
'* 
'* FECHA MODIFIC. : 17/12/2016 [OSCAR HUAMAN CABRERA]
'* MOTIVO MODIFIC.: Se agregó carga de Grilla y funcionalidad de los botones
**********************************************************/

namespace SGR.ViewModels.Presentation.PedidoTipo.Pages
{
    using ComputerSystems;
    using ComputerSystems.WPF;
    using ComputerSystems.WPF.Acciones.Controles.Menus;
    using ComputerSystems.WPF.Acciones.Controles.ModalDialog;
    using ComputerSystems.WPF.Notificaciones;
    using SGR.Models.Business;
    using SGR.Models.Entity;
    using System;
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;
    using System.Windows.Input;

    public class VMSGR_ListadoPedidoTipo : CmpNotifyPropertyChanged
    {

        public VMSGR_ListadoPedidoTipo()
        {
            MethodLoadDetail();
        }

        #region OBJETOS SECUNDARIOS

        private ESGR_PedidoTipo _SelectedESGR_PedidoTipo;
        public ESGR_PedidoTipo SelectedESGR_PedidoTipo
        {
            get
            {
                return _SelectedESGR_PedidoTipo;
            }
            set
            {
                _SelectedESGR_PedidoTipo = value;
                OnPropertyChanged("SelectedESGR_PedidoTipo");
            }
        }

        #endregion

        #region COLECCIONES

        private CmpObservableCollection<ESGR_PedidoTipo> _CollectionESGR_PedidoTipo;
        public CmpObservableCollection<ESGR_PedidoTipo> CollectionESGR_PedidoTipo
        {
            get
            {
                if (_CollectionESGR_PedidoTipo == null)
                    _CollectionESGR_PedidoTipo = new CmpObservableCollection<ESGR_PedidoTipo>();
                return _CollectionESGR_PedidoTipo;
            }
        }

        #endregion

        #region PROPERTY
        #endregion

        #region ICOMMAND

        public ICommand INuevo
        {
            get
            {
                return CmpICommand.GetICommand((T) =>
                    {
                        CmpModalDialog.GetContent().ShowDialog("MPSGR_PedidoTipo", new ESGR_PedidoTipo { Opcion = "I" });
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
                    if (SelectedESGR_PedidoTipo == null)
                    {
                        CmpMessageBox.Show(SGRMessage.AdministratorPedidoTipo, SGRMessageContent.ContentSelectNull + "Editar", CmpButton.Aceptar);
                        return;
                    }
                    SelectedESGR_PedidoTipo.Opcion = "U";
                    CmpModalDialog.GetContent().ShowDialog("MPSGR_PedidoTipo", SelectedESGR_PedidoTipo);
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
                    if (SelectedESGR_PedidoTipo == null)
                    {
                        CmpMessageBox.Show(SGRMessage.AdministratorPedidoTipo, SGRMessageContent.ContentSelectNull + "Eliminar", CmpButton.Aceptar);
                        return;
                    }
                    await Task.Factory.StartNew(() =>
                    {
                        CmpMessageBox.Show(SGRMessage.AdministratorPedidoTipo, "¿Seguro que desea eliminar el registro?", CmpButton.AceptarCancelar, () =>
                        {
                            try
                            {
                                SelectedESGR_PedidoTipo.Opcion = "D";
                                new BSGR_PedidoTipo().TransPedidoTipo(SelectedESGR_PedidoTipo);
                                CmpMessageBox.Show(SGRMessage.AdministratorPedidoTipo, SGRMessageContent.ContentDeleteOK, CmpButton.Aceptar);
                                MethodLoadDetail();
                            }
                            catch (Exception ex)
                            {
                                CmpMessageBox.Show(SGRMessage.AdministratorPedidoTipo, ex.Message, CmpButton.Aceptar);
                            }
                        });
                    });
                });
            }
        }

        public ICommand ISalir
        {
            get
            {
                return CmpICommand.GetICommand((D) =>
                {
                    new CmpNavigationService().CloseCleanWindow();
                });
            }
        }

        #endregion

        #region METODOS

        public async void MethodLoadDetail()
        {
            await Task.Factory.StartNew(() =>
            {
                try
                {
                    CollectionESGR_PedidoTipo.Source = new BSGR_PedidoTipo().GetCollectionPedidoTipo();
                }
                catch (Exception ex)
                {
                    CmpMessageBox.Show(SGRMessage.AdministratorPedidoTipo, ex.Message, CmpButton.Aceptar);
                }
            });
        }

        #endregion
    }
}
