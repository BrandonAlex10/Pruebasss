/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				  ABEL QUISPE ORELLANA
'* FCH. CREACIÓN : 24/04/2017
**********************************************************/

namespace SGR.ViewModels.Presentation.Usuario.Pages
{
    using ComputerSystems;
    using ComputerSystems.WPF.Acciones.Controles.ModalDialog;
    using ComputerSystems.WPF.Interfaces;
    using SGR.Models.Entity;
    using System.Windows.Input;

    public class VMSGR_Information : CmpNotifyPropertyChanged, CmpIModalDialog
    {
        public object Parameter
        {
            set
            {

            }
        }

        #region ICOMMAND

        public ICommand IBuscarImage
        {
            get
            {
                return CmpICommand.GetICommand((T) =>
                {
                    CmpModalDialog.GetContent().ShowDialog("CmpCropImage", null);
                });
            }
        }

        #endregion

    }
}
