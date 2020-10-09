using Analogy.Interfaces;
using Analogy.Interfaces.Factories;
using Analogy.LogViewer.Example.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace Analogy.LogViewer.Example
{
    public class PrimaryFactory : IAnalogyFactory
    {
        internal static Guid Id = new Guid("4B1EBC0F-64DD-44A1-BC27-79DBFC6384CC");
        public Guid FactoryId { get; set; } = Id;

        public string Title { get; set; } = "Analogy Examples";
        public Image? SmallImage { get; set; } = Resources.Analogy_image_16x16;
        public Image? LargeImage { get; set; } = Resources.Analogy_image_32x32;

        public IEnumerable<IAnalogyChangeLog> ChangeLog { get; set; } = new List<AnalogyChangeLog>
        {
            new AnalogyChangeLog("Update Analogy.Interface version to 2.2.0",AnalogChangeLogType.None, "Lior Banai",new DateTime(2020, 03, 30)),
            new AnalogyChangeLog("Update Analogy.Interface version to 2.1.7",AnalogChangeLogType.None, "Lior Banai",new DateTime(2019, 09, 14)),
            new AnalogyChangeLog("Create example implementation",AnalogChangeLogType.None, "Lior Banai",new DateTime(2019, 08, 15)),
            new AnalogyChangeLog("Add Thread ID",AnalogChangeLogType.None, "Lior Banai",new DateTime(2019, 08, 20)),
            new AnalogyChangeLog("Add File handler for online data source (aligned with new interface)",AnalogChangeLogType.None, "Lior Banai",new DateTime(2019, 09, 09)),
            new AnalogyChangeLog("Update new interface and add more than 1 data provider",AnalogChangeLogType.None, "Lior Banai",new DateTime(2019, 12, 01))
        };
        public IEnumerable<string> Contributors { get; set; } = new List<string> { "Lior Banai" };
        public string About { get; set; } = "Analogy Example Data Source";
    }
}
