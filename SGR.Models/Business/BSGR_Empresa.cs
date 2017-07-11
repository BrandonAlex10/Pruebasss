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

    public class BSGR_Empresa
    {
        /// <summary>
        /// Insertar, Editar y Eliminar Empresa
        /// </summary>
        /// <param name="ESGR_Empresa">Objecto de la Entidad Empresa</param>
        public void TransEmpresa(ESGR_Empresa ESGR_Empresa)
        {
            try
            {
                var objCmpSql = new CmpSql(SGRVariables.ConectionString);
                objCmpSql.CommandProcedure("spSGR_SET_Empresa");
                objCmpSql.AddParameter("@Opcion", SqlDbType.Char, ESGR_Empresa.Opcion);
                objCmpSql.AddParameter("@IdEmpresa", SqlDbType.SmallInt, ESGR_Empresa.IdEmpresa);
                objCmpSql.AddParameter("@RazonSocial", SqlDbType.VarChar, ESGR_Empresa.RazonSocial);
                objCmpSql.AddParameter("@Ruc", SqlDbType.Char, ESGR_Empresa.Ruc);
                objCmpSql.AddParameter("@DireccionFiscal", SqlDbType.VarChar, ESGR_Empresa.DireccionFiscal);
                objCmpSql.AddParameter("@Rubro", SqlDbType.VarChar, ESGR_Empresa.Rubro);
                objCmpSql.AddParameter("@Telefono", SqlDbType.VarChar, ESGR_Empresa.Telefono);
                objCmpSql.AddParameter("@RegimenTributario", SqlDbType.VarChar, ESGR_Empresa.RegimenTributario);
                objCmpSql.AddParameter("@NombreComercial", SqlDbType.VarChar, ESGR_Empresa.NombreComercial);
                objCmpSql.AddParameter("@RepresentanteLegal", SqlDbType.VarChar, ESGR_Empresa.RepresentanteLegal);
                //objCmpSql.AddParameter("@IdCliProveedor", SqlDbType.Int, ESGR_Empresa.IdCliProveedor);
                objCmpSql.AddParameter("@ExoneradoIGV", SqlDbType.Bit, (ESGR_Empresa.ExoneradoIGV == null) ? false : ESGR_Empresa.ExoneradoIGV);
                objCmpSql.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Lista de Empresa
        /// </summary>
        /// <returns>Colección de Empresa</returns>
        public CmpObservableCollection<ESGR_Empresa> GetCollectionEmpresa(string Filtro = "%")
        {
            try
            {
                var objCmpSql = new CmpSql(SGRVariables.ConectionString);
                var CollectionEmpresa = new CmpObservableCollection<ESGR_Empresa>();

                objCmpSql.CommandProcedure("spSGR_GET_BusquedaGeneral");
                objCmpSql.AddParameter("@Opcion", SqlDbType.VarChar,"FiltrarEmpresa");
                objCmpSql.AddParameter("@Filtro", SqlDbType.VarChar, (Filtro == string.Empty) ? "%" : Filtro);
                objCmpSql.AddParameter("@ParameterId", SqlDbType.Int, 0);
                DataTable dt = objCmpSql.ExecuteDataTable();

                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    CollectionEmpresa.Add(new ESGR_Empresa
                    {
                        IdEmpresa = (dt.Rows[x]["IdEmpresa"] != DBNull.Value) ? Convert.ToInt16(dt.Rows[x]["IdEmpresa"]) : Convert.ToInt16(0),
                        RazonSocial = (dt.Rows[x]["RazonSocial"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["RazonSocial"]) : string.Empty,
                        Ruc = (dt.Rows[x]["Ruc"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Ruc"]) : string.Empty,
                        DireccionFiscal = (dt.Rows[x]["DireccionFiscal"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["DireccionFiscal"]) : string.Empty,
                        Rubro = (dt.Rows[x]["Rubro"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Rubro"]) : string.Empty,
                        Telefono = (dt.Rows[x]["Telefono"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Telefono"]) : string.Empty,
                        RegimenTributario = (dt.Rows[x]["RegimenTributario"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["RegimenTributario"]) : string.Empty,
                        NombreComercial = (dt.Rows[x]["NombreComercial"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["NombreComercial"]) : string.Empty,
                        RepresentanteLegal = (dt.Rows[x]["RepresentanteLegal"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["RepresentanteLegal"]) : string.Empty,
                        IdCliProveedor = (dt.Rows[x]["IdCliProveedor"] != DBNull.Value) ? Convert.ToInt16(dt.Rows[x]["IdCliProveedor"]) : Convert.ToInt16(0),
                        ExoneradoIGV = (dt.Rows[x]["ExoneradoIGV"] != DBNull.Value) ? Convert.ToBoolean(dt.Rows[x]["ExoneradoIGV"]) : false,
                      });
                }

                return CollectionEmpresa;
            }

            catch (Exception)
            {
                throw;
            }
        }

    }
}
