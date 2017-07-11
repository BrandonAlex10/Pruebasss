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

    public class BSGR_PedidoTipo
    {
        /// <summary>
        /// Insertar, Editar y Eliminar Pedido Tipo
        /// </summary>
        /// <param name="ESGR_PedidoTipo">Objecto de la Entidad Pedido Tipo</param>
        public void TransPedidoTipo(ESGR_PedidoTipo ESGR_PedidoTipo)
        {
            try
            {
                var objCmpSql = new CmpSql(SGRVariables.ConectionString);
                objCmpSql.CommandProcedure("spSGR_SET_PedidoTipo");
                objCmpSql.AddParameter("@Opcion", SqlDbType.VarChar, ESGR_PedidoTipo.Opcion);
                objCmpSql.AddParameter("@IdPedidoTipo", SqlDbType.SmallInt, ESGR_PedidoTipo.IdPedidoTipo);
                objCmpSql.AddParameter("@PedidoTipo", SqlDbType.VarChar, ESGR_PedidoTipo.PedidoTipo);
                objCmpSql.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Lista de Pedido Tipo
        /// </summary>
        /// <returns>Colección de las Pedido Tipo</returns>
        public CmpObservableCollection<ESGR_PedidoTipo> GetCollectionPedidoTipo()
        {
            try
            {
                var objCmpSql = new CmpSql(SGRVariables.ConectionString);
                var CollectionPedidoTipo = new CmpObservableCollection<ESGR_PedidoTipo>();

                objCmpSql.CommandProcedure("spSGR_GET_BusquedaGeneral");
                objCmpSql.AddParameter("@Opcion", SqlDbType.VarChar, "GetPedidoTipo");
                objCmpSql.AddParameter("@Filtro", SqlDbType.VarChar, "%");
                objCmpSql.AddParameter("@ParameterId", SqlDbType.VarChar, 0);
                DataTable dt = objCmpSql.ExecuteDataTable();

                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    CollectionPedidoTipo.Add(new ESGR_PedidoTipo
                    {
                        IdPedidoTipo = (dt.Rows[x]["IdPedidoTipo"] != DBNull.Value) ? Convert.ToInt16(dt.Rows[x]["IdPedidoTipo"]) : Convert.ToInt16(0),
                        PedidoTipo = (dt.Rows[x]["PedidoTipo"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["PedidoTipo"]) : string.Empty,
                    });
                }

                return CollectionPedidoTipo;
            }

            catch (Exception)
            {
                throw;
            }
        }

    }
}
