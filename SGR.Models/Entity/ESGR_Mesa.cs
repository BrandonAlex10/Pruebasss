/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   ABEL QUISPE ORELLANA
'* FCH. CREACIÓN : 28/11/2016
**********************************************************/

namespace SGR.Models.Entity
{
    using System.Windows;

    public class ESGR_Mesa
    {
        public string Opcion { get; set; }
        public int IdMesa { get; set; }
        public ESGR_MesaArea EESGR_MesaArea { get; set; }
        public string Mesa { get; set; }
        public ESGR_Estado ESGR_Estado { get; set; }
        public ESGR_Usuario ESGR_Usuario { get; set; }
        public string Identificador { get; set; }
        public string DetalleMesaXML { get; set; }
        public int IdPedido { get; set; }
        public Visibility PropertyVisibilityTextBlockNombreMesa
        {
            get
            {
                return (ESGR_Estado != null && ESGR_Estado.CodEstado == "RVPED") ? Visibility.Visible : Visibility.Collapsed;
            }
        }
    }
}
