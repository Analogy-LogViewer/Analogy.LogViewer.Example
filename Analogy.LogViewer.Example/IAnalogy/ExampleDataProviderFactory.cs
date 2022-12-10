using System;
using System.Collections.Generic;
using Analogy.Interfaces;
using Analogy.LogViewer.Template;

namespace Analogy.LogViewer.Example.IAnalogy
{
    public class ExampleDataProviderFactory : DataProvidersFactory
    {
        public override Guid FactoryId { get; set; } = PrimaryFactory.Id;
        public override string Title { get; set; } = "Analogy Online example";

        public override IEnumerable<IAnalogyDataProvider> DataProviders { get; set; } = new List<IAnalogyDataProvider>
        {
            //add 2 "real time data providers"
            new OnlineExampleDataProvider("Online Data Provider 1", new Guid("6642B160-F992-4120-B688-B02DE2E83256")), 
            new OnlineExampleDataProvider("Online Data Provider 2", new Guid("5AB690DC-545B-4150-B2CD-2534B2ACBF82")),
            //add 2 "offline data providers"
            new OfflineExampleDataProvider("Offline Data Provider 1", new Guid("2BFC1602-17EF-447D-8DDC-8A00F46C1CE1")),
            new OfflineExampleDataProvider("Offline Data Provider 2", new Guid("B22D4BD1-10D7-460B-ADCE-849E73BCA91D")),
          // new ServerSidePagingProvider()

        };
    }
}
