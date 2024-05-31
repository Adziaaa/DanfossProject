using Avalonia;
using Avalonia.ReactiveUI;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Danfoss;

sealed class Program
{
    // Initialization code. Don't use any Avalonia, third-party APIs or any
    // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
    // yet and stuff might break.
    [STAThread]
    public static void Main(string[] args)
    {
        BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);

        RunAdditionalFunctionality();
    }

    public static void RunAdditionalFunctionality()
    {
        FunctionalityOfAM AM = new FunctionalityOfAM();
        AM.AddAssets();

        CsvManager SdmObject = new CsvManager();

        List<SdmRecord> WinterSourceDataManager = SdmObject.ReadCsv("Winter_SDM.csv");
        List<SdmRecord> SummerSourceDataManager = SdmObject.ReadCsv("Summer_SDM.csv");

        optfromscratch optfromscratchObject = new optfromscratch();

        List<Model> models = new List<Model>();

        var (result_winter, CO2result_winter) = optfromscratchObject.OptimizeData(AM.AssetManager, WinterSourceDataManager);
        var (result_summer, CO2result_summer) = optfromscratchObject.OptimizeData(AM.AssetManager, SummerSourceDataManager);

        ResultDataCsvManager RDM = new ResultDataCsvManager();
        RDM.RdmCsvWriter(result_winter, "ResultWinter.csv");
        RDM.RdmCsvWriter(CO2result_winter, "CO2ResultWinter.csv");
        RDM.RdmCsvWriter(result_summer, "ResultSummer.csv");
        RDM.RdmCsvWriter(CO2result_summer, "CO2ResultSummer.csv");
    }

    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace()
            .UseReactiveUI();
}
