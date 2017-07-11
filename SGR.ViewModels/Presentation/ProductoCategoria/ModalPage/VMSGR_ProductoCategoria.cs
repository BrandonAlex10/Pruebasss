/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   OSCAR HUAMAN CABRERA
'* FCH. CREACIÓN : 01/12/2016
'*********************************************************
'* 
'* FECHA MODIFIC. : 13/12/2016 [OSCAR HUAMAN CABRERA]
'* MOTIVO MODIFIC.: Se agregó funcionalidad a los botones
**********************************************************/

namespace SGR.ViewModels.Presentation.ProductoCategoria.ModalPage
{
    using ComputerSystems;
    using ComputerSystems.WPF;
    using ComputerSystems.WPF.Acciones.Controles.ModalDialog;
    using ComputerSystems.WPF.Interfaces;
    using ComputerSystems.WPF.Notificaciones;
    using SGR.Models.Business;
    using SGR.Models.Entity;
    using SGR.ViewModels.Method;
    using System;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Input;

    public class VMSGR_ProductoCategoria : CmpNavigationService, CmpIModalDialog
    {
        private ESGR_ProductoCategoria _ESGR_ProductoCategoria;
        public ESGR_ProductoCategoria ESGR_ProductoCategoria
        {
            get
            {
                return _ESGR_ProductoCategoria;
            }
            set
            {
                _ESGR_ProductoCategoria = value;
                OnPropertyChanged("ESGR_ProductoCategoria");
            }
        }

        public object Parameter
        {
            set
            {
                Application.Current.Dispatcher.Invoke(async() =>
                {
                    var result = await MTSGR_GetPrivilege.Process(this.GetType(), SGRMessage.TitleMessage, SGRMessageContent.GetAccesoRestringido);
                    if (result == null)
                        CmpModalDialog.GetContent().Close();
                    else if (value is ESGR_ProductoCategoria)
                        ESGR_ProductoCategoria = (ESGR_ProductoCategoria)value;
                });
            }
        }

        #region COLECCIONES
        #endregion

        #region PROPERTY
        #endregion

        #region ICOMMAND

        public ICommand IGuardar
        {
            get
            {
                return CmpICommand.GetICommand(async(G) =>
                    {
                        if (MethodValidarDatos())
                            return;

                        await Task.Factory.StartNew(() =>
                        {
                            try
                            {
                                new BSGR_ProductoCategoria().TransProductoCategoria(ESGR_ProductoCategoria);
                                CmpMessageBox.Show(SGRMessage.AdministratorProductoCategoria, SGRMessageContent.ContentSaveOK, CmpButton.Aceptar, () =>
                                {
                                    CmpModalDialog.GetContent().Close();
                                });
                            }
                            catch (Exception ex)
                            {
                                CmpMessageBox.Show(SGRMessage.AdministratorProductoCategoria, ex.Message, CmpButton.Aceptar);
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

        private bool MethodValidarDatos()
        {
            bool vrValidaDato = false;
            if(ESGR_ProductoCategoria.Categoria == string.Empty || ESGR_ProductoCategoria.Categoria == null)
            {
                CmpMessageBox.Show(SGRMessage.AdministratorProductoCategoria, "Campo Obligatorio: Categoria.", CmpButton.Aceptar);
                vrValidaDato = true;
            }else if(ESGR_ProductoCategoria.Impresora == string.Empty || ESGR_ProductoCategoria.Impresora == null)
            {
                CmpMessageBox.Show(SGRMessage.AdministratorProductoCategoria, "Campo Obligatorio: Impresora.", CmpButton.Aceptar);
                vrValidaDato = true;
            }
            return vrValidaDato;
        }

        #endregion
    }
}
