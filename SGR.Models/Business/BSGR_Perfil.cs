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

    public class BSGR_Perfil
    {
        /// <summary>
        /// Insertar, Editar y Eliminar Perfil
        /// </summary>
        /// <param name="ESGR_Perfil">Objecto de la Entidad Perfil</param>
        public void TransPerfil(ESGR_Perfil ESGR_Perfil)
        {
            try
            {
                var objCmpSql = new CmpSql(SGRVariables.ConectionString);
                objCmpSql.CommandProcedure("spSGR_SET_Perfil");
                objCmpSql.AddParameter("@Opcion", SqlDbType.VarChar, ESGR_Perfil.Opcion);
                objCmpSql.AddParameter("@IdPerfil", SqlDbType.Int, ESGR_Perfil.IdPerfil);
                objCmpSql.AddParameter("@NombrePerfil", SqlDbType.VarChar, ESGR_Perfil.NombrePerfil);
                objCmpSql.AddParameter("@Descripcion", SqlDbType.VarChar, ESGR_Perfil.Descripcion);
                objCmpSql.AddParameter("@IdModulo", SqlDbType.Int, ESGR_Perfil.ESGR_Modulo.IdModulo);
                objCmpSql.AddParameter("@CadenaXML", SqlDbType.NText, ESGR_Perfil.XMLPermisoPerfil);
                objCmpSql.AddParameter("@IdUsuario", SqlDbType.Int, SGRVariables.ESGR_Usuario.IdUsuario);
                objCmpSql.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Lista de Perfil
        /// </summary>
        /// <returns>Colección de los Perfiles</returns>
        public CmpObservableCollection<ESGR_Perfil> GetCollectionPerfil()
        {
            try
            {
                var objCmpSql = new CmpSql(SGRVariables.ConectionString);
                var CollectionPerfil = new CmpObservableCollection<ESGR_Perfil>();

                objCmpSql.CommandProcedure("spSGR_GET_BusquedaGeneral");
                objCmpSql.AddParameter("@Opcion", SqlDbType.VarChar, "GetPerfil");
                objCmpSql.AddParameter("@Filtro", SqlDbType.VarChar, "%");
                objCmpSql.AddParameter("@ParameterId", SqlDbType.Int, 0);
                DataTable dt = objCmpSql.ExecuteDataTable();

                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    CollectionPerfil.Add(new ESGR_Perfil
                    {
                        IdPerfil = (dt.Rows[x]["IdPerfil"] != DBNull.Value) ? Convert.ToInt16(dt.Rows[x]["IdPerfil"]) : Convert.ToInt16(0),
                        Descripcion = (dt.Rows[x]["Descripcion"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Descripcion"]) : string.Empty,
                        NombrePerfil = (dt.Rows[x]["NombrePerfil"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["NombrePerfil"]) : string.Empty,
                        ESGR_Modulo = new ESGR_Modulo()
                        {
                            IdModulo = (dt.Rows[x]["IdModulo"] != DBNull.Value) ? Convert.ToInt16(dt.Rows[x]["IdModulo"]) : Convert.ToInt16(0),
                        },
                    });
                }

                return CollectionPerfil;
            }

            catch (Exception)
            {
                throw;
            }
        }
    }
}
