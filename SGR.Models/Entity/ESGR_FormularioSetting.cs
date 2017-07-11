/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   ABEL QUISPE ORELLANA
'* FCH. CREACIÓN : 28/11/2016
**********************************************************/
namespace SGR.Models.Entity
{
    using System;
    public class ESGR_FormularioSetting
    {
        public int IdFormularioSetting { get; set; }
        public ESGR_Empresa ESGR_Empresa { get; set; }
        public ESGR_Formulario ESGR_Formulario { get; set; }
        public string Campo { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
    }
}
