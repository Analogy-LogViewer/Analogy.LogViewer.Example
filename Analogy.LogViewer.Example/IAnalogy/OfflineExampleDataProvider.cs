using Analogy.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Analogy.LogViewer.Example
{
    class OfflineExampleDataProvider : IAnalogyOfflineDataProvider
    {
        public bool DisableFilePoolingOption { get; } = false;
        public Guid Id { get; set; }
        public Image LargeImage { get; set; } = null;
        public Image SmallImage { get; set; } = null;

        public string OptionalTitle { get; set; }

        public bool CanSaveToLogFile { get; } = false;
        public string FileOpenDialogFilters { get; } = "None (*.none)|*.none";
        public string FileSaveDialogFilters { get; } = string.Empty;
        public IEnumerable<string> SupportFormats { get; } = new[] { "*.none" };
        public string InitialFolderFullPath { get; } = Environment.CurrentDirectory;
        public bool UseCustomColors { get; set; } = false;
        public IEnumerable<(string originalHeader, string replacementHeader)> GetReplacementHeaders()
            => Array.Empty<(string, string)>();

        public (Color backgroundColor, Color foregroundColor) GetColorForMessage(IAnalogyLogMessage logMessage)
            => (Color.Empty, Color.Empty);
        public OfflineExampleDataProvider(string prefix, Guid guid)
        {
            Id = guid;
            OptionalTitle = $"Analogy Example:  Offline Data Provider ({prefix})";
        }

        public Task InitializeDataProviderAsync(IAnalogyLogger logger)
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
            return false;
        }

        public bool CanOpenAllFiles(IEnumerable<string> fileNames)
        {
            return fileNames.All(CanOpenFile);
        }


    }
}
