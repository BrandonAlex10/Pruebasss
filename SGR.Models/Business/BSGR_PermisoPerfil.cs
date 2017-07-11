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

    public class BSGR_PermisoPerfil
    {
        /// <summary>
        /// Insertar, Editar y Eliminar Permiso Perfil
        /// </summary>
        /// <param name="ESGR_PermisoPerfil">Objecto de la Entidad Permiso Perfil</param>
        public void TransPermisoPerfil(ESGR_PermisoPerfil ESGR_PermisoPerfil)
        {
            try
            {
                var objCmpSql = new CmpSql(SGRVariables.ConectionString);
                objCmpSql.CommandProcedure("spSGR_SET_PermisoPerfil");
                objCmpSql.AddParameter("@IdPermisoPerfil", SqlDbType.Int, ESGR_PermisoPerfil.IdPermisoPerfil);
                objCmpSql.AddParameter("@IdPerfil", SqlDbType.Int, ESGR_PermisoPerfil.ESGR_Perfil.IdPerfil);
                objCmpSql.AddParameter("@CodFormulario", SqlDbType.Char, ESGR_PermisoPerfil.ESGR_Formulario.CodFormulario);
                objCmpSql.AddParameter("@Consulta", SqlDbType.VarChar, ESGR_PermisoPerfil.Consulta);
                objCmpSql.AddParameter("@Nuevo", SqlDbType.VarChar, ESGR_PermisoPerfil.Nuevo);
                objCmpSql.AddParameter("@Editar", SqlDbType.VarChar, ESGR_PermisoPerfil.Editar);
                objCmpSql.AddParameter("@Eliminar", SqlDbType.VarChar, ESGR_PermisoPerfil.Eliminar);
                objCmpSql.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Lista de Permiso Perfil
        /// </summary>
        /// <returns>Colección de los Permiso Perfil</returns>
        public CmpObservableCollection<ESGR_PermisoPerfil> GetCollectionPermisoPerfil(int ParameterId)
        {
            try
            {
                var objCmpSql = new CmpSql(SGRVariables.ConectionString);
                var CollectionPermisoPerfil = new CmpObservableCollection<ESGR_PermisoPerfil>();

                objCmpSql.CommandProcedure("spSGR_GET_BusquedaGeneral");
                objCmpSql.AddParameter("@Opcion", SqlDbType.VarChar, "GetPermisoPerfil");
                objCmpSql.AddParameter("@Filtro", SqlDbType.VarChar, "%");
                objCmpSql.AddParameter("@ParameterId", SqlDbType.Int, ParameterId);
                DataTable dt = objCmpSql.ExecuteDataTable();

                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    CollectionPermisoPerfil.Add(new ESGR_PermisoPerfil
                    {
                        IdPermisoPerfil = (dt.Rows[x]["IdPermisoPerfil"] != DBNull.Value) ? Convert.ToInt16(dt.Rows[x]["IdPermisoPerfil"]) : Convert.ToInt16(0),
                        Consulta = (dt.Rows[x]["Consulta"] != DBNull.Value) ? Convert.ToBoolean(dt.Rows[x]["Consulta"]) : false,
                        Editar = (dt.Rows[x]["Editar"] != DBNull.Value) ? Convert.ToBoolean(dt.Rows[x]["Editar"]) : false,
                        Eliminar = (dt.Rows[x]["Eliminar"] != DBNull.Value) ? Convert.ToBoolean(dt.Rows[x]["Eliminar"]) : false,
                        Nuevo = (dt.Rows[x]["Nuevo"] != DBNull.Value) ? Convert.ToBoolean(dt.Rows[x]["Nuevo"]) : false,
                        ESGR_Formulario = new ESGR_Formulario()
                        {
                            CodFormulario = (dt.Rows[x]["CodFormulario"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["CodFormulario"]) : string.Empty,
                            NombreFormulario = (dt.Rows[x]["NombreFormulario"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["NombreFormulario"]) : string.Empty,
                            ESGR_Modulo = new ESGR_Modulo()
                            {
                                IdModulo = (dt.Rows[x]["IdModulo"] != DBNull.Value) ? Convert.ToInt16(dt.Rows[x]["IdModulo"]) : Convert.ToInt16(0),
                                Prefijo = (dt.Rows[x]["Prefijo"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Prefijo"]) : string.Empty,
                                NombreModulo = (dt.Rows[x]["NombreModulo"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["NombreModulo"]) : string.Empty
                            }
                        },
                        ESGR_Perfil = new ESGR_Perfil()
                        {
                            IdPerfil = (dt.Rows[x]["IdPerfil"] != DBNull.Value) ? Convert.ToInt16(dt.Rows[x]["IdPerfil"]) : Convert.ToInt16(0),
                        },
                    });
                }

                return CollectionPermisoPerfil;
            }

            catch (Exception)
            {
                throw;
            }
        }

    }
}
