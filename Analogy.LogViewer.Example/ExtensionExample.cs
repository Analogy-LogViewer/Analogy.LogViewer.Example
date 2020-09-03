using Analogy.Interfaces;
using Analogy.Interfaces.Factories;
using System;
using System.Collections.Generic;

namespace Analogy.LogViewer.Example
{
    public class ExtensionFactoryExample : IAnalogyExtensionsFactory
    {
        public Guid FactoryId { get; } = ExampleFactory.Id;
        public string Title { get; } = "Extension Example";
        public IEnumerable<IAnalogyExtension> Extensions { get; } = new List<IAnalogyExtension> { new ExtensionExample() };
    }


    public class ExtensionExample : IAnalogyExtension
    {
        public Guid Id { get; } = new Guid("8F66A278-CC9C-4643-8045-165572FF17D4");
        public Guid TargetProviderId { get; } = new Guid("6642B160-F992-4120-B688-B02DE2E83256");
        public string Author { get; } = "Lior Banai";
        public string AuthorMail { get; } = "LiorBanai@gmail.com";
        public List<string> AdditionalContributors { get; } = new List<string>(0);
        public string Title { get; } = "Extension Example";
        public string Description { get; } = "Example how to use extension columns (AnalogyExtensionType.InPlace)";
        public AnalogyExtensionType ExtensionType { get; } = AnalogyExtensionType.InPlace;

        public void CellClicked(object sender, AnalogyCellClickedEventArgs args)
        {
            //
        }

        public object GetValueForCellColumn(AnalogyLogMessage message, string columnName)
        {
            return "None";
        }

        public List<AnalogyColumnInfo> GetColumnsInfo()
        {
            return new List<AnalogyColumnInfo> { new AnalogyColumnInfo("Test", "ExampleTest", typeof(string)) };
        }

        public void NewMessage(AnalogyLogMessage message)
        {
            //
        }

        public void NewMessages(List<AnalogyLogMessage> messages)
        {
            //
        }


    }
}
