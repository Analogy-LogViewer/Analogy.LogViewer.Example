using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Analogy.LogViewer.Example.IAnalogy
{
    public partial class ExampleUserControlUC : UserControl
    {
        private ExampleOnDemandPlotting p;

        public ExampleUserControlUC()
        {
            InitializeComponent();
        }

        private void btnGenerator_Click(object sender, EventArgs e)
        {
            p = new ExampleOnDemandPlotting();
            OnDemandPlottingContainer.Instance.AddOnDemandPlotting(p);
        }

        private void btnGneratorShow_Click(object sender, EventArgs e)
        {
            p.StartPlotting();
        }

        private void btnGeneratorHide_Click(object sender, EventArgs e)
        {
            p.HidePlot();
        }

        private void btnStopPlotting_Click(object sender, EventArgs e)
        {
            p.StopPlotting();
        }

        private void btnShowPlot_Click(object sender, EventArgs e)
        {
            p.ShowPlot();
        }
    }
}