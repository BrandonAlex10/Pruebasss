/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   CRISTIAN A. HERNANDEZ VILLO
'* FCH. CREACIÓN : 20/04/2017
**********************************************************/
namespace SGR.Models.Business
{
    using System;
    using ComputerSystems;
    using ComputerSystems.DataAccess.LibrarySql;
    using System.Data;
    using SGR.Models.Entity;

    public class BSGR_ListadoVentaDia
    {
        public CmpObservableCollection<ESGR_ListadoVentaDia> CollectionESGR_ListadoVentaDia(int IdCategoria, int IdSubCategoria, int IdProducto, DateTime FechaIni, DateTime FechaFin, int IdUsuario)
        { 
            try
            {
                var ObjCmpSql = new CmpSql(SGRVariables.ConectionString);
                var CollectionESGR_ListadoVentaDia = new CmpObservableCollection<ESGR_ListadoVentaDia>();
                ObjCmpSql.CommandProcedure("spSGR_GET_ListaVentaPorFecha");
                ObjCmpSql.AddParameter("@IdCategoria",SqlDbType.Int,IdCategoria);
                ObjCmpSql.AddParameter("@IdSubCategoria", SqlDbType.Int, IdSubCategoria);
                ObjCmpSql.AddParameter("@IdProducto", SqlDbType.Int, IdProducto);
                ObjCmpSql.AddParameter("@FechaDesde", SqlDbType.SmallDateTime, FechaIni.ToShortDateString());
                ObjCmpSql.AddParameter("@FechaHasta", SqlDbType.SmallDateTime, FechaFin.ToShortDateString());
                ObjCmpSql.AddParameter("@Usuario", SqlDbType.Int,IdUsuario );
                DataTable dt = ObjCmpSql.ExecuteDataTable();
                for (int x = 0; x < dt.Rows.Count;x++ )
                {
                    CollectionESGR_ListadoVentaDia.Add(new ESGR_ListadoVentaDia()
                    {
                        Fecha = (dt.Rows[x]["Fecha"] != DBNull.Value) ? Convert.ToDateTime(dt.Rows[x]["Fecha"]) : DateTime.Now,
                        Producto = (dt.Rows[x]["Producto"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Producto"]) : string.Empty,
                        Cantidad = (dt.Rows[x]["Cantidad"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[x]["Cantidad"]) : 0,
                        Precio = (dt.Rows[x]["Precio"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[x]["Precio"]) : 0,
                        Descuento = (dt.Rows[x]["Descuento"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[x]["Descuento"]) : 0,
                        Importe = (dt.Rows[x]["Importe"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[x]["Importe"]) : 0,
                        Mozo = (dt.Rows[x]["Mozo"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Mozo"]) : string.Empty,
                    });
                }
                return CollectionESGR_ListadoVentaDia;
            }
            catch(Exception)
            {
                throw;
            }

        }
    }
}
