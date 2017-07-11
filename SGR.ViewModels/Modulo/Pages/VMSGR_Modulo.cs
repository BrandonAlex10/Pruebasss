/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   OSCAR HUAMAN CABRERA
'* FCH. CREACIÓN : 01/12/2016
**********************************************************/

namespace SGR.ViewModels.Modulo.Pages
{
    using ComputerSystems;
    using ComputerSystems.WPF.Interfaces;
    using ComputerSystems.WPF.Notificaciones;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media;

    public class VMSGR_Modulo : CmpNavigationService, CmpIVariable
    {
        public object MyDataContext
        {
            set
            {
            }
        }

        private Frame _MyFrame;
        public Frame MyFrame
        {
            set
            {
                _MyFrame = value;
            }
        }

        #region COLECCIONES
        #endregion

        #region PROPERTY
        #endregion

        #region ICOMMAND
        #endregion

        #region METODOS
        #endregion
    }
}
