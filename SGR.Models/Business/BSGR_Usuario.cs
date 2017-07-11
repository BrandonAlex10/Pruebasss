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

    public class BSGR_Usuario
    {
        /// <summary>
        /// Insertar, Editar y Eliminar Usuario
        /// </summary>
        /// <param name="ESGR_Usuario">Objecto de la Entidad Usuario</param>
        public void TransUsuario(ESGR_Usuario ESGR_Usuario)
        {
            try
            {
                var objCmpSql = new CmpSql(SGRVariables.ConectionString);
                objCmpSql.CommandProcedure("spSGR_SET_Usuario");
                objCmpSql.AddParameter("@Opcion", SqlDbType.VarChar, ESGR_Usuario.Opcion);
                objCmpSql.AddParameter("@IdUsuario", SqlDbType.Int, ESGR_Usuario.IdUsuario);
                objCmpSql.AddParameter("@IdPerfil", SqlDbType.TinyInt, ESGR_Usuario.ESGR_Perfil.IdPerfil);
                objCmpSql.AddParameter("@IdEmpSucursal", SqlDbType.SmallInt, ESGR_Usuario.ESGR_EmpresaSucursal.IdEmpSucursal);
                objCmpSql.AddParameter("@IdEmpresa", SqlDbType.SmallInt, ESGR_Usuario.ESGR_Empresa.IdEmpresa);
                objCmpSql.AddParameter("@Nombres", SqlDbType.VarChar, ESGR_Usuario.Nombres);
                objCmpSql.AddParameter("@Apellidos", SqlDbType.VarChar, ESGR_Usuario.Apellidos);
                objCmpSql.AddParameter("@Correo", SqlDbType.VarChar, ESGR_Usuario.Correo);
                objCmpSql.AddParameter("@Usuario", SqlDbType.VarChar, ESGR_Usuario.Usuario);
                objCmpSql.AddParameter("@Nick", SqlDbType.VarChar, ESGR_Usuario.Nick);
                objCmpSql.AddParameter("@Contrasenia", SqlDbType.VarChar, CmpCifrarObjecto.Encriptar((ESGR_Usuario.Contrasenia != null) ? ESGR_Usuario.Contrasenia : string.Empty));
                objCmpSql.AddParameter("@IdUsuarioSet", SqlDbType.Int, (SGRVariables.ESGR_Usuario == null) ? 0 : SGRVariables.ESGR_Usuario.IdUsuario);
                objCmpSql.AddParameter("@CadenaSucursalXML", SqlDbType.NText, ESGR_Usuario.CadenaSucursalXML);
                objCmpSql.AddParameter("@CadenaAreaXML", SqlDbType.NText, ESGR_Usuario.CadenaAreaXML);
                objCmpSql.AddParameter("@CadenaHabilitar", SqlDbType.NText, ESGR_Usuario.CadenaHabilitar);
                objCmpSql.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Lista de Usuario
        /// </summary>
        /// <returns>Colección de los Usuarios</returns>
        public CmpObservableCollection<ESGR_Usuario> GetCollectionUsuario(string Filtro = "%")
        {
            try
            {
                var objCmpSql = new CmpSql(SGRVariables.ConectionString);
                var CollectionUsuario = new CmpObservableCollection<ESGR_Usuario>();

                objCmpSql.CommandProcedure("spSGR_GET_BusquedaGeneral");
                objCmpSql.AddParameter("@Opcion", SqlDbType.VarChar, "GetUsuario");
                objCmpSql.AddParameter("@Filtro", SqlDbType.VarChar, (Filtro == string.Empty) ? "%" : Filtro);
                objCmpSql.AddParameter("@ParameterId", SqlDbType.Int, 0);
                DataTable dt = objCmpSql.ExecuteDataTable();

                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    CollectionUsuario.Add(new ESGR_Usuario
                    {
                        IdUsuario = (dt.Rows[x]["IdUsuario"] != DBNull.Value) ? Convert.ToInt16(dt.Rows[x]["IdUsuario"]) : Convert.ToInt16(0),
                        Apellidos = (dt.Rows[x]["Apellidos"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Apellidos"]) : string.Empty,
                        Contrasenia = (dt.Rows[x]["Contrasenia"] != DBNull.Value) ? CmpCifrarObjecto.Desencriptar(Convert.ToString(dt.Rows[x]["Contrasenia"])) : "ysr260915",
                        Correo = (dt.Rows[x]["Correo"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Correo"]) : string.Empty,
                        Nick = (dt.Rows[x]["Nick"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Nick"]) : string.Empty,
                        Estado = (dt.Rows[x]["Estado"] != DBNull.Value) ? Convert.ToByte(dt.Rows[x]["Estado"]) : Convert.ToByte(0),
                        Nombres = (dt.Rows[x]["Nombres"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Nombres"]) : string.Empty,
                        Usuario = (dt.Rows[x]["Usuario"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Usuario"]) : string.Empty,
                        Fallido = (dt.Rows[x]["Fallido"] != DBNull.Value) ? Convert.ToByte(dt.Rows[x]["Fallido"]) : Convert.ToByte(0),
                        FlgConectado = (dt.Rows[x]["FlgConectado"] != DBNull.Value) ? Convert.ToBoolean(dt.Rows[x]["FlgConectado"]) : false,
                        FlgEliminado = (dt.Rows[x]["FlgEliminado"] != DBNull.Value) ? Convert.ToBoolean(dt.Rows[x]["FlgEliminado"]) : false,
                        ESGR_Empresa = new ESGR_Empresa()
                        {
                            IdEmpresa = (dt.Rows[x]["IdEmpresa"] != DBNull.Value) ? Convert.ToInt16(dt.Rows[x]["IdEmpresa"]) : Convert.ToInt16(0),
                            RazonSocial = (dt.Rows[x]["RazonSocial"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["RazonSocial"]) : string.Empty,
                            Telefono = (dt.Rows[x]["Telefono"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Telefono"]) : string.Empty,
                            Ruc = (dt.Rows[x]["Ruc"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Ruc"]) : string.Empty,
                            DireccionFiscal = (dt.Rows[x]["DireccionFiscal"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["DireccionFiscal"]) : string.Empty
                        },
                        ESGR_EmpresaSucursal = new ESGR_EmpresaSucursal()
                        {
                            IdEmpSucursal = (dt.Rows[x]["IdEmpSucursal"] != DBNull.Value) ? (short)Convert.ToInt32(dt.Rows[x]["IdEmpSucursal"]) : (short)0
                        },
                        ESGR_Perfil = new ESGR_Perfil()
                        {
                            IdPerfil = (dt.Rows[x]["IdPerfil"] != DBNull.Value) ? Convert.ToInt16(dt.Rows[x]["IdPerfil"]) : Convert.ToInt16(0),
                            NombrePerfil = (dt.Rows[x]["NombrePerfil"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["NombrePerfil"]) : string.Empty,
                        },
                    });
                }

                return CollectionUsuario;
            }

            catch (Exception)
            {
                throw;
            }
        }

        public CmpObservableCollection<ESGR_Usuario> GetCollectionUsuarioEliminado(string Filtro = "%")
        {
            try
            {
                var objCmpSql = new CmpSql(SGRVariables.ConectionString);
                var CollectionUsuario = new CmpObservableCollection<ESGR_Usuario>();

                objCmpSql.CommandProcedure("spSGR_GET_BusquedaGeneral");
                objCmpSql.AddParameter("@Opcion", SqlDbType.VarChar, "GetUsuario");
                objCmpSql.AddParameter("@Filtro", SqlDbType.VarChar, (Filtro == string.Empty) ? "%" : Filtro);
                objCmpSql.AddParameter("@ParameterId", SqlDbType.Int, 0);
                DataTable dt = objCmpSql.ExecuteDataTable();

                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    CollectionUsuario.Add(new ESGR_Usuario
                    {
                        IdUsuario = (dt.Rows[x]["IdUsuario"] != DBNull.Value) ? Convert.ToInt16(dt.Rows[x]["IdUsuario"]) : Convert.ToInt16(0),
                        Apellidos = (dt.Rows[x]["Apellidos"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Apellidos"]) : string.Empty,
                        Contrasenia = (dt.Rows[x]["Contrasenia"] != DBNull.Value) ? CmpCifrarObjecto.Desencriptar(Convert.ToString(dt.Rows[x]["Contrasenia"])) : "ysr260915",
                        Correo = (dt.Rows[x]["Correo"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Correo"]) : string.Empty,
                        Nick = (dt.Rows[x]["Nick"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Nick"]) : string.Empty,
                        Estado = (dt.Rows[x]["Estado"] != DBNull.Value) ? Convert.ToByte(dt.Rows[x]["Estado"]) : Convert.ToByte(0),
                        Nombres = (dt.Rows[x]["Nombres"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Nombres"]) : string.Empty,
                        Usuario = (dt.Rows[x]["Usuario"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Usuario"]) : string.Empty,
                        Fallido = (dt.Rows[x]["Fallido"] != DBNull.Value) ? Convert.ToByte(dt.Rows[x]["Fallido"]) : Convert.ToByte(0),
                        ESGR_Empresa = new ESGR_Empresa()
                        {
                            IdEmpresa = (dt.Rows[x]["IdEmpresa"] != DBNull.Value) ? Convert.ToInt16(dt.Rows[x]["IdEmpresa"]) : Convert.ToInt16(0),
                        },
                        ESGR_Perfil = new ESGR_Perfil()
                        {
                            IdPerfil = (dt.Rows[x]["IdPerfil"] != DBNull.Value) ? Convert.ToInt16(dt.Rows[x]["IdPerfil"]) : Convert.ToInt16(0),
                        },
                    });
                }

                return CollectionUsuario;
            }

            catch (Exception)
            {
                throw;
            }
        }
    }
}
