/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   ABEL QUISPE ORELLANA
'* FCH. CREACIÓN : 28/11/2016
'* '********************************************************
'* FCH. MODIFICACION : 10/12/2016 [CRISTIAN HERNANDEZ VILLO]
'* MOTIVO: Se modifico el tipo de dato de Short a int
**********************************************************/
namespace SGR.Models.Entity
{
    using ComputerSystems;
    using System;
    using System.Collections.Generic;
    using System.Windows.Media;
    public class ESGR_Producto : CmpNotifyPropertyChanged
    {
        public ESGR_Producto()
        {
            ESGR_ProductoSubCategoria = new ESGR_ProductoSubCategoria();
        }

        public int Stock { get; set; }
        public string Opcion { get; set; }
        private bool _IsCheck;
        public bool IsCheck
        {
            get
            {
                return _IsCheck;
            }
            set
            {
                _IsCheck = value;
                OnPropertyChanged("IsCheck");
            }
        }
        private bool _IsCheckedQuitar;
        public bool IsCheckeQuitar
        {
            get
            {
                return _IsCheckedQuitar;
            }
            set
            {
                _IsCheckedQuitar = value;
                OnPropertyChanged("IsCheckeQuitar");
            }
        }
        public  int IdProducto { get; set; }
        public ESGR_ProductoSubCategoria ESGR_ProductoSubCategoria { get; set; }
        public  string Producto { get; set; }
        private byte[] _Imagen;
        public  byte[] Imagen
        {
            get
            {
                return _Imagen;
            }
            set
            {
                _Imagen = value;
                OnPropertyChanged("Imagen");
            }
        }
        private ImageSource _ImageSource;
        public ImageSource ImageSource
        {
            get
            {
                return _ImageSource;
            }
            set
            {
                _ImageSource = value;
                OnPropertyChanged("ImageSource");
            }
        }
        public decimal Precio { get; set; }
        
    }
}
