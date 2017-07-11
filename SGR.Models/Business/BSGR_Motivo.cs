using ComputerSystems;
using ComputerSystems.DataAccess.LibrarySql;
using SGR.Models.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGR.Models.Business
{
    public class BSGR_Motivo
    {
        CmpSql ObjCmpSql = new CmpSql(SGRVariables.ConectionString);
        public CmpObservableCollection<ESGR_Motivo> CollectionESGR_Motivo()
        {
            var CollectionESGR_Motivo = new CmpObservableCollection<ESGR_Motivo>();
            ObjCmpSql.CommandProcedure("spSGR_GET_BusquedaGeneral");
            ObjCmpSql.AddParameter("@Opcion",SqlDbType.VarChar,"GetMotivo");
            ObjCmpSql.AddParameter("@Filtro",SqlDbType.VarChar,"%");
            ObjCmpSql.AddParameter("@ParameterId",SqlDbType.Int,0);
            DataTable dt = ObjCmpSql.ExecuteDataTable();
            for (int x = 0; x < dt.Rows.Count; x++)
            {
                CollectionESGR_Motivo.Add(new ESGR_Motivo()
                {
                    CodMotivo = (dt.Rows[x]["CodMotivo"]!= DBNull.Value)? Convert.ToString(dt.Rows[x]["CodMotivo"]):string.Empty,
                    Motivo = (dt.Rows[x]["Motivo"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Motivo"]) : string.Empty,
                    Modulo = (dt.Rows[x]["Modulo"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Modulo"]) : string.Empty,
                    Campo = (dt.Rows[x]["Campo"] != DBNull.Value) ? Convert.ToString(dt.Rows[x]["Campo"]) : string.Empty,
                });
            }
            return CollectionESGR_Motivo;
        }
    }
}
