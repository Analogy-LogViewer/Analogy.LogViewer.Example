using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
            Task.Run(() =>
            {
               var  msg = new List<AnalogyLogMessage>();

                for (int i = 0; i < 300000; i++)
                {
                    AnalogyLogLevel randomLevel = (AnalogyLogLevel)_values.GetValue(_random.Next(_values.Length))!;
                    string randomProcess = _processes[_random.Next(_processes.Count)];
                    AnalogyLogMessage m = new AnalogyLogMessage
                    {
                        Text = $"Generated message #{i}" + string.Join(Environment.NewLine,
                            Enumerable.Range(0, _random.Next(1, 5)).Select(i => $" row {i}")),
                        Level = randomLevel,
                        Class = AnalogyLogClass.General,
                        Source = "Example",
                        User = Environment.UserName,
                        Module = randomProcess,
                        MachineName = Environment.MachineName,
                        ThreadId = Thread.CurrentThread.ManagedThreadId,

                    };
                    msg.Add(m);
                }

                messages = msg;
            });
            
        }

        public override Task<IEnumerable<AnalogyLogMessage>> FetchMessages(int pageNumber, int pageCount, FilterCriteria filterCriteria, CancellationToken token,
            ILogMessageCreatedHandler messagesHandler)
        {
            var filters = messages.Where(m =>
                m.Date >= filterCriteria.StartTime && m.Date <= filterCriteria.EndTime);

            if (filterCriteria.IncludeText.Any())
            {
                filters = filters.Where(m =>
                    filterCriteria.IncludeText.Contains(m.Text, StringComparer.InvariantCultureIgnoreCase));

            }

            if (filterCriteria.ExcludeText.Any())
            {
                filters = filters.Where(m => !filterCriteria.ExcludeText.Contains(m.Text, StringComparer.InvariantCultureIgnoreCase));
            }

            if (filterCriteria.IncludeSources.Any())
            {
                filters = filters.Where(m =>
                    filterCriteria.IncludeSources.Contains(m.Source, StringComparer.InvariantCultureIgnoreCase));

            }

            if (filterCriteria.ExcludeSources.Any())
            {
                filters = filters.Where(m => !filterCriteria.ExcludeSources.Contains(m.Source, StringComparer.InvariantCultureIgnoreCase));
            }

            if (filterCriteria.IncludeSources.Any())
            {
                filters = filters.Where(m =>
                    filterCriteria.IncludeSources.Contains(m.Source, StringComparer.InvariantCultureIgnoreCase));

            }

            if (filterCriteria.ExcludeModules.Any())
            {
                filters = filters.Where(m => !filterCriteria.ExcludeModules.Contains(m.Module, StringComparer.InvariantCultureIgnoreCase));
            }

            if (filterCriteria.IncludeSources.Any())
            {
                filters = filters.Where(m =>
                    filterCriteria.IncludeSources.Contains(m.Source, StringComparer.InvariantCultureIgnoreCase));

            }

            if (filterCriteria.IncludeLevels.Any())
            {
                filters = filters.Where(m => !filterCriteria.IncludeLevels.Contains(m.Level));
            }
            if (filterCriteria.ExcludeLevels.Any())
            {
                filters = filters.Where(m => !filterCriteria.ExcludeLevels.Contains(m.Level));
            }

            foreach (AnalogyColumnFilter dynamicColumn in filterCriteria.DynamicColumns)
            {
                if (dynamicColumn.FilterType == AnalogyColumnFilterType.Include)
                {

                    filters = filters.Where(m => m.AdditionalInformation != null &&
                                                 m.AdditionalInformation.ContainsKey(dynamicColumn.ColumnName) &&
                                                 m.AdditionalInformation[dynamicColumn.ColumnName]
                                                     .Equals(dynamicColumn.FilterValue,
                                                         StringComparison.InvariantCultureIgnoreCase));
                }
                else
                {
                    filters = filters.Where(m =>
                    {
                        if (m.AdditionalInformation == null)
                        {
                            return true;

                        }

                        return
                            !m.AdditionalInformation.ContainsKey(dynamicColumn.ColumnName) ||
                            !m.AdditionalInformation[dynamicColumn.ColumnName].Contains(dynamicColumn.FilterValue);
                    });
                }
            }

            return Task.FromResult(filters.Skip((pageNumber - 1) * pageCount).Take(pageCount));
        }


    }
}
