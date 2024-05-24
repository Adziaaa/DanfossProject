using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DanfossProject
{
    public class Model : ITemplate
    {
        public string Name { get; set; }
        public double MaxHeat { get; set; }
        public double MaxElectricity { get; set; }
        public double ProductionCosts { get; set; }
        public double CO2Consumpition { get; set; }
        public double GasConsumption { get; set; }
        public bool IsOperating { get; set; } = false;
        //constructor
        public Model(string name, double maxHeat, double maxElectricity, double productionCosts, double cO2Consumption, double gasConsumption, bool isOperating)
        {
            Name = name;
            MaxHeat = maxHeat;
            MaxElectricity = maxElectricity;
            ProductionCosts = productionCosts;
            CO2Consumpition = cO2Consumption;
            GasConsumption = gasConsumption;
            IsOperating = isOperating;
        }

        public void DispConfiDat()
        {
            Console.WriteLine($"Name: {Name} \n Max Electricity: {MaxElectricity} \n Production Costs: {ProductionCosts} \n" +
                    $"CO2 Consumption: {CO2Consumpition} \n Gas Consumption: {GasConsumption} \n Is Operating?: {IsOperating}");
        }

        public bool IsOperatingg()
        {
            return IsOperating;
        }
    }
}
