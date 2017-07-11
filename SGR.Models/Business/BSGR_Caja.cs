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
    public class BSGR_Caja
    {
        CmpSql ObjCmpSql = new CmpSql(SGRVariables.ConectionString);
        public void MethodTransaccionCaja(ESGR_Caja ESGR_Caja)
        {
            try
            {
                ObjCmpSql.CommandProcedure("spSGR_SET_Caja");
                ObjCmpSql.AddParameter("@Opcion", SqlDbType.VarChar, ESGR_Caja.Opcion);
                ObjCmpSql.AddParameter("@IdCaja", SqlDbType.Int, ESGR_Caja.IdCaja);
                ObjCmpSql.AddParameter("@IdEmpSucursal", SqlDbType.SmallInt, ESGR_Caja.ESGR_UsuarioEmpresaSucursal.ESGR_EmpresaSucursal.IdEmpSucursal);
                ObjCmpSql.AddParameter("@Descripcion", SqlDbType.VarChar, ESGR_Caja.Descripcion);
                ObjCmpSql.ExecuteNonQuery();
            }
            catch(Exception)
            {
                throw;
            }
        }

        public CmpObservableCollection<ESGR_Caja> CollectionESGR_Caja(string Filtro = "%")
        {
            try
            {
                var CollectionESGR_Caja = new CmpObservableCollection<ESGR_Caja>();
                ObjCmpSql.CommandProcedure("spSGR_GET_BusquedaGeneral");
                ObjCmpSql.AddParameter("@Opcion", SqlDbType.VarChar, "GetCaja");
                ObjCmpSql.AddParameter("@Filtro", SqlDbType.VarChar, (Filtro == null || Filtro.Trim().Length == 0) ? "%" : Filtro);
                ObjCmpSql.AddParameter("@ParameterId", SqlDbType.Int, SGRVariables.ESGR_Usuario.IdUsuario);
                DataTable dt = ObjCmpSql.ExecuteDataTable();
                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    CollectionESGR_Caja.Add(new ESGR_Caja()
                    {
                        IdCaja = (dt.Rows[x]["IdCaja"] != DBNull.Value) ? Convert.ToInt32(dt.Rows[x]["IdCaja"]) : 0,
                        Descripcion = (dt.Rows[x]["Descripcion"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Descripcion"]) : string.Empty,
                        ESGR_Estado = new ESGR_Estado()
                        {
                            CodEstado = (dt.Rows[x]["CodEstado"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["CodEstado"]) : string.Empty,
                            Estado = (dt.Rows[x]["Estado"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Estado"]) : string.Empty,
                        },
                        ESGR_UsuarioEmpresaSucursal = new ESGR_UsuarioEmpresaSucursal()
                        {
                            ESGR_EmpresaSucursal = new ESGR_EmpresaSucursal()
                            {
                                IdEmpSucursal = (dt.Rows[x]["IdEmpSucursal"] != DBNull.Value) ? (short)Convert.ToInt32(dt.Rows[x]["IdEmpSucursal"]) : (short)0,
                                Sucursal = (dt.Rows[x]["Sucursal"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Sucursal"]) : string.Empty
                            }
                        },
                        FlgEliminado = (dt.Rows[x]["FlgEliminado"] != DBNull.Value) ? Convert.ToBoolean(dt.Rows[x]["FlgEliminado"]) : false
                    });
                }
                return CollectionESGR_Caja;
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}
