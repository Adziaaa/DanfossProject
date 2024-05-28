using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Transactions;

namespace DanfossProject
{
    internal class Program
    {
        static void Main(string[] args)
        {
            FunctionalityOfAM AM = new FunctionalityOfAM();
            AM.AddAssets();

            CsvManager SdmObject = new CsvManager();

            
            List<SdmRecord> WinterSourceDataManager = SdmObject.ReadCsv("Winter_SDM.csv");
            List<SdmRecord> SummerSourceDataManager = SdmObject.ReadCsv("Summer_SDM.csv");
            //Console.WriteLine("\nWinter:");
            ///SdmObject.DisplaySdm(WinterSourceDataManager);
            //Console.WriteLine("\nSummer: ");
            //SdmObject.DisplaySdm(SummerSourceDataManager);
            //Console.Read();

            optfromscratch optfromscratchObject = new optfromscratch();

            List<Model> models = new List<Model>();



            var (result_winter, CO2result_winter) = optfromscratchObject.OptimizeData(AM.AssetManager, WinterSourceDataManager);
            var (result_summer, CO2result_summer) = optfromscratchObject.OptimizeData(AM.AssetManager, SummerSourceDataManager);

            Console.WriteLine("Winter Results - Cheapest Model:");
            foreach (var result in result_winter)
            {
                result.DisplayResult();
            }

            Console.WriteLine("\nWinter Results - Least CO2 Model:");
            foreach (var result in CO2result_winter)
            {
                result.DisplayResult();
            }

            Console.WriteLine("\nSummer Results - Cheapest Model:");
            foreach (var result in result_summer)
            {
                result.DisplayResult();
            }

            Console.WriteLine("\nSummer Results - Least CO2 Model:");
            foreach (var result in CO2result_summer)
            {
                result.DisplayResult();
            }



            Console.WriteLine("1.Start asset\n2.Start asset\n3.Exit");

            while (true)
            {

                var option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        foreach (Model asset in AM.AssetManager)
                        {
                            AM.StopAsset(asset);
                        }
                        break;
                    case "2":
                        foreach (Model asset in AM.AssetManager)
                        {
                            AM.StartAsset(asset);
                        }
                        break;
                    case "3":
                        // Exit application
                        Console.WriteLine("Exiting application...");
                        return;
                    default:
                        Console.WriteLine("Invalid option, please try again.");
                        break;
                }



            }
        }
    }
}