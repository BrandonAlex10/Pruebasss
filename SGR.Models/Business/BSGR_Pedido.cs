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

    public class BSGR_Pedido
    {
        /// <summary>
        /// Insertar, Editar y Eliminar Pedido
        /// </summary>
        /// <param name="ESGR_Pedido">Objecto de la Entidad Pedido</param>
        public void TransPedido(ESGR_Pedido ESGR_Pedido)
        {
            try
            {
                var objCmpSql = new CmpSql(SGRVariables.ConectionString);
                objCmpSql.CommandProcedure("spSGR_SET_Pedido");
                objCmpSql.AddParameter("@Opcion", SqlDbType.VarChar, ESGR_Pedido.Opcion);
                objCmpSql.AddParameter("@IdPedidoTipo", SqlDbType.SmallInt, ESGR_Pedido.ESGR_PedidoTipo.IdPedidoTipo);
                objCmpSql.AddParameter("@IdEmpSucursal", SqlDbType.SmallInt, SGRVariables.ESGR_Usuario.ESGR_Empresa.IdEmpresa);
                objCmpSql.AddParameter("@IdUsuarioPedido", SqlDbType.Int, SGRVariables.ESGR_Usuario.IdUsuario);
                objCmpSql.AddParameter("@CodEstado", SqlDbType.Char, ESGR_Pedido.ESGR_Estado.CodEstado);
                objCmpSql.AddParameter("@Identificador", SqlDbType.VarChar, ESGR_Pedido.Identificador);
                objCmpSql.AddParameter("@CadenaMesa", SqlDbType.VarChar, ESGR_Pedido.CadenaMesa);
                objCmpSql.AddParameter("@Cubierto", SqlDbType.SmallInt, ESGR_Pedido.Cubierto);
                objCmpSql.AddParameter("@Observacion", SqlDbType.VarChar, ESGR_Pedido.Observacion);
                objCmpSql.AddParameter("@DetalleXML", SqlDbType.NText, ESGR_Pedido.CadenaDetalleXML);
                objCmpSql.AddParameter("@PedidoMesaXML", SqlDbType.NText, ESGR_Pedido.ESGR_Mesa.DetalleMesaXML);

                if (ESGR_Pedido.Opcion == "I")
                {
                    objCmpSql.AddParameterOut("@IdPedido", SqlDbType.SmallInt, 4);
                    objCmpSql.ExecuteNonQuery();

                    ESGR_Pedido.IdPedido = Convert.ToInt32(objCmpSql.GetParameterOut("@IdPedido"));
                }
                else
                {
                    objCmpSql.AddParameter("@IdPedido", SqlDbType.SmallInt, ESGR_Pedido.IdPedido);
                    objCmpSql.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Lista de Pedido
        /// </summary>
        /// <returns>Colección de las Pedido</returns>
        public CmpObservableCollection<ESGR_Pedido> GetCollectionPedido()
        {
            try
            {
                var objCmpSql = new CmpSql(SGRVariables.ConectionString);
                var CollectionPedido = new CmpObservableCollection<ESGR_Pedido>();

                objCmpSql.CommandProcedure("spSGR_GET_BusquedaGeneral");
                objCmpSql.AddParameter("@Opcion", SqlDbType.VarChar, "FiltrarPedidoUsuario");
                objCmpSql.AddParameter("@Filtro", SqlDbType.VarChar, "%");
                objCmpSql.AddParameter("@ParameterId", SqlDbType.Int, SGRVariables.ESGR_Usuario.IdUsuario);
                DataTable dt = objCmpSql.ExecuteDataTable();

                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    CollectionPedido.Add(new ESGR_Pedido
                    {
                        IdPedido = (dt.Rows[x]["IdPedido"] != DBNull.Value) ? Convert.ToInt32(dt.Rows[x]["IdPedido"]) : Convert.ToInt32(0),
                        ESGR_PedidoTipo = new ESGR_PedidoTipo()
                        {
                            IdPedidoTipo = (dt.Rows[x]["IdPedidoTipo"] != DBNull.Value) ? Convert.ToInt16(dt.Rows[x]["IdPedidoTipo"]) : Convert.ToInt16(0),
                            PedidoTipo = (dt.Rows[x]["PedidoTipo"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["PedidoTipo"]) : string.Empty,
                        },
                        ESGR_EmpresaSucursal = new ESGR_EmpresaSucursal()
                        {
                            IdEmpSucursal = (dt.Rows[x]["IdEmpSucursal"] != DBNull.Value) ? Convert.ToInt16(dt.Rows[x]["IdEmpSucursal"]) : Convert.ToInt16(0),
                        },
                        ESGR_Usuario = new ESGR_Usuario()
                        {
                            IdUsuario = (dt.Rows[x]["IdUsuarioPedido"] != DBNull.Value) ? Convert.ToInt16(dt.Rows[x]["IdUsuarioPedido"]) : Convert.ToInt16(0),
                            Nombres = (dt.Rows[x]["Nombres"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Nombres"]) : string.Empty,
                            Apellidos = (dt.Rows[x]["Apellidos"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Apellidos"]) : string.Empty,
                            ESGR_Perfil = new ESGR_Perfil()
                            {
                                IdPerfil = (dt.Rows[x]["IdPerfil"] != DBNull.Value) ? Convert.ToInt16(dt.Rows[x]["IdPerfil"]) : Convert.ToInt16(0),
                                NombrePerfil = (dt.Rows[x]["NombrePerfil"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["NombrePerfil"]) : string.Empty
                            }
                        },
                        ESGR_Estado = new ESGR_Estado()
                        {
                            CodEstado = (dt.Rows[x]["CodEstado"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["CodEstado"]) : string.Empty,
                            Estado = (dt.Rows[x]["Estado"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Estado"]) : string.Empty,
                        },
                        Identificador = (dt.Rows[x]["Identificador"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Identificador"]).Trim() : string.Empty,
                        CadenaMesa = (dt.Rows[x]["CadenaMesa"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["CadenaMesa"]).Trim() : string.Empty,
                        Cubierto = (dt.Rows[x]["Cubierto"] != DBNull.Value) ? Convert.ToInt16(dt.Rows[x]["Cubierto"]) : Convert.ToInt16(0),
                    });
                }

                return CollectionPedido;
            }

            catch (Exception)
            {
                throw;
            }
        }

        public ESGR_Pedido GetPedido()
        {
            try
            {
                var objCmpSql = new CmpSql(SGRVariables.ConectionString);
                objCmpSql.CommandProcedure("spSGR_GET_BusquedaGeneral");
                objCmpSql.AddParameter("@Opcion", SqlDbType.VarChar, "GetIdPedido");
                objCmpSql.AddParameter("@Filtro", SqlDbType.VarChar, "%");
                objCmpSql.AddParameter("@ParameterId", SqlDbType.Int, 0);
                DataTable dt = objCmpSql.ExecuteDataTable();
                return new ESGR_Pedido() { Opcion = "I", IdPedido = Convert.ToInt32(dt.Rows[0][0]) };
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
