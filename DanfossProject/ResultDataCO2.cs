using System.Reflection;
namespace DanfossProject
{
    public class CO2ResultData
    {
        public string TimeFrom { get; set; }
        public string TimeTo { get; set; }
        public string ModelName { get; set; }
        public double ProducedHeat { get; set; }
        public double NetElectricity { get; set; }
        public double ProductionCosts { get; set; }
        public double ProducedCO2 { get; set; }
        public double Percentage { get; set; }
        public double MaxHeat { get; set; }
        public double MaxElectricity { get; set; }
        public double CO2Consumpition { get; set; }

        public void DisplayResult()
        {
            Console.WriteLine( "Model Name:" + ModelName + "Time from:" + TimeFrom + "Time to" + TimeTo + "Produced heat" + ProducedHeat + "Net Electricity" + NetElectricity + "Production Costs" + ProductionCosts + "Produced CO2" + ProducedCO2);
        }
    }
}