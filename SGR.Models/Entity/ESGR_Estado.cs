/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   ABEL QUISPE ORELLANA
'* FCH. CREACIÓN : 28/11/2016
**********************************************************/
namespace SGR.Models.Entity
{

    public class ESGR_Estado
    {
        public string CodEstado { get; set; }
        public string Tabla { get; set; }
        public string Campo { get; set; }
        public string Estado { get; set; }
        public decimal Total { get; set; } //Para reporte
    }
}
