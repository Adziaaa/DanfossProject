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

                        break;
                    case "4":
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