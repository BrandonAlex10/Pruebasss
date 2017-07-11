/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   OSCAR HUAMAN CABRERA
'* FCH. CREACIÓN : 30/11/2016
'********************************************************
'* FCH. MODIFICACION : 10/12/2016 [CRISTIAN HERNANDEZ VILLO]
'* MOTIVO: Se modifico el filtrado de CATEGORIA
**********************************************************/
namespace SGR.Models.Business
{
    using ComputerSystems;
    using ComputerSystems.DataAccess.LibrarySql;
    using SGR.Models.Entity;
    using System;
    using System.Collections.ObjectModel;
    using System.Data;

    public class BSGR_ProductoCategoria
    {
        /// <summary>
        /// Insertar, Editar y Eliminar Producto Categoria
        /// </summary>
        /// <param name="ESGR_ProductoCategoria">Objecto de la Entidad Producto Categoria</param>
        public void TransProductoCategoria(ESGR_ProductoCategoria ESGR_ProductoCategoria)
        {
            try
            {
                var objCmpSql = new CmpSql(SGRVariables.ConectionString);
                objCmpSql.CommandProcedure("spSGR_SET_ProductoCategoria");
                objCmpSql.AddParameter("@Opcion", SqlDbType.VarChar, ESGR_ProductoCategoria.Opcion);
                objCmpSql.AddParameter("@IdCategoria", SqlDbType.SmallInt, ESGR_ProductoCategoria.IdCategoria);
                objCmpSql.AddParameter("@Categoria", SqlDbType.VarChar, ESGR_ProductoCategoria.Categoria);
                objCmpSql.AddParameter("@Impresora", SqlDbType.VarChar, ESGR_ProductoCategoria.Impresora);
                objCmpSql.AddParameter("@ValidaStock", SqlDbType.Bit, ESGR_ProductoCategoria.ValidaStock);
                objCmpSql.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Lista de Producto Categoria
        /// </summary>
        /// <returns>Colección de los Producto Categoria</returns>
        public CmpObservableCollection<ESGR_ProductoCategoria> GetCollectionProductoCategoria(string Filtro = "%")
        {
            try
            {
                var objCmpSql = new CmpSql(SGRVariables.ConectionString);
                var CollectionProductoCategoria = new CmpObservableCollection<ESGR_ProductoCategoria>();

                objCmpSql.CommandProcedure("spSGR_GET_BusquedaGeneral");
                objCmpSql.AddParameter("@Opcion", SqlDbType.VarChar, "GetCategoria");
                objCmpSql.AddParameter("@Filtro", SqlDbType.VarChar, (Filtro == string.Empty || Filtro == null) ? "%" : Filtro);
                objCmpSql.AddParameter("@ParameterId", SqlDbType.Int, 0);
                DataTable dt = objCmpSql.ExecuteDataTable();

                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    CollectionProductoCategoria.Add(new ESGR_ProductoCategoria
                    {
                        IdCategoria = (dt.Rows[x]["IdCategoria"] != DBNull.Value) ? Convert.ToInt16(dt.Rows[x]["IdCategoria"]) : Convert.ToInt16(0),
                        Categoria = (dt.Rows[x]["Categoria"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Categoria"]) : string.Empty,
                        Impresora = (dt.Rows[x]["Impresora"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Impresora"]) : string.Empty,
                        ValidaStock = (dt.Rows[x]["ValidaStock"] != DBNull.Value) ? Convert.ToBoolean(dt.Rows[x]["ValidaStock"]) : false,
                    });
                }

                return CollectionProductoCategoria;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public CmpObservableCollection<ESGR_ProductoCategoria> GetCollectionProductoCategoriaCartaDia()
        {
            var objCmpSql = new CmpSql(SGRVariables.ConectionString);
            var CollectionProductoCategoriaCartaDia = new CmpObservableCollection<ESGR_ProductoCategoria>();

            objCmpSql.CommandProcedure("spSGR_GET_BusquedaGeneral");
            objCmpSql.AddParameter("@Opcion", SqlDbType.VarChar, "GetCartaDiaCatProducto");
            objCmpSql.AddParameter("@Filtro", SqlDbType.VarChar, "%");
            objCmpSql.AddParameter("@ParameterId", SqlDbType.Int, 0);
            DataTable dt = objCmpSql.ExecuteDataTable();

            for (int x = 0; x < dt.Rows.Count; x++)
            {
                CollectionProductoCategoriaCartaDia.Add(new ESGR_ProductoCategoria
                {
                    IdCategoria = (dt.Rows[x]["IdCategoria"] != DBNull.Value) ? Convert.ToInt16(dt.Rows[x]["IdCategoria"]) : Convert.ToInt16(0),
                    Categoria = (dt.Rows[x]["Categoria"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Categoria"]) : string.Empty,
                    Impresora = (dt.Rows[x]["Impresora"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Impresora"]) : string.Empty,
                    ValidaStock = (dt.Rows[x]["ValidaStock"] != DBNull.Value) ? Convert.ToBoolean(dt.Rows[x]["ValidaStock"]) : false,
                });
            }

            return CollectionProductoCategoriaCartaDia;
        }

    }
}
