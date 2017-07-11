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

    public class BSGR_FormularioSetting
    {
        /// <summary>
        /// Insertar, Editar y Eliminar FormularioSetting
        /// </summary>
        /// <param name="ESGR_FormularioSetting">Objecto de la Entidad FormularioSetting</param>
        public void TransFormularioSetting(ESGR_FormularioSetting ESGR_FormularioSetting)
        {
            try
            {
                var objCmpSql = new CmpSql(SGRVariables.ConectionString);
                objCmpSql.CommandProcedure("spSGR_SET_FormularioSetting");
                objCmpSql.AddParameter("@IdFormularioSetting", SqlDbType.Int, ESGR_FormularioSetting.IdFormularioSetting);
                objCmpSql.AddParameter("@IdEmpresa", SqlDbType.SmallInt, ESGR_FormularioSetting.ESGR_Empresa.IdEmpresa);
                objCmpSql.AddParameter("@CodFormulario", SqlDbType.Char, ESGR_FormularioSetting.ESGR_Formulario.CodFormulario);
                objCmpSql.AddParameter("@Campo", SqlDbType.VarChar, ESGR_FormularioSetting.Campo);
                objCmpSql.AddParameter("@Codigo", SqlDbType.VarChar, ESGR_FormularioSetting.Codigo);
                objCmpSql.AddParameter("@Descripcion", SqlDbType.VarChar, ESGR_FormularioSetting.Descripcion);
                objCmpSql.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Lista de FormularioSetting
        /// </summary>
        /// <returns>Colección de FormularioSetting</returns>
        public CmpObservableCollection<ESGR_FormularioSetting> GetCollectionFormularioSetting()
        {
            try
            {
                var objCmpSql = new CmpSql(SGRVariables.ConectionString);
                var CollectionFormularioSetting = new CmpObservableCollection<ESGR_FormularioSetting>();

                objCmpSql.CommandProcedure("spSGR_GET_FormularioSetting");
                DataTable dt = objCmpSql.ExecuteDataTable();

                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    CollectionFormularioSetting.Add(new ESGR_FormularioSetting
                    {
                        IdFormularioSetting = (dt.Rows[x]["IdFormularioSetting"] != DBNull.Value) ? Convert.ToInt16(dt.Rows[x]["IdFormularioSetting"]) : Convert.ToInt16(0),
                        Codigo = (dt.Rows[x]["Codigo"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Codigo"]) : string.Empty,
                        Campo = (dt.Rows[x]["Campo"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Campo"]) : string.Empty,
                        Descripcion = (dt.Rows[x]["Descripcion"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Descripcion"]) : string.Empty,
                        ESGR_Empresa = new ESGR_Empresa()
                        {
                            IdEmpresa = (dt.Rows[x]["IdEmpresa"] != DBNull.Value) ? Convert.ToInt16(dt.Rows[x]["IdEmpresa"]) : Convert.ToInt16(0),
                        },
                        ESGR_Formulario = new ESGR_Formulario()
                        {
                            CodFormulario = (dt.Rows[x]["CodFormulario"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["CodFormulario"]) : string.Empty,
                        },
                    });
                }

                return CollectionFormularioSetting;
            }

            catch (Exception)
            {
                throw;
            }
        }

    }
}
