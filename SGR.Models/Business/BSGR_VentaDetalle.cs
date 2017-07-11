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
    using System.Collections.ObjectModel;
    using System.Data;

    public class BSGR_VentaDetalle
    {
        /// <summary>
        /// Lista de Venta Detalle
        /// </summary>-
        /// <returns>Colección de las Venta Detalle</returns>
        public CmpObservableCollection<ESGR_VentaDetalle> GetCollectionVentaDetalle(int ParameterId, string Filtro = "%")
        {
            try
            {
                var objCmpSql = new CmpSql(SGRVariables.ConectionString);
                var CollectionVentaDetalle = new CmpObservableCollection<ESGR_VentaDetalle>();

                objCmpSql.CommandProcedure("spSGR_GET_BusquedaGeneral");
                objCmpSql.AddParameter("@Opcion", SqlDbType.VarChar, "GetVentaDetalle");
                objCmpSql.AddParameter("@Filtro", SqlDbType.VarChar, (Filtro != null || Filtro.Trim().Length == 0) ? Filtro : "%");
                objCmpSql.AddParameter("@ParameterId", SqlDbType.Int, ParameterId);
                DataTable dt = objCmpSql.ExecuteDataTable();

                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    CollectionVentaDetalle.Add(new ESGR_VentaDetalle
                    {
                        Item = (dt.Rows[x]["Item"] != DBNull.Value) ? Convert.ToInt16(dt.Rows[x]["Item"]) : Convert.ToInt16(0),
                        ESGR_VentaCuenta = new ESGR_VentaCuenta()
                        {
                            IdCuenta = (dt.Rows[x]["IdCuenta"] != DBNull.Value) ? Convert.ToInt16(dt.Rows[x]["IdCuenta"]) : Convert.ToInt16(0),
                            Adicional = (dt.Rows[x]["Adicional"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[x]["Adicional"]) : 0m,
                            Descuento = (dt.Rows[x]["Descuento"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[x]["Descuento"]) : 0m,
                            ESGR_Venta = new ESGR_Venta()
                            {
                                IdVenta = (dt.Rows[x]["IdVenta"] != DBNull.Value) ? Convert.ToInt16(dt.Rows[x]["IdVenta"]) : Convert.ToInt16(0),
                                Fecha = (dt.Rows[x]["Fecha"] != DBNull.Value) ? Convert.ToDateTime(dt.Rows[x]["Fecha"]) : DateTime.Now
                            },
                            ESGR_MedioPago = new ESGR_MedioPago()
                            {
                                IdMedioPago = (dt.Rows[x]["IdMedioPago"] != DBNull.Value) ? Convert.ToInt16(dt.Rows[x]["IdMedioPago"]) : Convert.ToInt16(0),
                            },
                            ESGR_Moneda = new ESGR_Moneda()
                            {
                                CodMoneda = (dt.Rows[x]["CodMoneda"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["CodMoneda"]) : string.Empty,
                            }
                        },
                        CantidadPagar = (dt.Rows[x]["Cantidad"] != DBNull.Value) ? Convert.ToInt16(dt.Rows[x]["Cantidad"]) : Convert.ToInt16(0),
                        Descuento = (dt.Rows[x]["Descuento"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[x]["Descuento"]) : Convert.ToDecimal(0),
                        Importe = (dt.Rows[x]["Importe"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[x]["Importe"]) : Convert.ToDecimal(0),
                        Precio = (dt.Rows[x]["Precio"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[x]["Precio"]) : Convert.ToDecimal(0),
                        ESGR_Producto = new ESGR_Producto()
                        {
                            IdProducto = (dt.Rows[x]["IdProducto"] != DBNull.Value) ? Convert.ToInt16(dt.Rows[x]["IdProducto"]) : Convert.ToInt16(0),
                            Producto = (dt.Rows[x]["Producto"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Producto"]) : string.Empty,
                            Precio = (dt.Rows[x]["Precio"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[x]["Precio"]) : Convert.ToDecimal(0)
                        },
                    });
                }

                return CollectionVentaDetalle;
            }

            catch (Exception)
            {
                throw;
            }
        }

        public CmpObservableCollection<ESGR_VentaDetalle> GetReportVentaCategoria(int ParameterId)
        {
            try
            {
                var objCmpSql = new CmpSql(SGRVariables.ConectionString);
                var CollectionVentaCategoria = new CmpObservableCollection<ESGR_VentaDetalle>();

                objCmpSql.CommandProcedure("spSGR_GET_BusquedaGeneral");
                objCmpSql.AddParameter("@Opcion", SqlDbType.VarChar, "GetVentaCategoria");
                objCmpSql.AddParameter("@Filtro", SqlDbType.VarChar, "%");
                objCmpSql.AddParameter("@ParameterId", SqlDbType.VarChar, ParameterId);
                DataTable dt = objCmpSql.ExecuteDataTable();

                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    CollectionVentaCategoria.Add(new ESGR_VentaDetalle()
                    {
                        ESGR_Producto = new ESGR_Producto()
                        {
                            ESGR_ProductoSubCategoria = new ESGR_ProductoSubCategoria()
                            {
                                ESGR_ProductoCategoria = new ESGR_ProductoCategoria()
                                {
                                    IdCategoria = (dt.Rows[x]["IdCategoria"] != DBNull.Value) ? Convert.ToInt32(dt.Rows[x]["IdCategoria"]) : 0,
                                    Categoria = (dt.Rows[x]["Categoria"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Categoria"]) : string.Empty
                                }
                            }
                        },
                        ESGR_VentaCuenta = new ESGR_VentaCuenta()
                        {
                            ESGR_Moneda = new ESGR_Moneda()
                            {
                                Simbolo = (dt.Rows[x]["Simbolo"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Simbolo"]) : string.Empty,
                            }
                        },
                        Importe = (dt.Rows[x]["Total"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[x]["Total"]) : 0m
                    });
                }

                return CollectionVentaCategoria;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
