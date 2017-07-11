using ComputerSystems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGR.Models.Entity
{
    public class ESGR_AperturaCierreCaja :CmpNotifyPropertyChanged
    {
        public string Opcion { get; set; }
        public int IdAperturaCierreCaja { get; set; }
        public ESGR_Caja ESGR_Caja { get; set; }
        public DateTime FechaApertura { get; set; }
        public DateTime FechaCierre { get; set; }
        public string strFechaCierre//para validar si existe Cierre
        {
            get
            {
                if (ESGR_Estado.CodEstado == "APTCJ")
                    return "----";
                else
                    return FechaCierre.ToString();
            }
        }
        private ESGR_Motivo _ESGR_Motivo;
        public ESGR_Motivo ESGR_Motivo
        {
            get
            {
                return _ESGR_Motivo;
            }
            set
            {
                _ESGR_Motivo = value;
                OnPropertyChanged("ESGR_Motivo");
            }
        }
        private ESGR_Estado _ESGR_Estado;
        public ESGR_Estado ESGR_Estado
        {
            get
            {
                return _ESGR_Estado;
            }
            set
            {
                _ESGR_Estado = value;
                OnPropertyChanged("ESGR_Estado");
            }
        }
        private ESGR_Usuario _ESGR_UsuarioCajero;
        public ESGR_Usuario ESGR_UsuarioCajero
        {
            get
            {
                return _ESGR_UsuarioCajero;
            }
            set
            {
                _ESGR_UsuarioCajero = value;
                OnPropertyChanged("ESGR_UsuarioCajero");
            }
        }
        private ESGR_Usuario _ESGR_UsuarioApertura;
        public ESGR_Usuario ESGR_UsuarioApertura
        {
            get
            {
                if (_ESGR_UsuarioApertura == null)
                    _ESGR_UsuarioApertura = SGRVariables.ESGR_Usuario;
                return _ESGR_UsuarioApertura;
            }
            set
            {
                _ESGR_UsuarioApertura = value;
                OnPropertyChanged("ESGR_UsuarioApertura");
            }
        }
        private string _Glosa;
        public string Glosa
        {
            get
            {
                return _Glosa;
            }
            set
            {
                _Glosa = value;
                OnPropertyChanged("Glosa");
            }
        }
        public string DetalleXML { get; set; }
    }
}
