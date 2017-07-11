/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   ABEL QUISPE ORELLANA
'* FCH. CREACIÓN : 29/11/2016
**********************************************************/
namespace SGR.Models.Entity
{
    using System;
    public class ESGR_Contrasenia_Hist
    {
        public ESGR_Usuario ESGR_Usuario { get; set; }
        public string Contrasenia { get; set; }
        public Nullable<System.DateTime> Fecha { get; set; }

    }
}
