/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   ABEL QUISPE ORELLANA
'* FCH. CREACIÓN : 29/11/2016
**********************************************************/
namespace SGR.Models.Entity
{

    public class ESGR_MedioPago
    {
        public string Opcion { get; set; }
        public short IdMedioPago { get; set; }
        public ESGR_FormaPago ESGR_FormaPago { get; set; }
        public string MedioPago { get; set; }
        public int DiasCredito { get; set; }
    }
}
