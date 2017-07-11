/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   ABEL QUISPE ORELLANA
'* FCH. CREACIÓN : 05/12/2016
**********************************************************/
namespace SGR.ViewModels.Presentation
{
    public static class SGRMessage
    {
        public static string TitleMessage { get { return "Sistema Gestión de Restaurant"; } }
        public static string AdministratorArea { get { return "Administración de Área."; } }
        public static string AdministratorEmpresa { get { return "Administración de Empresa."; } }
        public static string AdministratorEmpresaSucursal { get { return "Administración de Empresa Sucursal."; } }
        public static string AdministratorProducto { get { return "Administración de Producto."; } }
        public static string AdministratorTipoCambio { get { return "Administración de Tipo de Cambio."; } }
        public static string AdministratorDocumento { get { return "Administración de Documento."; } }
        public static string AdministratorProductoCategoria { get { return "Administración de Producto Categoría."; } }
        public static string AdministratorProductoSubCategoria { get { return "Administración de Producto Sub Categoría."; } }
        public static string AdministratorMoneda { get { return "Administración de Moneda."; } }
        public static string AdministratorMedioPago { get { return "Administración de Medio Pago."; } }
        public static string AdministratorCartaDia { get { return "Administración de Carta del Día."; } }
        public static string AdministratorPedidoTipo { get { return "Administración de Pedido Tipo."; } }
        public static string AdministratorPerfil { get { return "Administración de Perfil."; } }
        public static string AdministratorMesaArea { get { return "Administración de Área de Mesa."; } }
        public static string AdministratorMesa { get { return "Administración de Mesa."; } }
        public static string AdministratorUsuario { get { return "Administración de Usuario."; } }
        public static string AdministratorCliente { get { return "Administración de Cliente."; } }
        public static string AdministratorUsuarioEmpSucursal { get { return "Administración de Usuario Empresa Sucursal."; } }
        public static string AdministratorVenta { get { return "Administración de Ventas"; } }
        public static string AdministratorImpuesto { get { return "Administración de Impuesto"; } }
        public static string ListadoVentaDia { get { return "Listado de Venta por Dia"; } }
        public static string AdministracionCaja { get { return "Administracion de Caja"; } }
        public static string AdministracionAperturaCierreCaja { get { return "Administracion de Apertura/Cierre Caja"; } }
        public static string AdministracionMovimientoCaja { get { return "Administracion de Movimiento Caja"; } }
        public static string ListadoPedidoAnulado { get { return "Listado de Pedido Anulado"; } }
    }

    public static class SGRMessageContent
    {
        public static string ContentSelectNull { get { return "Seleccione un registro para proceder a "; } }
        public static string ContentSaveOK { get { return "Datos Guardados Correctamente"; } }
        public static string ContentDeleteOK { get { return "Registro Eliminado Correctamente"; } }
        public static string ContentCancelOK { get { return "Registro Anulado Correctamente"; } }
        public static string LogeoSuperoElNumeroIntentos { get { return "Supero el número de Intentos"; } }
        public static string LogeoLeQuedaUnIntento { get { return "Le queda un Intento"; } }
        public static string LogeoLeQueda { get { return "Le queda "; } }
        public static string LogeoContraseniaIncorrecta { get { return "Contraseña Incorrecta"; } }
        public static string ProcesandoDatos { get { return "Procesando Datos, Espere por favor..."; } }
        public static string DatosProcesados { get { return "Datos procesados correctamente."; } }
        public static string QuestionDelete { get { return "¿Seguro que desea eliminar el registro?"; } }
        public static string QuestionCancel { get { return "¿Seguro que desea anular el registro?"; } }
        public static string GetAccesoRestringido { get { return "Perfil de usuario no cuenta con acceso a este Formulario"; } }
    }
}
