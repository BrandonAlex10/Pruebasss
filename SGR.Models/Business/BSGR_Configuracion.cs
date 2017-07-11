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

    public class BSGR_Configuracion
    {
        /// <summary>
        /// Insertar, Editar y Eliminar Configuracion
        /// </summary>
        /// <param name="ESGR_Configuracion">Objecto de la Entidad Configuracion</param>
        public void TransConfiguracion(ESGR_Configuracion ESGR_Configuracion)
        {
            try
            {
                var objCmpSql = new CmpSql(SGRVariables.ConectionString);
                objCmpSql.CommandProcedure("spSGR_SET_Configuracion");
                objCmpSql.AddParameter("@IdConfig", SqlDbType.VarChar, ESGR_Configuracion.IdConfig);
                objCmpSql.AddParameter("@NivelConfig", SqlDbType.Char, ESGR_Configuracion.NivelConfig);
                objCmpSql.AddParameter("@Valor", SqlDbType.NText, (ESGR_Configuracion.NivelConfig == "ST") ? CmpCifrarObjecto.Encriptar(ESGR_Configuracion.Valor) : ESGR_Configuracion.Valor);
                objCmpSql.AddParameter("@Descripcion", SqlDbType.VarChar, ESGR_Configuracion.Descripcion);
                objCmpSql.AddParameter("@FlgEliminado", SqlDbType.VarChar, ESGR_Configuracion.FlgEliminado);
                objCmpSql.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Lista de Configuracion
        /// </summary>
        /// <returns>Colección de Configuracion</returns>
        public CmpObservableCollection<ESGR_Configuracion> GetCollectionConfiguracion()
        {
            try
            {
                var objCmpSql = new CmpSql(SGRVariables.ConectionString);
                var CollectionConfiguracion = new CmpObservableCollection<ESGR_Configuracion>();

                objCmpSql.CommandProcedure("spSGR_GET_BusquedaGeneral");
                objCmpSql.AddParameter("@Opcion", SqlDbType.VarChar, "GetConfiguracion");
                objCmpSql.AddParameter("@Filtro", SqlDbType.VarChar, "");
                objCmpSql.AddParameter("@ParameterId", SqlDbType.Int, 0);
                DataTable dt = objCmpSql.ExecuteDataTable();

                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    string NivelConfig = (dt.Rows[x]["NivelConfig"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["NivelConfig"]) : string.Empty;
                    string Valor = (dt.Rows[x]["Valor"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Valor"]) : string.Empty;

                    CollectionConfiguracion.Add(new ESGR_Configuracion
                    {
                        IdConfig = (dt.Rows[x]["IdConfig"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["IdConfig"]) : string.Empty,
                        NivelConfig = NivelConfig,
                        Valor = (NivelConfig == "ST") ? CmpCifrarObjecto.Desencriptar(Valor) : Valor,
                        Descripcion = (dt.Rows[x]["Descripcion"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Descripcion"]) : string.Empty,
                        FlgEliminado = (dt.Rows[x]["FlgEliminado"] != DBNull.Value) ? Convert.ToBoolean(dt.Rows[x]["FlgEliminado"]) : false,
                    });
                }

                return CollectionConfiguracion;
            }

            catch (Exception)
            {
                throw;
            }
        }

    }
}
