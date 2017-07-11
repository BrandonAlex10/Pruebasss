using ComputerSystems;
using ComputerSystems.DataAccess.LibrarySql;
using SGR.Models.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGR.Models.Business
{
    public class BSGR_MovimientoCaja
    {
        CmpSql ObjCmpSql = new CmpSql(SGRVariables.ConectionString);
        public void MethodTransaction(ESGR_MovimientoCaja ESGR_MovimientoCaja)
        {
            try
            {
                ObjCmpSql.CommandProcedure("spSGR_SET_MovimientoCaja");
                ObjCmpSql.AddParameter("@Opcion", SqlDbType.VarChar, ESGR_MovimientoCaja.Opcion);
                ObjCmpSql.AddParameter("@IdMovimiento", SqlDbType.Int, ESGR_MovimientoCaja.IdMovimientoCaja);
                ObjCmpSql.AddParameter("@IdEmpSucursal", SqlDbType.Int, ESGR_MovimientoCaja.ESGR_EmpresaSucursal.IdEmpSucursal);
                ObjCmpSql.AddParameter("@IdUsuario", SqlDbType.Int, SGRVariables.ESGR_Usuario.IdUsuario);
                ObjCmpSql.AddParameter("@IdCaja", SqlDbType.SmallInt, ESGR_MovimientoCaja.ESGR_Caja.IdCaja);
                ObjCmpSql.AddParameter("@CodOperacion", SqlDbType.VarChar, ESGR_MovimientoCaja.CodOperacion);
                ObjCmpSql.AddParameter("@CodMotivo", SqlDbType.VarChar, ESGR_MovimientoCaja.ESGR_Motivo.CodMotivo);
                ObjCmpSql.AddParameter("@CodDocumento", SqlDbType.VarChar, ESGR_MovimientoCaja.ESGR_Documento.CodDocumento);
                ObjCmpSql.AddParameter("@CodMoneda", SqlDbType.VarChar, ESGR_MovimientoCaja.ESGR_Moneda.CodMoneda);
                ObjCmpSql.AddParameter("@Fecha", SqlDbType.SmallDateTime, ESGR_MovimientoCaja.Fecha);
                ObjCmpSql.AddParameter("@Serie", SqlDbType.VarChar, ESGR_MovimientoCaja.ESGR_Documento.Serie);
                ObjCmpSql.AddParameter("@Numero", SqlDbType.VarChar, ESGR_MovimientoCaja.ESGR_Documento.Correlativo);
                ObjCmpSql.AddParameter("@Glosa", SqlDbType.VarChar, ESGR_MovimientoCaja.Glosa);
                ObjCmpSql.AddParameter("@DetalleXML", SqlDbType.NText, ESGR_MovimientoCaja.DetalleXML);
                ObjCmpSql.ExecuteNonQuery();
            }
            catch(Exception)
            {
                throw;
            }
        }

        public CmpObservableCollection<ESGR_MovimientoCaja> CollectionESGR_MovimientoCaja(string CodMotivo, DateTime FechaIni, DateTime FechaFin, bool ValidaFecha, ESGR_Caja ESGR_Caja)
        {
            try
            {
                var CollectionESGR_MovimientoCaja = new CmpObservableCollection<ESGR_MovimientoCaja>();
                ObjCmpSql.CommandProcedure("spSGR_GET_MovimientoCaja");
                ObjCmpSql.AddParameter("@CodMotivo",SqlDbType.VarChar,CodMotivo);
                ObjCmpSql.AddParameter("@FechaIni", SqlDbType.SmallDateTime, FechaIni.ToShortDateString());
                ObjCmpSql.AddParameter("@FechaFin", SqlDbType.SmallDateTime, FechaFin.ToShortDateString());
                ObjCmpSql.AddParameter("@IdCaja", SqlDbType.SmallInt, (ESGR_Caja == null) ? 0 : ESGR_Caja.IdCaja);
                ObjCmpSql.AddParameter("@ValidaFecha", SqlDbType.Bit, ValidaFecha);
                DataTable dt = ObjCmpSql.ExecuteDataTable();
                for (int x = 0; x < dt.Rows.Count;x++ )
                {
                    CollectionESGR_MovimientoCaja.Add(new ESGR_MovimientoCaja()
                    {
                        IdMovimientoCaja = (dt.Rows[x]["IdMovimiento"] != DBNull.Value) ? Convert.ToInt32(dt.Rows[x]["IdMovimiento"]) : 0,
                        ESGR_Motivo = new ESGR_Motivo()
                        {
                            CodMotivo = (dt.Rows[x]["CodMotivo"]!= DBNull.Value)? Convert.ToString(dt.Rows[x]["CodMotivo"]):string.Empty,
                            Motivo = (dt.Rows[x]["Motivo"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Motivo"]) : string.Empty,
                        },
                        ESGR_EmpresaSucursal= new ESGR_EmpresaSucursal()
                        {
                            IdEmpSucursal = (dt.Rows[x]["IdEmpSucursal"]!= DBNull.Value) ? (short)(dt.Rows[x]["IdEmpSucursal"]):Convert.ToInt16(0),
                            Sucursal = (dt.Rows[x]["Sucursal"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Sucursal"]) : string.Empty,
                        },
                        ESGR_Moneda = new ESGR_Moneda()
                        {
                            CodMoneda = (dt.Rows[x]["CodMoneda"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["CodMoneda"]) : string.Empty
                        },
                        ESGR_Documento = new ESGR_Documento()
                        {
                            CodDocumento = (dt.Rows[x]["CodDocumento"]!= DBNull.Value)? Convert.ToString(dt.Rows[x]["CodDocumento"]):string.Empty,
                            Serie = (dt.Rows[x]["Serie"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Serie"]) : string.Empty,
                            Correlativo = (dt.Rows[x]["Correlativo"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Correlativo"]) : string.Empty,
                        },
                        ESGR_Estado = new ESGR_Estado()
                        {
                            CodEstado = (dt.Rows[x]["CodEstado"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["CodEstado"]) : string.Empty,
                            Estado = (dt.Rows[x]["Estado"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Estado"]) : string.Empty
                        },
                        ESGR_Caja = new ESGR_Caja()
                        {
                            IdCaja = (dt.Rows[x]["IdCaja"] != DBNull.Value) ? Convert.ToInt32(dt.Rows[x]["IdCaja"]) : 0,
                            Descripcion = (dt.Rows[x]["Descripcion"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Descripcion"]) : string.Empty
                        },
                        Fecha = (dt.Rows[x]["Fecha"] != DBNull.Value) ? Convert.ToDateTime(dt.Rows[x]["Fecha"]) : DateTime.Now,
                        CodOperacion = (dt.Rows[x]["CodOperacion"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["CodOperacion"]) : string.Empty,
                        Glosa= (dt.Rows[x]["Glosa"]!= DBNull.Value)? Convert.ToString(dt.Rows[x]["Glosa"]):string.Empty,
                    });
                }
                return CollectionESGR_MovimientoCaja;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public CmpObservableCollection<ESGR_MovimientoCajaDetalle> CollectionESGR_MovimientoCajaDetalle(ESGR_MovimientoCaja ESGR_MovimientoCaja)
        {
            try
            {
                var CollectionESGR_MovimientoCajaDetalle = new CmpObservableCollection<ESGR_MovimientoCajaDetalle>();
                ObjCmpSql.CommandProcedure("spSGR_GET_BusquedaGeneral");
                ObjCmpSql.AddParameter("@Opcion", SqlDbType.VarChar, "GetMovimientoCajaDetalle");
                ObjCmpSql.AddParameter("@Filtro", SqlDbType.VarChar, "%");
                ObjCmpSql.AddParameter("@ParameterId", SqlDbType.Int, ESGR_MovimientoCaja.IdMovimientoCaja);
                DataTable dt = ObjCmpSql.ExecuteDataTable();
                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    CollectionESGR_MovimientoCajaDetalle.Add(new ESGR_MovimientoCajaDetalle()
                    {
                        ConceptoDescripcion = (dt.Rows[x]["ConceptoDescripcion"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["ConceptoDescripcion"]) : string.Empty,
                        Monto = (dt.Rows[x]["Monto"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[x]["Monto"]) : Convert.ToDecimal(0)
                    });
                }
                return CollectionESGR_MovimientoCajaDetalle;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public CmpObservableCollection<ESGR_MovimientoCaja> GetReportMovimientoCaja(int ParameterId)
        {
            try
            {
                var objCmpSql = new CmpSql(SGRVariables.ConectionString);
                var CollectionVentaMedioPago = new CmpObservableCollection<ESGR_MovimientoCaja>();

                objCmpSql.CommandProcedure("spSGR_GET_BusquedaGeneral");
                objCmpSql.AddParameter("@Opcion", SqlDbType.VarChar, "GetMovimientoCaja");
                objCmpSql.AddParameter("@Filtro", SqlDbType.VarChar, "%");
                objCmpSql.AddParameter("@ParameterId", SqlDbType.VarChar, ParameterId);
                DataTable dt = objCmpSql.ExecuteDataTable();

                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    CollectionVentaMedioPago.Add(new ESGR_MovimientoCaja()
                    {
                        ESGR_Motivo = new ESGR_Motivo()
                        {
                            Campo = (dt.Rows[x]["CodOperacion"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["CodOperacion"]) : string.Empty,
                            CodMotivo = (dt.Rows[x]["CodMotivo"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["CodMotivo"]) : string.Empty,
                            Motivo = (dt.Rows[x]["Motivo"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Motivo"]) : string.Empty
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
    }
}
