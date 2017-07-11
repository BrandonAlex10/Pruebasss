/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   OSCAR HUAMAN CABRERA
'* FCH. CREACIÓN : 29/11/2016
**********************************************************/
namespace SGR.Models.Business
{
    using ComputerSystems;
    using ComputerSystems.DataAccess.LibrarySql;
    using SGR.Models.Entity;
    using System;
    using System.Collections.ObjectModel;
    using System.Data;

    public class BSGR_PedidoDetalle
    {
        /// <summary>
        /// Insertar, Editar y Eliminar Pedido Detalle
        /// </summary>
        /// <param name="ESGR_PedidoDetalle">Objecto de la Entidad Pedido Detalle</param>
        public void TransPedidoDetalle(ESGR_PedidoDetalle ESGR_PedidoDetalle)
        {
            try
            {
                var objCmpSql = new CmpSql(SGRVariables.ConectionString);
                objCmpSql.CommandProcedure("spSGR_SET_PedidoDetalle");
                objCmpSql.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Lista de Pedido Detalle
        /// </summary>
        /// <returns>Colección de las Pedido Detalle</returns>
        public CmpObservableCollection<ESGR_PedidoDetalle> GetCollectionPedidoDetalle(ESGR_Pedido ESGR_Pedido)
        {
            try
            {
                var objCmpSql = new CmpSql(SGRVariables.ConectionString);
                var CollectionPedidoDetalle = new CmpObservableCollection<ESGR_PedidoDetalle>();

                objCmpSql.CommandProcedure("spSGR_GET_BusquedaGeneral");
                objCmpSql.AddParameter("@Opcion", SqlDbType.VarChar, "AdministrarPedidoDetalle");
                objCmpSql.AddParameter("@Filtro", SqlDbType.VarChar, "%");
                objCmpSql.AddParameter("@ParameterId", SqlDbType.Int, ESGR_Pedido.IdPedido);
                DataTable dt = objCmpSql.ExecuteDataTable();

                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    CollectionPedidoDetalle.Add(new ESGR_PedidoDetalle
                    {
                        ESGR_Pedido = new ESGR_Pedido()
                        {
                            IdPedido = (dt.Rows[x]["IdPedido"] != DBNull.Value) ? Convert.ToInt16(dt.Rows[x]["IdPedido"]) : Convert.ToInt16(0),
                        },
                        ESGR_Producto = new ESGR_Producto()
                        {
                            IdProducto = (dt.Rows[x]["IdProducto"] != DBNull.Value) ? Convert.ToInt16(dt.Rows[x]["IdProducto"]) : Convert.ToInt16(0),
                            Producto = (dt.Rows[x]["Producto"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Producto"]) : string.Empty,
                            Precio = (dt.Rows[x]["Precio"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[x]["Precio"]) : Convert.ToDecimal(0),
                            ESGR_ProductoSubCategoria = new ESGR_ProductoSubCategoria()
                            {
                                ESGR_ProductoCategoria = new ESGR_ProductoCategoria()
                                {
                                    Impresora = (dt.Rows[x]["Impresora"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Impresora"]) : string.Empty
                                }
                            }
                        },
                        Cantidad = (dt.Rows[x]["Cantidad"] != DBNull.Value) ? Convert.ToInt16(dt.Rows[x]["Cantidad"]) : Convert.ToInt16(0),
                        CantidadMesa = (dt.Rows[x]["CantidadMesa"] != DBNull.Value) ? Convert.ToInt16(dt.Rows[x]["CantidadMesa"]) : Convert.ToInt16(0),
                        CantidadLlevar = (dt.Rows[x]["CantidadLlevar"] != DBNull.Value) ? Convert.ToInt16(dt.Rows[x]["CantidadLlevar"]) : Convert.ToInt16(0),
                        Enviado = (dt.Rows[x]["Enviado"] != DBNull.Value) ? Convert.ToBoolean(dt.Rows[x]["Enviado"]) : false,
                    });
                }

                return CollectionPedidoDetalle;
            }

            catch (Exception)
            {
                throw;
            }
        }

    }
}
