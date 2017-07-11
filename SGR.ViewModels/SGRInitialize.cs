/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   ABEL QUISPE ORELLANA
'* FCH. CREACIÓN : 20/12/2016
**********************************************************/
namespace SGR.ViewModels
{
    using ComputerSystems;
    using SGR.Models;
    using SGR.ViewModels.Method;
    using System.IO;

    public class SGRInitialize
    {
        public static void Component()
        {
            //SGRVariables.ConectionString = @"Data Source = DESKTOP-MUKJC5Q\SQLEXPRESS; DataBase = SGR_LOCAL; User Id = sa; Password = 123456";
            //SGRVariables.ConectionString = @"Data Source= DSK-SYSTEMS-03; DataBase = SGR_LOCAL; Integrated Security = true";
            //string strConexion = CmpCifrarObjecto.Encriptar("Data Source = devservercss.dyndns.org,1433; DataBase = SGR_ONOFRES; User Id = sysdemo; Password = sysadmin1");
            SGRVariables.ConectionString = CmpCifrarObjecto.Desencriptar((new System.IO.StreamReader(Directory.GetCurrentDirectory() + "\\conexion.txt")).ReadLine());
            InicializeConfiguracion.Load();
        }
    }
}