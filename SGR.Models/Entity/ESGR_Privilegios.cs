using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGR.Models.Entity
{
    public class ESGR_Privilegios
    {
        public bool Consulta { get; set; }
        public bool Nuevo { get; set; }
        public bool Editar { get; set; }
        public bool Eliminar { get; set; }

        //Inicialación de botones en grillas
        public static bool IsConsulta { get; set; }
        public static bool IsNuevo { get; set; }
        public static bool IsEditar { get; set; }
        public static bool IsEliminar { get; set; }
    }
}
