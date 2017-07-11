/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
 * ********************************************************
 * 
'* FCH. CREACIÓN : 23/12/2016 [ABEL QUISPE ORELLANA]
 * 
**********************************************************/

namespace SGR.ViewModels.Orders.Index.Pages
{
    using ComputerSystems;
    using ComputerSystems.WPF.Acciones.Controles.Menus;
    using ComputerSystems.WPF.Acciones.Controles.ModalDialog;
    using ComputerSystems.WPF.Interfaces;
    using ComputerSystems.WPF.Notificaciones;
    using SGR.Models.Business;
    using SGR.Models.Entity;
    using System;
    using System.Linq;
    using System.Windows.Input;
    using ComputerSystems.WPF;
    using SGR.Models;
    using System.Threading.Tasks;

    public class VMSGR_Index : CmpNavigationService, CmpINavigation
    {
        public VMSGR_Index() { }

        public object Parameter
        {
            set
            {
                if (value is string)
                {
                    try
                    {
                        SGRVariables.ESGR_Usuario.Opcion = "N";
                        new BSGR_Usuario().TransUsuario(SGRVariables.ESGR_Usuario);
                    }
                    catch (Exception) { }
                    Volver(null);
                }
                NombreMozo = SGR.Models.SGRVariables.ESGR_Usuario.NombresApellidos;
                Perfil = SGRVariables.ESGR_Usuario.ESGR_Perfil.NombrePerfil;
            }
        }
        
        #region OBJETOS SECUNDARIOS

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

        private string _Perfil;
        public string Perfil
        {
            get
            {
                return _Perfil + ": ";
            }
            set
            {
                _Perfil = value;
                OnPropertyChanged("Perfil");
            }
        }

        private string _NombreMozo;
        public string NombreMozo
        {
            get
            {
                return _NombreMozo;
            }
            set
            {
                _NombreMozo = value;
                OnPropertyChanged("NombreMozo");
            }
        }

        #endregion

        #region ICOMMAND

        public ICommand ISolicitar
        {
            get
            {
                return CmpICommand.GetICommand((T) =>
                {
                    ESGR_Pedido ESGR_Pedido = new ESGR_Pedido() { Opcion = "I" };

                    var Result = CmpModalDialog.GetContent().ShowDialog("MPSGR_MesasDisponibles", new ESGR_Mesa());
                    if (Result is object[])
                    {
                        var result = (object[])Result;
                        CollectionSelectESGR_Mesa.Source = (CmpObservableCollection<ESGR_Mesa>)result[0];
                        ESGR_Pedido.Identificador = (string)result[1];
                        ESGR_Pedido.Cubierto = (short)(int)result[2];

                        object[] objPedido = new object[2];
                        objPedido[0] = ESGR_Pedido;
                        objPedido[1] = CollectionSelectESGR_Mesa;
                        Ir(CmpMenu.GetPage("PGSGR_Pedido"), objPedido);
                    }
                });
            }
        }

        public ICommand IEditar
        {
            get
            {
                return CmpICommand.GetICommand((E) =>
                {
                    ESGR_Mesa result = new ESGR_Mesa();
                    result = (ESGR_Mesa)CmpModalDialog.GetContent().ShowDialog("MPSGR_MesasAtendidas", new ESGR_Mesa() { Opcion = "U" });
                    if (result != null && result.IdPedido != 0)
                    {
                        try
                        {
                            ESGR_Pedido ObjESGR_Pedido = new ESGR_Pedido();
                            ObjESGR_Pedido = new BSGR_Pedido().GetCollectionPedido().ToList().FirstOrDefault(x => x.IdPedido == ((ESGR_Mesa)result).IdPedido);
                            ObjESGR_Pedido.Opcion = "U";
                            Ir(CmpMenu.GetPage("PGSGR_Pedido"), ObjESGR_Pedido);
                        }
                        catch (Exception ex)
                        {
                            CmpMessageBox.Show(SGRMessage.TitlePedido, ex.Message, CmpButton.Aceptar);
                        }
                    }
                });
            }
        }

        public ICommand ICerrar
        {
            get
            {
                return CmpICommand.GetICommand(async(E) =>
                {
                    ESGR_Mesa result = null;
                    var Result = CmpModalDialog.GetContent().ShowDialog("MPSGR_MesasCerrarAnular", new ESGR_Mesa() { Opcion = "C" });
                    if (Result is string)
                    {
                        await Task.Factory.StartNew(() =>
                        {
                            try
                            {
                                SGRVariables.ESGR_Usuario.Opcion = "N";
                                new BSGR_Usuario().TransUsuario(SGRVariables.ESGR_Usuario);
                            }
                            catch (Exception) { }
                        });
                        Volver(null);
                    }
                    else if (Result is ESGR_Mesa)
                    {
                        result = (ESGR_Mesa)Result;
                    }
                });
            }
        }

        public ICommand IAnular
        {
            get
            {
                return CmpICommand.GetICommand(async(E) =>
                {
                    ESGR_Mesa result = null;
                    var Result = CmpModalDialog.GetContent().ShowDialog("MPSGR_MesasCerrarAnular", new ESGR_Mesa() { Opcion = "A" });
                    if (Result is ESGR_Mesa)
                        result = (ESGR_Mesa)Result;
                    else if (Result is string)
                    {
                        await Task.Factory.StartNew(() =>
                        {
                            try
                            {
                                SGRVariables.ESGR_Usuario.Opcion = "N";
                                new BSGR_Usuario().TransUsuario(SGRVariables.ESGR_Usuario);
                            }
                            catch (Exception) { }
                        });
                        Volver(null);
                    }
                    if (result != null && result.IdPedido != 0)
                    {
                        try
                        {
                            ESGR_Pedido ObjESGR_Pedido = new ESGR_Pedido();
                            ObjESGR_Pedido = new BSGR_Pedido().GetCollectionPedido().ToList().FirstOrDefault(x => x.IdPedido == ((ESGR_Mesa)result).IdPedido);
                            ObjESGR_Pedido.Observacion = result.Opcion;
                            ObjESGR_Pedido.Opcion = "A";
                            Ir(CmpMenu.GetPage("PGSGR_Pedido"), ObjESGR_Pedido);
                        }
                        catch (Exception ex)
                        {
                            CmpMessageBox.Show(SGRMessage.TitlePedido, ex.Message, CmpButton.Aceptar);
                        }
                    }
                });
            }
        }

        public ICommand ISalir
        {
            get
            {
                return CmpICommand.GetICommand((T) =>
                {
                    try
                    {
                        SGRVariables.ESGR_Usuario.Opcion = "N";
                        new BSGR_Usuario().TransUsuario(SGRVariables.ESGR_Usuario);
                    }
                    catch (Exception) { }
                    Volver(null);
                });
            }
        }

        #endregion
    }
}
