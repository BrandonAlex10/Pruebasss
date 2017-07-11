using ComputerSystems.WPF.Notificaciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComputerSystems.WPF.Interfaces;
using SGR.Models.Entity;
using System.Windows.Input;
using ComputerSystems;
using ComputerSystems.WPF;
using SGR.Models.Business;
using ComputerSystems.WPF.Acciones.Controles.ModalDialog;

namespace SGR.ViewModels.Presentation.Caja.ModalPage
{
    public class VMSGR_Caja : CmpNavigationService, CmpIModalDialog
    {
        private ESGR_Caja _ESGR_Caja;
        public ESGR_Caja ESGR_Caja
        {
            get
            {
                if (_ESGR_Caja == null)
                    _ESGR_Caja = new ESGR_Caja();
                return _ESGR_Caja;
            }
            set
            {
                _ESGR_Caja = value;
                OnPropertyChanged("ESGR_Caja");
            }
        }

        public object Parameter
        {
            set
            {
                if (value is ESGR_Caja)
                {
                   ESGR_Caja = (ESGR_Caja)value;
                   MethodLoadEmpresaSucursal();
                }
            }
        }

        #region COLLECTION

        private CmpObservableCollection<ESGR_UsuarioEmpresaSucursal> _CollectionESGR_UsuarioEmpresaSucursal;
        public CmpObservableCollection<ESGR_UsuarioEmpresaSucursal> CollectionESGR_UsuarioEmpresaSucursal
        {
            get
            {
                if (_CollectionESGR_UsuarioEmpresaSucursal == null)
                    _CollectionESGR_UsuarioEmpresaSucursal = new CmpObservableCollection<ESGR_UsuarioEmpresaSucursal>();
                return _CollectionESGR_UsuarioEmpresaSucursal;
            }
        }

        #endregion

        #region OBJETOS SECUNDARIOS

        private ESGR_UsuarioEmpresaSucursal _SelectedESGR_UsuarioEmpresaSucursal;
        public ESGR_UsuarioEmpresaSucursal SelectedESGR_UsuarioEmpresaSucursal
        {
            get
            {
                return _SelectedESGR_UsuarioEmpresaSucursal;
            }
            set
            {
                _SelectedESGR_UsuarioEmpresaSucursal = value;
                if (value != null)
                    ESGR_Caja.ESGR_UsuarioEmpresaSucursal = value;
                OnPropertyChanged("SelectedESGR_UsuarioEmpresaSucursal");
            }
        }

        #endregion

        #region ICOMMAND

        public ICommand IGuardar
        {
            get
            {
                return CmpICommand.GetICommand(async (T) =>
                {
                    await Task.Factory.StartNew(() =>
                    {
                        try
                        {
                            if (MethodValidaDatos()) return;
                            new BSGR_Caja().MethodTransaccionCaja(ESGR_Caja);
                            CmpMessageBox.Show(SGRMessage.AdministracionCaja, SGRMessageContent.ContentSaveOK, CmpButton.Aceptar, () =>
                            {
                                CmpModalDialog.GetContent().Close();
                            });
                        }
                        catch (Exception ex)
                        {
                            CmpMessageBox.Show(SGRMessage.AdministracionCaja, ex.Message, CmpButton.Aceptar);
                        }
                    });
                });
            }
        }

        public ICommand IVolver
        {
            get
            {
                return CmpICommand.GetICommand((T) =>
                {
                    CmpModalDialog.GetContent().Close(null);
                });
            }
        }

        #endregion

        #region METODOS

        private async void MethodLoadEmpresaSucursal()
        {
            await Task.Factory.StartNew(() =>
            {
                try
                {
                    CollectionESGR_UsuarioEmpresaSucursal.Source = new BSGR_UsuarioEmpresaSucursal().GetCollectionUsuarioEmpresaSucursal();
                    if (ESGR_Caja.Opcion == "I")
                        SelectedESGR_UsuarioEmpresaSucursal = CollectionESGR_UsuarioEmpresaSucursal.FirstOrDefault(x => (bool)x.ESGR_EmpresaSucursal.Principal);
                    else
                        SelectedESGR_UsuarioEmpresaSucursal = CollectionESGR_UsuarioEmpresaSucursal.FirstOrDefault(x => x.ESGR_EmpresaSucursal.IdEmpSucursal == ESGR_Caja.ESGR_UsuarioEmpresaSucursal.ESGR_EmpresaSucursal.IdEmpSucursal);
                }
                catch (Exception ex)
                {
                    CmpMessageBox.Show(SGRMessage.AdministracionCaja, ex.Message, CmpButton.Aceptar);
                }
            });
        }

        private bool MethodValidaDatos()
        {
            if (ESGR_Caja.Descripcion == null || ESGR_Caja.Descripcion.Trim().Length == 0)
            {
                CmpMessageBox.Show(SGRMessage.AdministracionCaja, "Dato Obligatorio: Caja", CmpButton.Aceptar);
                return true;
            }
            return false;
        }

        #endregion

    }
}
