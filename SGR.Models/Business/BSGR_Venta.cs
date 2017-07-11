/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   OSCAR HUAMAN CABRERA
'* FCH. CREACIÓN : 30/11/2016
**********************************************************/
namespace SGR.Models.Business
{
    using ComputerSystems;
    using ComputerSystems.DataAccess.LibrarySql;
    using SGR.Models.Entity;
    using System;
    using System.Collections.ObjectModel;
    using System.Data;

    public class BSGR_Venta
    {
        /// <summary>
        /// Insertar, Editar y Eliminar Venta
        /// </summary>
        /// <param name="ESGR_Venta">Objecto de la Entidad Venta</param>
        public void TransVenta(ESGR_Venta ESGR_Venta)
        {
            try
            {
                var objCmpSql = new CmpSql(SGRVariables.ConectionString);
                objCmpSql.CommandProcedure("spSGR_SET_Venta");
                objCmpSql.AddParameter("@Opcion", SqlDbType.VarChar,ESGR_Venta.Opcion);
                objCmpSql.AddParameter("@IdVenta", SqlDbType.Int, ESGR_Venta.IdVenta);
                objCmpSql.AddParameter("@IdEmpSucursal", SqlDbType.SmallInt, SGRVariables.ESGR_Usuario.ESGR_Empresa.IdEmpresa);
                objCmpSql.AddParameter("@IdPedido", SqlDbType.Int, ESGR_Venta.ESGR_Pedido.IdPedido);
                objCmpSql.AddParameter("@IdCaja", SqlDbType.Int, ESGR_Venta.ESGR_Caja.IdCaja);
                objCmpSql.AddParameter("@CodEstado", SqlDbType.Char, "ATPED");
                objCmpSql.AddParameter("@Fecha", SqlDbType.DateTime, DateTime.Now);
                objCmpSql.AddParameter("@CuentasXML", SqlDbType.NText, ESGR_Venta.CuentasXML);
                objCmpSql.AddParameter("@DetalleXML", SqlDbType.NText, ESGR_Venta.DetalleXML);
                objCmpSql.AddParameter("@IdUsuario", SqlDbType.Int, SGRVariables.ESGR_Usuario.IdUsuario);
                objCmpSql.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Lista de Venta
        /// </summary>
        /// <returns>Colección de las Ventas</returns>
        public CmpObservableCollection<ESGR_Venta> GetCollectionVenta(string Opcion, DateTime FechaIni, DateTime FechaFin, string Filtro, ESGR_Caja ESGR_Caja)
        {
            try
            {
                var objCmpSql = new CmpSql(SGRVariables.ConectionString);
                var CollectionVenta = new CmpObservableCollection<ESGR_Venta>();

                objCmpSql.CommandProcedure("spSGR_GET_Venta");
                objCmpSql.AddParameter("@Opcion",SqlDbType.VarChar,Opcion);
                objCmpSql.AddParameter("@FechaIni", SqlDbType.SmallDateTime, FechaIni);
                objCmpSql.AddParameter("@FechaFin", SqlDbType.SmallDateTime, FechaFin);
                objCmpSql.AddParameter("@Filtro", SqlDbType.VarChar, (Filtro.Trim().Length == 0 || Filtro==null)?"%": Filtro);
                objCmpSql.AddParameter("@ParameterId", SqlDbType.VarChar, ESGR_Caja.IdCaja);
                DataTable dt = objCmpSql.ExecuteDataTable();

                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    CollectionVenta.Add(new ESGR_Venta
                    {
                        IdVenta = (dt.Rows[x]["IdVenta"] != DBNull.Value) ? Convert.ToInt16(dt.Rows[x]["IdVenta"]) : Convert.ToInt16(0),
                        Fecha = (dt.Rows[x]["Fecha"] != DBNull.Value) ? Convert.ToDateTime(dt.Rows[x]["Fecha"]) : DateTime.Now,
                        ESGR_Pedido = new ESGR_Pedido()
                        {
                            IdPedido = (dt.Rows[x]["IdPedido"] != DBNull.Value) ? Convert.ToInt16(dt.Rows[x]["IdPedido"]) : Convert.ToInt16(0),
                            Identificador = (dt.Rows[x]["Identificador"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Identificador"]) : string.Empty,
                            ESGR_Usuario = new ESGR_Usuario()
                            {
                                IdUsuario = (dt.Rows[x]["IdUsuarioPedido"] != DBNull.Value) ? Convert.ToInt16(dt.Rows[x]["IdUsuarioPedido"]) : Convert.ToInt16(0),
                                Nombres = (dt.Rows[x]["Nombres"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Nombres"]) : string.Empty,
                                Apellidos = (dt.Rows[x]["Apellidos"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Apellidos"]) : string.Empty,
                            }
                        }
                    });
                }

                return CollectionVenta;
            }

            catch (Exception)
            {
                throw;
            }
        }

        public CmpObservableCollection<ESGR_Venta> GetTotalPorPedido(int ParameterId)
        {
            try
            {
                var objCmpSql = new CmpSql(SGRVariables.ConectionString);
                var CollectionVenta = new CmpObservableCollection<ESGR_Venta>();

                objCmpSql.CommandProcedure("spSGR_GET_BusquedaGeneral");
                objCmpSql.AddParameter("@Opcion", SqlDbType.VarChar, "GetTotalPedido");
                objCmpSql.AddParameter("@Filtro", SqlDbType.VarChar, "%");
                objCmpSql.AddParameter("@ParameterId", SqlDbType.VarChar, ParameterId);
                DataTable dt = objCmpSql.ExecuteDataTable();
                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    CollectionVenta.Add(new ESGR_Venta()
                    {
                        ESGR_Pedido = new ESGR_Pedido()
                        {
                            IdPedido = (dt.Rows[x]["IdPedido"] != DBNull.Value) ? Convert.ToInt32(dt.Rows[x]["IdPedido"]) : 0,
                            Cubierto = (dt.Rows[x]["Cubierto"] != DBNull.Value) ? (short)Convert.ToInt32(dt.Rows[x]["Cubierto"]) : (short)0
                        }
                    });
                }

                return CollectionVenta;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
