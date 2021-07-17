using Analogy.Interfaces;
using System.Windows.Forms;

namespace Analogy.LogViewer.Example.IAnalogy
{
    public partial class UserControlExtensionExample : UserControl
    {
        private ExampleOnDemandPlotting p;
        public UserControlExtensionExample()
        {
            InitializeComponent();
        }

        public void UserClickMessage(AnalogyLogMessage msg) => lblMsg.Text = msg.Text;

        private void btnGneratorShow_Click(object sender, System.EventArgs e)
        {
            p.StartPlotting();

        }

        private void btnGenerator_Click(object sender, System.EventArgs e)
        {
            p = new ExampleOnDemandPlotting();
            OnDemandPlottingContainer.Instance.AddOnDemandPlotting(p);
            p.ShowPlot();
        }
    }
}
