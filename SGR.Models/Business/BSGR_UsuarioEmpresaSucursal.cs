/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   OSCAR HUAMAN CABRERA
'* FCH. CREACIÓN : 30/11/2016
**********************************************************/
namespace SGR.Models.Business
{
    using ComputerSystems;
    using ComputerSystems.DataAccess.LibrarySql;
    using SGR.Models.Entity;
    using System;
    using System.Collections.ObjectModel;
    using System.Data;

    public class BSGR_UsuarioEmpresaSucursal
    {
        /// <summary>
        /// Insertar, Editar y Eliminar Usuario Empresa Sucursal
        /// </summary>
        /// <param name="ESGR_UsuarioEmpresaSucursal">Objecto de la Entidad Usuario Empresa Sucursal</param>
        public void TransUsuarioEmpresaSurcursal(ESGR_UsuarioEmpresaSucursal ESGR_UsuarioEmpresaSucursal)
        {
            try
            {
                var objCmpSql = new CmpSql(SGRVariables.ConectionString);
                objCmpSql.CommandProcedure("spSGR_SET_Usuario");
                objCmpSql.AddParameter("@Opcion", SqlDbType.VarChar, "DefaultEmpSucursal");
                objCmpSql.AddParameter("@IdUsuario", SqlDbType.Int, SGRVariables.ESGR_Usuario.IdUsuario);
                objCmpSql.AddParameter("@IdPerfil",SqlDbType.Int,0);
                objCmpSql.AddParameter("@IdEmpSucursal", SqlDbType.Int, ESGR_UsuarioEmpresaSucursal.ESGR_EmpresaSucursal.IdEmpSucursal);
                objCmpSql.AddParameter("@IdEmpresa",SqlDbType.Int,0);
                objCmpSql.AddParameter("@Nombres",SqlDbType.VarChar,"");
                objCmpSql.AddParameter("@Apellidos", SqlDbType.VarChar,  "");
                objCmpSql.AddParameter("@Correo", SqlDbType.VarChar,  "");
                objCmpSql.AddParameter("@Usuario", SqlDbType.VarChar,  "");
                objCmpSql.AddParameter("@Contrasenia", SqlDbType.VarChar,  "");
                objCmpSql.AddParameter("@IdUsuarioSet",SqlDbType.Int,SGRVariables.ESGR_Usuario.IdUsuario);
                objCmpSql.AddParameter("@CadenaSucursalXML", SqlDbType.NText, "");
                objCmpSql.AddParameter("@CadenaAreaXML", SqlDbType.NText,"");
                objCmpSql.AddParameter("@Nick",SqlDbType.VarChar,"");
                objCmpSql.AddParameter("@CadenaHabilitar", SqlDbType.NText, "");
                objCmpSql.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Lista de Usuario Empresa Sucursal
        /// </summary>
        /// <returns>Colección de los Usuario Empresa Sucursal</returns>
        public CmpObservableCollection<ESGR_UsuarioEmpresaSucursal> GetCollectionUsuarioEmpresaSucursal()
        {
            try
            {
                var objCmpSql = new CmpSql(SGRVariables.ConectionString);
                var CollectionUsuarioEmpresaSucursal = new CmpObservableCollection<ESGR_UsuarioEmpresaSucursal>();

                objCmpSql.CommandProcedure("spSGR_GET_BusquedaGeneral");
                objCmpSql.AddParameter("@Opcion", SqlDbType.VarChar, "GetUsuarioEmpresaSucursal");
                objCmpSql.AddParameter("@Filtro",SqlDbType.VarChar,"%");
                objCmpSql.AddParameter("@ParameterId",SqlDbType.Int, SGRVariables.ESGR_Usuario.IdUsuario);
                DataTable dt = objCmpSql.ExecuteDataTable();
                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    CollectionUsuarioEmpresaSucursal.Add(new ESGR_UsuarioEmpresaSucursal
                    {
                        ESGR_Usuario = new ESGR_Usuario()
                        {
                            IdUsuario = (dt.Rows[x]["IdUsuario"] != DBNull.Value) ? Convert.ToInt16(dt.Rows[x]["IdUsuario"]) : Convert.ToInt16(0),
                        },
                        ESGR_EmpresaSucursal = new ESGR_EmpresaSucursal()
                        {
                            IdEmpSucursal = (dt.Rows[x]["IdEmpSucursal"] != DBNull.Value) ? Convert.ToInt16(dt.Rows[x]["IdEmpSucursal"]) : Convert.ToInt16(0),
                            Sucursal = (dt.Rows[x]["SUcursal"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Sucursal"]) : string.Empty,
                            Principal = (dt.Rows[x]["Principal"] != DBNull.Value) ? Convert.ToBoolean(dt.Rows[x]["Principal"]) : false,
                            ESGR_Empresa = new ESGR_Empresa()
                            {
                                IdEmpresa = (dt.Rows[x]["IdEmpresa"]!=DBNull.Value)?Convert.ToInt32(dt.Rows[x]["IdEmpresa"]):0,
                                RazonSocial=(dt.Rows[x]["RazonSocial"]!=DBNull.Value)?Convert.ToString(dt.Rows[x]["RazonSocial"]):string.Empty
                            }
                        },
                    });
                }

                return CollectionUsuarioEmpresaSucursal;
            }

            catch (Exception)
            {
                throw;
            }
        }

    }
}
