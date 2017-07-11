/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   OSCAR HUAMAN CABRERA
'* FCH. CREACIÓN : 30/11/2016
**********************************************************/
namespace SGR.Models.Business
{
    using ComputerSystems;
    using ComputerSystems.DataAccess.LibrarySql;
    using SGR.Models.Entity;
    using System;
    using System.Data;

    public class BSGR_TipoCambio
    {
        /// <summary>
        /// Insertar, Editar y Eliminar Tipo Cambio
        /// </summary>
        /// <param name="ESGR_TipoCambio">Objecto de la Entidad Tipo Cambio</param>
        public void TransTipoCambio(ESGR_TipoCambio ESGR_TipoCambio)
        {
            try
            {
                var objCmpSql = new CmpSql(SGRVariables.ConectionString);
                objCmpSql.CommandProcedure("spSGR_SET_TipoCambio");
                objCmpSql.AddParameter("@Opcion", SqlDbType.VarChar, ESGR_TipoCambio.Opcion);
                objCmpSql.AddParameter("@FechaTcb", SqlDbType.DateTime, ESGR_TipoCambio.FechaTcb);
                objCmpSql.AddParameter("@CodMoneda", SqlDbType.Char, ESGR_TipoCambio.ESGR_Moneda.CodMoneda);
                objCmpSql.AddParameter("@BuyRate", SqlDbType.Decimal, ESGR_TipoCambio.BuyRate);
                objCmpSql.AddParameter("@SelRate", SqlDbType.Decimal, ESGR_TipoCambio.SelRate);
                objCmpSql.AddParameter("@IdUsuario", SqlDbType.Int, 1);
                objCmpSql.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Lista de Tipo Cambio
        /// </summary>
        /// <returns>Colección de los Tipo Cambio</returns>
        public CmpObservableCollection<ESGR_TipoCambio> GetCollectionTipoCambio()
        {
            try
            {
                var objCmpSql = new CmpSql(SGRVariables.ConectionString);
                var CollectionTipoCambio = new CmpObservableCollection<ESGR_TipoCambio>();

                objCmpSql.CommandProcedure("spSGR_GET_BusquedaGeneral");
                objCmpSql.AddParameter("@Opcion", SqlDbType.VarChar, "GetTipoCambio");
                objCmpSql.AddParameter("@Filtro", SqlDbType.VarChar, "");
                objCmpSql.AddParameter("@ParameterId", SqlDbType.VarChar, "");  
                DataTable dt = objCmpSql.ExecuteDataTable();

                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    CollectionTipoCambio.Add(new ESGR_TipoCambio
                    {
                        ESGR_Moneda = new ESGR_Moneda()
                        {
                            CodMoneda = (dt.Rows[x]["CodMoneda"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["CodMoneda"]) : string.Empty,
                        },
                        SelRate = (dt.Rows[x]["SelRate"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[x]["SelRate"]) : Convert.ToDecimal(0),
                        BuyRate = (dt.Rows[x]["BuyRate"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[x]["BuyRate"]) : Convert.ToDecimal(0),
                        FechaTcb = (dt.Rows[x]["FechaTcb"] != DBNull.Value) ? Convert.ToDateTime(dt.Rows[x]["FechaTcb"]) : DateTime.Now,
                    });
                }

                return CollectionTipoCambio;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
