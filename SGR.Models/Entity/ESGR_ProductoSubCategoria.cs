/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   ABEL QUISPE ORELLANA
'* FCH. CREACIÓN : 28/11/2016
**********************************************************/
using ComputerSystems;
namespace SGR.Models.Entity
{
    public class ESGR_ProductoSubCategoria : CmpNotifyPropertyChanged
    {
        public ESGR_ProductoSubCategoria() 
        {
            ESGR_ProductoCategoria = new ESGR_ProductoCategoria();
        }

        public string Opcion { get; set; }
        public int IdSubCategoria { get; set; }
        public ESGR_ProductoCategoria ESGR_ProductoCategoria { get; set; }
        private string _SubCategoria;
        public string SubCategoria
        {
            get
            {
                return _SubCategoria;
            }
            set
            {
                _SubCategoria = value;
                OnPropertyChanged("SubCategoria");
            }
        }
       
    }
}
