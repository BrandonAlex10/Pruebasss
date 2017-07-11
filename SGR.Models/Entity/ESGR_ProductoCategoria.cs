/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   ABEL QUISPE ORELLANA
'* FCH. CREACIÓN : 28/11/2016
**********************************************************/
namespace SGR.Models.Entity
{
    using System;
    using System.Collections.Generic;
    public class ESGR_ProductoCategoria
    {
        public String Opcion { get; set; }
        public int IdCategoria { get; set; }
        public string Categoria { get; set; }
        public string Impresora { get; set; }
        public bool ValidaStock { get; set; }
        
    }
}
