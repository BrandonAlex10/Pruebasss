/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   ABEL QUISPE ORELLANA
'* FCH. CREACIÓN : 28/11/2016
**********************************************************/
namespace SGR.Models.Entity
{
    using System;
    public class ESGR_Moneda
    {
        public string Opcion { get; set; }
        public string CodMoneda { get; set; }
        public string Descripcion { get; set; }
        public string Abreviacion { get; set; }
        public string Simbolo { get; set; }
        public bool Defecto { get; set; }
    }
}
