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

    public class BSGR_EmpresaSucursal
    {
        /// <summary>
        /// Insertar, Editar y Eliminar Empresa Sucursal
        /// </summary>
        /// <param name="ESGR_EmpresaSucursal">Objecto de la Entidad Empresa Sucursal</param>
        public void TransEmpresaSucursal(ESGR_EmpresaSucursal ESGR_EmpresaSucursal)
        {
            try
            {
                var objCmpSql = new CmpSql(SGRVariables.ConectionString);
                objCmpSql.CommandProcedure("spSGR_SET_EmpresaSucursal");
                objCmpSql.AddParameter("@Opcion", SqlDbType.Char, ESGR_EmpresaSucursal.Opcion);
                objCmpSql.AddParameter("@IdEmpSucursal", SqlDbType.SmallInt, ESGR_EmpresaSucursal.IdEmpSucursal);
                objCmpSql.AddParameter("@IdEmpresa", SqlDbType.SmallInt, ESGR_EmpresaSucursal.ESGR_Empresa.IdEmpresa);
                objCmpSql.AddParameter("@Sucursal", SqlDbType.VarChar, ESGR_EmpresaSucursal.Sucursal);
                objCmpSql.AddParameter("@Principal", SqlDbType.Bit, ESGR_EmpresaSucursal.Principal);
                objCmpSql.AddParameter("@IdUsuario", SqlDbType.Int, 1);
                objCmpSql.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Lista de Empresa Sucursal
        /// </summary>
        /// <returns>Colección de Empresa Sucursal</returns>
        public CmpObservableCollection<ESGR_EmpresaSucursal> GetCollectionEmpresaSucursal(ESGR_Empresa ESGR_Empresa)
        {
            try
            {
                var objCmpSql = new CmpSql(SGRVariables.ConectionString);
                var CollectionEmpresaSucursal = new CmpObservableCollection<ESGR_EmpresaSucursal>();

                objCmpSql.CommandProcedure("spSGR_GET_BusquedaGeneral");
                objCmpSql.AddParameter("@Opcion", SqlDbType.VarChar, "GetEmpSucursal");
                objCmpSql.AddParameter("@Filtro", SqlDbType.VarChar, "");
                objCmpSql.AddParameter("@ParameterId", SqlDbType.Int, ESGR_Empresa.IdEmpresa);
                DataTable dt = objCmpSql.ExecuteDataTable();

                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    CollectionEmpresaSucursal.Add(new ESGR_EmpresaSucursal
                    {
                        IdEmpSucursal = (dt.Rows[x]["IdEmpSucursal"] != DBNull.Value) ? Convert.ToInt16(dt.Rows[x]["IdEmpSucursal"]) : Convert.ToInt16(0),
                        Principal = (dt.Rows[x]["Principal"] != DBNull.Value) ? Convert.ToBoolean(dt.Rows[x]["Principal"]) : false,
                        Sucursal = (dt.Rows[x]["Sucursal"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Sucursal"]) : string.Empty,
                        ESGR_Empresa = new ESGR_Empresa()
                        {
                            IdEmpresa = (dt.Rows[x]["IdEmpresa"] != DBNull.Value) ? Convert.ToInt16(dt.Rows[x]["IdEmpresa"]) : Convert.ToInt16(0),
                        },
                    });
                }

                return CollectionEmpresaSucursal;
            }

            catch (Exception)
            {
                throw;
            }
        }
    }
}
