using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Analogy.Interfaces;
using Analogy.Interfaces.DataTypes;

namespace Analogy.LogViewer.Example.IAnalogy
{
    public class ServerSidePagingProvider : Template.IAnalogy.AnalogyProviderSidePagingProvider
    {
        public override Guid Id { get; set; } = new Guid("877808EC-A3DC-4451-986F-6A7569CDE660");
        readonly Array _values = Enum.GetValues(typeof(AnalogyLogLevel));
        private readonly Random _random = new Random();
        private readonly List<string> _processes = Process.GetProcesses().Select(p => p.ProcessName).ToList();
        private List<AnalogyLogMessage> messages;
        public ServerSidePagingProvider()
        {
            messages = new List<AnalogyLogMessage>();
            for (int i = 0; i < 30000; i++)
            {
                AnalogyLogLevel randomLevel = (AnalogyLogLevel)_values.GetValue(_random.Next(_values.Length))!;
                string randomProcess = _processes[_random.Next(_processes.Count)];
                AnalogyLogMessage m = new AnalogyLogMessage
                {
                    Text = $"Generated message #{i++}" + string.Join(Environment.NewLine,
                        Enumerable.Range(0, _random.Next(1, 5)).Select(i => $" row {i}")),
                    Level = randomLevel,
                    Class = AnalogyLogClass.General,
                    Source = "Example",
                    User = Environment.UserName,
                    Module = randomProcess,
                    MachineName = Environment.MachineName,
                    ThreadId = Thread.CurrentThread.ManagedThreadId,

                };
                messages.Add(m);
            }
        }

        public override Task<IEnumerable<AnalogyLogMessage>> FetchMessages(int pageNumber, int pageCount, FilterCriteria filterCriteria, CancellationToken token,
            ILogMessageCreatedHandler messagesHandler)
        {

            var messages = new List<AnalogyLogMessage>();
        }


    }
}
