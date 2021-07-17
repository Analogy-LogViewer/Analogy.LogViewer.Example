using Analogy.Interfaces;
using Analogy.Interfaces.Factories;
using System;
using System.Collections.Generic;

namespace Analogy.LogViewer.Example.IAnalogy
{
    public class ExampleOnDemandPlottingFactory : IAnalogyOnDemandPlottingFactory
    {

        public Guid Id { get; set; } = new Guid("4f59de24-aaab-4de0-9269-ef681f8f3ee6");
        public string Title { get; set; } = "on Demand Plots Example";
        public List<IAnalogyOnDemandPlotting> OnDemandPlottingGenerators { get; set; }
        public event EventHandler<IAnalogyOnDemandPlotting>? OnAddedOnDemandPlottingGenerator;
        public event EventHandler<IAnalogyOnDemandPlotting>? OnRemovedOnDemandPlottingGenerator;

        public ExampleOnDemandPlottingFactory()
        {
            OnDemandPlottingContainer.Instance.SetFactory(this);
        }

        public void AddedOnDemandPlottingGenerator(IAnalogyOnDemandPlotting plotGenerator)
        {
            OnAddedOnDemandPlottingGenerator?.Invoke(this, plotGenerator);
        }
    }
}
