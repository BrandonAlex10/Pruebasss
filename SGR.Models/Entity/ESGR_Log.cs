/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   ABEL QUISPE ORELLANA
'* FCH. CREACIÓN : 28/11/2016
**********************************************************/
namespace SGR.Models.Entity
{
    using System;
    public class ESGR_Log
    {
        public string Tabla { get; set; }
        public int IdRegistro { get; set; }
        public int IdUsuario { get; set; }
        public int Item { get; set; }
        public string Operacion { get; set; }
    }
}
