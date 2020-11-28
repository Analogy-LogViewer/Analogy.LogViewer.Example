using Analogy.Interfaces;
using Analogy.Interfaces.DataTypes;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Analogy.LogViewer.Example.IAnalogy
{
    public class ExamplePlotting : IAnalogyPlotting
    {
        public event EventHandler<AnalogyPlottingPointData> OnNewPointData;
        public Guid Id { get; set; } = new Guid("b8b4be8d-2cff-48ac-90f6-477d36271e84");
        public string Title { get; set; } = "Example real time plotting";
        private Timer simulateData;
        int counter = 0;
        public Task InitializePlottingAsync(IAnalogyLogger logger)
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

        public List<(string SeriesName, AnalogyPlottingSeriesType SeriesViewType)> GetChartSeries()
        {
            var items = new List<(string SeriesName, AnalogyPlottingSeriesType SeriesViewType)>();
            items.Add(("series1", AnalogyPlottingSeriesType.Line));
            items.Add(("series2", AnalogyPlottingSeriesType.Line));
            return items;
        }
        private void SimulateData_Tick(object sender, EventArgs e)
        {
            var now = DateTime.Now;
            AnalogyPlottingPointData d1 = new AnalogyPlottingPointData("series1", GenerateValue(counter), now);
            OnNewPointData?.Invoke(this, d1);
            AnalogyPlottingPointData d2 = new AnalogyPlottingPointData("series1", GenerateValue(counter + 50), now);
            OnNewPointData?.Invoke(this, d2);
            counter++;
        }
        double GenerateValue(double x) { return Math.Sin(x / 1000.0) * 3 * x + x / 2 + 5; }

    }
}
