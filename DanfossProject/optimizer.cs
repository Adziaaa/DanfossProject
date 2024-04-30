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

        public void OptimizeEnergy(string csvFilePath)
        {
            // Readind data off CSV (?)
            List<SdmRecord> sdmRecords = _csvManager.ReadCsv(csvFilePath);

            // Energy demand calc
            double totalEnergyDemand = sdmRecords.Sum(record => record.HeatDemand);

            // Assets available
            List<Model> availableAssets = _assetManager.AssetManager;

            // Assets that are currently operative
            List<Model> operatingAssets = availableAssets.Where(asset => asset.IsOperating).ToList();

            // Getting the ratio of energy gained per dkr spent
            var efficiencyByCost = operatingAssets.Select(asset => new
            {
                Model = asset,
                Efficiency = asset.ProductionCosts / asset.MaxElectricity
            }).OrderByDescending(x => x.Efficiency).FirstOrDefault();

            // CO2 thingy
            var lowestCO2Emission = operatingAssets.OrderBy(asset => asset.CO2Consumpition).FirstOrDefault();

            // Here it selects the best by costs
            if (efficiencyByCost != null)
            {
                Console.WriteLine($"The most efficient unit in terms of price/energy produced is: {efficiencyByCost.Model.Name}");
            }
            else
            {
                Console.WriteLine("No operating assets available.");
            }

            // Here it does the same but with the least Co2
            if (lowestCO2Emission != null)
            {
                Console.WriteLine($"The unit emitting the least CO2 is: {lowestCO2Emission.Name}");
            }
            else
            {
                Console.WriteLine("No operating assets available.");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            CsvManager csvManager = new CsvManager();
            FunctionalityOfAM assetManager = new FunctionalityOfAM();
            assetManager.AddAssets(); // Agregar las unidades de energía disponibles

            EnergyOptimizer energyOptimizer = new EnergyOptimizer(csvManager, assetManager);
            energyOptimizer.OptimizeEnergy(".csv"); // Ruta del archivo CSV con los datos de demanda y precios de electricidad
        }
    }
}