/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   ABEL QUISPE ORELLANA
'* FCH. CREACIÓN : 28/11/2016
**********************************************************/
namespace SGR.Models.Entity
{
    using System;
    public class ESGR_CierreCaja
    {
        public int IdCaja { get; set; }
        public Nullable<System.DateTime> FechaCierre { get; set; }
        public Nullable<int> IdUsuarioCierre { get; set; }
        public Nullable<decimal> SaldoF_SOL { get; set; }
        public Nullable<decimal> SaldoF_USD { get; set; }
        public Nullable<int> IdUsuarioSuper { get; set; }
        public Nullable<decimal> Ajuste_SOL { get; set; }
        public Nullable<decimal> Ajuste_USD { get; set; }
    }
}
