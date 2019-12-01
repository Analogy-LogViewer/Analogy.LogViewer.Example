using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Analogy.Interfaces;

namespace Analogy.LogViewer.Example
{
    class OfflineExampleDataProvider : IAnalogyOfflineDataProvider
    {

        public Guid ID { get; }
        public string OptionalTitle { get; }

        public bool CanSaveToLogFile { get; } = false;
        public string FileOpenDialogFilters { get; } = "All supported Analogy log file types|*.log;*.json|Plain Analogy XML log file (*.log)|*.log|Analogy JSON file (*.json)|*.json";
        public string FileSaveDialogFilters { get; }=String.Empty;
        public IEnumerable<string> SupportFormats { get; } = new[] { "*.log", "*.json" };
        public string InitialFolderFullPath { get; } = Environment.CurrentDirectory;

        public OfflineExampleDataProvider(string prefix, Guid guid)
        {
            ID = guid;
            OptionalTitle = $"Analogy Example:  Offline Data Provider ({prefix})";
        }

        public Task InitializeDataProviderAsync()
        {
            return Task.CompletedTask;
        }

        public void MessageOpened(AnalogyLogMessage message)
        {
            //nop
        }

        public Task<IEnumerable<AnalogyLogMessage>> Process(string fileName, CancellationToken token, ILogMessageCreatedHandler messagesHandler)
        {
            return Task.FromResult(new List<AnalogyLogMessage>(0).AsEnumerable());
        }

        public IEnumerable<FileInfo> GetSupportedFiles(DirectoryInfo dirInfo, bool recursiveLoad)
        {
           return new List<FileInfo>(0);
        }

        public Task SaveAsync(List<AnalogyLogMessage> messages, string fileName)
        {
           return Task.CompletedTask;
        }

        public bool CanOpenFile(string fileName)
        {
            return true;
        }

        public bool CanOpenAllFiles(IEnumerable<string> fileNames)
        {
            return fileNames.All(CanOpenFile);
        }

    }
}
