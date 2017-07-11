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

    public class BSGR_FormaPago
    {
        /// <summary>
        /// Insertar, Editar y Eliminar Forma Pago
        /// </summary>
        /// <param name="ESGR_FormaPago">Objecto de la Entidad Forma Pago</param>
        public void TransFormaPago(ESGR_FormaPago ESGR_FormaPago)
        {
            try
            {
                var objCmpSql = new CmpSql(SGRVariables.ConectionString);
                objCmpSql.CommandProcedure("spSGR_SET_FormaPago");
                objCmpSql.AddParameter("@Opcion", SqlDbType.VarChar, ESGR_FormaPago.Opcion);
                objCmpSql.AddParameter("@IdFormaPago", SqlDbType.SmallInt, ESGR_FormaPago.IdFormaPago);
                objCmpSql.AddParameter("@IdEmpresa", SqlDbType.SmallInt, ESGR_FormaPago.ESGR_Empresa.IdEmpresa);
                objCmpSql.AddParameter("@FormaPago", SqlDbType.VarChar, ESGR_FormaPago.FormaPago);
                objCmpSql.AddParameter("@IdUsuario", SqlDbType.Int, SGRVariables.ESGR_Usuario.IdUsuario);
                objCmpSql.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Lista de Forma Pago
        /// </summary>
        /// <returns>Colección de Forma Pago</returns>
        public CmpObservableCollection<ESGR_FormaPago> GetCollectionFormaPago()
        {
            try
            {
                var objCmpSql = new CmpSql(SGRVariables.ConectionString);
                var CollectionFormaPago = new CmpObservableCollection<ESGR_FormaPago>();

                objCmpSql.CommandProcedure("spSGR_GET_BusquedaGeneral");
                objCmpSql.AddParameter("@Opcion", SqlDbType.VarChar, "GetFormaPago");
                objCmpSql.AddParameter("@Filtro", SqlDbType.VarChar, "");
                objCmpSql.AddParameter("@ParameterId", SqlDbType.Int, SGRVariables.ESGR_Usuario.ESGR_Empresa.IdEmpresa);
                DataTable dt = objCmpSql.ExecuteDataTable();

                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    CollectionFormaPago.Add(new ESGR_FormaPago
                    {
                        IdFormaPago = (dt.Rows[x]["IdFormaPago"] != DBNull.Value) ? Convert.ToInt16(dt.Rows[x]["IdFormaPago"]) : Convert.ToInt16(0),
                        FormaPago = (dt.Rows[x]["FormaPago"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["FormaPago"]) : string.Empty,
                        ESGR_Empresa = new ESGR_Empresa()
                        {
                            IdEmpresa = (dt.Rows[x]["IdEmpresa"] != DBNull.Value) ? Convert.ToInt16(dt.Rows[x]["IdEmpresa"]) : Convert.ToInt16(0),
                        },  
                    });
                }

                return CollectionFormaPago;
            }

            catch (Exception)
            {
                throw;
            }
        }

    }
}
