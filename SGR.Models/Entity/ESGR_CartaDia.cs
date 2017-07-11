/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   ABEL QUISPE ORELLANA
'* FCH. CREACIÓN : 28/11/2016
**********************************************************/
namespace SGR.Models.Entity
{
    using System;
    public class ESGR_CartaDia
    {
        public ESGR_CartaDia()
        {
            ESGR_EmpresaSucursal = new ESGR_EmpresaSucursal();
        }
        public string Opcion { get; set; }
        public int IdCartaDia { get; set; }
        public ESGR_EmpresaSucursal ESGR_EmpresaSucursal { get; set; }
        public Nullable<DateTime> Fecha { get; set; }
        public string DetalleXML { get; set; }

        
    }
}
