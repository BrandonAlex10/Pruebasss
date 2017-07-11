/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   OSCAR HUAMAN CABRERA
'* FCH. CREACIÓN : 29/11/2016
**********************************************************/
namespace SGR.Models.Business
{
    using ComputerSystems;
    using ComputerSystems.DataAccess.LibrarySql;
    using SGR.Models.Entity;
    using System;
    using System.Collections.ObjectModel;
    using System.Data;

    public class BSGR_Documento
    {
        /// <summary>
        /// Insertar, Editar y Eliminar Documento
        /// </summary>
        /// <param name="ESGR_Documento">Objecto de la Entidad Documento</param>
        public void TransDocumento(ESGR_Documento ESGR_Documento)
        {
            try
            {
                var objCmpSql = new CmpSql(SGRVariables.ConectionString);
                objCmpSql.CommandProcedure("spSGR_SET_Documento");
                objCmpSql.AddParameter("@Opcion", SqlDbType.VarChar, ESGR_Documento.Opcion);
                objCmpSql.AddParameter("@IdEmpSucursal", SqlDbType.SmallInt, ESGR_Documento.ESGR_EmpresaSucursal.IdEmpSucursal);
                objCmpSql.AddParameter("@CodDocumento", SqlDbType.Char, ESGR_Documento.CodDocumento);
                objCmpSql.AddParameter("@Serie", SqlDbType.Char, ESGR_Documento.Serie);
                objCmpSql.AddParameter("@Descripcion", SqlDbType.VarChar, ESGR_Documento.Descripcion);
                objCmpSql.AddParameter("@Longitud", SqlDbType.SmallInt, ESGR_Documento.Longitud);
                objCmpSql.AddParameter("@Correlativo", SqlDbType.Decimal, ESGR_Documento.Correlativo);
                objCmpSql.AddParameter("@Sunat", SqlDbType.Bit, ESGR_Documento.Sunat);
                objCmpSql.AddParameter("@IdUsuario", SqlDbType.Int, SGRVariables.ESGR_Usuario.IdUsuario);
                objCmpSql.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Lista de Documento
        /// </summary>
        /// <returns>Colección de Documento</returns>
        public CmpObservableCollection<ESGR_Documento> GetCollectionDocumento(string Filtro = "%")
        {
            try
            {
                var objCmpSql = new CmpSql(SGRVariables.ConectionString);
                var CollectionDocumento = new CmpObservableCollection<ESGR_Documento>();

                objCmpSql.CommandProcedure("spSGR_GET_BusquedaGeneral");
                objCmpSql.AddParameter("@Opcion", SqlDbType.VarChar, "GetDocumento");
                objCmpSql.AddParameter("@Filtro", SqlDbType.VarChar, Filtro);
                objCmpSql.AddParameter("@ParameterId", SqlDbType.Int, 0);
                DataTable dt = objCmpSql.ExecuteDataTable();

                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    CollectionDocumento.Add(new ESGR_Documento
                    {
                        ESGR_EmpresaSucursal = new ESGR_EmpresaSucursal
                        {
                            IdEmpSucursal = (dt.Rows[x]["IdEmpSucursal"] != DBNull.Value) ? Convert.ToInt16(dt.Rows[x]["IdEmpSucursal"]) : Convert.ToInt16(0),
                        },
                        CodDocumento = (dt.Rows[x]["CodDocumento"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["CodDocumento"]) : string.Empty,
                        Correlativo = (dt.Rows[x]["Correlativo"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Correlativo"]) : string.Empty,
                        Descripcion = (dt.Rows[x]["Descripcion"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Descripcion"]) : string.Empty,
                        Longitud = (dt.Rows[x]["Longitud"] != DBNull.Value) ? Convert.ToInt16(dt.Rows[x]["Longitud"]) : Convert.ToInt16(0),
                        Serie = (dt.Rows[x]["Serie"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Serie"]) : string.Empty,
                        Sunat = (dt.Rows[x]["Sunat"] != DBNull.Value) ? Convert.ToBoolean(dt.Rows[x]["Sunat"]) : false,
                    });
                }

                return CollectionDocumento;
            }

            catch (Exception)
            {
                throw;
            }
        }

        public CmpObservableCollection<ESGR_Documento> CollectionESGR_DocumentoSerieNumero()
        {
            try
            {
                var ObjCmpSql = new CmpSql(SGRVariables.ConectionString);
                var CollectionESGR_DocumentoSerie = new CmpObservableCollection<ESGR_Documento>();
                ObjCmpSql.CommandProcedure("spSGR_GET_BusquedaGeneral");
                ObjCmpSql.AddParameter("@Opcion", SqlDbType.VarChar, "GetEmpSucursalDocumentoSerie");
                ObjCmpSql.AddParameter("@Filtro", SqlDbType.VarChar, "");
                ObjCmpSql.AddParameter("@ParameterId", SqlDbType.Int, 1);
                DataTable dt = ObjCmpSql.ExecuteDataTable();
                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    CollectionESGR_DocumentoSerie.Add(new ESGR_Documento
                    {
                        ESGR_EmpresaSucursal = new ESGR_EmpresaSucursal
                        {
                            IdEmpSucursal = (dt.Rows[x]["IdEmpSucursal"] != DBNull.Value) ? Convert.ToInt16(dt.Rows[x]["IdEmpSucursal"]) : Convert.ToInt16(0),
                        },
                        CodDocumento = (dt.Rows[x]["CodDocumento"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["CodDocumento"]) : string.Empty,
                        Correlativo = (dt.Rows[x]["Correlativo"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Correlativo"]) : string.Empty,
                        Descripcion = (dt.Rows[x]["Descripcion"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Descripcion"]) : string.Empty,
                        Longitud = (dt.Rows[x]["Longitud"] != DBNull.Value) ? Convert.ToInt16(dt.Rows[x]["Longitud"]) : Convert.ToInt16(0),
                        Serie = (dt.Rows[x]["Serie"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Serie"]) : string.Empty,
                        Sunat = (dt.Rows[x]["Sunat"] != DBNull.Value) ? Convert.ToBoolean(dt.Rows[x]["Sunat"]) : false,
                    });
                }
                return CollectionESGR_DocumentoSerie;
            }
            catch(Exception)
            {
                throw;
            }

        }

    }
}
