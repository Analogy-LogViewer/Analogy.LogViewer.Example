using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using Analogy.Interfaces;
using Analogy.Interfaces.DataTypes;
using Analogy.Interfaces.Factories;
using Microsoft.Extensions.Logging;

namespace Analogy.LogViewer.Example.IAnalogy
{
    public class ExtensionFactoryExample : IAnalogyExtensionsFactory
    {
        public Guid FactoryId { get; set; } = PrimaryFactory.Id;
        public string Title { get; set; } = "Extension Example";
        public IEnumerable<IAnalogyExtension> Extensions { get; } = new List<IAnalogyExtension> { new ExtensionInPlaceExample(), new ExtensionUserControlExample() };
    }


    public class ExtensionInPlaceExample : IAnalogyExtensionInPlace
    {
        public Guid Id { get; set; } = new Guid("8F66A278-CC9C-4643-8045-165572FF17D4");
        public Guid TargetComponentId { get; set; } = new Guid("6642B160-F992-4120-B688-B02DE2E83256");
        public string Author { get; set; } = "Lior Banai";
        public string AuthorMail { get; set; } = "LiorBanai@gmail.com";
        public List<string> AdditionalContributors { get; } = new List<string>(0);
        public string Title { get; set; } = "Extension Example";
        public string Description { get; set; } = "Example how to use extension columns (AnalogyExtensionType.InPlace)";

        public void CellClicked(object sender, AnalogyCellClickedEventArgs args)
        {
            //
        }

        public object GetValueForCellColumn(IAnalogyLogMessage message, string columnName)
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
    public class ExtensionUserControlExample : IAnalogyExtensionUserControl
    {
        public Guid Id { get; set; } = new Guid("34c45425-8acd-4f8e-b901-d234297fe3ec");
        public Guid TargetComponentId { get; set; } = new Guid("6642B160-F992-4120-B688-B02DE2E83256");
        public string Author { get; set; } = "Lior Banai";
        public string AuthorMail { get; set; } = "LiorBanai@gmail.com";
        public List<string> AdditionalContributors { get; } = new List<string>(0);
        public string Title { get; set; } = "Extension Example";
        public string Description { get; set; } = "Example how to use extension User Control";
        private UserControl UserControl { get; set; }

        Task IAnalogyExtensionUserControl.InitializeUserControl(Control hostingControl, Guid logWindowsId, ILogger logger)
        {
            return Task.CompletedTask;
        }
        public UserControl CreateUserControl(Guid logWindowsId,ILogger logger)
        {
            UserControl = new UserControlExtensionExample();
            return UserControl;
        }
        public UserControl GetUserControl(Guid logWindowsId)
        {
            return UserControl;
        }
        public void NewMessage(IAnalogyLogMessage message, Guid logWindowsId)
        {
            (UserControl as UserControlExtensionExample)?.UserClickMessage(message);
        }

        public void NewMessages(List<IAnalogyLogMessage> messages, Guid logWindowsId)
        {
            //
        }


    }

}
