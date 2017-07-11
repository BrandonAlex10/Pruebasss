/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   ABEL QUISPE ORELLANA
'* FCH. CREACIÓN : 03/12/2016
**********************************************************/

namespace SGR.ViewModels.Documento.Pages
{
    using ComputerSystems;
    using ComputerSystems.WPF;
    using ComputerSystems.WPF.Acciones.Controles.Menus;
    using ComputerSystems.WPF.Acciones.Controles.ModalDialog;
    using ComputerSystems.WPF.Interfaces;
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

    public class VMSGR_ListadoDocumento : CmpNavigationService, CmpIVariable
    {
        private ESGR_Documento _ESGR_Documento;
        public ESGR_Documento ESGR_Documento
        {
            get
            {
                return _ESGR_Documento;
            }
            set
            {
                _ESGR_Documento = value;
                OnPropertyChanged();
            }
        }

        public object MyDataContext
        {
            set
            {
                MethodLoadSucursal();
            }
        }

        private Frame _MyFrame;
        public Frame MyFrame
        {
            set
            {
                _MyFrame = value;
            }
        }

        #region COLECCIONES

        private ObservableCollection<ESGR_Documento> _CollectionESGR_Documento;
        public ObservableCollection<ESGR_Documento> CollectionESGR_Documento
        {
            get
            {
                return _CollectionESGR_Documento;
            }
            set
            {
                _CollectionESGR_Documento = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<ESGR_EmpresaSucursal> _CollectionESGR_EmpresaSucursal;
        public ObservableCollection<ESGR_EmpresaSucursal> CollectionESGR_EmpresaSucursal
        {
            get
            {
                return _CollectionESGR_EmpresaSucursal;
            }
            set
            {
                _CollectionESGR_EmpresaSucursal = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region OBJETOS SECUNDARIOS

        private ESGR_Documento _SelectESGR_Documento;
        public ESGR_Documento SelectESGR_Documento
        {
            get
            {
                return _SelectESGR_Documento;
            }
            set
            {
                _SelectESGR_Documento = value;
                OnPropertyChanged();
            }
        }

        private ESGR_EmpresaSucursal _SelectESGR_EmpresaSucursal;
        public ESGR_EmpresaSucursal SelectESGR_EmpresaSucursal
        {
            get
            {
                return _SelectESGR_EmpresaSucursal;
            }
            set
            {
                if (CollectionESGR_Documento != null)
                    CollectionESGR_Documento.Clear();
                _SelectESGR_EmpresaSucursal = value;
                if(value != null)
                    MethodLoadDetail();
                OnPropertyChanged();
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
                    CmpModalDialog.GetContent().ShowDialog(CmpMenu.GetPage("PGSGR_Documento"), new ESGR_Documento() { Opcion = "I" , IdEmpSucursal = SelectESGR_EmpresaSucursal.IdEmpSucursal});
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
                    if (SelectESGR_Documento == null)
                    {
                        CmpMessageBox.Show(SGRMessage.AdministratorDocumento, SGRMessageContent.ContentSelectNull + "Editar", CmpButton.Aceptar);
                        return;
                    }
                    SelectESGR_Documento.Opcion = "U";
                    CmpModalDialog.GetContent().ShowDialog(CmpMenu.GetPage("PGSGR_Documento"), SelectESGR_Documento);
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
                    if (SelectESGR_Documento == null)
                    {
                        CmpMessageBox.Show(SGRMessage.AdministratorDocumento, SGRMessageContent.ContentSelectNull + "Eliminar", CmpButton.Aceptar);
                        return;
                    }
                    await Task.Factory.StartNew(() =>
                    {
                        try
                        {
                            CmpMessageBox.Show(SGRMessage.AdministratorDocumento, "¿Seguro que desea eliminar el registro?", CmpButton.AceptarCancelar, () =>
                            {
                                SelectESGR_Documento.Opcion = "D";
                                new BSGR_Documento().TransDocumento(SelectESGR_Documento);
                                CmpMessageBox.Show(SGRMessage.AdministratorDocumento, SGRMessageContent.ContentDeleteOK, CmpButton.Aceptar);
                                MethodLoadDetail();
                            });
                        }
                        catch (Exception ex)
                        {
                            CmpMessageBox.Show(SGRMessage.AdministratorDocumento, ex.Message, CmpButton.Aceptar);
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
                    new CmpNavigationService();
                    Volver();
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
                    CollectionESGR_Documento = new ObservableCollection<ESGR_Documento>(new BSGR_Documento().GetCollectionDocumento().Where(x => x.IdEmpSucursal == ((SelectESGR_EmpresaSucursal == null) ? 0 : SelectESGR_EmpresaSucursal.IdEmpSucursal)));
                }
                catch (Exception ex)
                {
                    CmpMessageBox.Show(SGRMessage.AdministratorDocumento, ex.Message, CmpButton.Aceptar);
                }
            });
        }

        public void MethodLoadSucursal()
        {
            try
            {
                CollectionESGR_EmpresaSucursal = new BSGR_EmpresaSucursal().GetCollectionEmpresaSucursal(SGRVariables.ESGR_Usuario.SGR_Empresa);
                SelectESGR_EmpresaSucursal = /*null;//*/CollectionESGR_EmpresaSucursal.FirstOrDefault();
            }
            catch (Exception ex)
            {
                CmpMessageBox.Show(SGRMessage.AdministratorDocumento, ex.Message, CmpButton.Aceptar);
            }
        }

        #endregion
    }
}
