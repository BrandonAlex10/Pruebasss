/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   ABEL QUISPE ORELLANA
'* FCH. CREACIÓN : 28/11/2016
**********************************************************/
namespace SGR.Models.Entity
{
    using System;

    public class ESGR_Empresa
    {
        public string Opcion { get; set; }
        public int IdEmpresa { get; set; }
        public string RazonSocial { get; set; }
        public string Ruc { get; set; }
        public string DireccionFiscal { get; set; }
        public string Rubro { get; set; }
        public string Telefono { get; set; }
        public string RegimenTributario { get; set; }
        public string NombreComercial { get; set; }
        public string RepresentanteLegal { get; set; }
        public Nullable<int> IdCliProveedor { get; set; }
        public Nullable<bool> ExoneradoIGV { get; set; }
    }
}
