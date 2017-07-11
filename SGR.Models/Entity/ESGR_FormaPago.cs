/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   ABEL QUISPE ORELLANA
'* FCH. CREACIÓN : 28/11/2016
**********************************************************/
namespace SGR.Models.Entity
{
    using System;
    public class ESGR_FormaPago
    {
        public string Opcion { get; set; }
        public int IdFormaPago { get; set; }
        public ESGR_Empresa ESGR_Empresa { get; set; }
        public string FormaPago { get; set; }
    }
}
