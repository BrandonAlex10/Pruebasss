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

    public class BSGR_Contrasenia_Hist
    {
        /// <summary>
        /// Insertar, Editar y Eliminar Contrasenia_Hist
        /// </summary>
        /// <param name="ESGR_Contrasenia_Hist">Objecto de la Entidad Contrasenia_Hist</param>
        public void TransContrasenia_Hist(ESGR_Contrasenia_Hist ESGR_Contrasenia_Hist)
        {
            try
            {
                var objCmpSql = new CmpSql(SGRVariables.ConectionString);
                objCmpSql.CommandProcedure("spSGR_SET_Contrasenia_Hist");
                objCmpSql.AddParameter("@IdUsuario", SqlDbType.Int, ESGR_Contrasenia_Hist.ESGR_Usuario.IdUsuario);
                objCmpSql.AddParameter("@Contrasenia", SqlDbType.VarChar, ESGR_Contrasenia_Hist.Contrasenia);
                objCmpSql.AddParameter("@Fecha", SqlDbType.DateTime, ESGR_Contrasenia_Hist.Fecha);
                objCmpSql.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Lista de Contrasenia_Hist
        /// </summary>
        /// <returns>Colección de Contrasenia_Hist</returns>
        public CmpObservableCollection<ESGR_Contrasenia_Hist> GetCollectionContrasenia_Hist()
        {
            try
            {
                var objCmpSql = new CmpSql(SGRVariables.ConectionString);
                var CollectionContrasenia_Hist = new CmpObservableCollection<ESGR_Contrasenia_Hist>();

                objCmpSql.CommandProcedure("spSGR_GET_Contrasenia_Hist");
                DataTable dt = objCmpSql.ExecuteDataTable();

                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    CollectionContrasenia_Hist.Add(new ESGR_Contrasenia_Hist
                    {
                        Contrasenia = (dt.Rows[x]["Contrasenia"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Contrasenia"]) : string.Empty,
                        Fecha = (dt.Rows[x]["Contrasenia"] != DBNull.Value) ? Convert.ToDateTime(dt.Rows[x]["Contrasenia"]) : DateTime.Now,
                        ESGR_Usuario = new ESGR_Usuario()
                        {
                            IdUsuario = (dt.Rows[x]["Contrasenia"] != DBNull.Value) ? Convert.ToInt16(dt.Rows[x]["Contrasenia"]) : Convert.ToInt16(0),
                        },
                    });
                }

                return CollectionContrasenia_Hist;
            }

            catch (Exception)
            {
                throw;
            }
        }

    }
}
