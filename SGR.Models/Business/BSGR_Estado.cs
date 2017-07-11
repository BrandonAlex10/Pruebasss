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

    public class BSGR_Estado
    {
        /// <summary>
        /// Insertar, Editar y Eliminar Estado
        /// </summary>
        /// <param name="ESGR_Estado">Objecto de la Entidad Estado</param>
        public void TransEstado(ESGR_Estado ESGR_Estado)
        {
            try
            {
                var objCmpSql = new CmpSql(SGRVariables.ConectionString);
                objCmpSql.CommandProcedure("spSGR_SET_Estado");
                objCmpSql.AddParameter("@CodEstado", SqlDbType.Char, ESGR_Estado.CodEstado);
                objCmpSql.AddParameter("@Tabla", SqlDbType.Char, ESGR_Estado.Tabla);
                objCmpSql.AddParameter("@Campo", SqlDbType.Char, ESGR_Estado.Campo);
                objCmpSql.AddParameter("@Estado", SqlDbType.Char, ESGR_Estado.Estado);
                objCmpSql.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Lista de Estado
        /// </summary>
        /// <returns>Colección de Estado</returns>
        public CmpObservableCollection<ESGR_Estado> GetCollectionEstado(string Filtro = "%")
        {
            try
            {
                var objCmpSql = new CmpSql(SGRVariables.ConectionString);
                var CollectionEstado = new CmpObservableCollection<ESGR_Estado>();

                objCmpSql.CommandProcedure("spSGR_GET_BusquedaGeneral");
                objCmpSql.AddParameter("@Opcion", SqlDbType.VarChar, "FiltrarEstado");
                objCmpSql.AddParameter("@Filtro", SqlDbType.VarChar, (Filtro == null || Filtro.Trim().Length == 0) ? "%" : Filtro);
                objCmpSql.AddParameter("@ParameterId", SqlDbType.Int, 0);
                DataTable dt = objCmpSql.ExecuteDataTable();

                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    CollectionEstado.Add(new ESGR_Estado
                    {
                        CodEstado = (dt.Rows[x]["CodEstado"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["CodEstado"]) : string.Empty,
                        Campo = (dt.Rows[x]["Campo"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Campo"]) : string.Empty,
                        Estado = (dt.Rows[x]["Estado"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Estado"]) : string.Empty,
                        Tabla = (dt.Rows[x]["Tabla"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Tabla"]) : string.Empty,
                    });
                }

                return CollectionEstado;
            }

            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Lista de Reporte por Estado
        /// </summary>
        /// <returns>Colección de Reporte por Estado</returns>
        public CmpObservableCollection<ESGR_Estado> GetReportCollectionEstado(int ParameterId)
        {
            try
            {
                var objCmpSql = new CmpSql(SGRVariables.ConectionString);
                var CollectionEstado = new CmpObservableCollection<ESGR_Estado>();

                objCmpSql.CommandProcedure("spSGR_GET_BusquedaGeneral");
                objCmpSql.AddParameter("@Opcion", SqlDbType.VarChar, "GetTotalEstado");
                objCmpSql.AddParameter("@Filtro", SqlDbType.VarChar, "%");
                objCmpSql.AddParameter("@ParameterId", SqlDbType.Int, ParameterId);
                DataTable dt = objCmpSql.ExecuteDataTable();

                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    CollectionEstado.Add(new ESGR_Estado
                    {
                        CodEstado = (dt.Rows[x]["CodEstado"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["CodEstado"]) : string.Empty,
                        Estado = (dt.Rows[x]["Estado"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Estado"]) : string.Empty,
                        Total = (dt.Rows[x]["Total"] != DBNull.Value) ? Convert.ToInt16(dt.Rows[x]["Total"]) : 0
                    });
                }

                return CollectionEstado;
            }

            catch (Exception)
            {
                throw;
            }
        }
    }
}
