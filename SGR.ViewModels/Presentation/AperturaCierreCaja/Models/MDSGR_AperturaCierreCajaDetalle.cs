/*****************************************************************
 * CREADO POR : COMPUTER SYSTEMS SOLUTION
 *              CRISTIAN A. HERNANDEZ VILLO
 * FECHA CREA : 27/04/2017
 ****************************************************************/

namespace SGR.ViewModels.AperturaCierreCaja.Models
{
    using ComputerSystems;
    using SGR.Models.Entity;
    using System;
    using System.Linq;
    using System.Collections.ObjectModel;

    public class MDSGR_AperturaCierreCajaDetalle : ESGR_AperturaCierreCajaDetalle
    {
        #region COLECCIONES

        private ObservableCollection<ESGR_Moneda> _CollectionESGR_Moneda;
        public ObservableCollection<ESGR_Moneda> CollectionESGR_Moneda
        {
            get
            {
                if (_CollectionESGR_Moneda == null)
                    _CollectionESGR_Moneda = new ObservableCollection<ESGR_Moneda>();
                return _CollectionESGR_Moneda;
            }
            set
            {
                _CollectionESGR_Moneda = value;
                OnPropertyChanged("CollectionESGR_Moneda");
            }
        }

        private ObservableCollection<ESGR_MedioPago> _CollectionESGR_MedioPago;
        public ObservableCollection<ESGR_MedioPago> CollectionESGR_MedioPago
        {
            get
            {
                if (_CollectionESGR_MedioPago == null)
                    _CollectionESGR_MedioPago = new ObservableCollection<ESGR_MedioPago>();
                return _CollectionESGR_MedioPago;
            }
            set
            {
                _CollectionESGR_MedioPago = value;
                OnPropertyChanged("CollectionESGR_MedioPago");
            }
        }

        #endregion

        #region OBJETOS SECUNDARIOS

        private ESGR_Moneda _SelectedESGR_Moneda;
        public ESGR_Moneda SelectedESGR_Moneda
        {
            get
            {
                return _SelectedESGR_Moneda;
            }
            set
            {
                _SelectedESGR_Moneda = value;
                ESGR_Moneda = value;
                OnPropertyChanged("SelectedESGR_Moneda");
            }
        }

        private ESGR_MedioPago _SelectedESGR_MedioPago;
        public ESGR_MedioPago SelectedESGR_MedioPago
        {
            get
            {
                return _SelectedESGR_MedioPago;
            }
            set
            {
                _SelectedESGR_MedioPago = value;
                ESGR_MedioPago = value;
                OnPropertyChanged("SelectedESGR_MedioPago");
            }
        }

        #endregion
    }
}
