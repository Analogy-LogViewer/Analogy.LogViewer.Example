﻿using Analogy.Interfaces;
using Analogy.Interfaces.DataTypes;
using Analogy.LogViewer.Example.Properties;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Analogy.LogViewer.Example.IAnalogy
{
    public sealed class OfflineExampleDataProvider : Template.OfflineDataProvider
    {
        public override Guid Id { get; set; }
        public override Image? LargeImage { get; set; } = Resources.Analogy_image_32x32;
        public override Image? SmallImage { get; set; } = Resources.Analogy_image_16x16;

        public override string? OptionalTitle { get; set; } 
        public override string FileOpenDialogFilters { get; set; } = "None (*.none)|*.none";
        public override IEnumerable<string> SupportFormats { get; set; } = new[] { "*.none" };
        public override string? InitialFolderFullPath { get; set; } = Environment.CurrentDirectory;
        public override IEnumerable<(string OriginalHeader, string ReplacementHeader)> GetReplacementHeaders()
            => Array.Empty<(string, string)>();

        public override (Color BackgroundColor, Color ForegroundColor) GetColorForMessage(IAnalogyLogMessage logMessage)
            => (Color.Empty, Color.Empty);
        public OfflineExampleDataProvider(string prefix, Guid guid)
        {
            Id = guid;
            OptionalTitle = $"Analogy Example:  Offline Data Provider ({prefix})";
        }

        public override  Task InitializeDataProvider(ILogger logger)
        {
            //do some initialization for this provider
            return base.InitializeDataProvider(logger);
        }

        public override void MessageOpened(IAnalogyLogMessage message)
        {
            base.MessageOpened(message);
        }

        public override IEnumerable<string> HideAdditionalColumns()
        {
            return base.HideAdditionalColumns();
        }

        public override IEnumerable<AnalogyLogMessagePropertyName> HideExistingColumns()
        {
            return base.HideExistingColumns();
        }

        public override Task<IEnumerable<IAnalogyLogMessage>> Process(string fileName, CancellationToken token, ILogMessageCreatedHandler messagesHandler)
        {
            return Task.FromResult(new List<IAnalogyLogMessage>(0).AsEnumerable());
        }

        protected override List<FileInfo> GetSupportedFilesInternal(DirectoryInfo dirInfo, bool recursive)
        {
            return base.GetSupportedFilesInternal(dirInfo, recursive);
        }

        public override Task SaveAsync(List<IAnalogyLogMessage> messages, string fileName)
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