/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   ABEL QUISPE ORELLANA
'* FCH. CREACIÓN : 28/11/2016
**********************************************************/
namespace SGR.Models.Entity
{
    using System;
    public class ESGR_Documento
    {
        public string Opcion { get; set; }
        public ESGR_EmpresaSucursal ESGR_EmpresaSucursal { get; set; }
        public string CodDocumento { get; set; }
        public string Serie { get; set; }
        public string Descripcion { get; set; }
        public Nullable<short> Longitud { get; set; }
        public string Correlativo { get; set; }
        public Nullable<bool> Sunat { get; set; }
    }
}
