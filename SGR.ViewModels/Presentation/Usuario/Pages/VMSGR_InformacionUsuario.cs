/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   ABEL QUISPE ORELLANA
'* FCH. CREACIÓN : 24/04/2017
**********************************************************/

namespace SGR.ViewModels.Presentation.Usuario.Pages
{
    using ComputerSystems;
    using ComputerSystems.WPF.Acciones.Controles.ModalDialog;
    using ComputerSystems.WPF.Interfaces;
    using ComputerSystems.WPF.Notificaciones;
    using System;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;

    public class VMSGR_InformacionUsuario : CmpNavigationService, CmpINavigation
    {
        public object Parameter
        {
            set
            {
                BitmapImage logo = new BitmapImage();
                logo.BeginInit();
                logo.UriSource = new Uri("pack://application:,,,/SGR.Presentation;component/Resource/Usuario/avatar.png");
                logo.EndInit();
                PropertyImageSource = logo;
            }
        }


        #region PROPERTY
        private ImageSource _PropertyImageSource;
        public ImageSource PropertyImageSource
        {
            get
            {
                return _PropertyImageSource;
            }
            set
            {
                _PropertyImageSource = value;
                OnPropertyChanged("PropertyImageSource");
            }
        }

        #endregion


        #region ICOMMAND

        public ICommand ICambiarPerfil
        {
            get
            {
                return CmpICommand.GetICommand((N) =>
                {
                    PropertyImageSource = (ImageSource)CmpModalDialog.GetContent().ShowDialog("CmpCropImage", null);
                });
            }
        }

        #endregion

    }
}
