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
    public class BSGR_AperturaCierreCaja
    {
        CmpSql ObjCmpSql = new CmpSql(SGRVariables.ConectionString);

        public void MethodTransaccionAperturaCaja(ESGR_AperturaCierreCaja ESGR_AperturaCierreCaja)
        {
            try
            {
                ObjCmpSql.CommandProcedure("spSGR_SET_AperturaCierreCaja");
                ObjCmpSql.AddParameter("@Opcion", SqlDbType.Char, ESGR_AperturaCierreCaja.Opcion);
                ObjCmpSql.AddParameter("@IdAperturaCierreCaja", SqlDbType.SmallInt, ESGR_AperturaCierreCaja.IdAperturaCierreCaja);
                ObjCmpSql.AddParameter("@IdCaja", SqlDbType.SmallInt, ESGR_AperturaCierreCaja.ESGR_Caja.IdCaja);
                ObjCmpSql.AddParameter("@FechaApertura", SqlDbType.DateTime, ESGR_AperturaCierreCaja.FechaApertura);
                ObjCmpSql.AddParameter("@FechaCierre", SqlDbType.DateTime, (ESGR_AperturaCierreCaja.FechaCierre == new DateTime()) ? DateTime.Now : ESGR_AperturaCierreCaja.FechaCierre);
                ObjCmpSql.AddParameter("@CodMotivo", SqlDbType.Char, ESGR_AperturaCierreCaja.ESGR_Motivo.CodMotivo);
                ObjCmpSql.AddParameter("@CodEstado", SqlDbType.Char, ESGR_AperturaCierreCaja.ESGR_Estado.CodEstado);
                ObjCmpSql.AddParameter("@Glosa", SqlDbType.VarChar, ESGR_AperturaCierreCaja.Glosa);
                ObjCmpSql.AddParameter("@IdUsuarioCajero", SqlDbType.Int, ESGR_AperturaCierreCaja.ESGR_UsuarioCajero.IdUsuario);
                ObjCmpSql.AddParameter("@IdUsuarioApertura", SqlDbType.Int, ESGR_AperturaCierreCaja.ESGR_UsuarioApertura.IdUsuario);
                ObjCmpSql.AddParameter("@DetalleXML", SqlDbType.NText, ESGR_AperturaCierreCaja.DetalleXML);
                ObjCmpSql.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public CmpObservableCollection<ESGR_AperturaCierreCaja> CollectionESGR_AperturaCierreCaja()
        {
            try
            {
                var CollectionESGR_AperturaCierreCaja = new CmpObservableCollection<ESGR_AperturaCierreCaja>();
                ObjCmpSql.CommandProcedure("spSGR_GET_BusquedaGeneral");
                ObjCmpSql.AddParameter("@Opcion", SqlDbType.VarChar, "GetAperturaCierreCaja");
                ObjCmpSql.AddParameter("@Filtro", SqlDbType.VarChar, "%");
                ObjCmpSql.AddParameter("@ParameterId", SqlDbType.Int, SGRVariables.ESGR_Usuario.IdUsuario);
                DataTable dt = ObjCmpSql.ExecuteDataTable();
                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    CollectionESGR_AperturaCierreCaja.Add(new ESGR_AperturaCierreCaja()
                    {
                        IdAperturaCierreCaja = (dt.Rows[x]["IdAperturaCierreCaja"] != DBNull.Value) ? Convert.ToInt32(dt.Rows[x]["IdAperturaCierreCaja"]) : 0,
                        ESGR_Estado = new ESGR_Estado()
                        {
                            CodEstado = (dt.Rows[x]["CodEstado"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["CodEstado"]) : string.Empty,
                            Estado = (dt.Rows[x]["Estado"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Estado"]) : string.Empty
                        },
                        ESGR_Motivo = new ESGR_Motivo()
                        {
                            CodMotivo = (dt.Rows[x]["CodMotivo"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["CodMotivo"]) : string.Empty,
                            Motivo = (dt.Rows[x]["Motivo"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Motivo"]) : string.Empty
                        },
                        ESGR_UsuarioCajero = new ESGR_Usuario()
                        {
                            IdUsuario = (dt.Rows[x]["IdUsuarioCajero"] != DBNull.Value) ? Convert.ToInt32(dt.Rows[x]["IdUsuarioCajero"]) : 0,
                            Nombres = (dt.Rows[x]["Nombres"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Nombres"]) : string.Empty,
                            Apellidos = (dt.Rows[x]["Apellidos"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Apellidos"]) : string.Empty
                        },
                        ESGR_Caja = new ESGR_Caja()
                        {
                            IdCaja = (dt.Rows[x]["IdCaja"] != DBNull.Value) ? Convert.ToInt32(dt.Rows[x]["IdCaja"]) : 0,
                            Descripcion = (dt.Rows[x]["Descripcion"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Descripcion"]) : string.Empty,
                        },
                        FechaApertura = (dt.Rows[x]["FechaApertura"] != DBNull.Value) ? Convert.ToDateTime(dt.Rows[x]["FechaApertura"]) : DateTime.Now,
                        FechaCierre = (dt.Rows[x]["FechaCierre"] != DBNull.Value) ? Convert.ToDateTime(dt.Rows[x]["FechaCierre"]) : DateTime.Now,
                        Glosa = (dt.Rows[x]["Glosa"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Glosa"]) : string.Empty,
                    });
                }
                return CollectionESGR_AperturaCierreCaja;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public CmpObservableCollection<ESGR_AperturaCierreCajaDetalle> CollectionESGR_AperturaCierreCajaDetalle(ESGR_AperturaCierreCaja ESGR_AperturaCierreCaja)
        {
            try
            {
                var CollectionESGR_AperturaCierreCaja = new CmpObservableCollection<ESGR_AperturaCierreCajaDetalle>();
                ObjCmpSql.CommandProcedure("spSGR_GET_BusquedaGeneral");
                ObjCmpSql.AddParameter("@Opcion", SqlDbType.VarChar, "GetAperturaCierreCajaDetalle");
                ObjCmpSql.AddParameter("@Filtro", SqlDbType.VarChar, "%");
                ObjCmpSql.AddParameter("@ParameterId", SqlDbType.Int, ESGR_AperturaCierreCaja.IdAperturaCierreCaja);
                DataTable dt = ObjCmpSql.ExecuteDataTable();
                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    CollectionESGR_AperturaCierreCaja.Add(new ESGR_AperturaCierreCajaDetalle()
                    {
                        Item = (dt.Rows[x]["Item"] != DBNull.Value) ? Convert.ToInt32(dt.Rows[x]["Item"]) : 0,
                        ESGR_Moneda = new ESGR_Moneda()
                        {
                            CodMoneda = (dt.Rows[x]["CodMoneda"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["CodMoneda"]) : string.Empty,
                            Descripcion = (dt.Rows[x]["Descripcion"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Descripcion"]) : string.Empty
                        },
                        ESGR_MedioPago = new ESGR_MedioPago()
                        {
                            IdMedioPago = (dt.Rows[x]["IdMedioPago"] != DBNull.Value) ? (short)Convert.ToInt32(dt.Rows[x]["IdMedioPago"]) : (short)0,
                            MedioPago = (dt.Rows[x]["MedioPago"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["MedioPago"]) : string.Empty
                        },
                        Monto_Inicio = (dt.Rows[x]["Monto_Inicio"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[x]["Monto_Inicio"]) : 0,
                        Monto_Sistema = (dt.Rows[x]["Monto_Sistema"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[x]["Monto_Sistema"]) : 0,
                        Monto_Cierre = (dt.Rows[x]["Monto_Cierre"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[x]["Monto_Cierre"]) : 0,
                        DiferExceso = (dt.Rows[x]["DiferExceso"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[x]["DiferExceso"]) : 0
                    });
                }
                return CollectionESGR_AperturaCierreCaja;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
