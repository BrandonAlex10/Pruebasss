/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   ABEL QUISPE ORELLANA
'* FCH. CREACIÓN : 28/12/2016
**********************************************************/
namespace SGR.Models.Models
{
    using ComputerSystems;
    using ComputerSystems.WPF;
    using SGR.Models.Entity;
    using SGR.ViewModels.Orders;
    using SGR.ViewModels.Orders.Pedido.Pages;
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    public class VMSGR_PedidoDetalle : CmpNotifyPropertyChanged
    {
        public VMSGR_PedidoDetalle()
        {
            ESGR_Producto = new ESGR_Producto();
            PropertyVisibilityComentario = Visibility.Collapsed;
        }

        #region OBJETOS SECUNDARIOS

        public ESGR_Pedido ESGR_Pedido { get; set; }
        public ESGR_Producto ESGR_Producto { get; set; }

        private Nullable<int> _Cantidad;
        public Nullable<int> Cantidad
        {
            get
            {
                return _Cantidad;
            }
            set
            {
                if (value != null)
                {
                    if (value > ((_Cantidad) ?? 0))
                        CantidadMesa += 1;
                    else
                    {
                        if (CantidadMesa > 0)
                            CantidadMesa -= 1;
                        else
                            CantidadLlevar -= 1;
                    }
                }
                _Cantidad = value;
                OnPropertyChanged("Cantidad");
            }
        }

        private int _CantidadAux;
        public int CantidadAux
        {
            get
            {
                return _CantidadAux;
            }
            set
            {
                _CantidadAux = value;
                OnPropertyChanged("CantidadAux");
            }
        }

        private Visibility _PropertyVisibilityPrecioDolar;
        public Visibility PropertyVisibilityPrecioDolar
        {
            get
            {
                return _PropertyVisibilityPrecioDolar;
            }
            set
            {
                _PropertyVisibilityPrecioDolar = value;
                OnPropertyChanged("PropertyVisibilityPrecioDolar");
            }
        }

        private decimal _PrecioDolar;
        public decimal PrecioDolar
        {
            get
            {
                return _PrecioDolar;
            }
            set
            {
                _PrecioDolar = value;
                PropertyVisibilityPrecioDolar = (value == 0) ? Visibility.Collapsed : Visibility.Visible;
                OnPropertyChanged("PrecioDolar");
            }
        }

        private int _CantidadMesa;
        public int CantidadMesa
        {
            get
            {
                return _CantidadMesa;
            }
            set
            {
                _CantidadMesa = ((value < 0) ? 1 : value);
                OnPropertyChanged("CantidadMesa");
            }
        }

        private int _CantidadLlevar;
        public int CantidadLlevar
        {
            get
            {
                return _CantidadLlevar;
            }
            set
            {
                _CantidadLlevar = ((value < 0) ? 0 : value);
                OnPropertyChanged("CantidadLlevar");
            }
        }

        private string _Observacion;
        public string Observacion
        {
            get
            {
                return _Observacion;
            }
            set
            {
                _Observacion = value;
                Enviado = !(value != null || value.Trim().Length == 0);
                OnPropertyChanged("Observacion");
            }
        }

        private bool _Enviado;
        public bool Enviado
        {
            get
            {
                return _Enviado;
            }
            set
            {
                _Enviado = value;
                OnPropertyChanged("Enviado");
            }
        }

        private Visibility _PropertyVisibilityComentario;
        public Visibility PropertyVisibilityComentario
        {
            get
            {
                return _PropertyVisibilityComentario;
            }
            set
            {
                _PropertyVisibilityComentario = value;
                OnPropertyChanged("PropertyVisibilityComentario");
            }
        }

        private bool _MsjMostrado;
        public bool MsjMostrado
        {
            get
            {
                return _MsjMostrado;
            }
            set
            {
                _MsjMostrado = value;
                OnPropertyChanged("MsjMostrado");
            }
        }

        #endregion

        #region ICOMMAND

        public ICommand ICalcularCantidad
        {
            get
            {
                return CmpICommand.GetICommand((T) =>
                {
                    TextBox vrTextBox = (TextBox)T;
                    if (vrTextBox.Name == "CantidadLlevar")
                    {
                        CantidadLlevar = (vrTextBox.Text.Trim().Length == 0) ? 0 : Convert.ToInt16(vrTextBox.Text.Trim());
                        CantidadMesa = (int)Cantidad - CantidadLlevar;
                    }
                    else
                    {
                        CantidadMesa = (vrTextBox.Text.Trim().Length == 0) ? 0 : Convert.ToInt16(vrTextBox.Text.Trim());
                        CantidadLlevar = (int)Cantidad - CantidadMesa;
                    }
                });
            }
        }

        public ICommand IAddQuantity
        {
            get
            {
                return CmpICommand.GetICommand((T) =>
                {
                    var Producto = ((VMSGR_PedidoDetalle)T).ESGR_Producto;
                    if (!MsjMostrado && (Cantidad + 1) < Producto.Stock && Producto.Stock <= 5)
                    {
                        CmpMessageBox.Show(SGRMessage.TitlePedido, "Solo quedan " + (Producto.Stock - (Cantidad + 1)) + " unidades de " + Producto.Producto, CmpButton.Aceptar);
                        MsjMostrado = true;
                    }

                    if ((Producto.ESGR_ProductoSubCategoria.ESGR_ProductoCategoria.ValidaStock && Cantidad < Producto.Stock) || !Producto.ESGR_ProductoSubCategoria.ESGR_ProductoCategoria.ValidaStock)
                        Cantidad += 1;
                    else
                        CmpMessageBox.Show(SGRMessage.TitlePedido, "Ha alcanzado el límite de Stock", CmpButton.Aceptar);

                    Enviado = (Cantidad == CantidadAux);
                });
            }
        }

        public ICommand ISubtractQuantity
        {
            get
            {
                return CmpICommand.GetICommand((T) =>
                {
                    if (Cantidad > 1)
                        Cantidad -= 1;

                    Enviado = false;
                });
            }
        }

        public ICommand IAgregarComentario
        {
            get
            {
                return CmpICommand.GetICommand((T) =>
                {
                    PropertyVisibilityComentario = (PropertyVisibilityComentario == Visibility.Visible) ? Visibility.Collapsed : Visibility.Visible;

                    if (PropertyVisibilityComentario == Visibility.Visible)
                        CmpMessageBox.Show(SGRMessage.TitlePedido, "Para más de un Comentario, separelo por un guión ( - )", CmpButton.Aceptar);
                });
            }
        }

        #endregion

    }
}
