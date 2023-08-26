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
        public override string? OptionalTitle { get; set; } = "Example Server Side Paging";
        readonly Array _values = Enum.GetValues(typeof(AnalogyLogLevel));
        private readonly Random _random = new Random();
        private readonly List<string> _processes = Process.GetProcesses().Select(p => p.ProcessName).ToList();
        private List<IAnalogyLogMessage> messages;
        public ServerSidePagingProvider()
        {
            messages = new List<IAnalogyLogMessage>();
            Task.Run(() =>
            {
                var msg = new List<IAnalogyLogMessage>();

                for (int i = 0; i < 30000; i++)
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

        public override Task<IEnumerable<IAnalogyLogMessage>> FetchMessages(int pageNumber, int pageCount, FilterCriteria filterCriteria, CancellationToken token,
            ILogMessageCreatedHandler messagesHandler)
        {

            var filters = messages.Where(m =>
                m.Date >= (filterCriteria.StartTime ?? DateTime.MinValue)
                && m.Date <= (filterCriteria.EndTime ?? DateTime.MaxValue));

            foreach (var include in filterCriteria.IncludeText)
            {
                if (string.IsNullOrEmpty(include))
                {
                    continue;
                }
                filters = filters.Where(m => m.Text is not null && Contains(m.Text, include, StringComparison.InvariantCultureIgnoreCase));
            }

            foreach (var exclude in filterCriteria.ExcludeText)
            {
                if (string.IsNullOrEmpty(exclude))
                {
                    continue;
                }

                filters = filters.Where(m => m.Text is not null &&
                                             !Contains(m.Text, exclude, StringComparison.InvariantCultureIgnoreCase));
            }

            foreach (var include in filterCriteria.IncludeSources)
            {
                if (string.IsNullOrEmpty(include))
                {
                    continue;
                }

                filters = filters.Where(m => m.Source is not null && Contains(m.Source, include, StringComparison.InvariantCultureIgnoreCase));
            }

            foreach (var exclude in filterCriteria.ExcludeSources)
            {
                if (string.IsNullOrEmpty(exclude))
                {
                    continue;
                }

                filters = filters.Where(m => m.Source is not null &&
                                             !Contains(m.Source, exclude, StringComparison.InvariantCultureIgnoreCase));
            }

            foreach (var include in filterCriteria.IncludeModules)
            {
                if (string.IsNullOrEmpty(include))
                {
                    continue;
                }

                filters = filters.Where(m => m.Module is not null && Contains(m.Module, include, StringComparison.InvariantCultureIgnoreCase));
            }

            foreach (var exclude in filterCriteria.ExcludeModules)
            {
                if (string.IsNullOrEmpty(exclude))
                {
                    continue;
                }

                filters = filters.Where(m => m.Module is not null &&
                                             !Contains(m.Module, exclude, StringComparison.InvariantCultureIgnoreCase));
            }

            foreach (var include in filterCriteria.IncludeLevels)
            {
                filters = filters.Where(m => m.Level ==include);
            }
            foreach (var exclude in filterCriteria.ExcludeLevels)
            {
                filters = filters.Where(m => m.Level != exclude);
            }

            foreach (AnalogyColumnFilter dynamicColumn in filterCriteria.DynamicColumns)
            {
                if (dynamicColumn.FilterType == AnalogyColumnFilterType.Include)
                {

                    filters = filters.Where(m => m.AdditionalProperties != null &&
                                                 m.AdditionalProperties.ContainsKey(dynamicColumn.ColumnName) &&
                                                 m.AdditionalProperties[dynamicColumn.ColumnName]
                                                     .Equals(dynamicColumn.FilterValue,
                                                         StringComparison.InvariantCultureIgnoreCase));
                }
                else
                {
                    filters = filters.Where(m =>
                    {
                        if (m.AdditionalProperties == null)
                        {
                            return true;

                        }

                        return
                            !m.AdditionalProperties.ContainsKey(dynamicColumn.ColumnName) ||
                            !m.AdditionalProperties[dynamicColumn.ColumnName].Contains(dynamicColumn.FilterValue);
                    });
                }
            }
            var result = filters.Skip((pageNumber - 1) * pageCount).Take(pageCount).ToList();
            messagesHandler.AppendMessages(result, OptionalTitle ?? "Example ");
            return Task.FromResult(result.AsEnumerable());
        }

        private static bool Contains(string source, string toCheck, StringComparison comp)
        {
            return string.IsNullOrEmpty(toCheck) || (!string.IsNullOrEmpty(source) && source.IndexOf(toCheck, comp) >= 0);
        }

    }
}
