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

    public class BSGR_CierreCaja_Detalle
    {
        /// <summary>
        /// Lista de Cierre Caja Detalle
        /// </summary>
        /// <returns>Colección de Cierre Caja Detalle</returns>
        public CmpObservableCollection<ESGR_CierreCaja_Detalle> GetCollectionCierreCajaDetalle()
        {
            try
            {
                var objCmpSql = new CmpSql(SGRVariables.ConectionString);
                var CollectionCierreCaja_Detalle = new CmpObservableCollection<ESGR_CierreCaja_Detalle>();

                objCmpSql.CommandProcedure("spSGR_GET_BusquedaGeneral");
                objCmpSql.AddParameter("@Parametro", SqlDbType.VarChar, "CierreCaja_Detalle");
                DataTable dt = objCmpSql.ExecuteDataTable();

                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    CollectionCierreCaja_Detalle.Add(new ESGR_CierreCaja_Detalle
                    {
                        Item = (dt.Rows[x]["Item"] != DBNull.Value) ? Convert.ToByte(dt.Rows[x]["Item"]) : Convert.ToByte(0),
                        TipoPago = (dt.Rows[x]["TipoPago"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["TipoPago"]) : string.Empty,
                        TipoTarjeta = (dt.Rows[x]["TipoTarjeta"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["TipoTarjeta"]) : string.Empty,
                        Total = (dt.Rows[x]["Total"] != DBNull.Value) ? Convert.ToByte(dt.Rows[x]["Total"]) : Convert.ToByte(0),
                        MndCodMnd = (dt.Rows[x]["MndCodMnd"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["MndCodMnd"]) : string.Empty,
                        ESGR_CierreCaja = new ESGR_CierreCaja()
                        {
                            IdCaja = (dt.Rows[x]["IdCaja"] != DBNull.Value) ? Convert.ToInt16(dt.Rows[x]["IdCaja"]) : Convert.ToInt16(0),
                        },
                    });
                }

                return CollectionCierreCaja_Detalle;
            }

            catch (Exception)
            {
                throw;
            }
        }

    }
}
