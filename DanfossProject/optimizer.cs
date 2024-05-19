using DanfossProject;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DanfossProject
{
    public class EnergyOptimizer
    {
        private readonly CsvManager _csvManager;
        private readonly FunctionalityOfAM _assetManager;

        public EnergyOptimizer(CsvManager csvManager, FunctionalityOfAM assetManager)
        {
            _csvManager = csvManager;
            _assetManager = assetManager;
        }

        public void OptimizeEnergy()
        {

            // Energy demand calc
            double totalEnergyDemand = sdmRecords.Sum(record => record.HeatDemand);

            // Assets available
            List<Model> availableAssets = _assetManager.AssetManager;

            // Assets that are currently operative
            List<Model> operatingAssets = availableAssets.Where(asset => asset.IsOperating).ToList();

            // Getting the ratio of energy gained per dkr spent
            var mostCostEfficientAsset = operatingAssets
                .Select(asset => new
                {
                    Asset = asset,
                    CostEfficiency = asset.MaxElectricity / asset.ProductionCosts
                })
                .OrderByDescending(x => x.CostEfficiency)
                .FirstOrDefault()?.Asset;
            // CO2 thingy
            var lowestCO2EmissionAsset = operatingAssets.OrderBy(asset => asset.CO2Consumpition).FirstOrDefault();

            // Here it selects the best by costs
            if (mostCostEfficientAsset != null)
            {
                Console.WriteLine($"The most efficient unit in terms of cost/energy produced is: {mostCostEfficientAsset.Name}");
            }
            else
            {
                Console.WriteLine("No operating assets available for cost optimization.");
            }

            // Here it does the same but with the least Co2
            if (lowestCO2EmissionAsset != null)
            {
                Console.WriteLine($"The unit emitting the least CO2 is: {lowestCO2EmissionAsset.Name}");
            }
            else
            {
                Console.WriteLine("No operating assets available for CO2 optimization.");
            }
        }
    }