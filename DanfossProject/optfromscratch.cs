using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace DanfossProject
{
    public class Optimizer
    {
        public double CalculateNetProductionCosts(DanfossProject Model, SdmRecord SdmRecord)
        {
            double netProductionCosts = Model.ProductionCosts;
            double electricityPrice = SdmRecord.ElectricityPrice;
            netProductionCosts -= Model.MaxElectricity * electricityPrice;
            return netProductionCosts;
        }

        public DanfossProject GetCheapestUnit(List<Model> model, SdmRecord SdmRecord)
        {
            Model CheapestModel = new();
            double lowestCost = double.MaxValue;

            foreach (Model model in model)
            {
                double netProductionCosts = CalculateNetProductionCosts(model, SdmRecord);
                if (netProductionCosts < lowestCost)
                {
                    lowestCost = netProductionCosts;
                    CheapestModel = model;
                }
            }

            return CheapestModel;
        }
        //takes original model values and uses them proportionally with the current demand
        public ResultData(Model model, double Percentage)
        {
            sdmRecord.OperationPercentage = Percentage;
            sdmRecord.MaxHeat *= Percentage;
            sdmRecord.MaxElectricity *= Percentage;
            sdmRecord.CO2Consumpition *= Percentage;
            sdmRecord.PrimaryEnergyConsumption *= Percentage;
            sdmRecord.ProductionCosts *= Percentage;

            return originalData;
        }

        private List<Results> CalculateResultDataForInterval(List<Model> model, SdmRecord sdmRecord)
        {
            List<Results> resultDatas = [];

            List<Model> unusedModels = model.ToList();

            double currentHeatDemand = SdmRecord.HeatDemand;

            while (currentHeatDemand > 0.0)
            {
                // conditional for demand not fullfilled after using all resources available
                if (unusedModels.Count == 0 && currentHeatDemand > 0)
                {
                    Console.WriteLine("Unable to meet heat demand for time interval " + sourceDataPoint.TimeFrom + " to " + sourceDataPoint.TimeTo);
                    return resultData;
                }

                Model economicModel = GetCheapestUnit(unusedModels, sdmRecord);
                unusedModels.Remove(economicModel);
                //create new sheet of results regarding the model that is going to be used and specs like time
                Results resultData = new()
                {
                    TimeFrom = sdmRecord.TimeFrom,
                    TimeTo = sdmRecord.TimeTo,
                    ProductionUnitName = economicModel.Name,
                    ProducedHeat = economicModel.MaxHeat,
                    NetElectricity = economicModel.MaxElectricity,
                    ProductionCosts = economicModel.ProductionCosts,
                    ProducedCO2 = economicModel.CO2Consumpition,
                    PrimaryEnergyConsumption = economicModel.GasConsumption,
                    Percentage = 1
                };
                //conditional that allows the cheapest model to run only for the actual demand if it's lower than what the model provides
                if (economicModel.MaxHeat > currentHeatDemand)
                {
                    Percentage = currentHeatDemand / economicModel.MaxHeat;
                    resultData = resultData * Percentage;
                }
                currentHeatDemand = currentHeatDemand - economicModel.MaxHeat * Percentage;

                resultDatas.Add(resultData);
            }
            return resultDatas;
        }
        public List<Results> OptimizeData(List<Model> model, List<SdmRecord> sdmRecord)
        {
            List<Result> resultData = [];
            foreach (SdmRecord sdmRecord in SdmRecord)
            {
                List<Results> intervalResults = CalculateResultDataForInterval(model, sdmRecord);
                resultData.AddRange(intervalResults);
            }
            return resultData;
        }
    }
}

