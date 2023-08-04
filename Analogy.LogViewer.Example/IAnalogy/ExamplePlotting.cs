using Analogy.Interfaces;
using Analogy.Interfaces.DataTypes;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Analogy.LogViewer.Example.IAnalogy
{
    public class ExamplePlotting : IAnalogyPlotting
    {
        public event EventHandler<AnalogyPlottingPointData> OnNewPointData;
        public event EventHandler<List<AnalogyPlottingPointData>>? OnNewPointsData;
        public Guid Id { get; set; } = new Guid("b8b4be8d-2cff-48ac-90f6-477d36271e84");
        public Guid FactoryId { get; set; } = PrimaryFactory.Id;
        public string Title { get; set; } = "Example real time plotting";
        private Timer simulateData;
        int counter;
        public Task InitializePlotting(IAnalogyPlottingInteractor uiInteractor, ILogger logger)
        {
            simulateData = new Timer();
            simulateData.Interval = 1;
            simulateData.Tick += SimulateData_Tick;
            simulateData.Enabled = false;
            return Task.CompletedTask;
        }

        public Task StartPlotting()
        {
            simulateData.Enabled = true;
            return Task.CompletedTask;
        }
        public Task StopPlotting()
        {
            simulateData.Enabled = false;
            return Task.CompletedTask;
        }

        public IEnumerable<(string SeriesName, AnalogyPlottingSeriesType SeriesViewType)> GetChartSeries()
        {
            yield return ("series1", AnalogyPlottingSeriesType.Line);
            yield return ("series2", AnalogyPlottingSeriesType.Line);
        }
        private void SimulateData_Tick(object sender, EventArgs e)
        {
            var now = DateTime.Now;
            AnalogyPlottingPointData d1 = new AnalogyPlottingPointData("series1", GenerateValue(counter), now);
            OnNewPointData?.Invoke(this, d1);
            AnalogyPlottingPointData d2 = new AnalogyPlottingPointData("series2", GenerateValue(counter + 50), now);
            OnNewPointData?.Invoke(this, d2);
            counter++;
        }
        double GenerateValue(double x) { return Math.Sin(x / 1000.0) * 3 * x + x / 2 + 5; }

    }
}
