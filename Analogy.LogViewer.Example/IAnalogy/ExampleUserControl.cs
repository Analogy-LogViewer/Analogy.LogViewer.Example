using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Analogy.Interfaces;
using Analogy.Interfaces.DataTypes;

namespace Analogy.LogViewer.Example.IAnalogy
{
    public class ExampleUserControl :IAnalogyCustomUserControl
    {
        public Task InitializeUserControl(Control hostingControl, IAnalogyLogger logger)
        {
            return Task.CompletedTask;
        }

        public Task UserControlRemoved()
        {
            return Task.CompletedTask;
        }

        public UserControl UserControl { get; } = new ExampleUserControlUC();
        public Guid Id { get; set; } = new Guid("ec1264aa-d503-4888-9772-572faa3f9a0c");
        public Image? SmallImage { get; set; }
        public Image? LargeImage { get; set; }
        public string Title { get; set; } = "Example User Control";
        public AnalogyToolTip? ToolTip { get; set; }
    }
}
