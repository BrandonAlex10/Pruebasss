/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   ABEL QUISPE ORELLANA
'* FCH. CREACIÓN : 29/11/2016
**********************************************************/
namespace SGR.Models.Entity
{
    using ComputerSystems;
    using System;
    public class ESGR_TipoCambio : CmpNotifyPropertyChanged
    {
        public string Opcion { get; set; }
        public System.DateTime FechaTcb { get; set; }
        private ESGR_Moneda _ESGR_Moneda;
        public ESGR_Moneda ESGR_Moneda
        { 
            get 
            {
                if (_ESGR_Moneda == null)
                    _ESGR_Moneda = new ESGR_Moneda();
                return _ESGR_Moneda;
            } 
            set 
            {
                _ESGR_Moneda = value;
                OnPropertyChanged();
            }
        }
        public Nullable<decimal> BuyRate { get; set; }
        public Nullable<decimal> SelRate { get; set; }
    }
}
