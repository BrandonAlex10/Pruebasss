/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   OSCAR HUAMAN CABRERA
'* FCH. CREACIÓN : 30/11/2016
'*********************************************************
'* FCH. MODIFICACION : 10/12/2016 [CRISTIAN HERNANDEZ VILLO]
'* MOTIVO: Se modificaron los metodos TransProductos y GetCollectionProducto
**********************************************************/
namespace SGR.Models.Business
{
    using ComputerSystems;
    using ComputerSystems.DataAccess.LibrarySql;
    using SGR.Models.Entity;
    using System;
    using System.Collections.ObjectModel;
    using System.Data;
    using System.IO;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;

    public class BSGR_Producto
    {
        /// <summary>
        /// Insertar, Editar y Eliminar Producto
        /// </summary>
        /// <param name="ESGR_Producto">Objecto de la Entidad Producto</param>
        public void TransProducto(ESGR_Producto ESGR_Producto)
        {
            try
            {
                var objCmpSql = new CmpSql(SGRVariables.ConectionString);
                objCmpSql.CommandProcedure("spSGR_SET_Producto");
                objCmpSql.AddParameter("@Opcion",SqlDbType.VarChar,ESGR_Producto.Opcion);
                objCmpSql.AddParameter("@IdProducto", SqlDbType.Int, ESGR_Producto.IdProducto);
                objCmpSql.AddParameter("@IdSubCategoria", SqlDbType.SmallInt, ESGR_Producto.ESGR_ProductoSubCategoria.IdSubCategoria);
                objCmpSql.AddParameter("@Producto", SqlDbType.VarChar, ESGR_Producto.Producto);
                objCmpSql.AddParameter("@Precio", SqlDbType.Decimal, ESGR_Producto.Precio);
                objCmpSql.AddParameter("@Imagen", SqlDbType.Image, ESGR_Producto.Imagen);
                objCmpSql.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Lista de Producto
        /// </summary>
        /// <returns>Colección de los Producto</returns>
        public CmpObservableCollection<ESGR_Producto> GetCollectionProducto(ESGR_ProductoSubCategoria ESGR_ProductoSubCategoria, string Filtro = "%", string Opcion = "CATEGORIAYSUB")
        {
            try
            {
                var objCmpSql = new CmpSql(SGRVariables.ConectionString);
                var CollectionProducto = new CmpObservableCollection<ESGR_Producto>();

                objCmpSql.CommandProcedure("spSGR_GET_Producto");
                objCmpSql.AddParameter("@Opcion", SqlDbType.VarChar, Opcion);
                objCmpSql.AddParameter("@Filtro", SqlDbType.VarChar, (Filtro == null || Filtro == string.Empty) ? "%" : Filtro);
                objCmpSql.AddParameter("@IdCategoria", SqlDbType.Int, ((ESGR_ProductoSubCategoria == null) ? 0 : ESGR_ProductoSubCategoria.ESGR_ProductoCategoria.IdCategoria));
                objCmpSql.AddParameter("@IdSubCategoria", SqlDbType.Int, ((ESGR_ProductoSubCategoria == null) ? 0 : ESGR_ProductoSubCategoria.IdSubCategoria));
                DataTable dt = objCmpSql.ExecuteDataTable();

                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    CollectionProducto.Add(new ESGR_Producto
                    {
                        IdProducto = (dt.Rows[x]["IdProducto"] != DBNull.Value) ? Convert.ToInt16(dt.Rows[x]["IdProducto"]) : Convert.ToInt16(0),
                        Precio = (dt.Rows[x]["Precio"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[x]["Precio"]) : Convert.ToDecimal(0),
                        Producto = (dt.Rows[x]["Producto"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Producto"]) : string.Empty,
                        Imagen = (dt.Rows[x]["Imagen"] != DBNull.Value) ? (Byte[])(dt.Rows[x]["Imagen"]) : new byte[] { },
                        ESGR_ProductoSubCategoria = new ESGR_ProductoSubCategoria()
                        {
                            IdSubCategoria = (dt.Rows[x]["IdSubCategoria"] != DBNull.Value) ? Convert.ToInt16(dt.Rows[x]["IdSubCategoria"]) : Convert.ToInt16(0),
                            SubCategoria = (dt.Rows[x]["SubCategoria"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["SubCategoria"]) : string.Empty,
                            ESGR_ProductoCategoria = new ESGR_ProductoCategoria()
                            {
                                Categoria = (dt.Rows[x]["Categoria"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Categoria"]) : string.Empty,
                                IdCategoria = (dt.Rows[x]["IdCategoria"] != DBNull.Value) ? Convert.ToInt32(dt.Rows[x]["IdCategoria"]) : 0,
                                ValidaStock = (dt.Rows[x]["ValidaStock"] != DBNull.Value) ? Convert.ToBoolean(dt.Rows[x]["ValidaStock"]) : false,
                            }
                        },
                        ImageSource = (dt.Rows[x]["Imagen"] != DBNull.Value)  ? ToImage((Byte[])(dt.Rows[x]["Imagen"])) :  null
                    });
                }

                return CollectionProducto;
            }

            catch (Exception)
            {
                throw;
            }
        }

        public CmpObservableCollection<ESGR_Producto> GetCollectionProductoCartaDia(ESGR_ProductoSubCategoria ESGR_ProductoSubCategoria, string Filtro = "%")
        {
            try
            {
                var objCmpSql = new CmpSql(SGRVariables.ConectionString);
                var CollectionProductoCartaDia = new CmpObservableCollection<ESGR_Producto>();

                objCmpSql.CommandProcedure("spSGR_GET_CartaDiaProducto");
                objCmpSql.AddParameter("@Filtro", SqlDbType.VarChar, (Filtro == null || Filtro == string.Empty) ? "%" : Filtro);
                objCmpSql.AddParameter("@IdCategoria", SqlDbType.Int, ESGR_ProductoSubCategoria.ESGR_ProductoCategoria.IdCategoria);
                objCmpSql.AddParameter("@IdSubCategoria", SqlDbType.Int, ESGR_ProductoSubCategoria.IdSubCategoria);
                DataTable dt = objCmpSql.ExecuteDataTable();

                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    CollectionProductoCartaDia.Add(new ESGR_Producto
                    {
                        IdProducto = (dt.Rows[x]["IdProducto"] != DBNull.Value) ? Convert.ToInt16(dt.Rows[x]["IdProducto"]) : Convert.ToInt16(0),
                        Precio = (dt.Rows[x]["Precio"] != DBNull.Value) ? Convert.ToDecimal(dt.Rows[x]["Precio"]) : Convert.ToDecimal(0),
                        Producto = (dt.Rows[x]["Producto"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Producto"]) : string.Empty,
                        Imagen = (dt.Rows[x]["Imagen"] != DBNull.Value) ? (Byte[])(dt.Rows[x]["Imagen"]) : new byte[] { },
                        ESGR_ProductoSubCategoria = new ESGR_ProductoSubCategoria()
                        {
                            IdSubCategoria = (dt.Rows[x]["IdSubCategoria"] != DBNull.Value) ? Convert.ToInt16(dt.Rows[x]["IdSubCategoria"]) : Convert.ToInt16(0),
                            SubCategoria = (dt.Rows[x]["SubCategoria"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["SubCategoria"]) : string.Empty,
                            ESGR_ProductoCategoria = new ESGR_ProductoCategoria()
                            {
                                Categoria = (dt.Rows[x]["Categoria"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Categoria"]) : string.Empty,
                                IdCategoria = (dt.Rows[x]["IdCategoria"] != DBNull.Value) ? Convert.ToInt32(dt.Rows[x]["IdCategoria"]) : 0,
                                ValidaStock = (dt.Rows[x]["ValidaStock"] != DBNull.Value) ? Convert.ToBoolean(dt.Rows[x]["ValidaStock"]) : false,
                                Impresora = (dt.Rows[x]["Impresora"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Impresora"]) : string.Empty
                            }
                        },
                        Stock = (dt.Rows[x]["Stock"] != DBNull.Value) ? Convert.ToInt16(dt.Rows[x]["Stock"]) : Convert.ToInt16(0),
                        ImageSource = (dt.Rows[x]["Imagen"] != DBNull.Value) ? ToImage((Byte[])(dt.Rows[x]["Imagen"])) : null
                    });
                }

                return CollectionProductoCartaDia;
            }

            catch (Exception)
            {
                throw;
            }
        }

        private BitmapImage ToImage(byte[] array)
        {
            try
            {
                using (var ms = new System.IO.MemoryStream(array))
                {
                    var image = new BitmapImage();
                    image.BeginInit();
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.StreamSource = ms;
                    image.EndInit();
                    image.Freeze();
                    return image;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

    }
}
