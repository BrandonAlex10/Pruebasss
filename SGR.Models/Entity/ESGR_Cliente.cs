/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   ABEL QUISPE ORELLANA
'* FCH. CREACIÓN : 28/11/2016
**********************************************************/
namespace SGR.Models.Entity
{
    using ComputerSystems;
    using System;
    using System.Collections.ObjectModel;
    public class ESGR_Cliente : CmpNotifyPropertyChanged
    {
        public string Opcion { get; set; }
        public int IdCliente { get; set; }
        public string DocIdentidad { get; set; }
        private string _NroDocIdentidad;
        public string NroDocIdentidad 
        { 
            get
            {
                return _NroDocIdentidad;
            }
            set
            {
                _NroDocIdentidad = value;
                OnPropertyChanged("NroDocIdentidad");
            }
        }
        private string _Cliente;
        public string Cliente
        {
            get
            {
                return _Cliente;
            }
            set
            {
                _Cliente = value;
                OnPropertyChanged("Cliente");
            }
        }
        private string _Direccion;
        public string Direccion 
        { 
            get
            {
                return _Direccion;
            }
            set 
            {
                _Direccion = value;
                OnPropertyChanged("Direccion");
            }
        }
        public string Localidad { get; set; }
        private ObservableCollection<ESGR_Item> _Telefono;
        public ObservableCollection<ESGR_Item> Telefono 
        {
            get 
            {
                if (_Telefono == null)
                    _Telefono = new ObservableCollection<ESGR_Item>();
                return _Telefono; 
            }
            set
            {
                _Telefono = value;
                OnPropertyChanged("Telefono");
            }
        }
        public string Correo { get; set; }
        private Nullable<System.DateTime> _FechaNacimiento;
        public Nullable<System.DateTime> FechaNacimiento 
        {
            get
            {
                return _FechaNacimiento;
            }
            set
            {
                _FechaNacimiento = value;
                OnPropertyChanged("FechaNacimiento");
            } 
        }
    }
}
