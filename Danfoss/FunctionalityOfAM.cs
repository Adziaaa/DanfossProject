using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Danfoss
{
    internal class FunctionalityOfAM
    {
        // list of our models
        public List<Model> AssetManager = new List<Model>();
        public void AddAssets()
        {
            AssetManager.Add(new Model("Gas Boiler", 5.0, 0, 500, 215, 1.1, false));
            AssetManager.Add(new Model("Oil boiler", 4.0, 0, 700, 265, 1.2, false));
            AssetManager.Add(new Model("Gas Motor", 3.6, 2.7, 1100, 640, 1.9, false));
            AssetManager.Add(new Model("Electric Boiler", 8.0, -8.0, 50, 0, 0, false));

        }
        public void StopAsset(Model model)
        {
            foreach (var asset in AssetManager)
            {
                if (asset == model)
                    asset.IsOperating = false;
            }
        }
        public void StartAsset(Model model)
        {
            foreach (var asset in AssetManager)
            {
                if (asset == model)
                    asset.IsOperating = true;

            }
        }

        public void DiplAllDataModel()
        {
            foreach (var asset in AssetManager)
            {
                Console.WriteLine($"Name: {asset.Name} \n Max Electricity: {asset.MaxElectricity} \n Production Costs: {asset.ProductionCosts} \n" +
                    $"CO2 Consumption: {asset.CO2Consumpition} \n Gas Consumption: {asset.GasConsumption} \n Is Operating?: {asset.IsOperating}");
            }
        }
    }
}