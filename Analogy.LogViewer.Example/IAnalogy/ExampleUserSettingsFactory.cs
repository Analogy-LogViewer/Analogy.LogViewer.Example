using Analogy.Interfaces.WinForms.DataTypes;
using Analogy.LogViewer.Example.Properties;
using Analogy.LogViewer.Template.WinForms;
using Microsoft.Extensions.Logging;
using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Analogy.LogViewer.Example.IAnalogy
{
    public class ExampleUserSettingsFactory : TemplateUserSettingsFactoryWinForms
    {
        public override Guid FactoryId { get; set; } = ExamplePrimaryFactory.Id;
        public override Guid Id { get; set; } = new Guid("fe9d38dc-dd31-4f15-8aee-acb7f7e9085b");
        public override UserControl DataProviderSettings { get; set; }
        public override string Title { get; set; } = "Example User Settings";
        public override Image? SmallImage { get; set; } = Resources.Analogy_image_16x16;
        public override Image? LargeImage { get; set; } = Resources.Analogy_image_32x32;
        public override AnalogyToolTipWinForms? ToolTip { get; set; } = new AnalogyToolTipWinForms("Example tooltip", "some content",
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