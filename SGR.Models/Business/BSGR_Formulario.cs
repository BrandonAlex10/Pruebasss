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
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Data;

    public class BSGR_Formulario
    {
        /// <summary>
        /// Insertar, Editar y Eliminar Formulario
        /// </summary>
        /// <param name="ESGR_Formulario">Objecto de la Entidad Formulario</param>
        public void TransFormulario(ESGR_Formulario ESGR_Formulario)
        {
            try
            {
                var objCmpSql = new CmpSql(SGRVariables.ConectionString);
                objCmpSql.CommandProcedure("spSGR_SET_Formulario");
                objCmpSql.AddParameter("@CodFormulario", SqlDbType.Char, ESGR_Formulario.CodFormulario);
                objCmpSql.AddParameter("@IdModulo", SqlDbType.Int, ESGR_Formulario.ESGR_Modulo.IdModulo);
                objCmpSql.AddParameter("@NombreFormulario", SqlDbType.VarChar, ESGR_Formulario.NombreFormulario);
                objCmpSql.AddParameter("@Descripcion", SqlDbType.VarChar, ESGR_Formulario.Descripcion);
                objCmpSql.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Lista de Formulario
        /// </summary>
        /// <returns>Colección de Formulario</returns>
        public CmpObservableCollection<ESGR_Formulario> GetCollectionFormulario()
        {
            try
            {
                var objCmpSql = new CmpSql(SGRVariables.ConectionString);
                var CollectionFormulario = new CmpObservableCollection<ESGR_Formulario>();

                objCmpSql.CommandProcedure("spSGR_GET_Formulario");
                DataTable dt = objCmpSql.ExecuteDataTable();

                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    CollectionFormulario.Add(new ESGR_Formulario
                    {
                        CodFormulario = (dt.Rows[x]["CodFormulario"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["CodFormulario"]) : string.Empty,
                        Descripcion = (dt.Rows[x]["CodFormulario"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["CodFormulario"]) : string.Empty,
                        NombreFormulario = (dt.Rows[x]["CodFormulario"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["CodFormulario"]) : string.Empty,
                        ESGR_Modulo = new ESGR_Modulo()
                        {
                            IdModulo = (dt.Rows[x]["CodFormulario"] != DBNull.Value) ? Convert.ToInt16(dt.Rows[x]["CodFormulario"]) : Convert.ToInt16(0),
                        },
                    });
                }

                return CollectionFormulario;
            }

            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Lista de Formulario
        /// </summary>
        /// <returns>Colección de Formulario</returns>
        public List<ESGR_Formulario> ListGetAllFormulario()
        {
            try
            {
                var objCmpSql = new CmpSql(SGRVariables.ConectionString);
                var CollectionFormulario = new List<ESGR_Formulario>();

                objCmpSql.CommandProcedure("spSGR_GET_BusquedaGeneral");
                objCmpSql.AddParameter("@Opcion", SqlDbType.VarChar, "GetAllFormulario");
                objCmpSql.AddParameter("@Filtro", SqlDbType.VarChar, "%");
                objCmpSql.AddParameter("@ParameterId", SqlDbType.Int, 0);
                DataTable dt = objCmpSql.ExecuteDataTable();

                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    CollectionFormulario.Add(new ESGR_Formulario
                    {
                        CodFormulario = (dt.Rows[x]["CodFormulario"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["CodFormulario"]) : string.Empty,
                        Descripcion = (dt.Rows[x]["Descripcion"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Descripcion"]) : string.Empty,
                        NombreFormulario = (dt.Rows[x]["NombreFormulario"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["NombreFormulario"]) : string.Empty,
                        ESGR_Modulo = new ESGR_Modulo()
                        {
                            IdModulo = (dt.Rows[x]["IdModulo"] != DBNull.Value) ? Convert.ToInt16(dt.Rows[x]["IdModulo"]) : Convert.ToInt16(0),
                            NombreModulo = (dt.Rows[x]["NombreModulo"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["NombreModulo"]) : string.Empty,
                        },
                    });
                }

                return CollectionFormulario;
            }

            catch (Exception)
            {
                throw;
            }
        }
        
    }
}
