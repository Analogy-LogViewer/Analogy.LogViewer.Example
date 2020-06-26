﻿using Analogy.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;

namespace Analogy.LogViewer.Example
{
    class OnlineExampleDataProvider : IAnalogyRealTimeDataProvider
    {
        public string OptionalTitle { get; }
        public Guid ID { get; }

        public bool AutoStartAtLaunch => true;
        public bool IsConnected => true;
        public event EventHandler<AnalogyDataSourceDisconnectedArgs> OnDisconnected;
        public event EventHandler<AnalogyLogMessageArgs> OnMessageReady;
        public event EventHandler<AnalogyLogMessagesArgs> OnManyMessagesReady;

        public async Task<bool> CanStartReceiving() => await Task.FromResult(true);

        public IAnalogyOfflineDataProvider FileOperationsHandler { get; }
        private Timer SimulateOnlineMessages;
        private int messageCount = 0;
        readonly Random random = new Random();
        readonly Array values = Enum.GetValues(typeof(AnalogyLogLevel));
        private readonly List<string> processes = Process.GetProcesses().Select(p => p.ProcessName).ToList();
        private readonly string prefixMessage;
        private Random rnd = new Random();
        private IAnalogyLogger Logger { get; set; }
        public bool UseCustomColors { get; set; } = true;

        public IEnumerable<(string originalHeader, string replacementHeader)> GetReplacementHeaders()
        {
            yield return ("Category", "Test");
        }

        public (Color backgroundColor, Color foregroundColor) GetColorForMessage(IAnalogyLogMessage logMessage)
            => logMessage.Level == AnalogyLogLevel.Unknown ? (Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256)), Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256))) : (Color.Empty, Color.Empty);
        public OnlineExampleDataProvider(string prefix, Guid guid)
        {
            prefixMessage = prefix;
            ID = guid;
            OptionalTitle = $"Analogy Example: Real time Data Provider {prefix}";
        }
        public Task InitializeDataProviderAsync(IAnalogyLogger logger)
        {
            Logger = logger;
            SimulateOnlineMessages = new Timer(100);

            SimulateOnlineMessages.Elapsed += (s, e) =>
            {
                if (OnMessageReady == null)
                    return;
                unchecked
                {

                    AnalogyLogLevel randomLevel = (AnalogyLogLevel)values.GetValue(random.Next(values.Length));
                    string randomProcess = processes[random.Next(processes.Count)];
                    AnalogyLogMessage m = new AnalogyLogMessage
                    {
                        Text = $"{prefixMessage}: Generated message #{messageCount++}",
                        Level = randomLevel,
                        Class = AnalogyLogClass.General,
                        Source = "Example",
                        Module = randomProcess,
                        MachineName = Environment.MachineName
                    };

                    OnMessageReady?.Invoke(this, new AnalogyLogMessageArgs(m, Environment.MachineName, "Example", ID));
                }
            };
            return Task.CompletedTask;
        }

        public void MessageOpened(AnalogyLogMessage message)
        {
            //nop
        }

        public void StartReceiving()
        {
            InitializeDataProviderAsync(Logger);
            SimulateOnlineMessages?.Start();
        }

        public void StopReceiving()
        {
            SimulateOnlineMessages?.Stop();
            OnDisconnected?.Invoke(this, new AnalogyDataSourceDisconnectedArgs("user disconnected", Environment.MachineName, ID));
        }
    }
}
