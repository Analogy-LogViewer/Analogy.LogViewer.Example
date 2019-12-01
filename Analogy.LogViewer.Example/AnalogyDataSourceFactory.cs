using System;
using System.Collections.Generic;
using Analogy.Interfaces;
using Analogy.Interfaces.Factories;

namespace Analogy.LogViewer.Example
{
    public class AnalogyDataProviderFactory : IAnalogyDataProvidersFactory
    {
        public string Title => "Analogy Online example";

        public IEnumerable<IAnalogyDataProvider> Items => new List<IAnalogyDataProvider>
        {
            new AnalogyOnlineExampleDataProvider("Data Provider 1", new Guid("6642B160-F992-4120-B688-B02DE2E83256")),
            new AnalogyOnlineExampleDataProvider("Data Provider 2", new Guid("5AB690DC-545B-4150-B2CD-2534B2ACBF82"))
        };
    }
}
