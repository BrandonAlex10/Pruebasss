/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   ABEL QUISPE ORELLANA
'* FCH. CREACIÓN : 02/01/2016
**********************************************************/
namespace SGR.ViewModels.Presentation.CartaDia.Controls
{
    using System.Collections.ObjectModel;

    public class VMSGR_CartaDiaDetalleCategoria
    {
        public string Categoria { get; set; }
        public bool ViewCategoria { get; set; }
        public ObservableCollection<VMSGR_CartaDiaDetalleSubCategoria> CollectionVMSGR_CartaDiaDetalleSubCategoria { get; set; }
    }

}