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

    public class BSGR_MedioPago
    {
        /// <summary>
        /// Insertar, Editar y Eliminar Medio Pago
        /// </summary>
        /// <param name="ESGR_MedioPago">Objecto de la Entidad Medio Pago</param>
        public void TransMedioPago(ESGR_MedioPago ESGR_MedioPago)
        {
            try
            {
                var objCmpSql = new CmpSql(SGRVariables.ConectionString);
                objCmpSql.CommandProcedure("spSGR_SET_MedioPago");
                objCmpSql.AddParameter("@Opcion", SqlDbType.VarChar, ESGR_MedioPago.Opcion);
                objCmpSql.AddParameter("@IdMedioPago", SqlDbType.SmallInt, ESGR_MedioPago.IdMedioPago);
                objCmpSql.AddParameter("@IdFormaPago", SqlDbType.SmallInt, ESGR_MedioPago.ESGR_FormaPago.IdFormaPago);
                objCmpSql.AddParameter("@MedioPago", SqlDbType.VarChar, ESGR_MedioPago.MedioPago);
                objCmpSql.AddParameter("@DiasCredito", SqlDbType.TinyInt, ESGR_MedioPago.DiasCredito);
                objCmpSql.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Lista de Medio Pago
        /// </summary>
        /// <returns>Colección de Medio Pago</returns>
        public CmpObservableCollection<ESGR_MedioPago> GetCollectionMedioPago()
        {
            try
            {
                var objCmpSql = new CmpSql(SGRVariables.ConectionString);
                var CollectionMedioPago = new CmpObservableCollection<ESGR_MedioPago>();

                objCmpSql.CommandProcedure("spSGR_GET_BusquedaGeneral");
                objCmpSql.AddParameter("@Opcion", SqlDbType.VarChar, "GetMedioPago");
                objCmpSql.AddParameter("@Filtro", SqlDbType.VarChar, "");
                objCmpSql.AddParameter("@ParameterId", SqlDbType.Int, SGRVariables.ESGR_Usuario.ESGR_Empresa.IdEmpresa);
                DataTable dt = objCmpSql.ExecuteDataTable();

                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    CollectionMedioPago.Add(new ESGR_MedioPago
                    {
                        IdMedioPago = (dt.Rows[x]["IdMedioPago"] != DBNull.Value) ? Convert.ToInt16(dt.Rows[x]["IdMedioPago"]) : Convert.ToInt16(0),
                        MedioPago = (dt.Rows[x]["MedioPago"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["MedioPago"]) : string.Empty,
                        DiasCredito = (dt.Rows[x]["DiasCredito"] != DBNull.Value) ? Convert.ToInt16(dt.Rows[x]["DiasCredito"]) : Convert.ToInt16(0),
                        ESGR_FormaPago = new ESGR_FormaPago()
                        {
                            IdFormaPago = (dt.Rows[x]["IdFormaPago"] != DBNull.Value) ? Convert.ToInt16(dt.Rows[x]["IdFormaPago"]) : Convert.ToInt16(0),
                            FormaPago = (dt.Rows[x]["FormaPago"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["FormaPago"]) : string.Empty,
                        },
                    });
                }

                return CollectionMedioPago;
            }

            catch (Exception)
            {
                throw;
            }
        }

    }
}
