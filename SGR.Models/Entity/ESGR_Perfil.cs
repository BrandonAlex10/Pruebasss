/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   ABEL QUISPE ORELLANA
'* FCH. CREACIÓN : 28/11/2016
**********************************************************/
namespace SGR.Models.Entity
{
    using ComputerSystems;
    using System;

    public class ESGR_Perfil : CmpNotifyPropertyChanged
    {
        public ESGR_Perfil()
        {
            ESGR_Modulo = new ESGR_Modulo();
        }

        public string Opcion { get; set; }
        private int _IdPerfil;
        public int IdPerfil
        {
            get
            {
                return _IdPerfil;
            }
            set
            {
                _IdPerfil = value;
                OnPropertyChanged("IdPerfil");
            }
        }
        private ESGR_Modulo _ESGR_Modulo;
        public ESGR_Modulo ESGR_Modulo
        {
            get
            {
                return _ESGR_Modulo;
            }
            set
            {
                _ESGR_Modulo = value;
                OnPropertyChanged("ESGR_Modulo");
            }
        }
        public string NombrePerfil { get; set; }
        public string Descripcion { get; set; }
        public string XMLPermisoPerfil { get; set; }

    }
}
