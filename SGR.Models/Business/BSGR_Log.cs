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

    public class BSGR_Log
    {
        /// <summary>
        /// Insertar, Editar Log
        /// </summary>
        /// <param name="ESGR_Log">Objecto de la Entidad Log</param>
        public void TransLog(ESGR_Log ESGR_Log)
        {
            try
            {
                var objCmpSql = new CmpSql(SGRVariables.ConectionString);
                objCmpSql.CommandProcedure("spSGR_SET_Log");
                objCmpSql.AddParameter("@Tabla", SqlDbType.VarChar, ESGR_Log.Tabla);
                objCmpSql.AddParameter("@IdRegistro", SqlDbType.Int, ESGR_Log.IdRegistro);
                objCmpSql.AddParameter("@IdRegistro", SqlDbType.Int, ESGR_Log.IdUsuario);
                objCmpSql.AddParameter("@Item", SqlDbType.Int, ESGR_Log.Item);
                objCmpSql.AddParameter("@Operacion", SqlDbType.Char, ESGR_Log.Operacion);
                objCmpSql.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Lista de Log
        /// </summary>
        /// <returns>Colección de Log</returns>
        public CmpObservableCollection<ESGR_Log> GetCollectionLog()
        {
            try
            {
                var objCmpSql = new CmpSql(SGRVariables.ConectionString);
                var CollectionLog = new CmpObservableCollection<ESGR_Log>();

                objCmpSql.CommandProcedure("spSGR_GET_Log");
                DataTable dt = objCmpSql.ExecuteDataTable();

                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    CollectionLog.Add(new ESGR_Log
                    {
                        Item = (dt.Rows[x]["Item"] != DBNull.Value) ? Convert.ToInt16(dt.Rows[x]["Item"]) : Convert.ToInt16(0),
                        IdRegistro = (dt.Rows[x]["IdRegistro"] != DBNull.Value) ? Convert.ToInt16(dt.Rows[x]["IdRegistro"]) : Convert.ToInt16(0),
                        IdUsuario = (dt.Rows[x]["IdUsuario"] != DBNull.Value) ? Convert.ToInt16(dt.Rows[x]["IdUsuario"]) : Convert.ToInt16(0),
                        Operacion = (dt.Rows[x]["Operacion"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Operacion"]) : string.Empty,
                        Tabla = (dt.Rows[x]["Tabla"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Tabla"]) : string.Empty,
                    });
                }

                return CollectionLog;
            }

            catch (Exception)
            {
                throw;
            }
        }

    }
}
