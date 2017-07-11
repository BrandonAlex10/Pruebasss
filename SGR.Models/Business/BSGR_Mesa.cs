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

    public class BSGR_Mesa
    {
        /// <summary>
        /// Insertar, Editar y Eliminar Mesa
        /// </summary>
        /// <param name="ESGR_Mesa">Objecto de la Entidad Mesa</param>
        public void TransMesa(ESGR_Mesa ESGR_Mesa)
        {
            try
            {
                var objCmpSql = new CmpSql(SGRVariables.ConectionString);
                objCmpSql.CommandProcedure("spSGR_SET_Mesa");
                objCmpSql.AddParameter("@Opcion",SqlDbType.VarChar, ESGR_Mesa.Opcion);
                objCmpSql.AddParameter("@IdMesa", SqlDbType.Int, ESGR_Mesa.IdMesa);
                objCmpSql.AddParameter("@IdMesaArea", SqlDbType.SmallInt, ESGR_Mesa.EESGR_MesaArea.IdMesaArea);
                objCmpSql.AddParameter("@Mesa", SqlDbType.VarChar, ESGR_Mesa.Mesa);
                objCmpSql.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Lista de Mesa
        /// </summary>
        /// <returns>Colección de Mesa</returns>
        public CmpObservableCollection<ESGR_Mesa> GetCollectionMesa(ESGR_MesaArea ESGR_MesaArea, int IdUsuario)
        {
            try
            {
                var objCmpSql = new CmpSql(SGRVariables.ConectionString);
                var CollectionMesa = new CmpObservableCollection<ESGR_Mesa>();

                objCmpSql.CommandProcedure("spSGR_GET_BusquedaGeneral");
                objCmpSql.AddParameter("@Opcion", SqlDbType.VarChar, "GetMesa");
                objCmpSql.AddParameter("@Filtro", SqlDbType.VarChar, (ESGR_MesaArea == null) ? 0 : ESGR_MesaArea.IdMesaArea);
                objCmpSql.AddParameter("@ParameterId", SqlDbType.Int, IdUsuario);
                DataTable dt = objCmpSql.ExecuteDataTable();

                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    CollectionMesa.Add(new ESGR_Mesa
                    {
                        IdMesa = (dt.Rows[x]["IdMesa"] != DBNull.Value) ? Convert.ToInt16(dt.Rows[x]["IdMesa"]) : Convert.ToInt16(0),
                        Mesa = (dt.Rows[x]["Mesa"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Mesa"]) : string.Empty,
                        EESGR_MesaArea = new ESGR_MesaArea()
                        {
                            IdMesaArea = (dt.Rows[x]["IdMesaArea"] != DBNull.Value) ? Convert.ToInt16(dt.Rows[x]["IdMesaArea"]) : Convert.ToInt16(0),
                            MesaArea = (dt.Rows[x]["MesaArea"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["MesaArea"]) : string.Empty
                        },
                        ESGR_Estado = new ESGR_Estado()
                        {
                            CodEstado = (dt.Rows[x]["CodEstado"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["CodEstado"]) : string.Empty,
                            Estado = (dt.Rows[x]["Estado"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Estado"]) : string.Empty
                        },
                        Identificador = (dt.Rows[x]["Identificador"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Identificador"]) : string.Empty,
                        IdPedido = (dt.Rows[x]["IdPedido"] != DBNull.Value) ? Convert.ToInt32(dt.Rows[x]["IdPedido"]) : 0,
                        ESGR_Usuario = new ESGR_Usuario()
                        {
                            IdUsuario = (dt.Rows[x]["IdUsuarioPedido"] != DBNull.Value) ? Convert.ToInt32(dt.Rows[x]["IdUsuarioPedido"]) : 0,
                        }
                    });
                }

                return CollectionMesa;
            }

            catch (Exception)
            {
                throw;
            }
        }

        public CmpObservableCollection<ESGR_Mesa> GetCollectionMesaMantenimiento()
        {
            try
            {
                var objCmpSql = new CmpSql(SGRVariables.ConectionString);
                var CollectionMesa = new CmpObservableCollection<ESGR_Mesa>();

                objCmpSql.CommandProcedure("spSGR_GET_BusquedaGeneral");
                objCmpSql.AddParameter("@Opcion", SqlDbType.VarChar, "GetMesaMantenimiento");
                objCmpSql.AddParameter("@Filtro", SqlDbType.VarChar, "%");
                objCmpSql.AddParameter("@ParameterId", SqlDbType.Int, SGRVariables.ESGR_Usuario.IdUsuario);
                DataTable dt = objCmpSql.ExecuteDataTable();

                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    CollectionMesa.Add(new ESGR_Mesa
                    {
                        IdMesa = (dt.Rows[x]["IdMesa"] != DBNull.Value) ? Convert.ToInt16(dt.Rows[x]["IdMesa"]) : Convert.ToInt16(0),
                        Mesa = (dt.Rows[x]["Mesa"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Mesa"]) : string.Empty,
                        EESGR_MesaArea = new ESGR_MesaArea()
                        {
                            IdMesaArea = (dt.Rows[x]["IdMesaArea"] != DBNull.Value) ? Convert.ToInt16(dt.Rows[x]["IdMesaArea"]) : Convert.ToInt16(0),
                            MesaArea = (dt.Rows[x]["MesaArea"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["MesaArea"]) : string.Empty
                        },
                    });
                }

                return CollectionMesa;
            }

            catch (Exception)
            {
                throw;
            }
        }
    }
}
