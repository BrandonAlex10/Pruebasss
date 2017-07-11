/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   OSCAR HUAMAN CABRERA
'* FCH. CREACIÓN : 01/12/2016
**********************************************************/

namespace SGR.ViewModels.Area.Pages
{
    using ComputerSystems;
    using ComputerSystems.WPF;
    using ComputerSystems.WPF.Acciones.Controles.ModalDialog;
    using ComputerSystems.WPF.Interfaces;
    using ComputerSystems.WPF.Notificaciones;
    using SGR.Models.Business;
    using SGR.Models.Entity;
    using SGR.ViewModels;
    using System;
    using System.Threading.Tasks;
    using System.Windows.Input;

    public class VMSGR_Area : CmpNavigationService, CmpIModalDialog
    {
        private ESGR_Area _ESGR_Area;
        public ESGR_Area ESGR_Area
        {
            get
            {
                return _ESGR_Area;
            }
            set
            {
                _ESGR_Area = value;
                OnPropertyChanged("ESGR_Area");
            }
        }

        public object Parameter
        {
            set
            {
                if (value is ESGR_Area)
                {
                    ESGR_Area = (ESGR_Area)value;
                }
            }
        }

        #region COLECCIONES
        #endregion

        #region OBJETOS SECUNDARIOS

        public string MsjValidarDatos { get; set; }

        #endregion

        #region ICOMMAND

        public ICommand IGuardar
        {
            get
            {
                return CmpICommand.GetICommand(async(T) =>
                {
                    if (MethodValidarDatos())
                    {
                        CmpMessageBox.Show(SGRMessage.AdministratorArea, MsjValidarDatos, CmpButton.Aceptar);
                        return;
                    }

                    await Task.Factory.StartNew(() =>
                    {
                        try
                        {
                            new BSGR_Area().TransArea(ESGR_Area);
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
                return CmpICommand.GetICommand((T) =>
                {
                    CmpModalDialog.GetContent().Close();
                });
            }
        }

        #endregion

        #region METODOS

        public bool MethodValidarDatos()
        {
            bool vrValidaDato = false;
            MsjValidarDatos = string.Empty;
            if (ESGR_Area.Area == string.Empty || ESGR_Area.Area == null)
            {
                //MsjValidarDatos = "Campo Obligatorio: Área.";
                CmpMessageBox.Show(SGRMessage.AdministratorArea, "Campo Obligatorio: Área.", CmpButton.Aceptar);
            }
            if (MsjValidarDatos.Length > 0)
                vrValidaDato = true;
            return vrValidaDato;
        }

        #endregion
    }
}
