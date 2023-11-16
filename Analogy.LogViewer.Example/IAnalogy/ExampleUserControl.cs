using Analogy.Interfaces;
using Analogy.Interfaces.DataTypes;
using Analogy.Interfaces.Factories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Analogy.LogViewer.Example.IAnalogy
{
    public class ExampleUserControlFactory : IAnalogyCustomUserControlsFactory
    {
        public Guid FactoryId { get; set; } = PrimaryFactory.Id;
        public string Title { get; set; } = "User Control Examples";

        public IEnumerable<IAnalogyCustomUserControl> UserControls { get; } = new List<IAnalogyCustomUserControl>
        {
            new ExampleUserControl(),
        };
    }

    public class ExampleUserControl :IAnalogyCustomUserControl
    {
        public Task InitializeUserControl(Control hostingControl, ILogger logger)
        {
            return Task.CompletedTask;
        }

        public Task UserControlRemoved()
        {
            return Task.CompletedTask;
        }

        public UserControl UserControl => new ExampleUserControlUC();
        public Guid Id { get; set; } = new Guid("ec1264aa-d503-4888-9772-572faa3f9a0c");
        public Image? SmallImage { get; set; }
        public Image? LargeImage { get; set; }
        public string Title { get; set; } = "Example User Control";
        public AnalogyToolTip? ToolTip { get; set; }
    }
}