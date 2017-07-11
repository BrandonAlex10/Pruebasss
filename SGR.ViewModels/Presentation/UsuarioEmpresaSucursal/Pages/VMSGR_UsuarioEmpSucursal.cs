/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   ABEL QUISPE ORELLANA
'* FCH. CREACIÓN : 06/12/2016
**********************************************************/

namespace SGR.ViewModels.Presentation.UsuarioEmpresaSucursal.Pages
{
    using ComputerSystems;
    using ComputerSystems.WPF;
    using ComputerSystems.WPF.Notificaciones;
    using SGR.Models.Business;
    using SGR.Models.Entity;
    using System;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Input;
    using System.Linq;
    using SGR.Models;
    using ComputerSystems.WPF.Interfaces;
    using SGR.ViewModels.Method;

    public class VMSGR_UsuarioEmpSucursal : CmpNavigationService, CmpINavigation
    {
        private ESGR_UsuarioEmpresaSucursal _ESGR_UsuarioEmpresaSucursal;
        public ESGR_UsuarioEmpresaSucursal ESGR_UsuarioEmpresaSucursal
        {
            get
            {
                if (_ESGR_UsuarioEmpresaSucursal == null)
                    _ESGR_UsuarioEmpresaSucursal = new ESGR_UsuarioEmpresaSucursal();
                return _ESGR_UsuarioEmpresaSucursal;
            }
            set
            {
                _ESGR_UsuarioEmpresaSucursal = value;
                OnPropertyChanged("ESGR_UsuarioEmpresaSucursal");
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
                        CloseCleanWindow();
                    else
                        MethodLoadHeader();
                });
            }
        }

        #region COLECCIONES

        private CmpObservableCollection<ESGR_UsuarioEmpresaSucursal> _CollectionUsuarioEmpresaSucursal;
        public CmpObservableCollection<ESGR_UsuarioEmpresaSucursal> CollectionUsuarioEmpresaSucursal
        {
            get
            {
                if (_CollectionUsuarioEmpresaSucursal == null)
                    _CollectionUsuarioEmpresaSucursal = new CmpObservableCollection<ESGR_UsuarioEmpresaSucursal>();
                return _CollectionUsuarioEmpresaSucursal;
            }
            set
            {
                _CollectionUsuarioEmpresaSucursal = value;
                OnPropertyChanged("CollectionUsuarioEmpresaSucursal");
            }
        }

        #endregion

        #region OBJ SECUNDARIO

        private ESGR_UsuarioEmpresaSucursal _SelectUsuarioEmpresaSucursal;
        public ESGR_UsuarioEmpresaSucursal SelectUsuarioEmpresaSucursal
        {
            get
            {
                return _SelectUsuarioEmpresaSucursal;
            }
            set
            {
                _SelectUsuarioEmpresaSucursal = value;
                if (value != null)
                    ESGR_UsuarioEmpresaSucursal.ESGR_EmpresaSucursal = value.ESGR_EmpresaSucursal;
                OnPropertyChanged("SelectUsuarioEmpresaSucursal");
            }
        }

        #endregion

        #region PROPERTY
        #endregion

        #region ICOMMAND

        public ICommand IGuardar
        {
            get
            {
                return CmpICommand.GetICommand(async (N) =>
                {
                    await Task.Factory.StartNew(() =>
                    {
                        new BSGR_UsuarioEmpresaSucursal().TransUsuarioEmpresaSurcursal(ESGR_UsuarioEmpresaSucursal);
                        CmpMessageBox.Show(SGRMessage.AdministratorUsuarioEmpSucursal, SGRMessageContent.ContentSaveOK + "\n" + "Se requiere reiniciar el Sistema", CmpButton.Aceptar, () =>
                        {
                            System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
                            Application.Current.Shutdown();
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
                    new CmpNavigationService().CloseCleanWindow();
                });
            }
        }

        #endregion

        #region METODOS

        private async void MethodLoadHeader()
        {
            await Task.Factory.StartNew(() =>
            {
                try
                {
                    CollectionUsuarioEmpresaSucursal = new BSGR_UsuarioEmpresaSucursal().GetCollectionUsuarioEmpresaSucursal();
                    SelectUsuarioEmpresaSucursal = CollectionUsuarioEmpresaSucursal.FirstOrDefault(x => x.ESGR_EmpresaSucursal.IdEmpSucursal == SGRVariables.ESGR_Usuario.ESGR_EmpresaSucursal.IdEmpSucursal);
                }
                catch (Exception ex)
                {
                    CmpMessageBox.Show(SGRMessage.AdministratorUsuarioEmpSucursal, ex.Message, CmpButton.Aceptar);
                }
            });
        }

        #endregion
    }
}
