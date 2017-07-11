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

    public class BSGR_Modulo
    {
        /// <summary>
        /// Insertar, Editar y Eliminar Modulo
        /// </summary>
        /// <param name="ESGR_Modulo">Objecto de la Entidad Modulo</param>
        public void TransMoneda(ESGR_Modulo ESGR_Modulo)
        {
            try
            {
                var objCmpSql = new CmpSql(SGRVariables.ConectionString);
                objCmpSql.CommandProcedure("spSGR_SET_Modulo");
                objCmpSql.AddParameter("@IdModulo", SqlDbType.Int, ESGR_Modulo.IdModulo);
                objCmpSql.AddParameter("@NombreModulo", SqlDbType.VarChar, ESGR_Modulo.NombreModulo);
                objCmpSql.AddParameter("@Prefijo", SqlDbType.Char, ESGR_Modulo.Prefijo);
                objCmpSql.AddParameter("@Descripcion", SqlDbType.VarChar, ESGR_Modulo.Descripcion);
                objCmpSql.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Lista de Modulo
        /// </summary>
        /// <returns>Colección de Modulo</returns>
        public CmpObservableCollection<ESGR_Modulo> GetCollectionModulo()
        {
            try
            {
                var objCmpSql = new CmpSql(SGRVariables.ConectionString);
                var CollectionModulo = new CmpObservableCollection<ESGR_Modulo>();

                objCmpSql.CommandProcedure("spSGR_GET_BusquedaGeneral");
                objCmpSql.AddParameter("@Opcion", SqlDbType.VarChar, "GetModulo");
                objCmpSql.AddParameter("@Filtro", SqlDbType.VarChar, "%");
                objCmpSql.AddParameter("@ParameterId", SqlDbType.Int, 0);
                DataTable dt = objCmpSql.ExecuteDataTable();

                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    CollectionModulo.Add(new ESGR_Modulo
                    {
                        IdModulo = (dt.Rows[x]["IdModulo"] != DBNull.Value) ? Convert.ToInt16(dt.Rows[x]["IdModulo"]) : Convert.ToInt16(0),
                        Descripcion = (dt.Rows[x]["Descripcion"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Descripcion"]) : string.Empty,
                        NombreModulo = (dt.Rows[x]["NombreModulo"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["NombreModulo"]) : string.Empty,
                        Prefijo = (dt.Rows[x]["Prefijo"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Prefijo"]) : string.Empty,
                    });
                }

                return CollectionModulo;
            }

            catch (Exception)
            {
                throw;
            }
        }

    }
}
