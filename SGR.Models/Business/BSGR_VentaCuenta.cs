/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   OSCAR HUAMAN CABRERA
'* FCH. CREACIÓN : 18/04/2017
**********************************************************/

namespace SGR.Models.Business
{
    using ComputerSystems;
    using ComputerSystems.DataAccess.LibrarySql;
    using SGR.Models.Entity;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class BSGR_VentaCuenta
    {
        /// <summary>
        /// Lista de Venta
        /// </summary>
        /// <returns>Colección de las Ventas Cuentas</returns>
        public CmpObservableCollection<ESGR_VentaCuenta> GetCollectionESGR_VentaCuenta(int ParameterId, string Filtro = "%")
        {
            try
            {
                var objCmpSql = new CmpSql(SGRVariables.ConectionString);
                var CollectionVentaCuenta = new CmpObservableCollection<ESGR_VentaCuenta>();

                objCmpSql.CommandProcedure("spSGR_GET_BusquedaGeneral");
                objCmpSql.AddParameter("@Opcion", SqlDbType.VarChar, "GetVentaCuenta");
                objCmpSql.AddParameter("@Filtro", SqlDbType.VarChar, (Filtro != null || Filtro.Trim().Length != 0) ? Filtro : "%");
                objCmpSql.AddParameter("@ParameterId", SqlDbType.Int, ParameterId);
                DataTable dt = objCmpSql.ExecuteDataTable();

                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    CollectionVentaCuenta.Add(new ESGR_VentaCuenta
                    {
                        IdCuenta = (dt.Rows[x]["IdCuenta"] != DBNull.Value) ? Convert.ToInt16(dt.Rows[x]["IdCuenta"]) : Convert.ToInt16(0),
                        ESGR_Venta = new ESGR_Venta()
                        {
                            IdVenta = (dt.Rows[x]["IdVenta"] != DBNull.Value) ? Convert.ToInt16(dt.Rows[x]["IdVenta"]) : Convert.ToInt16(0),
                            ESGR_Caja = new ESGR_Caja()
                            {
                                IdCaja = (dt.Rows[x]["IdCaja"] != DBNull.Value) ? Convert.ToInt16(dt.Rows[x]["IdCaja"]) : Convert.ToInt16(0),
                            }
                        },
                        ESGR_Cliente = new ESGR_Cliente()
                        {
                            IdCliente = (dt.Rows[x]["IdCliente"] != DBNull.Value) ? Convert.ToInt16(dt.Rows[x]["IdCliente"]) : Convert.ToInt16(0),
                            Cliente = (dt.Rows[x]["Cliente"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Cliente"]) : string.Empty,
                            NroDocIdentidad = (dt.Rows[x]["NroDocIdentidad"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["NroDocIdentidad"]) : string.Empty,
                        },
                        ESGR_MedioPago = new ESGR_MedioPago()
                        {
                            IdMedioPago = (dt.Rows[x]["IdMedioPago"] != DBNull.Value) ? Convert.ToInt16(dt.Rows[x]["IdMedioPago"]) : Convert.ToInt16(0),
                        },
                        ESGR_Documento = new ESGR_Documento()
                        {
                            CodDocumento = (dt.Rows[x]["CodDocumento"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["CodDocumento"]) : string.Empty,
                            Descripcion = (dt.Rows[x]["Descripcion"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Descripcion"]) : string.Empty,
                        },
                        ESGR_Moneda = new ESGR_Moneda()
                        {
                            CodMoneda = (dt.Rows[x]["CodMoneda"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["CodMoneda"]) : string.Empty,
                            Descripcion = (dt.Rows[x]["Descripcion"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Descripcion"]) : string.Empty,
                            Defecto = (dt.Rows[x]["Defecto"] != DBNull.Value) ? Convert.ToBoolean(dt.Rows[x]["Defecto"]) : false,
                        },
                        Serie = (dt.Rows[x]["Serie"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Serie"]) : string.Empty,
                        Numero = (dt.Rows[x]["Numero"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Numero"]) : string.Empty,
                        Fecha = (dt.Rows[x]["Fecha"] != DBNull.Value) ? Convert.ToDateTime(dt.Rows[x]["Fecha"]) : DateTime.Now,
                        TipoCambio = (dt.Rows[x]["TipoCambio"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[x]["TipoCambio"]) : 0,
                        Gravada = (dt.Rows[x]["Gravada"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[x]["Gravada"]) : 0,
                        IGV = (dt.Rows[x]["IGV"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[x]["IGV"]) : 0,
                        ImporteIGV = (dt.Rows[x]["ImporteIGV"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[x]["ImporteIGV"]) : 0,
                        Adicional = (dt.Rows[x]["Adicional"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[x]["Adicional"]) : 0,
                        Descuento = (dt.Rows[x]["Descuento"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[x]["Descuento"]) : 0,
                        ImporteDscto = (dt.Rows[x]["ImporteDscto"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[x]["ImporteDscto"]) : 0,
                    });
                }

                return CollectionVentaCuenta;
            }

            catch (Exception)
            {
                throw;
            }
        }

        public CmpObservableCollection<ESGR_VentaCuenta> GetReportVentaMedioPago(int ParameterId)
        {
            try
            {
                var objCmpSql = new CmpSql(SGRVariables.ConectionString);
                var CollectionVentaMedioPago = new CmpObservableCollection<ESGR_VentaCuenta>();

                objCmpSql.CommandProcedure("spSGR_GET_BusquedaGeneral");
                objCmpSql.AddParameter("@Opcion", SqlDbType.VarChar, "GetVentaMedioPago");
                objCmpSql.AddParameter("@Filtro", SqlDbType.VarChar, "%");
                objCmpSql.AddParameter("@ParameterId", SqlDbType.VarChar, ParameterId);
                DataTable dt = objCmpSql.ExecuteDataTable();

                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    CollectionVentaMedioPago.Add(new ESGR_VentaCuenta()
                    {
                        ESGR_MedioPago = new ESGR_MedioPago()
                        {
                            IdMedioPago = (dt.Rows[x]["IdMedioPago"] != DBNull.Value) ? (short)Convert.ToInt32(dt.Rows[x]["IdMedioPago"]) : (short)0,
                            MedioPago = (dt.Rows[x]["MedioPago"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["MedioPago"]) : string.Empty
                        },
                        ESGR_Moneda = new ESGR_Moneda()
                        {
                            Simbolo = (dt.Rows[x]["Simbolo"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Simbolo"]) : string.Empty
                        },
                        Total = (dt.Rows[x]["Total"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[x]["Total"]) : 0m
                    });
                }

                return CollectionVentaMedioPago;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public CmpObservableCollection<ESGR_VentaCuenta> GetReportVentaPedidoTipo(int ParameterId)
        {
            try
            {
                var objCmpSql = new CmpSql(SGRVariables.ConectionString);
                var CollectionVentaMedioPago = new CmpObservableCollection<ESGR_VentaCuenta>();

                objCmpSql.CommandProcedure("spSGR_GET_BusquedaGeneral");
                objCmpSql.AddParameter("@Opcion", SqlDbType.VarChar, "GetVentaPedidoTipo");
                objCmpSql.AddParameter("@Filtro", SqlDbType.VarChar, "%");
                objCmpSql.AddParameter("@ParameterId", SqlDbType.VarChar, ParameterId);
                DataTable dt = objCmpSql.ExecuteDataTable();

                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    CollectionVentaMedioPago.Add(new ESGR_VentaCuenta()
                    {
                        ESGR_Venta = new ESGR_Venta()
                        {
                            ESGR_Pedido = new ESGR_Pedido()
                            {
                                ESGR_PedidoTipo = new ESGR_PedidoTipo()
                                {
                                    IdPedidoTipo = (dt.Rows[x]["IdPedidoTipo"] != DBNull.Value) ? (short)Convert.ToInt32(dt.Rows[x]["IdPedidoTipo"]) : (short)0,
                                    PedidoTipo = (dt.Rows[x]["PedidoTipo"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["PedidoTipo"]) : string.Empty
                                }
                            }
                        },
                        ESGR_Moneda = new ESGR_Moneda()
                        {
                            Simbolo = (dt.Rows[x]["Simbolo"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Simbolo"]) : string.Empty
                        },
                        Total = (dt.Rows[x]["Total"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[x]["Total"]) : 0m
                    });
                }

                return CollectionVentaMedioPago;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public CmpObservableCollection<ESGR_VentaCuenta> GetReportVentaFormaPago(int ParameterId)
        {
            try
            {
                var objCmpSql = new CmpSql(SGRVariables.ConectionString);
                var CollectionVentaMedioPago = new CmpObservableCollection<ESGR_VentaCuenta>();

                objCmpSql.CommandProcedure("spSGR_GET_BusquedaGeneral");
                objCmpSql.AddParameter("@Opcion", SqlDbType.VarChar, "GetVentaFormaPago");
                objCmpSql.AddParameter("@Filtro", SqlDbType.VarChar, "%");
                objCmpSql.AddParameter("@ParameterId", SqlDbType.VarChar, ParameterId);
                DataTable dt = objCmpSql.ExecuteDataTable();

                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    CollectionVentaMedioPago.Add(new ESGR_VentaCuenta()
                    {
                        ESGR_MedioPago = new ESGR_MedioPago()
                        {
                            ESGR_FormaPago = new ESGR_FormaPago()
                            {
                                IdFormaPago = (dt.Rows[x]["IdFormaPago"] != DBNull.Value) ? Convert.ToInt32(dt.Rows[x]["IdFormaPago"]) : 0,
                                FormaPago = (dt.Rows[x]["FormaPago"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["FormaPago"]) : string.Empty,
                            }
                        },
                        ESGR_Moneda = new ESGR_Moneda()
                        {
                            Simbolo = (dt.Rows[x]["Simbolo"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Simbolo"]) : string.Empty
                        },
                        Total = (dt.Rows[x]["Total"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[x]["Total"]) : 0m
                    });
                }

                return CollectionVentaMedioPago;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public decimal GetTotalAdicionalDescuentos(int ParameterId, string Opcion = "GetTotalDescuento")
        {
            try
            {
                var objCmpSql = new CmpSql(SGRVariables.ConectionString);
                var CollectionVentaMedioPago = new CmpObservableCollection<ESGR_VentaCuenta>();

                objCmpSql.CommandProcedure("spSGR_GET_BusquedaGeneral");
                objCmpSql.AddParameter("@Opcion", SqlDbType.VarChar, (Opcion.Trim().Length == 0) ? "GetTotalDescuento" : Opcion);
                objCmpSql.AddParameter("@Filtro", SqlDbType.VarChar, "%");
                objCmpSql.AddParameter("@ParameterId", SqlDbType.VarChar, ParameterId);
                var dml = objCmpSql.ExecutePrint();
                return Convert.ToDecimal(dml);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
