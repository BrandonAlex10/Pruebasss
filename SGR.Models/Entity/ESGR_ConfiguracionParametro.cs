using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGR.Models.Entity
{
    public class ESGR_PoliticaSeguridad
    {
        public int ExpiracionClave { get; set; }
        public int UtilizarClaveAnterior { get; set; }
        public int MaximoClave { get; set; }
        public int MinimoClave { get; set; }
        public int MaximoIntento { get; set; }
        public int MinimoLetrasClave { get; set; }
        public int MinimoDigitosClaves { get; set; }
        public int MinimoCaracterEspecial { get; set; }
        public int MinimoLetraMayuscula { get; set; }

        public bool ValidaPolitica { get; set; }
    }

    public class ESGR_Retencion
    {
        public decimal IGV { get; set; }
    }
}
