using Analogy.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Analogy.LogViewer.Example.Properties;
using Analogy.LogViewer.Template;

namespace Analogy.LogViewer.Example
{
    class OfflineExampleDataProvider : OfflineDataProvider
    {
        public override Guid Id { get; set; }
        public override Image? LargeImage { get; set; } = Resources.Analogy_image_32x32;
        public override Image? SmallImage { get; set; } = Resources.Analogy_image_16x16;

        public override string? OptionalTitle { get; set; } 
        public override string FileOpenDialogFilters { get; set; } = "None (*.none)|*.none";
        public override IEnumerable<string> SupportFormats { get; set; } = new[] { "*.none" };
        public override string InitialFolderFullPath { get;  } = Environment.CurrentDirectory;
        public override IEnumerable<(string originalHeader, string replacementHeader)> GetReplacementHeaders()
            => Array.Empty<(string, string)>();

        public override (Color backgroundColor, Color foregroundColor) GetColorForMessage(IAnalogyLogMessage logMessage)
            => (Color.Empty, Color.Empty);
        public OfflineExampleDataProvider(string prefix, Guid guid)
        {
            Id = guid;
            OptionalTitle = $"Analogy Example:  Offline Data Provider ({prefix})";
        }

        public override  Task InitializeDataProviderAsync(IAnalogyLogger logger)
        {
            //do some initialization for this provider
            return base.InitializeDataProviderAsync(logger);
        }

        public override  void MessageOpened(AnalogyLogMessage message)
        {
            //nop
        }

        public override Task<IEnumerable<AnalogyLogMessage>> Process(string fileName, CancellationToken token, ILogMessageCreatedHandler messagesHandler)
        {
            return Task.FromResult(new List<AnalogyLogMessage>(0).AsEnumerable());
        }

        protected override List<FileInfo> GetSupportedFilesInternal(DirectoryInfo dirInfo, bool recursive)
        {
            return base.GetSupportedFilesInternal(dirInfo, recursive);
        }

        public override Task SaveAsync(List<AnalogyLogMessage> messages, string fileName)
        {
            return Task.CompletedTask;
        }

        public override bool CanOpenFile(string fileName)
        {
            return false;
        }

        public override bool CanOpenAllFiles(IEnumerable<string> fileNames)
        {
            return fileNames.All(CanOpenFile);
        }


    }
}
