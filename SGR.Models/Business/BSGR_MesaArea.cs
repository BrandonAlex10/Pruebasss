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

    public class BSGR_MesaArea
    {
        /// <summary>
        /// Insertar, Editar y Eliminar Mesa Area
        /// </summary>
        /// <param name="ESGR_MesaArea">Objecto de la Entidad Mesa Area</param>
        public void TransMesaArea(ESGR_MesaArea ESGR_MesaArea)
        {
            try
            {
                var objCmpSql = new CmpSql(SGRVariables.ConectionString);
                objCmpSql.CommandProcedure("spSGR_SET_MesaArea");
                objCmpSql.AddParameter("@Opcion",SqlDbType.VarChar,ESGR_MesaArea.Opcion);
                objCmpSql.AddParameter("@IdMesaArea", SqlDbType.SmallInt, ESGR_MesaArea.IdMesaArea);
                objCmpSql.AddParameter("@MesaArea", SqlDbType.VarChar, ESGR_MesaArea.MesaArea);
                objCmpSql.ExecuteNonQuery();
            }
            catch (Exception )
            {
               throw;
            }
        }

        /// <summary>
        /// Lista de Mesa Area
        /// </summary>
        /// <returns>Colección de Mesa Area</returns>
        public CmpObservableCollection<ESGR_MesaArea> GetCollectionMesaArea()
        {
            try
            {
                var objCmpSql = new CmpSql(SGRVariables.ConectionString);
                var CollectionMesaArea = new CmpObservableCollection<ESGR_MesaArea>();

                objCmpSql.CommandProcedure("spSGR_GET_BusquedaGeneral");
                objCmpSql.AddParameter("@Opcion", SqlDbType.VarChar, "GetMesaArea");
                objCmpSql.AddParameter("@Filtro", SqlDbType.VarChar, "%");
                objCmpSql.AddParameter("@ParameterId", SqlDbType.Int, 0);
                DataTable dt = objCmpSql.ExecuteDataTable();

                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    CollectionMesaArea.Add(new ESGR_MesaArea
                    {
                        IdMesaArea = (dt.Rows[x]["IdMesaArea"] != DBNull.Value) ? Convert.ToInt16(dt.Rows[x]["IdMesaArea"]) : Convert.ToInt16(0),
                        MesaArea = (dt.Rows[x]["MesaArea"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["MesaArea"]) : string.Empty,
                    });
                }

                return CollectionMesaArea;
            }

            catch (Exception)
            {
                throw;
            }
        }

    }
}
