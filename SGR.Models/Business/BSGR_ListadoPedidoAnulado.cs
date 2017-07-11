/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   CRISTIAN A. HERNANDEZ VILLO
'* FCH. CREACIÓN : 27/06/2017
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

    public class BSGR_ListadoPedidoAnulado
    {
        CmpSql ObjCmpSql = new CmpSql(SGRVariables.ConectionString);

        public CmpObservableCollection<ESGR_ListadoPedidoAnulado> CollectionESGR_ListadoPedidoAnulado(int IdCategoria, int IdSubCategoria, int IdProducto, DateTime FechaIni, DateTime FechaFin, int IdUsuario)
        {
            try
            {
                var CollectionESGR_ListadoPedidoAnulado = new CmpObservableCollection<ESGR_ListadoPedidoAnulado>();
                ObjCmpSql.CommandProcedure("spSGR_GET_ListaPedidoAnulado");
                ObjCmpSql.AddParameter("@IdCategoria", SqlDbType.Int, IdCategoria);
                ObjCmpSql.AddParameter("@IdSubCategoria", SqlDbType.Int, IdSubCategoria);
                ObjCmpSql.AddParameter("@IdProducto", SqlDbType.Int, IdProducto);
                ObjCmpSql.AddParameter("@FechaDesde", SqlDbType.SmallDateTime, FechaIni.ToShortDateString());
                ObjCmpSql.AddParameter("@FechaHasta", SqlDbType.SmallDateTime, FechaFin.ToShortDateString());
                ObjCmpSql.AddParameter("@Usuario", SqlDbType.Int,IdUsuario);
                DataTable dt = ObjCmpSql.ExecuteDataTable();
                for (var x = 0; x < dt.Rows.Count; x++)
                {
                    CollectionESGR_ListadoPedidoAnulado.Add(new ESGR_ListadoPedidoAnulado()
                    {
                        Fecha = (dt.Rows[x]["Fecha"]!= DBNull.Value)? Convert.ToDateTime(dt.Rows[x]["Fecha"]): DateTime.Now,
                        Mozo = (dt.Rows[x]["Mozo"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Mozo"]) : string.Empty,
                        Producto = (dt.Rows[x]["Producto"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Producto"]) : string.Empty,
                        Cantidad = (dt.Rows[x]["Cantidad"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[x]["Cantidad"]) : 0,
                        Precio = (dt.Rows[x]["Precio"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[x]["Precio"]) : 0,
                        Cubierto = (dt.Rows[x]["Cubierto"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[x]["Cubierto"]) : 0,
                        Mesa = (dt.Rows[x]["Mesa"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Mesa"]) : string.Empty,
                        Importe = (dt.Rows[x]["Importe"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[x]["Importe"]) : 0,
                    });
                }
                return CollectionESGR_ListadoPedidoAnulado;
            }
            catch(Exception )
            {
                throw;
            }
        }
    }
}
