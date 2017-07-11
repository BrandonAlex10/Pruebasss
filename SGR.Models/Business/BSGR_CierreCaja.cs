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

    public class BSGR_CierreCaja
    {
        /// <summary>
        /// Insertar, Editar y Eliminar Cierre Caja
        /// </summary>
        /// <param name="ESGR_CierreCaja">Objecto de la Entidad Cierre Caja</param>
        public void TransCierreCaja(ESGR_CierreCaja ESGR_CierreCaja)
        {
            try
            {
                var objCmpSql = new CmpSql(SGRVariables.ConectionString);
                objCmpSql.CommandProcedure("spSGR_SET_CierreCaja");
                objCmpSql.AddParameter("@IdCaja", SqlDbType.Int, ESGR_CierreCaja.IdCaja);
                objCmpSql.AddParameter("@FechaCierre", SqlDbType.Int, ESGR_CierreCaja.FechaCierre);
                objCmpSql.AddParameter("@IdUsuarioCierre", SqlDbType.Int, ESGR_CierreCaja.IdUsuarioCierre.Value);
                objCmpSql.AddParameter("@SaldoF_SOL", SqlDbType.Int, ESGR_CierreCaja.SaldoF_SOL);
                objCmpSql.AddParameter("@SaldoF_USD", SqlDbType.Int, ESGR_CierreCaja.SaldoF_USD);
                objCmpSql.AddParameter("@IdUsuarioSuper", SqlDbType.Int, ESGR_CierreCaja.IdUsuarioSuper.Value);
                objCmpSql.AddParameter("@Ajuste_SOL", SqlDbType.Int, ESGR_CierreCaja.Ajuste_SOL);
                objCmpSql.AddParameter("@Ajuste_USD", SqlDbType.Int, ESGR_CierreCaja.Ajuste_USD);
                objCmpSql.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Lista de Cierre Caja
        /// </summary>
        /// <returns>Colección de Cierre Caja</returns>
        public CmpObservableCollection<ESGR_CierreCaja> GetCollectionCierreCaja()
        {
            try
            {
                var objCmpSql = new CmpSql(SGRVariables.ConectionString);
                var CollectionCierreCaja = new CmpObservableCollection<ESGR_CierreCaja>();

                objCmpSql.CommandProcedure("spSGR_GET_CierreCaja");
                DataTable dt = objCmpSql.ExecuteDataTable();

                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    CollectionCierreCaja.Add(new ESGR_CierreCaja
                    {
                        IdCaja = (dt.Rows[x]["IdCaja"] != DBNull.Value) ? Convert.ToInt16(dt.Rows[x]["IdCaja"]) : Convert.ToInt16(0),
                        Ajuste_SOL = (dt.Rows[x]["Ajuste_SOL"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[x]["Ajuste_SOL"]) : Convert.ToDecimal(0),
                        Ajuste_USD = (dt.Rows[x]["Ajuste_USD"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[x]["Ajuste_USD"]) : Convert.ToDecimal(0),
                        SaldoF_SOL = (dt.Rows[x]["SaldoF_SOL"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[x]["SaldoF_SOL"]) : Convert.ToDecimal(0),
                        SaldoF_USD = (dt.Rows[x]["SaldoF_USD"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[x]["SaldoF_USD"]) : Convert.ToDecimal(0),
                        FechaCierre = (dt.Rows[x]["FechaCierre"] != DBNull.Value) ? Convert.ToDateTime(dt.Rows[x]["FechaCierre"]) : DateTime.Now,
                        IdUsuarioCierre = (dt.Rows[x]["IdUsuarioCierre"] != DBNull.Value) ? Convert.ToInt16(dt.Rows[x]["IdUsuarioCierre"]) : Convert.ToInt16(0),
                        IdUsuarioSuper = (dt.Rows[x]["IdUsuarioSuper"] != DBNull.Value) ? Convert.ToInt16(dt.Rows[x]["IdUsuarioSuper"]) : Convert.ToInt16(0),
                    });
                }

                return CollectionCierreCaja;
            }

            catch (Exception)
            {
                throw;
            }
        }

    }
}
