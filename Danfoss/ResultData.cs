using System.Reflection;

namespace Danfoss
{
    public class ResultData
    {

        public double MaxHeat { get; set; }
        public double MaxElectricity { get; set; }
        public double CO2Consumpition { get; set; }
        public double ProductionCosts { get; set; }
        public string TimeFrom { get; set; }
        public string TimeTo { get; set; }
        public double ProducedHeat { get; set; }
        public double NetElectricity { get; set; }
        public double Percentage { get; set; }
        public double ProducedCO2 { get; set; }
        public string ModelName { get; set; }


    }
}