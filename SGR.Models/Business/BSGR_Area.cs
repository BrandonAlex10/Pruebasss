/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   ABEL QUISPE ORELLANA
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

    public class BSGR_Area
    {
        /// <summary>
        /// Insertar, Editar y Eliminar Area
        /// </summary>
        /// <param name="ESGR_Area">Objecto de la Entidad Area</param>
        public void TransArea(ESGR_Area ESGR_Area)
        {
            try
            {
                var objCmpSql = new CmpSql(SGRVariables.ConectionString);
                objCmpSql.CommandProcedure("spSGR_SET_Area");
                objCmpSql.AddParameter("@Opcion", SqlDbType.Char, ESGR_Area.Opcion);
                objCmpSql.AddParameter("@IdArea", SqlDbType.Int, ESGR_Area.IdArea);
                objCmpSql.AddParameter("@IdEmpresa", SqlDbType.SmallInt, 1);//ESGR_Area.SGR_Empresa.IdEmpresa);
                objCmpSql.AddParameter("@Area", SqlDbType.VarChar, ESGR_Area.Area);
                objCmpSql.AddParameter("@IdUsuario", SqlDbType.Int, 1);
                objCmpSql.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Lista de Area
        /// </summary>
        /// <returns>Colección de las areas</returns>
        public CmpObservableCollection<ESGR_Area> GetCollectionArea()
        {
            try
            {
                var objCmpSql = new CmpSql(SGRVariables.ConectionString);
                var CollectionArea = new CmpObservableCollection<ESGR_Area>();

                objCmpSql.CommandProcedure("spSGR_GET_BusquedaGeneral");
                objCmpSql.AddParameter("@Opcion", SqlDbType.VarChar, "GetArea");
                objCmpSql.AddParameter("@Filtro",SqlDbType.VarChar,"");
                objCmpSql.AddParameter("@ParameterId", SqlDbType.Int,1);
                DataTable dt = objCmpSql.ExecuteDataTable();

                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    CollectionArea.Add(new ESGR_Area
                    {
                        IdArea = (dt.Rows[x]["IdArea"] != DBNull.Value) ? Convert.ToInt16(dt.Rows[x]["IdArea"]) : Convert.ToInt16(0),
                        Area = (dt.Rows[x]["Area"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Area"]) : string.Empty,
                        ESGR_Empresa = new ESGR_Empresa()
                        {
                            IdEmpresa = (dt.Rows[x]["IdEmpresa"] != DBNull.Value) ? Convert.ToInt16(dt.Rows[x]["IdEmpresa"]) : Convert.ToInt16(0),
                            RazonSocial = (dt.Rows[x]["RazonSocial"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["RazonSocial"]) : string.Empty,
                        },
                    });
                }

                return CollectionArea;
            }

            catch (Exception)
            {
                throw;
            }
        }

    }
}
