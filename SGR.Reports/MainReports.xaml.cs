using MahApps.Metro.Controls;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SGR.Reports
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainReports :Window
    {
        public MainReports()
        {
            InitializeComponent();
        }
        public void InitializeComponet(string NameReport, string DataSource, object List, string[] Parametro)
        {
            _reportViewer.Reset();
            _reportViewer.LocalReport.DataSources.Clear();
            _reportViewer.ProcessingMode = ProcessingMode.Local;
            _reportViewer.LocalReport.ReportEmbeddedResource = NameReport;
            _reportViewer.LocalReport.DataSources.Add(new ReportDataSource(DataSource,List));
            _reportViewer.SetDisplayMode(DisplayMode.PrintLayout);
            _reportViewer.ZoomMode = ZoomMode.Percent;
            if (Parametro != null)
            {
                ReportParameter[] Parameter = new ReportParameter[Parametro.Length];
                int Items = 0;
                foreach (var Parameters in Parametro)
                {
                    string[] ParametersList = Parameters.Split("|".ToCharArray());
                    Parameter[Items] = new ReportParameter(ParametersList[0], ParametersList[1]);
                    Items++;
                }
                _reportViewer.LocalReport.SetParameters(Parameter);
            }
        }
    }
}
