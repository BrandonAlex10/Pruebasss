/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   OSCAR HUAMAN CABRERA
'* FCH. CREACIÓN : 30/11/2016
'********************************************************
'* FCH. MODIFICACION : 10/12/2016 [CRISTIAN HERNANDEZ VILLO]
'* MOTIVO: Se modifico el filtrado de SUBCATEGORIA
**********************************************************/
namespace SGR.Models.Business
{
    using ComputerSystems;
    using ComputerSystems.DataAccess.LibrarySql;
    using SGR.Models.Entity;
    using System;
    using System.Collections.ObjectModel;
    using System.Data;

    public class BSGR_ProductoSubCategoria
    {
        /// <summary>
        /// Insertar, Editar y Eliminar Producto Sub Categoria
        /// </summary>
        /// <param name="ESGR_ProductoSubCategoria">Objecto de la Entidad Producto Sub Categoria</param>
        public void TransProductoSubCategoria(ESGR_ProductoSubCategoria ESGR_ProductoSubCategoria)
        {
            try
            {
                var objCmpSql = new CmpSql(SGRVariables.ConectionString);
                objCmpSql.CommandProcedure("spSGR_SET_ProductoSubCategoria");
                objCmpSql.AddParameter("@Opcion", SqlDbType.VarChar, ESGR_ProductoSubCategoria.Opcion);
                objCmpSql.AddParameter("@IdSubCategoria", SqlDbType.SmallInt, ESGR_ProductoSubCategoria.IdSubCategoria);
                objCmpSql.AddParameter("@IdCategoria", SqlDbType.SmallInt, ESGR_ProductoSubCategoria.ESGR_ProductoCategoria.IdCategoria);
                objCmpSql.AddParameter("@SubCategoria", SqlDbType.VarChar, ESGR_ProductoSubCategoria.SubCategoria);
                objCmpSql.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Lista de Producto Sub Categoria
        /// </summary>
        /// <returns>Colección de los Producto Sub Categoria</returns>
        public CmpObservableCollection<ESGR_ProductoSubCategoria> GetCollectionProductoSubCategoria(ESGR_ProductoCategoria ESGR_ProductoCategoria)
        {
            try
            {
                var objCmpSql = new CmpSql(SGRVariables.ConectionString);
                var CollectionProductoSubCategoria = new CmpObservableCollection<ESGR_ProductoSubCategoria>();

                objCmpSql.CommandProcedure("spSGR_GET_BusquedaGeneral");
                objCmpSql.AddParameter("@Opcion", SqlDbType.VarChar, "GetSubCategoria");
                objCmpSql.AddParameter("@Filtro", SqlDbType.VarChar, "%");
                objCmpSql.AddParameter("@ParameterId", SqlDbType.Int, ESGR_ProductoCategoria.IdCategoria);
                DataTable dt = objCmpSql.ExecuteDataTable();

                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    CollectionProductoSubCategoria.Add(new ESGR_ProductoSubCategoria
                    {
                        IdSubCategoria = (dt.Rows[x]["IdSubCategoria"] != DBNull.Value) ? Convert.ToInt16(dt.Rows[x]["IdSubCategoria"]) : Convert.ToInt16(0),
                        SubCategoria = (dt.Rows[x]["SubCategoria"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["SubCategoria"]) : string.Empty,
                        ESGR_ProductoCategoria = new ESGR_ProductoCategoria()
                        {
                            IdCategoria = (dt.Rows[x]["IdCategoria"] != DBNull.Value) ? Convert.ToInt16(dt.Rows[x]["IdCategoria"]) : Convert.ToInt16(0),
                        },
                    });
                }

                return CollectionProductoSubCategoria;
            }

            catch (Exception)
            {
                throw;
            }
        }

        public CmpObservableCollection<ESGR_ProductoSubCategoria> GetCollectionProductoSubCategoriaCartaDia(ESGR_ProductoCategoria ESGR_ProductoCategoria)
        {
            try
            {
                var objCmpSql = new CmpSql(SGRVariables.ConectionString);
                var CollectionProductoSubCategoriaCartaDia = new CmpObservableCollection<ESGR_ProductoSubCategoria>();

                objCmpSql.CommandProcedure("spSGR_GET_BusquedaGeneral");
                objCmpSql.AddParameter("@Opcion", SqlDbType.VarChar, "GetCartaDiaSubCatProducto");
                objCmpSql.AddParameter("@Filtro", SqlDbType.VarChar, "%");
                objCmpSql.AddParameter("@ParameterId", SqlDbType.Int, ESGR_ProductoCategoria.IdCategoria);
                DataTable dt = objCmpSql.ExecuteDataTable();

                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    CollectionProductoSubCategoriaCartaDia.Add(new ESGR_ProductoSubCategoria
                    {
                        IdSubCategoria = (dt.Rows[x]["IdSubCategoria"] != DBNull.Value) ? Convert.ToInt16(dt.Rows[x]["IdSubCategoria"]) : Convert.ToInt16(0),
                        SubCategoria = (dt.Rows[x]["SubCategoria"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["SubCategoria"]) : string.Empty,
                        ESGR_ProductoCategoria = new ESGR_ProductoCategoria()
                        {
                            IdCategoria = (dt.Rows[x]["IdCategoria"] != DBNull.Value) ? Convert.ToInt16(dt.Rows[x]["IdCategoria"]) : Convert.ToInt16(0),
                        },
                    });
                }

                return CollectionProductoSubCategoriaCartaDia;
            }

            catch (Exception)
            {
                throw;
            }
        }

    }
}
