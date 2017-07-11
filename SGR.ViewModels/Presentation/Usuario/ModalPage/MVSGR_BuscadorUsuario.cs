using ComputerSystems;
using ComputerSystems.WPF;
using ComputerSystems.WPF.Acciones.Controles.ModalDialog;
using ComputerSystems.WPF.Interfaces;
using ComputerSystems.WPF.Notificaciones;
using SGR.Models.Business;
using SGR.Models.Entity;
using SGR.ViewModels.Method;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SGR.ViewModels.Presentation.Usuario.ModalPage
{
    public class MVSGR_BuscadorUsuario : CmpNavigationService,CmpIModalDialog
    {
        public object Parameter
        {
            set
            {
                Application.Current.Dispatcher.Invoke(async() =>
                {
                    var result = await MTSGR_GetPrivilege.Process(this.GetType(), SGRMessage.TitleMessage, SGRMessageContent.GetAccesoRestringido);
                    if (result == null)
                        CmpModalDialog.GetContent().Close();
                    else
                        MethodLoadDetails();
                });
            }
        }

        #region COLECCION

        private CmpObservableCollection<ESGR_Usuario> _CollectionESGR_Usuario;
        public CmpObservableCollection<ESGR_Usuario> CollectionESGR_Usuario
        {
            get
            {
                if (_CollectionESGR_Usuario == null)
                    _CollectionESGR_Usuario = new CmpObservableCollection<ESGR_Usuario>();
                return _CollectionESGR_Usuario;
            }
        }

        #endregion

        #region OBJ SECUNDARIO

        private ESGR_Usuario _SelectedESGR_Usuario;
        public ESGR_Usuario SelectedESGR_Usuario
        {
            get
            {
                return _SelectedESGR_Usuario;
            }
            set
            {
                _SelectedESGR_Usuario = value;
                OnPropertyChanged("SelectedESGR_Usuario");
            }
        }

        #endregion

        #region ICOMMAND

        public ICommand IMozo
        {
            get
            {
                return CmpICommand.GetICommand((T) =>
                {
                    CmpModalDialog.GetContent().Close(SelectedESGR_Usuario);
                });
            }
        }

        public ICommand ISalir
        {
            get
            {
                return CmpICommand.GetICommand((T) =>
                {
                    CmpModalDialog.GetContent().Close();
                });
            }
        }

        #endregion

        #region METODOS

        private async void MethodLoadDetails()
        {
            await Task.Factory.StartNew(() =>
            {
                try
                {
                    CollectionESGR_Usuario.Source = new CmpObservableCollection<ESGR_Usuario>(new BSGR_Usuario().GetCollectionUsuario().Where(x => x.ESGR_Perfil.IdPerfil == 3));
                }
                catch (Exception ex)
                {
                    CmpMessageBox.Show(SGRMessage.ListadoVentaDia, ex.Message, CmpButton.Aceptar);
                }
            });
        }

        #endregion

    }
}
