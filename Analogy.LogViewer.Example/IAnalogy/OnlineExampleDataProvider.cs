﻿using Analogy.Interfaces;
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
    public sealed class OnlineExampleDataProvider : Analogy.LogViewer.Template.OnlineDataProvider
    {
        public override string? OptionalTitle { get; set; }
        public override Guid Id { get; set; }

        public override Task<bool> CanStartReceiving() => Task.FromResult(true);

        private readonly Timer _simulateOnlineMessages;
        private long _messageCount;
        readonly Random _random = new Random();
        readonly Array _values = Enum.GetValues(typeof(AnalogyLogLevel));
        private readonly List<string> _processes = Process.GetProcesses().Select(p => p.ProcessName).ToList();
        private readonly string _prefixMessage;
        private readonly Random _rnd = new Random();

        public override IEnumerable<(string originalHeader, string replacementHeader)> GetReplacementHeaders()
        {
            yield return ("Category", "Test");
        }

        public override (Color backgroundColor, Color foregroundColor) GetColorForMessage(IAnalogyLogMessage logMessage)
            => logMessage.Level == AnalogyLogLevel.Unknown ? (Color.FromArgb(_rnd.Next(256), _rnd.Next(256), _rnd.Next(256)), Color.FromArgb(_rnd.Next(256), _rnd.Next(256), _rnd.Next(256))) : (Color.Empty, Color.Empty);
        public OnlineExampleDataProvider(string prefix, Guid guid)
        {
            _prefixMessage = prefix;
            Id = guid;
            OptionalTitle = $"Analogy Example: Real time Data Provider {prefix}";
            _simulateOnlineMessages = new Timer(100);

        }
        public override async Task InitializeDataProviderAsync(IAnalogyLogger logger)
        {
            await base.InitializeDataProviderAsync(logger);

            _simulateOnlineMessages.Elapsed += (s, e) =>
            {

                AnalogyLogLevel randomLevel = (AnalogyLogLevel)_values.GetValue(_random.Next(_values.Length))!;
                string randomProcess = _processes[_random.Next(_processes.Count)];
                AnalogyLogMessage m = new AnalogyLogMessage
                {
                    Text = $"{_prefixMessage}: Generated message #{_messageCount++}" + string.Join(Environment.NewLine, Enumerable.Range(0, _random.Next(1, 5)).Select(i => $" row {i}")),
                    Level = randomLevel,
                    Class = AnalogyLogClass.General,
                    Source = "Example",
                    User = Environment.UserName,
                    Module = randomProcess,
                    MachineName = Environment.MachineName,
                    ThreadId = Thread.CurrentThread.ManagedThreadId,
                    AdditionalInformation = new Dictionary<string, string> { { "Random Column", _random.Next(0, 10).ToString() }, { "Random Column 2", _random.Next(0, 10).ToString() } }

                };

                MessageReady(this, new AnalogyLogMessageArgs(m, Environment.MachineName, "Example", Id));

            };
        }

        public override Task StartReceiving()
        {
            _simulateOnlineMessages.Start();
            return Task.CompletedTask;
        }

        public override Task StopReceiving()
        {
            _simulateOnlineMessages.Stop();
            Disconnected(this, new AnalogyDataSourceDisconnectedArgs("user disconnected", Environment.MachineName, Id));
            return Task.CompletedTask;
        }
    }
}
