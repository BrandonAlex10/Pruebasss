/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   OSCAR HUAMAN CABRERA
'* FCH. CREACIÓN : 03/12/2016
**********************************************************/

namespace SGR.ViewModels.Presentation.AperturaCierreCaja.Pages
{
    using ComputerSystems;
    using ComputerSystems.MethodList;
    using ComputerSystems.WPF;
    using ComputerSystems.WPF.Acciones.Controles.ModalDialog;
    using ComputerSystems.WPF.Interfaces;
    using ComputerSystems.WPF.Notificaciones;
    using SGR.Models.Business;
    using SGR.Models.Entity;
    using SGR.ViewModels.Method;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Input;

    public class VMSGR_ListadoAperturaCierreCaja : CmpNavigationService, CmpINavigation
    {
        public object Parameter
        {
            set
            {
                Application.Current.Dispatcher.Invoke(async () =>
                {
                    var result = await MTSGR_GetPrivilege.Process(this.GetType(), SGRMessage.TitleMessage, SGRMessageContent.GetAccesoRestringido);
                    if (result == null)
                        Volver();
                    else
                    {
                        PropertyIsEnabledNuevo = result.Nuevo;
                        PropertyIsEnabledEditar = result.Editar;
                        PropertyIsEnabledEliminar = result.Eliminar;
                        MethodLoadDetail();
                    }
                });
            }
        }

        #region COLECCIONES

        private CmpObservableCollection<ESGR_AperturaCierreCaja> _CollectionESGR_AperturaCierreCaja;
        public CmpObservableCollection<ESGR_AperturaCierreCaja> CollectionESGR_AperturaCierreCaja
        {
            get
            {
                if (_CollectionESGR_AperturaCierreCaja == null)
                    _CollectionESGR_AperturaCierreCaja = new CmpObservableCollection<ESGR_AperturaCierreCaja>();
                return _CollectionESGR_AperturaCierreCaja;
            }
        }

        #endregion

        #region OBJETO SECUNDARIO

        private ESGR_AperturaCierreCaja _SelectedESGR_AperturaCierreCaja;
        public ESGR_AperturaCierreCaja SelectedESGR_AperturaCierreCaja
        {
            get
            {
                return _SelectedESGR_AperturaCierreCaja;
            }
            set
            {
                _SelectedESGR_AperturaCierreCaja = value;
                OnPropertyChanged("SelectedESGR_AperturaCierreCaja");
            }
        }

        #endregion

        #region PROPERTY

        private bool _PropertyIsEnabledNuevo;
        public bool PropertyIsEnabledNuevo
        {
            get
            {
                return _PropertyIsEnabledNuevo;
            }
            set
            {
                _PropertyIsEnabledNuevo = value;
                OnPropertyChanged("PropertyIsEnabledNuevo");
            }
        }

        private bool _PropertyIsEnabledEditar;
        public bool PropertyIsEnabledEditar
        {
            get
            {
                return _PropertyIsEnabledEditar;
            }
            set
            {
                _PropertyIsEnabledEditar = value;
                OnPropertyChanged("PropertyIsEnabledEditar");
            }
        }

        private bool _PropertyIsEnabledEliminar;
        public bool PropertyIsEnabledEliminar
        {
            get
            {
                return _PropertyIsEnabledEliminar;
            }
            set
            {
                _PropertyIsEnabledEliminar = value;
                OnPropertyChanged("PropertyIsEnabledEliminar");
            }
        }

        #endregion

        #region ICOMMAND

        public ICommand INuevo
        {
            get
            {
                return CmpICommand.GetICommand((T) =>
                {
                    var CollectionCaja = new BSGR_Caja().CollectionESGR_Caja().Where(x => x.ESGR_Estado.CodEstado == "CRDCJ").ToList();
                    if (CollectionCaja.Count == 0)
                    {
                        CmpMessageBox.Show(SGRMessage.AdministracionAperturaCierreCaja, "No hay caja disponible para aperturar", CmpButton.Aceptar);
                        return;
                    }
                    CmpModalDialog.GetContent().ShowDialog("MPSGR_AperturaCierreCaja", new ESGR_AperturaCierreCaja() { Opcion = "I", ESGR_Motivo = new ESGR_Motivo { CodMotivo = "SLI" } });
                    MethodLoadDetail();
                });
            }
        }

        public ICommand IView
        {
            get
            {
                return CmpICommand.GetICommand((T) =>
                {
                    SelectedESGR_AperturaCierreCaja.Opcion = "V";
                    CmpModalDialog.GetContent().ShowDialog("MPSGR_AperturaCierreCaja", SelectedESGR_AperturaCierreCaja);
                    MethodLoadDetail();
                });
            }
        }

        public ICommand ICerrarCaja
        {
            get
            {
                return CmpICommand.GetICommand((T) =>
                {
                    if (SelectedESGR_AperturaCierreCaja == null)
                        return;
                    if (SelectedESGR_AperturaCierreCaja.ESGR_Estado.CodEstado == "CRDCJ")
                    {
                        CmpMessageBox.Show(SGRMessage.AdministracionAperturaCierreCaja, "Esta caja ya ha sido cerrada.", CmpButton.Aceptar);
                        return;
                    }
                    else if (SelectedESGR_AperturaCierreCaja.ESGR_Estado.CodEstado == "ANUCJ")
                    {
                        CmpMessageBox.Show(SGRMessage.AdministracionAperturaCierreCaja, "Esta caja ha sido anulada.", CmpButton.Aceptar);
                        return;
                    }
                    SelectedESGR_AperturaCierreCaja.Opcion = "C";
                    CmpModalDialog.GetContent().ShowDialog("MPSGR_AperturaCierreCaja", SelectedESGR_AperturaCierreCaja);
                    MethodLoadDetail();
                });
            }
        }

        public ICommand IEliminar
        {
            get
            {
                return CmpICommand.GetICommand((T) =>
                {
                    if (SelectedESGR_AperturaCierreCaja == null)
                        return;
                    CmpMessageBox.Show(SGRMessage.AdministracionAperturaCierreCaja, SGRMessageContent.QuestionCancel, CmpButton.AceptarCancelar, () =>
                    {
                        SelectedESGR_AperturaCierreCaja.Opcion = "D";
                        try
                        {
                            new BSGR_AperturaCierreCaja().MethodTransaccionAperturaCaja(SelectedESGR_AperturaCierreCaja);
                            CmpMessageBox.Show(SGRMessage.AdministracionAperturaCierreCaja, SGRMessageContent.ContentCancelOK, CmpButton.Aceptar, () =>
                            {
                                MethodLoadDetail();
                            });
                        }
                        catch (Exception ex)
                        {
                            CmpMessageBox.Show(SGRMessage.AdministracionAperturaCierreCaja, ex.Message, CmpButton.Aceptar);
                        }
                    });
                });
            }
        }

        public ICommand IExportar
        {
            get
            {
                return CmpICommand.GetICommand((T) =>
                {
                    if (CollectionESGR_AperturaCierreCaja.Count == 0)
                    {
                        CmpMessageBox.Show(SGRMessage.ListadoVentaDia, "No hay Datos que Exportar", CmpButton.Aceptar);
                        return;
                    }
                    try
                    {
                        var ListarExcel = CollectionESGR_AperturaCierreCaja.Select(x => new
                        {
                            Estado = x.ESGR_Estado.Estado,
                            Motivo = x.ESGR_Motivo.Motivo,
                            FechaApertura = x.FechaApertura.ToString("dd/MM/yyyy"),
                            FechaCierre = x.FechaCierre.ToString("dd/MM/yyyy"),
                            Caja = x.ESGR_Caja.Descripcion,
                            Cajero = x.ESGR_UsuarioCajero.NombresApellidos,
                            Glosa = x.Glosa
                        }).ToList();
                        ListarExcel.Export("Apertura-Cierre Caja", ExportType.Excel, (value) =>
                        {
                            CmpMessageBox.Show(SGRMessage.ListadoVentaDia, value, CmpButton.Aceptar);
                        });
                    }
                    catch (Exception ex)
                    {
                        CmpMessageBox.Show(SGRMessage.ListadoVentaDia, ex.Message, CmpButton.Aceptar);
                    }
                });
            }
        }

        public ICommand ISalir
        {
            get
            {
                return CmpICommand.GetICommand((T) =>
                {
                    Volver();
                });
            }
        }

        #endregion

        #region METODOS

        public async void MethodLoadDetail()
        {
            await Task.Factory.StartNew(() =>
            {
                try
                {
                    CollectionESGR_AperturaCierreCaja.Source = new BSGR_AperturaCierreCaja().CollectionESGR_AperturaCierreCaja();
                }
                catch (Exception ex)
                {
                    CmpMessageBox.Show(SGRMessage.AdministracionAperturaCierreCaja, ex.Message, CmpButton.Aceptar);
                }
            });
        }

        #endregion
    }
}
