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

    public class BSGR_CartaDiaDetalle
    {

        /// <summary>
        /// Lista de Carta Dia Detalle
        /// </summary>
        /// <returns>Colección de las Cartas Dia Detalle</returns>
        public CmpObservableCollection<ESGR_CartaDiaDetalle> GetCollectionCartaDiaDetalle(ESGR_CartaDia ESGR_CartaDia)
        {
            try
            {
                var objCmpSql = new CmpSql(SGRVariables.ConectionString);
                var CollectionCartaDiaDetalle = new CmpObservableCollection<ESGR_CartaDiaDetalle>();

                objCmpSql.CommandProcedure("spSGR_GET_BusquedaGeneral");
                objCmpSql.AddParameter("@Opcion", SqlDbType.VarChar, "FiltrarCartaDiaDetalle");
                objCmpSql.AddParameter("@Filtro", SqlDbType.VarChar, "");
                objCmpSql.AddParameter("@ParameterId", SqlDbType.Int, ESGR_CartaDia.IdCartaDia);
                DataTable dt = objCmpSql.ExecuteDataTable();

                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    CollectionCartaDiaDetalle.Add(new ESGR_CartaDiaDetalle
                    {
                        IdCartaDia = (dt.Rows[x]["IdCartaDia"] != DBNull.Value) ? Convert.ToInt32(dt.Rows[x]["IdCartaDia"]) : 0,
                        Item = (dt.Rows[x]["Item"] != DBNull.Value) ? Convert.ToInt16(dt.Rows[x]["Item"]) : Convert.ToInt16(0),
                        Cantidad = (dt.Rows[x]["Cantidad"] != DBNull.Value) ? Convert.ToInt16(dt.Rows[x]["Cantidad"]) : Convert.ToInt16(0),
                        TempCantidad = (dt.Rows[x]["Cantidad"] != DBNull.Value) ? Convert.ToInt16(dt.Rows[x]["Cantidad"]) : Convert.ToInt16(0),
                        Precio = (dt.Rows[x]["Precio"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[x]["Precio"]) : 0,
                        Stock = (dt.Rows[x]["Stock"] != DBNull.Value) ? Convert.ToInt16(dt.Rows[x]["Stock"]) : Convert.ToInt16(0),
                        TempStock = (dt.Rows[x]["Stock"] != DBNull.Value) ? Convert.ToInt16(dt.Rows[x]["Stock"]) : Convert.ToInt16(0),
                        Observacion = (dt.Rows[x]["Observacion"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Observacion"]) : string.Empty,
                        ESGR_CartaDia = new ESGR_CartaDia()
                        {
                            IdCartaDia = (dt.Rows[x]["IdCartaDia"] != DBNull.Value) ? Convert.ToInt16(dt.Rows[x]["IdCartaDia"]) : Convert.ToInt16(0),
                        },
                        ESGR_Producto = new ESGR_Producto()
                        {
                            IdProducto = (dt.Rows[x]["IdProducto"] != DBNull.Value) ? Convert.ToInt16(dt.Rows[x]["IdProducto"]) : Convert.ToInt16(0),
                            Producto = (dt.Rows[x]["Producto"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Producto"]) : string.Empty,
                            ESGR_ProductoSubCategoria = new ESGR_ProductoSubCategoria()
                            {
                                IdSubCategoria = (dt.Rows[x]["IdSubCategoria"] != DBNull.Value) ? Convert.ToInt32(dt.Rows[x]["IdSubCategoria"]) : 0,
                                SubCategoria = (dt.Rows[x]["SubCategoria"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["SubCategoria"]) : string.Empty,
                                ESGR_ProductoCategoria = new ESGR_ProductoCategoria()
                                {
                                    IdCategoria = (dt.Rows[x]["IdCategoria"] != DBNull.Value) ? Convert.ToInt32(dt.Rows[x]["IdCategoria"]) : 0,
                                    Categoria = (dt.Rows[x]["Categoria"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Categoria"]) : string.Empty,
                                }
                            }
                        },
                    });
                }
                return CollectionCartaDiaDetalle;
            }

            catch (Exception)
            {
                throw;
            }
        }

    }
}
