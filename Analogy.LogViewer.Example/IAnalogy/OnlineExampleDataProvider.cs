using Analogy.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Timer = System.Timers.Timer;

namespace Analogy.LogViewer.Example
{
    class OnlineExampleDataProvider : Analogy.LogViewer.Template.OnlineDataProvider
    {
        public override string? OptionalTitle { get; set; }
        public override Guid Id { get; set; }

        public override async Task<bool> CanStartReceiving() => await Task.FromResult(true);

        private readonly Timer SimulateOnlineMessages;
        private int messageCount = 0;
        readonly Random _random = new Random();
        readonly Array _values = Enum.GetValues(typeof(AnalogyLogLevel));
        private readonly List<string> processes = Process.GetProcesses().Select(p => p.ProcessName).ToList();
        private readonly string prefixMessage;
        private readonly Random rnd = new Random();

        public override IEnumerable<(string originalHeader, string replacementHeader)> GetReplacementHeaders()
        {
            yield return ("Category", "Test");
        }

        public override (Color backgroundColor, Color foregroundColor) GetColorForMessage(IAnalogyLogMessage logMessage)
            => logMessage.Level == AnalogyLogLevel.Unknown ? (Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256)), Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256))) : (Color.Empty, Color.Empty);
        public OnlineExampleDataProvider(string prefix, Guid guid)
        {
            prefixMessage = prefix;
            Id = guid;
            OptionalTitle = $"Analogy Example: Real time Data Provider {prefix}";
            SimulateOnlineMessages = new Timer(100);

        }
        public override async Task InitializeDataProviderAsync(IAnalogyLogger logger)
        {
            await base.InitializeDataProviderAsync(logger);

            SimulateOnlineMessages.Elapsed += (s, e) =>
            {
                unchecked
                {
                    AnalogyLogLevel randomLevel = (AnalogyLogLevel)_values.GetValue(_random.Next(_values.Length))!;
                    string randomProcess = processes[_random.Next(processes.Count)];
                    AnalogyLogMessage m = new AnalogyLogMessage
                    {
                        Text = $"{prefixMessage}: Generated message #{messageCount++}" + string.Join(Environment.NewLine, Enumerable.Range(0, _random.Next(1, 5)).Select(i => $"row {i}")),
                        Level = randomLevel,
                        Class = AnalogyLogClass.General,
                        Source = "Example",
                        User = Environment.UserName,
                        Module = randomProcess,
                        MachineName = Environment.MachineName,
                        ThreadId = Thread.CurrentThread.ManagedThreadId,
                        AdditionalInformation = new Dictionary<string, string>() { { "Random Column", _random.Next(0, 10).ToString() }, { "Random Column 2", _random.Next(0, 10).ToString() } }

                    };

                    MessageReady(this, new AnalogyLogMessageArgs(m, Environment.MachineName, "Example", Id));
                }
            };
        }

        public override Task StartReceiving()
        {
            SimulateOnlineMessages.Start();
            return Task.CompletedTask;
        }

        public override Task StopReceiving()
        {
            SimulateOnlineMessages?.Stop();
            Disconnected(this, new AnalogyDataSourceDisconnectedArgs("user disconnected", Environment.MachineName, Id));
            return Task.CompletedTask;
        }
    }
}
