/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   ABEL QUISPE ORELLANA
'* FCH. CREACIÓN : 28/11/2016
**********************************************************/
namespace SGR.Models.Entity
{
    using System;
    public class ESGR_CierreCaja_Detalle
    {
        public ESGR_CierreCaja ESGR_CierreCaja { get; set; }
        public byte Item { get; set; }
        public string TipoPago { get; set; }
        public string TipoTarjeta { get; set; }
        public string MndCodMnd { get; set; }
        public Nullable<decimal> Total { get; set; }

    }
}
