using System;
using System.Collections.Generic;
using System.Drawing;
using Analogy.Interfaces;
using Analogy.LogViewer.Example.Properties;

namespace Analogy.LogViewer.Example.IAnalogy
{
    public class PrimaryFactory : Analogy.LogViewer.Template.PrimaryFactory
    {
        internal static readonly Guid Id = new Guid("4B1EBC0F-64DD-44A1-BC27-79DBFC6384CC");
        public override Guid FactoryId { get; set; } = Id;

        public override string Title { get; set; } = "Analogy Examples";
        public override Image? SmallImage { get; set; } = Resources.Analogy_image_16x16;
        public override Image? LargeImage { get; set; } = Resources.Analogy_image_32x32;

        public override IEnumerable<IAnalogyChangeLog> ChangeLog { get; set; } = new List<AnalogyChangeLog>
        {
            new AnalogyChangeLog("Update Analogy.Interface version to 2.8.0",AnalogChangeLogType.None, "Lior Banai",new DateTime(2020, 10, 24), ""),
            new AnalogyChangeLog("Update Analogy.Interface version to 2.2.0",AnalogChangeLogType.None, "Lior Banai",new DateTime(2020, 03, 30), ""),
            new AnalogyChangeLog("Update Analogy.Interface version to 2.1.7",AnalogChangeLogType.None, "Lior Banai",new DateTime(2019, 09, 14), ""),
            new AnalogyChangeLog("Create example implementation",AnalogChangeLogType.None, "Lior Banai",new DateTime(2019, 08, 15), ""),
            new AnalogyChangeLog("Add Thread ID",AnalogChangeLogType.None, "Lior Banai",new DateTime(2019, 08, 20), ""),
            new AnalogyChangeLog("Add File handler for online data source (aligned with new interface)",AnalogChangeLogType.None, "Lior Banai",new DateTime(2019, 09, 09), ""),
            new AnalogyChangeLog("Update new interface and add more than 1 data provider",AnalogChangeLogType.None, "Lior Banai",new DateTime(2019, 12, 01), "")
        };
        public override IEnumerable<string> Contributors { get; set; } = new List<string> { "Lior Banai" };
        public override string About { get; set; } = "Analogy Example Data Source";
    }
}
