using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace DanfossProject
{
    public class optfromscratch
    {
        public double CalculateNetProductionCosts(Model model, SdmRecord sdmRecord)
        {
            double netProductionCosts = model.ProductionCosts;
            double electricityPrice = sdmRecord.ElectricityPrice;
            netProductionCosts -= model.MaxElectricity * electricityPrice;
            return netProductionCosts;
        }

        public Model GetCheapestUnit(List<Model> models, SdmRecord sdmRecord)
        {
            Model cheapestModel = null;
            double lowestCost = double.MaxValue;

            foreach (Model model in models)
            {
                double netProductionCosts = CalculateNetProductionCosts(model, sdmRecord);
                if (netProductionCosts < lowestCost)
                {
                    lowestCost = netProductionCosts;
                    cheapestModel = model;
                }
            }

            return cheapestModel;
        }

        public ResultData CreateResultData(Model model, double percentage)
        {
            return new ResultData
            {
                OperationPercentage = percentage,
                MaxHeat = model.MaxHeat * percentage,
                MaxElectricity = model.MaxElectricity * percentage,
                CO2Consumpition = model.CO2Consumpition * percentage,
                ProductionCosts = model.ProductionCosts * percentage
            };
        }

        private List<ResultData> CalculateResultDataForInterval(List<Model> models, SdmRecord sdmRecord)
        {
            List<ResultData> resultDataList = new();

            List<Model> unusedModels = models.ToList();

            double currentHeatDemand = sdmRecord.HeatDemand;

            while (currentHeatDemand > 0.0)
            {
                // Conditional for demand not fulfilled after using all resources available
                if (unusedModels.Count == 0 && currentHeatDemand > 0)
                {
                    Console.WriteLine("Unable to meet heat demand for time interval " + sdmRecord.TimeFrom + " to " + sdmRecord.TimeTo);
                    return resultDataList;
                }

                Model economicModel = GetCheapestUnit(unusedModels, sdmRecord);
                unusedModels.Remove(economicModel);

                double percentage = 1;
                if (economicModel.MaxHeat > currentHeatDemand)
                {
                    percentage = currentHeatDemand / economicModel.MaxHeat;
                }

                ResultData resultData = new()
                {
                    TimeFrom = sdmRecord.TimeFrom,
                    TimeTo = sdmRecord.TimeTo,
                    ModelName = economicModel.Name,
                    ProducedHeat = economicModel.MaxHeat * percentage,
                    NetElectricity = economicModel.MaxElectricity * percentage,
                    ProductionCosts = economicModel.ProductionCosts * percentage,
                    ProducedCO2 = economicModel.CO2Consumpition * percentage,
                    Percentage = percentage
                };

                currentHeatDemand -= economicModel.MaxHeat * percentage;

                resultDataList.Add(resultData);
            }
            return resultDataList;
        }

        public List<ResultData> OptimizeData(List<Model> models, List<SdmRecord> sdmRecords)
        {
            List<ResultData> resultData = new List<ResultData>();
            foreach (SdmRecord sdmRecord in sdmRecords)
            {
                List<ResultData> intervalResults = CalculateResultDataForInterval(models, sdmRecord);
                //Console.WriteLine(intervalResults.Count);
                resultData.AddRange(intervalResults);
            }
            return resultData;
        }
    }
}
