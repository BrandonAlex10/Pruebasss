/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   ABEL QUISPE ORELLANA
'* FCH. CREACIÓN : 02/01/2016
**********************************************************/
namespace SGR.ViewModels.Presentation.CartaDia.Controls
{
    using SGR.Models.Entity;
    using System.Collections.ObjectModel;
    using ComputerSystems;
    using System.Linq;
    using System.Windows.Threading;
    using System;

    public class VMSGR_CartaDiaDetalleSubCategoria :CmpNotifyPropertyChanged
    {
        private bool UpdateIsSelectedForDatail;

        public VMSGR_CartaDiaDetalleSubCategoria()
        {
            UpdateIsSelectedForDatail = true;

            var LogTimer = new DispatcherTimer();
            LogTimer.Interval = TimeSpan.FromMilliseconds(250);
            LogTimer.Tick += (s, e) =>
            {
                if (CollectionESGR_CartaDiaDetalle.ToList().Exists(x => !x.CartaDia))
                {
                    UpdateIsSelectedForDatail = false;
                    IsSelected = false;
                    UpdateIsSelectedForDatail = true;
                }
                else
                    IsSelected = true;
            };
            LogTimer.Start();
        }

        public string SubCategoria { get; set; }

        public bool _IsSelected;
        public bool IsSelected
        {
            get
            {
                return _IsSelected;
            }
            set
            {
                _IsSelected = value;

                if (UpdateIsSelectedForDatail)
                    CollectionESGR_CartaDiaDetalle.ToList().ForEach(x => { x.CartaDia = value; });
                OnPropertyChanged("IsSelected");
            }
        }

        private bool _IsExpanded;
        public bool IsExpanded
        {
            get
            {
                return _IsExpanded = CollectionESGR_CartaDiaDetalle.ToList().Exists(x => x.CartaDia);
            }
            set
            {
                _IsExpanded = value;
                OnPropertyChanged("IsExpanded");
            }
        }

        public ObservableCollection<ESGR_CartaDiaDetalle> CollectionESGR_CartaDiaDetalle { get; set; }
    }

}