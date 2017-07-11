/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   ABEL QUISPE ORELLANA
'* FCH. CREACIÓN : 28/11/2016
**********************************************************/
namespace SGR.Models.Entity
{
    using System;
    public class ESGR_Configuracion
    {
        public string IdConfig { get; set; }
        public string NivelConfig { get; set; }
        public string Valor { get; set; }
        public string Descripcion { get; set; }
        public bool FlgEliminado { get; set; }
    }
}
