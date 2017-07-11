/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   OSCAR HUAMÁN CABRERA
'* FCH. CREACIÓN : 30/05/2017
**********************************************************/

namespace SGR.ViewModels.Method
{
    using ComputerSystems.WPF;
    using SGR.Models;
    using SGR.Models.Business;
    using SGR.Models.Entity;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class MTSGR_GetPrivilege
    {
        public static async Task<ESGR_PermisoPerfil> Process(Type Formulario, string titleMensaje, string mensaje)
        {
            var result = await Task.Run(() =>
            {
                var ListPrivileges = new List<ESGR_PermisoPerfil>();
                try
                {
                    string nameFormulario = Formulario.Name;
                    ListPrivileges = new BSGR_PermisoPerfil().GetCollectionPermisoPerfil(SGRVariables.ESGR_Usuario.ESGR_Perfil.IdPerfil).ToList();

                    SGRVariables.ESGR_Usuario.ListESGR_PermisoPerfil = ListPrivileges;

                    //Obtenemos prefijo
                    var ArrayPrefijo = nameFormulario.Split("_".ToArray());
                    var prefijo = (ArrayPrefijo.Count() > 0) ? ArrayPrefijo[0] : string.Empty;
                    prefijo = prefijo.Substring(prefijo.Length - 3, 3);

                    var vrObjEPermisoPerfil = SGRVariables.ESGR_Usuario.ListESGR_PermisoPerfil.Where(x => x.ESGR_Formulario.ESGR_Modulo.Prefijo == prefijo && x.ESGR_Formulario.NombreFormulario == nameFormulario && x.Consulta).FirstOrDefault();
                    if (vrObjEPermisoPerfil == null)
                    {
                        bool StatusShowMessage = true;
                        CmpMessageBox.Show(titleMensaje, mensaje, CmpButton.Aceptar, () =>
                        {
                            StatusShowMessage = false;
                        });
                        while (StatusShowMessage) { }
                    }
                    return vrObjEPermisoPerfil;
                }
                catch (Exception)
                {
                    throw;
                }
            });
            return result;
        }
    }
}
