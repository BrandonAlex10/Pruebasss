/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   OSCAR HUAMAN CABRERA
'* FCH. CREACIÓN : 01/12/2016
**********************************************************/

namespace SGR.ViewModels.Presentation.Documento.ModalPage
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
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Input;

    public class VMSGR_Documento : CmpNavigationService, CmpIModalDialog
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
                OnPropertyChanged("ESGR_Documento");
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
                    else if (value is ESGR_Documento)
                    {
                        ESGR_Documento = (ESGR_Documento)value;
                        if (ESGR_Documento.Opcion == "I")
                            PropertyEnabledEdit = true;
                        else
                            PropertyEnabledEdit = false;
                        MethodLoadHeader();
                    }
                });
            }
        }

        #region OBJETOS SECUNDARIOS

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
                if (value != null)
                    ESGR_Documento.ESGR_EmpresaSucursal = value;
                OnPropertyChanged("SelectedESGR_EmpresaSucursal");
            }
        }

        #endregion

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

        #region PROPERTY

        private bool _PropertyEnabledEdit;
        public bool PropertyEnabledEdit
        {
            get
            {
                return _PropertyEnabledEdit;
            }
            set
            {
                _PropertyEnabledEdit = value;
                OnPropertyChanged("PropertyEnabledEdit");
            }
        }

        #endregion

        #region ICOMMAND

        public ICommand IGuardar
        {
            get
            {
                return CmpICommand.GetICommand(async(S) =>
                    {
                        if (MethodValidarDatos())
                            return;

                        await Task.Factory.StartNew(() =>
                        {
                            try
                            {
                                new BSGR_Documento().TransDocumento(ESGR_Documento);
                                CmpMessageBox.Show(SGRMessage.AdministratorArea, SGRMessageContent.ContentSaveOK, CmpButton.Aceptar, () =>
                                {
                                    CmpModalDialog.GetContent().Close();
                                });
                            }
                            catch (Exception ex)
                            {
                                CmpMessageBox.Show(SGRMessage.AdministratorArea, ex.Message, CmpButton.Aceptar);
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

        public async void MethodLoadHeader()
        {
            await Task.Factory.StartNew(() =>
            {
                CollectionESGR_EmpresaSucursal.Source = new BSGR_EmpresaSucursal().GetCollectionEmpresaSucursal(SGRVariables.ESGR_Usuario.ESGR_Empresa);
                if (ESGR_Documento.Opcion == "I")
                    SelectedESGR_EmpresaSucursal = CollectionESGR_EmpresaSucursal.FirstOrDefault();
                else
                    SelectedESGR_EmpresaSucursal = CollectionESGR_EmpresaSucursal.FirstOrDefault(x => x.IdEmpSucursal == ESGR_Documento.ESGR_EmpresaSucursal.IdEmpSucursal);
            });
        }

        public bool MethodValidarDatos()
        {
            bool vrValidarDatos = false;
            if (ESGR_Documento.CodDocumento == string.Empty || ESGR_Documento.CodDocumento == null)
            {
                CmpMessageBox.Show(SGRMessage.AdministratorDocumento,"Campo Obligatorio: Código Documento.",CmpButton.Aceptar);
                vrValidarDatos = true;
            }
            else if (ESGR_Documento.Serie == string.Empty || ESGR_Documento.Serie == null)
            {
                CmpMessageBox.Show(SGRMessage.AdministratorDocumento, "Campo Obligatorio: Serie.", CmpButton.Aceptar);
                vrValidarDatos = true;
            }
            else if (ESGR_Documento.Descripcion == string.Empty || ESGR_Documento.Descripcion == null)
            {
                CmpMessageBox.Show(SGRMessage.AdministratorDocumento, "Campo Obligatorio: Descripción.", CmpButton.Aceptar);
                vrValidarDatos = true;
            }
            else if (ESGR_Documento.Longitud == 0 || ESGR_Documento.Longitud == null)
            {
                CmpMessageBox.Show(SGRMessage.AdministratorDocumento, "Campo Obligatorio: Longitud.", CmpButton.Aceptar);
                vrValidarDatos = true;
            }
            else if (ESGR_Documento.Correlativo.Trim().Length == 0 || ESGR_Documento.Correlativo == null)
            {
                CmpMessageBox.Show(SGRMessage.AdministratorDocumento, "Campo Obligatorio: Correlativo.", CmpButton.Aceptar);
                vrValidarDatos = true;
            }
            
            
            return vrValidarDatos;
        }

        #endregion
    }
}
