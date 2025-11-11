using Analogy.Interfaces;
using Analogy.Interfaces.DataTypes;
using Analogy.Interfaces.Winforms.DataTypes;
using Analogy.LogViewer.Example.Properties;
using Analogy.LogViewer.Template;
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
    public class ExampleUserSettingsFactory : TemplateUserSettingsFactoryWinforms
    {
        public override Guid FactoryId { get; set; } = PrimaryFactory.Id;
        public override Guid Id { get; set; } = new Guid("fe9d38dc-dd31-4f15-8aee-acb7f7e9085b");
        public override UserControl DataProviderSettings { get; set; }
        public override string Title { get; set; } = "Example User Settings";
        public override Image? SmallImage { get; set; } = Resources.Analogy_image_16x16;
        public override Image? LargeImage { get; set; } = Resources.Analogy_image_32x32;
        public override AnalogyToolTipWinforms? ToolTip { get; set; } = new AnalogyToolTipWinforms("Example tooltip", "some content",
            "footer/title", Resources.Analogy_image_16x16, Resources.Analogy_image_32x32);

        public override void CreateUserControl(ILogger logger)
        {
            DataProviderSettings = new UserControl();
        }

        public override Task SaveSettingsAsync()
        {
            return Task.CompletedTask;
        }
    }
}