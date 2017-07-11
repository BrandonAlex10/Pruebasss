/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   ABEL QUISPE ORELLANA
'* FCH. CREACIÓN : 28/11/2016
**********************************************************/
namespace SGR.Models.Entity
{
    public class ESGR_UsuarioEmpresaSucursal
    {
        public ESGR_UsuarioEmpresaSucursal()
        {
            ESGR_EmpresaSucursal = new ESGR_EmpresaSucursal();
            ESGR_Usuario = new ESGR_Usuario();
        }
        public ESGR_Usuario ESGR_Usuario { get; set; }
        public ESGR_EmpresaSucursal ESGR_EmpresaSucursal { get; set; }
    }
}
