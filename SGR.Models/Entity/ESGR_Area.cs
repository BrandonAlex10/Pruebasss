/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   ABEL QUISPE ORELLANA
'* FCH. CREACIÓN : 28/11/2016
**********************************************************/
namespace SGR.Models.Entity
{
    using System;
    public class ESGR_Area
    {
        public string Opcion { get; set; }
        public short IdArea { get; set; }
        public virtual ESGR_Empresa ESGR_Empresa { get; set; }
        public string Area { get; set; }

    }
}
