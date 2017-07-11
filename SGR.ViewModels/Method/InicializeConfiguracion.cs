using ComputerSystems.WPF;
using Newtonsoft.Json;
using SGR.Models;
using SGR.Models.Business;
using SGR.Models.Entity;
using SGR.ViewModels.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SGR.ViewModels.Method
{
    public static class InicializeConfiguracion
    {    
        /// <summary>
        /// Inicializa/desencripta los valores incriptados
        /// </summary>
        public async static void Load(Action ParameterTask = null)
        {
            await Task.Factory.StartNew(() =>
            {
                try
                {
                    var listaConfiguracion = new BSGR_Configuracion().GetCollectionConfiguracion();

                    //POLITICA DE SEGURIDAD
                    var objPoliticaSeguridad = listaConfiguracion.FirstOrDefault(x => x.IdConfig == "POLITICASEGURIDAD");
                    if (objPoliticaSeguridad != null)
                        SGRVariables.ESGR_PoliticaSeguridad = JsonConvert.DeserializeObject<ESGR_PoliticaSeguridad>(objPoliticaSeguridad.Valor);

                    //IMPUESTOS
                    var vrObjESGC_Retencion = listaConfiguracion.FirstOrDefault(x => x.IdConfig == "IGV");
                    if (vrObjESGC_Retencion != null)
                        SGRVariables.ESGR_Retencion.IGV = Convert.ToDecimal(vrObjESGC_Retencion.Valor);
                    
                    //IMPRESORA PEDIDO
                    var vrImpresoraPedido = listaConfiguracion.FirstOrDefault(x => x.IdConfig == "IMP");
                    if (vrImpresoraPedido != null)
                        SGRVariables.ImpresoraPedido = vrImpresoraPedido.Valor;

                    //IMPRESORA COPIA PEDIDO
                    var vrImpresoraCopiaPedido = listaConfiguracion.FirstOrDefault(x => x.IdConfig == "IPC");
                    if (vrImpresoraCopiaPedido != null)
                        SGRVariables.ImpresoraCopiaPedido = vrImpresoraCopiaPedido.Valor;

                    //IMPRESORA CAJA
                    var vrImpresoraCaja = listaConfiguracion.FirstOrDefault(x => x.IdConfig == "ICJ");
                    if (vrImpresoraCaja != null)
                        SGRVariables.ImpresoraCaja = vrImpresoraCaja.Valor;

                    //IMPRESORA CAJA
                    var vrNotificacion = listaConfiguracion.FirstOrDefault(x => x.IdConfig == "NTF");
                    if (vrNotificacion != null)
                        SGRVariables.NotificacionPreCuenta = vrNotificacion.Valor;

                    //MESA FIJA
                    var vrMesaFija = listaConfiguracion.FirstOrDefault(x => x.IdConfig == "MSA");
                    if (vrMesaFija != null)
                        SGRVariables.MesaFija = vrMesaFija.Valor == "SI";

                    //MESA PREDETERMINADA
                    var vrMesaPredeterminada = listaConfiguracion.FirstOrDefault(x => x.IdConfig == "MPD");
                    if (vrMesaPredeterminada != null)
                        SGRVariables.MesaPredeterminada = Convert.ToInt32(vrMesaPredeterminada.Valor);
                    
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        if (ParameterTask != null)
                            ParameterTask.Invoke();
                    });
                }
                catch (System.Exception ex)
                {
                    CmpMessageBox.Show(SGRMessage.TitleMessage, ex.Message, CmpButton.Aceptar);
                }
            });
        }
    }
}
