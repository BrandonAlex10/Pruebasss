/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   ABEL QUISPE ORELLANA
'* FCH. CREACIÓN : 29/11/2016
**********************************************************/
namespace SGR.Models
{
    using SGR.Models.Entity;
    using System;
    using System.Collections.Generic;
    using System.IO;

    public class SGRVariables
    {
        public static string ConectionString { get; set; }
        public static string ImpresoraCaja { get; set; }
        public static string ImpresoraPedido { get; set; }
        public static string ImpresoraCopiaPedido { get; set; }
        public static string NotificacionPreCuenta { get; set; }
        public static int MesaPredeterminada { get; set; }
        public static ESGR_Usuario ESGR_Usuario { get; set; }
        public static ESGR_PoliticaSeguridad ESGR_PoliticaSeguridad { get; set; }
        public static ESGR_Retencion _ESGR_Retencion;
        public static ESGR_Retencion ESGR_Retencion
        {
            get
            {
                if (_ESGR_Retencion == null)
                    _ESGR_Retencion = new ESGR_Retencion();
                return _ESGR_Retencion;
            }
            set
            {
                _ESGR_Retencion = value;
            }
        }

        public static string GetPathDirectoryLog { get { return Directory.GetCurrentDirectory(); } }

        public static bool MesaFija { get; set; }

        public static void SaveLog(string Menssage)
        {
            var vrDirectory = SGRVariables.GetPathDirectoryLog + @"\SGR " + DateTime.Now.Month + " - " + DateTime.Now.Year;
            if (!Directory.Exists(vrDirectory))
                Directory.CreateDirectory(vrDirectory);

            var File = vrDirectory + @"\" + "Log " + DateTime.Now.Day + "-" + DateTime.Now.Month + ".txt";
            using (StreamWriter writer = new StreamWriter(File, true))
            {
                writer.WriteLine(Menssage);
            }
        }
    }
}
