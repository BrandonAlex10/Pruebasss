/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   ABEL QUISPE ORELLANA
'* FCH. CREACIÓN : 28/11/2016
**********************************************************/
namespace SGR.Models.Entity
{
    using ComputerSystems;
    using System;
    using System.Collections.Generic;
    public class ESGR_Usuario : CmpNotifyPropertyChanged
    {
        public ESGR_Usuario()
        {
            ESGR_Perfil = new ESGR_Perfil();
            ESGR_Empresa = new ESGR_Empresa();
            ESGR_EmpresaSucursal = new ESGR_EmpresaSucursal();
        }

        public string Opcion { get; set; }
        public int IdUsuario { get; set; }
        private ESGR_Perfil _ESGR_Perfil;
        public ESGR_Perfil ESGR_Perfil
        {
            get
            {
                return _ESGR_Perfil;
            }
            set
            {
                _ESGR_Perfil = value;
                OnPropertyChanged("SGR_Perfil");
            }
        }
        public List<ESGR_PermisoPerfil> ListESGR_PermisoPerfil { get; set; }
        public ESGR_Empresa ESGR_Empresa { get; set; }
        public ESGR_EmpresaSucursal ESGR_EmpresaSucursal { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Correo { get; set; }
        public string Usuario { get; set; }
        public string Contrasenia { get; set; }
        public string Nick { get; set; }
        public Nullable<byte> Estado { get; set; }
        public int Fallido { get; set; }
        public bool FlgConectado { get; set; }
        public bool FlgEliminado { get; set; }

        public string CadenaSucursalXML { get; set; }
        public string CadenaAreaXML { get; set; }
        public string CadenaHabilitar { get; set; }
        public bool Habilitar { get; set; }
        public string NombresApellidos
        {
            get
            {
                return Nombres + " " + Apellidos;
            }
        }
    }
}
