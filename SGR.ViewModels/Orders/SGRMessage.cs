/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   ABEL QUISPE ORELLANA
'* FCH. CREACIÓN : 05/12/2016
**********************************************************/
namespace SGR.ViewModels.Orders
{
    public static class SGRMessage
    {
        public static string TitleSGR { get { return "Sistema de Gestión para Restaurant."; } }
        public static string TitlePedido { get { return "Módulo de Pedido."; } }
        public static string BusquedaMesaDisponible { get { return "Busqueda de Mesas disponibles."; } }
        public static string BusquedaMesaAtendido { get { return "Busqueda de Mesas Atendidas."; } }
    }

    public static class SGRMessageContent
    {
        public static string ContentSelecedtNull { get { return "Seleccione un registro para proceder a "; } }
        public static string ContentQuestionCancel { get { return "¿Seguro que desea Cancelar el Pedido?"; } }
        public static string ContentSaveOK { get { return "Datos Guardados Correctamente."; } }
        public static string ContentDeleteOK { get { return "Registro Eliminado Correctamente."; } }
        public static string ContentCanceledOK { get { return "Pedido Cancelado Correctamente."; } }
        public static string GetAccesoRestringido { get { return "Perfil de usuario no cuenta con acceso a este Formulario"; } }
    }
}
