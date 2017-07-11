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

    public class BSGR_Moneda
    {
        /// <summary>
        /// Insertar, Editar y Eliminar Moneda
        /// </summary>
        /// <param name="ESGR_Moneda">Objecto de la Entidad Moneda</param>
        public void TransMoneda(ESGR_Moneda ESGR_Moneda)
        {
            try
            {
                var objCmpSql = new CmpSql(SGRVariables.ConectionString);
                objCmpSql.CommandProcedure("spSGR_SET_Moneda");
                objCmpSql.AddParameter("@Opcion", SqlDbType.VarChar, ESGR_Moneda.Opcion);
                objCmpSql.AddParameter("@CodMoneda", SqlDbType.Char, ESGR_Moneda.CodMoneda);
                objCmpSql.AddParameter("@Descripcion", SqlDbType.VarChar, ESGR_Moneda.Descripcion);
                objCmpSql.AddParameter("@Abreviacion", SqlDbType.VarChar, ESGR_Moneda.Abreviacion);
                objCmpSql.AddParameter("@Simbolo", SqlDbType.VarChar, ESGR_Moneda.Simbolo);
                objCmpSql.AddParameter("@Defecto", SqlDbType.Bit, Convert.ToInt16(ESGR_Moneda.Defecto));
                objCmpSql.AddParameter("@IdUsuario", SqlDbType.Int, SGRVariables.ESGR_Usuario.IdUsuario);
                objCmpSql.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Lista de Moneda
        /// </summary>
        /// <returns>Colección de Moneda</returns>
        public CmpObservableCollection<ESGR_Moneda> GetCollectionMoneda()
        {
            try
            {
                var objCmpSql = new CmpSql(SGRVariables.ConectionString);
                var CollectionMoneda = new CmpObservableCollection<ESGR_Moneda>();

                objCmpSql.CommandProcedure("spSGR_GET_BusquedaGeneral");
                objCmpSql.AddParameter("@Opcion", SqlDbType.VarChar, "GetMoneda");
                objCmpSql.AddParameter("@Filtro", SqlDbType.VarChar, "");
                objCmpSql.AddParameter("@ParameterId", SqlDbType.Int, "");
                DataTable dt = objCmpSql.ExecuteDataTable();

                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    CollectionMoneda.Add(new ESGR_Moneda
                    {
                        Abreviacion = (dt.Rows[x]["Abreviacion"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Abreviacion"]) : string.Empty,
                        CodMoneda = (dt.Rows[x]["CodMoneda"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["CodMoneda"]) : string.Empty,
                        Defecto = (dt.Rows[x]["Defecto"] != DBNull.Value) ? Convert.ToBoolean(dt.Rows[x]["Defecto"]) : false,
                        Descripcion = (dt.Rows[x]["Descripcion"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Descripcion"]) : string.Empty,
                        Simbolo = (dt.Rows[x]["Simbolo"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Simbolo"]) : string.Empty,
                    });
                }

                return CollectionMoneda;
            }

            catch (Exception)
            {
                throw;
            }
        }

    }
}
