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


        public Model GetLeastCO2Unit(List<Model> models)
        {
            Model leastCO2Model = null;
            double lowestCO2Consumption = double.MaxValue;

            foreach (Model model in models)
            {
                if (model.CO2Consumpition < lowestCO2Consumption)
                {
                    lowestCO2Consumption = model.CO2Consumpition;
                    leastCO2Model = model;
                }
            }

            return leastCO2Model;
        }


        public ResultData CreateResultData(Model model, double percentage)
        {
            return new ResultData
            {

                Percentage = percentage,


                MaxHeat = model.MaxHeat * percentage,
                MaxElectricity = model.MaxElectricity * percentage,
                CO2Consumpition = model.CO2Consumpition * percentage,
                ProductionCosts = model.ProductionCosts * percentage
            };
        }


        public ResultData CreateCO2ResultData(Model model, double percentage)
        {
            return new ResultData
            {
                Percentage = percentage,
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


        private List<ResultData> CalculateCO2ResultDataForInterval(List<Model> models, SdmRecord sdmRecord)
        {
            List<ResultData> co2ResultDataList = new();
            List<Model> unusedModels = models.ToList();

            double currentHeatDemand = sdmRecord.HeatDemand;

            while (currentHeatDemand > 0.0)
            {
                if (unusedModels.Count == 0)
                {
                    Console.WriteLine($"Unable to meet heat demand for time interval {sdmRecord.TimeFrom} to {sdmRecord.TimeTo}");
                    break;
                }

                Model leastCO2Model = GetLeastCO2Unit(unusedModels);
                unusedModels.Remove(leastCO2Model);

                double percentage = 1;
                if (leastCO2Model.MaxHeat > currentHeatDemand)
                {
                    percentage = currentHeatDemand / leastCO2Model.MaxHeat;
                }

                ResultData co2ResultData = new()
                {
                    TimeFrom = sdmRecord.TimeFrom,
                    TimeTo = sdmRecord.TimeTo,
                    ModelName = leastCO2Model.Name,
                    ProducedHeat = leastCO2Model.MaxHeat * percentage,
                    NetElectricity = leastCO2Model.MaxElectricity * percentage,
                    ProductionCosts = leastCO2Model.ProductionCosts * percentage,
                    ProducedCO2 = leastCO2Model.CO2Consumpition * percentage,
                    Percentage = percentage
                };

                currentHeatDemand -= leastCO2Model.MaxHeat * percentage;
                co2ResultDataList.Add(co2ResultData);
            }

            return co2ResultDataList;
        }

        public (List<ResultData>, List<ResultData>) OptimizeData(List<Model> models, List<SdmRecord> sdmRecords)
        {
            List<ResultData> resultData = new List<ResultData>();
            List<ResultData> co2ResultData=new List<ResultData>();

            foreach (SdmRecord sdmRecord in sdmRecords)
            {
                List<ResultData> intervalResults = CalculateResultDataForInterval(models, sdmRecord);
                //Console.WriteLine(intervalResults.Count);
                resultData.AddRange(intervalResults);


                List<ResultData> intervalCO2Results = CalculateCO2ResultDataForInterval(models, sdmRecord);
                //Console.WriteLine(intervalResults.Count);
                co2ResultData.AddRange(intervalCO2Results);
            }
            return (resultData, co2ResultData);

        }
    }
}
