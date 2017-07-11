/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   ABEL QUISPE ORELLANA
'* FCH. CREACIÓN : 28/11/2016
**********************************************************/
namespace SGR.Models.Entity
{
    using System;
    public class ESGR_Formulario : ESGR_Privilegios
    {
        public string CodFormulario { get; set; }
        public ESGR_Modulo ESGR_Modulo { get; set; }
        public string NombreFormulario { get; set; }
        public string Descripcion { get; set; }
    }
}
