/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   OSCAR HUAMAN CABRERA
'* FCH. CREACIÓN : 30/11/2016
**********************************************************/
namespace SGR.Models.Business
{
    using ComputerSystems.DataAccess.LibrarySql;
    using SGR.Models.Entity;
    using System;
    using System.Collections.ObjectModel;
    using System.Data;

    public class BSGR_UsuarioArea
    {
        /// <summary>
        /// Insertar, Editar y Eliminar Usuario Area
        /// </summary>
        /// <param name="ESGR_UsuarioArea">Objecto de la Entidad Usuario Area</param>
        public void TransUsuarioArea(ESGR_UsuarioArea ESGR_UsuarioArea)
        {
            try
            {
                var objCmpSql = new CmpSql(SGRVariables.ConectionString);
                objCmpSql.CommandProcedure("spSGR_SET_UsuarioArea");
                objCmpSql.AddParameter("@IdUsuario", SqlDbType.Int, ESGR_UsuarioArea.ESGR_Usuario.IdUsuario);
                objCmpSql.AddParameter("@IdArea", SqlDbType.SmallInt, ESGR_UsuarioArea.ESGR_Area.IdArea);
                objCmpSql.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Lista de Usuario Area
        /// </summary>
        /// <returns>Colección de los Usuario Area</returns>
        public ObservableCollection<ESGR_UsuarioArea> GetCollectionUsuarioArea()
        {
            try
            {
                var objCmpSql = new CmpSql(SGRVariables.ConectionString);
                var CollectionUsuarioArea = new ObservableCollection<ESGR_UsuarioArea>();

                objCmpSql.CommandProcedure("spSGR_GET_BusquedaGeneral");
                objCmpSql.AddParameter("@Opcion", SqlDbType.VarChar, "GetUsuarioArea");
                objCmpSql.AddParameter("@Filtro", SqlDbType.VarChar, "%");
                objCmpSql.AddParameter("@ParameterId", SqlDbType.Int, 0);
                DataTable dt = objCmpSql.ExecuteDataTable();

                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    CollectionUsuarioArea.Add(new ESGR_UsuarioArea
                    {
                        ESGR_Usuario = new ESGR_Usuario()
                        {
                            IdUsuario = (dt.Rows[x]["IdUsuario"] != DBNull.Value) ? Convert.ToInt16(dt.Rows[x]["IdUsuario"]) : Convert.ToInt16(0),
                        },
                        ESGR_Area = new ESGR_Area()
                        {
                            IdArea = (dt.Rows[x]["IdArea"] != DBNull.Value) ? Convert.ToInt16(dt.Rows[x]["IdArea"]) : Convert.ToInt16(0),
                            Area = (dt.Rows[x]["Area"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Area"]) : string.Empty,
                        },
                    });
                }

                return CollectionUsuarioArea;
            }

            catch (Exception)
            {
                throw;
            }
        }

    }
}
