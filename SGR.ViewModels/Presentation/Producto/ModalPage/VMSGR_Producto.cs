/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   OSCAR HUAMAN CABRERA
'* FCH. CREACIÓN : 01/12/2016
'* '********************************************************
'* FCH. MODIFICACION : 10/12/2016 [CRISTIAN HERNANDEZ VILLO]
'* MOTIVO: Se dio funcionalidad a los botones
**********************************************************/

namespace SGR.ViewModels.Presentation.Producto.ModalPage
{
    using ComputerSystems;
    using ComputerSystems.WPF;
    using ComputerSystems.WPF.Acciones.Controles.Images;
    using ComputerSystems.WPF.Acciones.Controles.ModalDialog;
    using ComputerSystems.WPF.Interfaces;
    using ComputerSystems.WPF.Notificaciones;
    using Microsoft.Win32;
    using SGR.Models.Business;
    using SGR.Models.Entity;
    using SGR.ViewModels.Method;
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Input;

    public class VMSGR_Producto : CmpNavigationService, CmpIModalDialog
    {
        private ESGR_Producto _ESGR_Producto;
        public  ESGR_Producto ESGR_Producto
        {
            get
            {
               return _ESGR_Producto;
            }
            set
            {
                _ESGR_Producto = value;
                OnPropertyChanged("ESGR_Producto");
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
                    else if (value is ESGR_Producto)
                    {
                        ESGR_Producto = (ESGR_Producto)value;
                        MethodLoadCategoria();
                    }
                });
            }
        }

        #region COLECCIONES

        private CmpObservableCollection<ESGR_ProductoCategoria> _CollectionESGR_ProductoCategoria;
        public  CmpObservableCollection<ESGR_ProductoCategoria> CollectionESGR_ProductoCategoria
        {
            get
            {
                if (_CollectionESGR_ProductoCategoria == null)
                    _CollectionESGR_ProductoCategoria = new CmpObservableCollection<ESGR_ProductoCategoria>();
               return _CollectionESGR_ProductoCategoria;
            }
        }

        private CmpObservableCollection<ESGR_ProductoSubCategoria> _CollectionESGR_ProductoSubCategoria;
        public  CmpObservableCollection<ESGR_ProductoSubCategoria> CollectionESGR_ProductoSubCategoria
        {
            get
            {
                if (_CollectionESGR_ProductoSubCategoria == null)
                    _CollectionESGR_ProductoSubCategoria = new CmpObservableCollection<ESGR_ProductoSubCategoria>();
                return _CollectionESGR_ProductoSubCategoria;
            }
        }
        
        #endregion

        #region OBJ SECUNDARIOS

        private ESGR_ProductoCategoria _SelectESGR_ProductoCategoria;
        public ESGR_ProductoCategoria SelectESGR_ProductoCategoria
        {
            get
            {
                return _SelectESGR_ProductoCategoria;
            }
            set
            {
                _SelectESGR_ProductoCategoria = value;
                if (value != null)
                    MethodLoadSubCategoria(value);
                OnPropertyChanged("SelectESGR_ProductoCategoria");
            }
        }

        private ESGR_ProductoSubCategoria _SelectESGR_ProductoSubCategoria;
        public ESGR_ProductoSubCategoria SelectESGR_ProductoSubCategoria
        {
            get
            {
                if (_SelectESGR_ProductoSubCategoria == null)
                    _SelectESGR_ProductoSubCategoria = new ESGR_ProductoSubCategoria();
                return _SelectESGR_ProductoSubCategoria;
            }
            set
            {
                _SelectESGR_ProductoSubCategoria = value;
                if (value!= null)
                    ESGR_Producto.ESGR_ProductoSubCategoria = value;
                OnPropertyChanged("SelectESGR_ProductoSubCategoria");
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
                return CmpICommand.GetICommand(async(G) =>
                {
                    if (MethodValidarDatos())
                        return;

                    await Task.Factory.StartNew(() =>
                    {
                        try
                        {                        
                            new BSGR_Producto().TransProducto(ESGR_Producto);
                            CmpMessageBox.Show(SGRMessage.AdministratorProducto, SGRMessageContent.ContentSaveOK + "\nNo olvide agregar el producto a la Carta", CmpButton.Aceptar, () =>
                            {
                                CmpModalDialog.GetContent().Close();
                            });
                        }
                        catch (Exception ex)
                        {
                            CmpMessageBox.Show(SGRMessage.AdministratorProducto, ex.Message, CmpButton.Aceptar);
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

        public ICommand IBuscarImagen
        {
            get
            {
                return CmpICommand.GetICommand((V) =>
                {
                    var Image = SearchImage();
                    ESGR_Producto.Imagen = (Image.ToList().Count == 0 ) ? ESGR_Producto.Imagen : Image;
                });
            }
        }

        #endregion

        #region METODOS

        public async void MethodLoadCategoria()
        {
            await Task.Factory.StartNew(()=>
            {
                try
                {
                    CollectionESGR_ProductoCategoria.Source = new BSGR_ProductoCategoria().GetCollectionProductoCategoria();
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        if (ESGR_Producto.Opcion == "I")
                             SelectESGR_ProductoCategoria = CollectionESGR_ProductoCategoria.FirstOrDefault();
                        else
                             SelectESGR_ProductoCategoria = CollectionESGR_ProductoCategoria.FirstOrDefault(x => x.IdCategoria == ESGR_Producto.ESGR_ProductoSubCategoria.ESGR_ProductoCategoria.IdCategoria);
                    });
                }
                catch (Exception ex)
                {
                    CmpMessageBox.Show(SGRMessage.AdministratorProducto, ex.Message, CmpButton.Aceptar);
                }
            });
        }

        public async void MethodLoadSubCategoria(ESGR_ProductoCategoria ESGR_ProductoCategoria)
        {
            await Task.Factory.StartNew(() =>
            {
                try
                {
                    CollectionESGR_ProductoSubCategoria.Source = new BSGR_ProductoSubCategoria().GetCollectionProductoSubCategoria(ESGR_ProductoCategoria);
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        if (ESGR_Producto.Opcion == "I")
                             SelectESGR_ProductoSubCategoria = CollectionESGR_ProductoSubCategoria.FirstOrDefault();
                        else
                             SelectESGR_ProductoSubCategoria = CollectionESGR_ProductoSubCategoria.FirstOrDefault(x => x.IdSubCategoria == ESGR_Producto.ESGR_ProductoSubCategoria.IdSubCategoria);
                    });
                }
                catch (Exception ex)
                {
                    CmpMessageBox.Show(SGRMessage.AdministratorProducto, ex.Message, CmpButton.Aceptar);
                }
            });
        }
        
        public bool MethodValidarDatos()
        {  
            bool vrValidaDato = false;
            if (ESGR_Producto.Producto == null || ESGR_Producto.Producto == string.Empty)
            {
                CmpMessageBox.Show(SGRMessage.AdministratorProducto, "Campo Obligatorio: Nombre Producto.", CmpButton.Aceptar);
                vrValidaDato = true;
            }
            else if (ESGR_Producto.Precio == 0 )
            {
                CmpMessageBox.Show(SGRMessage.AdministratorProducto, "Campo Obligatorio: Precio.", CmpButton.Aceptar);
                vrValidaDato = true;
            }
            return vrValidaDato;
        }

        public static byte[] SearchImage()
        {
            byte[] ByteArray = new byte[] { };
            try
            {
                Stream myStream = null;
                OpenFileDialog openFileDialog1 = new OpenFileDialog();

                openFileDialog1.Filter = "Archivo de imagen (*.bmp, *.jpg, *.png)|*.bmp;*.jpg;*.png";
                openFileDialog1.RestoreDirectory = true;
                if (openFileDialog1.ShowDialog().Value)
                {
                    if ((myStream = openFileDialog1.OpenFile()) != null)
                    {
                        using (myStream)
                        {
                            ByteArray = new byte[myStream.Length];
                            myStream.Read(ByteArray, 0, System.Convert.ToInt32(myStream.Length));
                            myStream.Close();
                        }
                    }
                }

                //OpenFileDialog dlg = new OpenFileDialog();
                //dlg.Filter = "Archivo de imagen (*.bmp, *.jpg, *.png)|*.bmp;*.jpg;*.png";
                //if (dlg.ShowDialog().GetValueOrDefault())
                //{
                //    System.IO.FileStream fs = new System.IO.FileStream(dlg.FileName, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                //    ByteArray = new byte[fs.Length];
                //    fs.Read(ByteArray, 0, System.Convert.ToInt32(fs.Length));
                //    fs.Close();
                //}

            }
            catch (System.Exception ex) { CmpMessageBox.Show("ERROR", ex.Message, CmpButton.Aceptar); }

            return ByteArray;
        }

        #endregion
    }
}
