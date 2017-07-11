/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   ABEL QUISPE ORELLANA
'* FCH. CREACIÓN : 28/11/2016
**********************************************************/
namespace SGR.Models.Entity
{
    using ComputerSystems;
    using System;
    public class ESGR_EmpresaSucursal : CmpNotifyPropertyChanged
    {
        public string Opcion { get; set; }
        public short IdEmpSucursal { get; set; }
        public ESGR_Empresa ESGR_Empresa { get; set; }
        private string _Sucursal;
        public string Sucursal
        {
            get
            {
                return _Sucursal;
            }
            set
            {
                _Sucursal = value;
                OnPropertyChanged("Sucursal");
            }
        }
        private bool _Principal;
        public bool Principal
        {
            get
            {
                return _Principal;
            }
            set
            {
                _Principal = value;
                OnPropertyChanged("Principal");
            }
        }

    }
}
