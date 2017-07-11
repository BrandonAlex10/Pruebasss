/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   OSCAR HUAMAN CABRERA
'* FCH. CREACIÓN : 01/12/2016
**********************************************************/

namespace SGR.ViewModels.Presentation.Moneda.ModalPage
{
    using ComputerSystems;
    using ComputerSystems.WPF;
    using ComputerSystems.WPF.Acciones.Controles.ModalDialog;
    using ComputerSystems.WPF.Interfaces;
    using SGR.Models.Business;
    using SGR.Models.Entity;
    using SGR.ViewModels.Method;
    using System;
    using System.Windows;
    using System.Windows.Input;

    public class VMSGR_Moneda : CmpNotifyPropertyChanged, CmpIModalDialog
    {
        private ESGR_Moneda _ESGR_Moneda;
        public ESGR_Moneda ESGR_Moneda
        {
            get
            {
                return _ESGR_Moneda;
            }
            set
            {
                _ESGR_Moneda = value;
                OnPropertyChanged("ESGR_Moneda");
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
                    else if (value is ESGR_Moneda)
                    {
                        ESGR_Moneda = (ESGR_Moneda)value;
                        if (ESGR_Moneda.Opcion == "I")
                            PropertyEnabled = true;
                        else
                            PropertyEnabled = false;
                    }
                });
            }
        }

        #region COLECCIONES
        #endregion

        #region PROPERTY

        private bool _PropertyEnabled;
        public bool PropertyEnabled
        {
            get
            {
                return _PropertyEnabled;
            }
            set
            {
                _PropertyEnabled = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region ICOMMAND

        public ICommand IGuardar
        {
            get
            {
                return CmpICommand.GetICommand((G) =>
                {
                    try
                    {
                        if (MethoDValidaDatos())
                            return;
                        new BSGR_Moneda().TransMoneda(ESGR_Moneda);
                        CmpMessageBox.Show(SGRMessage.AdministratorMoneda, SGRMessageContent.ContentSaveOK, CmpButton.Aceptar, () =>
                        {
                            CmpModalDialog.GetContent().Close();
                        });
                    }
                    catch (Exception ex)
                    {
                        CmpMessageBox.Show(SGRMessage.AdministratorMoneda, ex.Message,CmpButton.Aceptar);
                    }
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

        public bool MethoDValidaDatos()
        {
            bool vrValidaDato = false;
            if( ESGR_Moneda.CodMoneda == null || ESGR_Moneda.CodMoneda.Trim().Length == 0 )
            {
                CmpMessageBox.Show(SGRMessage.AdministratorMoneda, "Campo Obligatorio: Código Moneda.", CmpButton.Aceptar);
                vrValidaDato = true;
            }
            else if ( ESGR_Moneda.Descripcion == null || ESGR_Moneda.Descripcion.Trim().Length == 0)
            {
                CmpMessageBox.Show(SGRMessage.AdministratorMoneda, "Campo Obligatorio: Descripción.", CmpButton.Aceptar);
                vrValidaDato = true;
            }
            else if ( ESGR_Moneda.Abreviacion == null || ESGR_Moneda.Abreviacion.Trim().Length == 0 )
            {
                CmpMessageBox.Show(SGRMessage.AdministratorMoneda, "Campo Obligatorio: Abreviatura.", CmpButton.Aceptar);
                vrValidaDato = true;
            }
            else if ( ESGR_Moneda.Simbolo == null ||ESGR_Moneda.Simbolo.Trim().Length == 0 )
            {
                CmpMessageBox.Show(SGRMessage.AdministratorMoneda, "Campo Obligatorio: Símbolo.", CmpButton.Aceptar);
                vrValidaDato = true;
            }
            return vrValidaDato;
        }

        #endregion
    }
}
