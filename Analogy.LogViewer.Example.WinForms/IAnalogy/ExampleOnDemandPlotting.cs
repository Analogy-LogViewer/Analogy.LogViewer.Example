using Analogy.Interfaces;
using Analogy.Interfaces.DataTypes;
using Analogy.LogViewer.Template.Managers;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Analogy.LogViewer.Example.IAnalogy
{
    public class ExampleOnDemandPlotting : IAnalogyOnDemandPlotting
    {
        public Guid Id { get; } = new Guid("bb38ccb7-8625-4b22-a33c-50a3cbd1e741");
        public event EventHandler<(Guid Id, IEnumerable<AnalogyPlottingPointData> PointsData)> OnNewPointsData;
        private Timer simulateData;
        private int counter;
        private IAnalogyOnDemandPlottingInteractor Interactor { get; set; }
        public Task InitializeOnDemandPlotting(IAnalogyOnDemandPlottingInteractor onDemandPlottingInteractor, ILogger logger)
        {
            LogManager.Instance.SetLogger(logger);
            Interactor = onDemandPlottingInteractor;
            simulateData = new Timer();
            simulateData.Interval = 1;
            simulateData.Tick += SimulateData_Tick;
            simulateData.Enabled = false;
            return Task.CompletedTask;
        }

        private void SimulateData_Tick(object sender, EventArgs e)
        {
            var now = DateTimeOffset.Now;
            AnalogyPlottingPointData d1 = new AnalogyPlottingPointData("series1", GenerateValue(counter), now);
            AnalogyPlottingPointData d2 = new AnalogyPlottingPointData("series2", GenerateValue(counter + 50), now);
            var list = new List<AnalogyPlottingPointData>(2) { d1, d2 };
            OnNewPointsData?.Invoke(this, (Id, list));

            counter++;
        }
        private double GenerateValue(double x) { return Math.Sin(x / 1000.0) * 3 * x + x / 2 + 5; }

        public void StartPlotting() => simulateData.Enabled = true;

        public void StopPlotting() => simulateData.Enabled = false;

        public void ShowPlot()
        {
            Interactor.ShowPlot(Id, "Example", AnalogyOnDemandPlottingStartupType.TabbedWindow);
        }

        public void ClosePlot()
        {
            Interactor.ClosePlot(Id);
        }

        public void RemoveSeriesFromPlot(string seriesName)
        {
            Interactor.RemoveSeriesFromPlot(Id, seriesName);
        }

        public void ClearSeriesData(string seriesNameToClear)
        {
            Interactor.ClearSeriesData(Id, seriesNameToClear);
        }

        public void ClearAllData()
        {
            Interactor.ClearAllData(Id);
        }

        public void HidePlot()
        {
            Interactor.ClosePlot(Id);
        }
    }
}