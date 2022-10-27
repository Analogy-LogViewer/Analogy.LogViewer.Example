using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Analogy.Interfaces;
using Analogy.Interfaces.DataTypes;
using Analogy.LogViewer.Example.Properties;
using Analogy.LogViewer.Template;

namespace Analogy.LogViewer.Example.IAnalogy
{
    public class ExampleUserSettingsFactory:TemplateUserSettingsFactory
    {
        public override Guid FactoryId { get; set; } = PrimaryFactory.Id;
        public override Guid Id { get; set; } = new Guid("fe9d38dc-dd31-4f15-8aee-acb7f7e9085b");
        public override UserControl DataProviderSettings { get; set; }
        public override string Title { get; set; } = "Example User Settings";
        public override Image? SmallImage { get; set; } = Resources.Analogy_image_16x16;
        public override Image? LargeImage { get; set; } = Resources.Analogy_image_32x32;


        public override AnalogyToolTip? ToolTip { get; set; } = new AnalogyToolTip("Example tooltip", "some content",
            "footer/title", Resources.Analogy_image_16x16, Resources.Analogy_image_32x32);

        public override void CreateUserControl(IAnalogyLogger logger)
        {
            DataProviderSettings = new UserControl();
        }

        public override Task SaveSettingsAsync()
        {
            return Task.CompletedTask;
        }

       
    }
}
