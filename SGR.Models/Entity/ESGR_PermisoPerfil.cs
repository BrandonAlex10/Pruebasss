/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   ABEL QUISPE ORELLANA
'* FCH. CREACIÓN : 28/11/2016
**********************************************************/
namespace SGR.Models.Entity
{
    public class ESGR_PermisoPerfil : ESGR_Privilegios
    {
        public int IdPermisoPerfil { get; set; }
        public ESGR_Perfil ESGR_Perfil { get; set; }
        public ESGR_Formulario ESGR_Formulario { get; set; }

    }
}
