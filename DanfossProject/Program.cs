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
            FunctionalityOfAM assetManager = new FunctionalityOfAM();
            assetManager.AddAssets();

            CsvManager SdmObject = new CsvManager();

            
            List<SdmRecord> WinterSourceDataManager = SdmObject.ReadCsv("Winter_SDM.csv");
            List<SdmRecord> SummerSourceDataManager = SdmObject.ReadCsv("Summer_SDM.csv");
            //Console.WriteLine("\nWinter:");
            ///SdmObject.DisplaySdm(WinterSourceDataManager);
            //Console.WriteLine("\nSummer: ");
            //SdmObject.DisplaySdm(SummerSourceDataManager);
            //Console.Read();

            var opti = new EnergyOptimizer(SdmObject, assetManager);
            opti.OptimizeEnergy(WinterSourceDataManager, SummerSourceDataManager);

            Console.WriteLine("1.Start asset\n2.Start asset\n3.Exit");

            while (true)
            {

                var option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        foreach (Model asset in assetManager.AssetManager)
                        {
                            assetManager.StopAsset(asset);
                        }
                        break;
                    case "2":
                        foreach (Model asset in assetManager.AssetManager)
                        {
                            assetManager.StartAsset(asset);
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