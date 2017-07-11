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

    public class BSGR_CartaDia
    {
        /// <summary>
        /// Insertar, Editar y Eliminar Carta Dia
        /// </summary>
        /// <param name="ESGR_CartaDia">Objecto de la Entidad Carta Dia</param>
        public void TransCartaDia(ESGR_CartaDia ESGR_CartaDia)
        {
            try
            {
                var objCmpSql = new CmpSql(SGRVariables.ConectionString);
                objCmpSql.CommandProcedure("spSGR_SET_CartaDia");
                objCmpSql.AddParameter("@Opcion", SqlDbType.VarChar, ESGR_CartaDia.Opcion);
                objCmpSql.AddParameter("@IdCartaDia", SqlDbType.Int, ESGR_CartaDia.IdCartaDia);
                objCmpSql.AddParameter("@IdEmpSucursal", SqlDbType.SmallInt, ESGR_CartaDia.ESGR_EmpresaSucursal.IdEmpSucursal);
                objCmpSql.AddParameter("@Fecha", SqlDbType.SmallDateTime, ESGR_CartaDia.Fecha.Value.ToShortDateString());
                objCmpSql.AddParameter("@DetalleXML", SqlDbType.NText, ESGR_CartaDia.DetalleXML);
                objCmpSql.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Lista de Carta Dia
        /// </summary>
        /// <returns>Colección de las Cartas Dia</returns>
        public CmpObservableCollection<ESGR_CartaDia> CollectionCartaDia()
        {
            try
            {
                var objCmpSql = new CmpSql(SGRVariables.ConectionString);
                var CollectionCartaDia = new CmpObservableCollection<ESGR_CartaDia>();

                objCmpSql.CommandProcedure("spSGR_GET_BusquedaGeneral");
                objCmpSql.AddParameter("@Opcion", SqlDbType.VarChar, "FiltrarCartaDiaSucursal");
                objCmpSql.AddParameter("@Filtro", SqlDbType.VarChar, "");
                objCmpSql.AddParameter("@ParameterId", SqlDbType.Int, 0);
                DataTable dt = objCmpSql.ExecuteDataTable();

                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    CollectionCartaDia.Add(new ESGR_CartaDia
                    {
                        IdCartaDia = (dt.Rows[x]["IdCartaDia"] != DBNull.Value) ? Convert.ToInt16(dt.Rows[x]["IdCartaDia"]) : Convert.ToInt16(0),
                        Fecha = (dt.Rows[x]["Fecha"] != DBNull.Value) ? Convert.ToDateTime(dt.Rows[x]["Fecha"]) : DateTime.Now,
                        ESGR_EmpresaSucursal = new ESGR_EmpresaSucursal()
                        {
                            IdEmpSucursal = (dt.Rows[x]["IdEmpSucursal"] != DBNull.Value) ? Convert.ToInt16(dt.Rows[x]["IdEmpSucursal"]) : Convert.ToInt16(0),
                            Sucursal = (dt.Rows[x]["Sucursal"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Sucursal"]) : string.Empty
                        },
                    });
                }
                return CollectionCartaDia;
            }

            catch (Exception)
            {
                throw;
            }
        }

    }
}
