/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   OSCAR HUAMAN CABRERA
'* FCH. CREACIÓN : 29/11/2016
**********************************************************/
namespace SGR.Models.Business
{
    using ComputerSystems.DataAccess.LibrarySql;
    using SGR.Models.Entity;
    using System;
    using System.Collections.ObjectModel;
    using System.Data;
    using Newtonsoft.Json;
    using ComputerSystems;

    public class BSGR_Cliente
    {
        /// <summary>
        /// Insertar, Editar y Eliminar Cliente
        /// </summary>
        /// <param name="ESGR_Cliente">Objecto de la Entidad Cliente</param>
        public void TransCliente(ESGR_Cliente ESGR_Cliente)
        {
            try
            {
                var objCmpSql = new CmpSql(SGRVariables.ConectionString);
                objCmpSql.CommandProcedure("spSGR_SET_Cliente");
                objCmpSql.AddParameter("@Opcion", SqlDbType.VarChar, ESGR_Cliente.Opcion);
                objCmpSql.AddParameter("@IdCliente", SqlDbType.Int, ESGR_Cliente.IdCliente);
                objCmpSql.AddParameter("@DocIdentidad", SqlDbType.Char, ESGR_Cliente.DocIdentidad);
                objCmpSql.AddParameter("@NroDocIdentidad", SqlDbType.VarChar, ESGR_Cliente.NroDocIdentidad);
                objCmpSql.AddParameter("@Cliente", SqlDbType.VarChar, ESGR_Cliente.Cliente);
                objCmpSql.AddParameter("@Direccion", SqlDbType.VarChar, ESGR_Cliente.Direccion);
                objCmpSql.AddParameter("@Localidad", SqlDbType.VarChar, ESGR_Cliente.Localidad);
                objCmpSql.AddParameter("@Telefono", SqlDbType.VarChar, JsonConvert.SerializeObject(ESGR_Cliente.Telefono));
                objCmpSql.AddParameter("@Correo", SqlDbType.VarChar, ESGR_Cliente.Correo);
                objCmpSql.AddParameter("@FechaNacimiento", SqlDbType.SmallDateTime, ESGR_Cliente.FechaNacimiento);
                objCmpSql.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Lista de Cliente
        /// </summary>
        /// <returns>Colección de Cliente</returns>

        public CmpObservableCollection<ESGR_Cliente> GetCollectionCliente(string Filtro)
        {
            try
            {
                var objCmpSql = new CmpSql(SGRVariables.ConectionString);
                var CollectionCliente = new CmpObservableCollection<ESGR_Cliente>();

                objCmpSql.CommandProcedure("spSGR_GET_BusquedaGeneral");
                objCmpSql.AddParameter("@Opcion", SqlDbType.VarChar, "GetCliente");
                objCmpSql.AddParameter("@Filtro", SqlDbType.VarChar, (Filtro == string.Empty || Filtro == null) ? "%" : Filtro);
                objCmpSql.AddParameter("@ParameterId", SqlDbType.Int, 0);
                DataTable dt = objCmpSql.ExecuteDataTable();

                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    var ListTelefono = new ObservableCollection<ESGR_Item>();
                    try
                    {
                        ListTelefono = (dt.Rows[x]["Telefono"] != DBNull.Value) ? JsonConvert.DeserializeObject<ObservableCollection<ESGR_Item>>(Convert.ToString(dt.Rows[x]["Telefono"])) : new ObservableCollection<ESGR_Item>();
                    }
                    catch (Exception) { }
                    CollectionCliente.Add(new ESGR_Cliente
                    {
                        IdCliente = (dt.Rows[x]["IdCliente"] != DBNull.Value) ? Convert.ToInt16(dt.Rows[x]["IdCliente"]) : Convert.ToInt16(0),
                        Cliente = (dt.Rows[x]["Cliente"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Cliente"]) : string.Empty,
                        Correo = (dt.Rows[x]["Correo"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Correo"]) : string.Empty,
                        Direccion = (dt.Rows[x]["Direccion"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Direccion"]) : string.Empty,
                        DocIdentidad = (dt.Rows[x]["DocIdentidad"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["DocIdentidad"]) : string.Empty,
                        FechaNacimiento = (dt.Rows[x]["FechaNacimiento"] != DBNull.Value) ? Convert.ToDateTime(dt.Rows[x]["FechaNacimiento"]) : DateTime.Now,
                        Localidad = (dt.Rows[x]["Localidad"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Localidad"]) : string.Empty,
                        NroDocIdentidad = (dt.Rows[x]["NroDocIdentidad"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["NroDocIdentidad"]) : string.Empty,
                        Telefono = ListTelefono
                    });
                }

                return CollectionCliente;
            }

            catch (Exception)
            {
                throw;
            }
        }
    }
}