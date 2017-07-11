/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
 * ********************************************************
 * 
'* FCH. CREACIÓN : 03/12/2016 [ABEL QUISPE ORELLANA]
 * 
 * ********************************************************
 * 
**********************************************************/

namespace SGR.ViewModels.Orders.Mesa.Pages
{
    using ComputerSystems;
    using ComputerSystems.WPF;
    using ComputerSystems.WPF.Acciones.Controles.ModalDialog;
    using ComputerSystems.WPF.Interfaces;
    using ComputerSystems.WPF.Notificaciones;
    using SGR.Models.Entity;
    using System;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Input;
    using System.Linq;
    using SGR.Models.Business;
    using SGR.Models;
    using SGR.ViewModels.Method;

    public class VMSGR_MesasDisponibles : CmpNavigationService, CmpIModalDialog
    {
        private ESGR_Mesa _ESGR_Mesa;
        public ESGR_Mesa ESGR_Mesa
        {
            get
            {
                return _ESGR_Mesa;
            }
            set
            {
                _ESGR_Mesa = value;
                OnPropertyChanged("ESGR_Mesa");
            }
        }

        public object Parameter
        {
            set
            {
                Application.Current.Dispatcher.Invoke(async () =>
                {
                    var result = await MTSGR_GetPrivilege.Process(this.GetType(), SGRMessage.TitleSGR, SGRMessageContent.GetAccesoRestringido);
                    if (result == null)
                        CmpModalDialog.GetContent().Close();
                    else if (value is ESGR_Mesa)
                    {
                        if (CollectionESGR_Mesa.Count > 0)
                            CollectionESGR_Mesa.Clear();
                        ESGR_Mesa = (ESGR_Mesa)value;
                        Reservado = ESGR_Mesa.Opcion == "R";
                        PropertyIsEnabledCheckReservado = ESGR_Mesa.Opcion != "R";
                        PropertyVisibilityTextBlockNombreMesa = (SGRVariables.MesaFija) ? Visibility.Visible : Visibility.Collapsed;
                        Identificador = string.Empty;
                        NumPersonas = 0;
                        MethodLoadArea();
                    }
                });
            }
        }

        #region COLECCIONES

        private CmpObservableCollection<ESGR_Mesa> _CollectionESGR_Mesa;
        public CmpObservableCollection<ESGR_Mesa> CollectionESGR_Mesa
        {
            get 
            {
                if (_CollectionESGR_Mesa == null)
                    _CollectionESGR_Mesa = new CmpObservableCollection<ESGR_Mesa>();
                return _CollectionESGR_Mesa;
            }
        }

        private CmpObservableCollection<ESGR_MesaArea> _CollectionESGR_MesaArea;
        public CmpObservableCollection<ESGR_MesaArea> CollectionESGR_MesaArea
        {
            get
            {
                if (_CollectionESGR_MesaArea == null)
                    _CollectionESGR_MesaArea = new CmpObservableCollection<ESGR_MesaArea>();
                return _CollectionESGR_MesaArea;
            }
        }

        #endregion

        #region OBJECTOS SECUNDARIO

        private CmpObservableCollection<ESGR_Mesa> _CollectionSelectESGR_Mesa;
        public CmpObservableCollection<ESGR_Mesa> CollectionSelectESGR_Mesa
        {
            get
            {
                if (_CollectionSelectESGR_Mesa == null)
                    _CollectionSelectESGR_Mesa = new CmpObservableCollection<ESGR_Mesa>();
                return _CollectionSelectESGR_Mesa;
            }
        }

        private ESGR_MesaArea _SelectedESGR_MesaArea;
        public ESGR_MesaArea SelectedESGR_MesaArea
        {
            get
            {
                return _SelectedESGR_MesaArea;
            }
            set
            {
                if (value != null)
                {
                    _SelectedESGR_MesaArea = value;
                    MethodLoadMesa();
                }
                OnPropertyChanged("SelectedESGR_MesaArea");
            }
        }

        private Visibility _PropertyVisibilityTextBlockNombreMesa;
        public Visibility PropertyVisibilityTextBlockNombreMesa
        {
            get
            {
                return _PropertyVisibilityTextBlockNombreMesa;//(SGRVariables.MesaFija) ? Visibility.Visible : Visibility.Collapsed;
            }
            set
            {
                _PropertyVisibilityTextBlockNombreMesa = value;
                OnPropertyChanged("PropertyVisibilityNameMesa");
            }
        }

        private int _NumPersonas;
        public int NumPersonas
        {
            get
            {
                return _NumPersonas;
            }
            set
            {
                _NumPersonas = value;
                OnPropertyChanged("NumPersonas");
            }
        }

        private string _Identificador;
        public string Identificador
        {
            get
            {
                return _Identificador;
            }
            set
            {
                _Identificador = value;
                OnPropertyChanged("Identificador");
            }
        }

        private bool _Reservado;
        public bool Reservado
        {
            get
            {
                return _Reservado;
            }
            set
            {
                _Reservado = value;
                OnPropertyChanged("Reservado");
            }
        }

        #endregion

        #region PROPERTY

        private bool _PropertyMetroProgressBar;
        public bool PropertyMetroProgressBar
        {
            get 
            {
                return _PropertyMetroProgressBar;
            }
            set 
            {
                _PropertyMetroProgressBar = value;
                OnPropertyChanged("PropertyMetroProgressBar");
            }
        }

        private bool _PropertyIsEnabledCheckReservado;
        public bool PropertyIsEnabledCheckReservado
        {
            get
            {
                return _PropertyIsEnabledCheckReservado;
            }
            set
            {
                _PropertyIsEnabledCheckReservado = value;
                OnPropertyChanged("PropertyIsEnabledCheckReservado");
            }
        }

        #endregion

        #region ICOMMAND

        public ICommand IGuardar
        {
            get
            {
                return CmpICommand.GetICommand((T) =>
                {
                    if (MethodValidaDatos()) return;

                    if (Reservado)
                    {
                        CmpMessageBox.Show(SGRMessage.BusquedaMesaDisponible, "¿Seguro que desea guardar los datos?", CmpButton.AceptarCancelar, () =>
                        {
                            try
                            {
                                ESGR_Pedido ESGR_Pedido = new ESGR_Pedido()
                                {
                                    Opcion = "I",
                                    IdPedido = 0,
                                    Identificador = Identificador,
                                    ESGR_Estado = new ESGR_Estado()
                                    {
                                        CodEstado = "RVPED"
                                    },
                                    ESGR_Usuario = SGRVariables.ESGR_Usuario,
                                    Cubierto = (short)NumPersonas,
                                    ESGR_EmpresaSucursal = SGRVariables.ESGR_Usuario.ESGR_EmpresaSucursal,
                                    ESGR_PedidoTipo = new ESGR_PedidoTipo()
                                    {
                                        IdPedidoTipo = 1
                                    },
                                    ESGR_Mesa = new ESGR_Mesa()
                                    {
                                        DetalleMesaXML = MethodDetalleXML()
                                    },
                                    CadenaMesa = MethodCadenaMesa()
                                };
                                new BSGR_Pedido().TransPedido(ESGR_Pedido);
                                CmpMessageBox.Show(SGRMessage.BusquedaMesaDisponible, SGRMessageContent.ContentSaveOK, CmpButton.Aceptar, () => CmpModalDialog.GetContent().Close());
                            }
                            catch (Exception ex)
                            {
                                CmpMessageBox.Show(SGRMessage.BusquedaMesaDisponible, ex.Message, CmpButton.Aceptar);
                            }
                        });
                    }
                    else
                    {
                        object[] objMesa = new object[4];
                        objMesa[0] = CollectionSelectESGR_Mesa;
                        objMesa[1] = Identificador;
                        objMesa[2] = NumPersonas;
                        objMesa[3] = Reservado;
                        CmpModalDialog.GetContent().Close(objMesa);
                    }
                });
            }
        }

        public ICommand IVolver
        {
            get
            {
                return CmpICommand.GetICommand((s) =>
                {
                    CmpModalDialog.GetContent().Close(null);
                });
            }
        }

        #endregion

        #region METODOS

        private async void MethodLoadArea()
        {
            await Task.Factory.StartNew(() =>
            {
                try
                {
                    CollectionESGR_MesaArea.Source = new BSGR_MesaArea().GetCollectionMesaArea();
                    SelectedESGR_MesaArea = CollectionESGR_MesaArea.FirstOrDefault();
                }
                catch (Exception ex)
                {
                    CmpMessageBox.Show(SGRMessage.BusquedaMesaDisponible, ex.Message, CmpButton.Aceptar);
                }
            });
        }

        private async void MethodLoadMesa()
        {
            await Task.Factory.StartNew(() =>
            {
                try
                {
                    PropertyMetroProgressBar = true;

                    CollectionESGR_Mesa.Source = new CmpObservableCollection<ESGR_Mesa>(new BSGR_Mesa().GetCollectionMesa(SelectedESGR_MesaArea, SGRVariables.ESGR_Usuario.IdUsuario).Where(x => x.ESGR_Estado.CodEstado == "PGPED"));

                    PropertyMetroProgressBar = false;
                }
                catch (Exception ex)
                {
                    CmpMessageBox.Show(SGRMessage.BusquedaMesaDisponible, ex.Message, CmpButton.Aceptar);
                    PropertyMetroProgressBar = false;
                }
            });
        }

        private string MethodDetalleXML()
        {
            string vrCadena = string.Empty;
            vrCadena = "<ROOT>";
            CollectionSelectESGR_Mesa.ToList().ForEach((x) =>
            {
                vrCadena += "<Listar ";
                vrCadena += "xIdMesa = \'" + x.IdMesa;
                vrCadena += "\'></Listar>";
            });
            vrCadena += "</ROOT>";
            return vrCadena;
        }

        private string MethodCadenaMesa()
        {
            string vrCadena = string.Empty;
            CollectionSelectESGR_Mesa.ToList().ForEach((x) =>
            {
                vrCadena += "|" + x.IdMesa;
            });
            return vrCadena;
        }

        private bool MethodValidaDatos()
        {
            if (CollectionSelectESGR_Mesa.Count == 0)
            {
                CmpMessageBox.Show(SGRMessage.BusquedaMesaDisponible, SGRMessageContent.ContentSelecedtNull + "Guardar Mesa", CmpButton.Aceptar);
                return true;
            }
            else if (Identificador.Trim().Length == 0)
            {
                CmpMessageBox.Show(SGRMessage.BusquedaMesaDisponible, "Debe ingresar un nombre de Mesa", CmpButton.Aceptar);
                return true;
            }
            else if(!Reservado && NumPersonas == 0)
            {
                CmpMessageBox.Show(SGRMessage.BusquedaMesaDisponible, "Debe ingresar cantidad de personas", CmpButton.Aceptar);
                return true;
            }
            return false;
        }

        #endregion
    }
}