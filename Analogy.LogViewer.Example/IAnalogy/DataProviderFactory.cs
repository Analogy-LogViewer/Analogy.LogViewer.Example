using Analogy.Interfaces;
using Analogy.Interfaces.Factories;
using System;
using System.Collections.Generic;

namespace Analogy.LogViewer.Example
{
    public class DataProviderFactory : IAnalogyDataProvidersFactory
    {
        public Guid FactoryId { get; set; } = PrimaryFactory.Id;
        public string Title { get; set; } = "Analogy Online example";

        public IEnumerable<IAnalogyDataProvider> DataProviders => new List<IAnalogyDataProvider>
        {
            //add 2 "real time data providers"
            new OnlineExampleDataProvider("Data Provider 1", new Guid("6642B160-F992-4120-B688-B02DE2E83256")),
            new OnlineExampleDataProvider("Data Provider 2", new Guid("5AB690DC-545B-4150-B2CD-2534B2ACBF82")),
            //add 2 "offline data providers"
            new OfflineExampleDataProvider("Data Provider 1", new Guid("2BFC1602-17EF-447D-8DDC-8A00F46C1CE1")),
            new OfflineExampleDataProvider("Data Provider 2", new Guid("B22D4BD1-10D7-460B-ADCE-849E73BCA91D")),

        };
    }
}
