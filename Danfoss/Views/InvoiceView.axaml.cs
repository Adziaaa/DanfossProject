using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Danfoss.ViewModels;
using OxyPlot;
using System.Collections.Generic;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore;
using ScottPlot;
using ScottPlot.Testing;
using ScottPlot.Avalonia;
using System;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace Danfoss.Views;

public partial class InvoiceView : UserControl
{
    public InvoiceView()
    {
        InitializeComponent();

        //winter 1 Electricity price per day 00-01, winter demand 
        double[] dataA = { 1, 2,3,4,5,6,7 };// days
        //winter
        double[] dataB = { 1190.94, 615.31, 580.99, 557.82, 965.70, 773.11, 1010.17 };//winter electricity price
        double[] dataY = { 6.62, 7.58, 6.8, 6.35, 6.05, 6.08, 6.15 };//heat demand per day 
        //summer
        double[] dataX = { 752.03, 820.80, 843.68, 933.08, 738.53, 786.90, 757.50 };  // electricity price per day in Winter winter 
        double[] dataZ = { 1.79, 1.54, 1.4, 1.53, 1.7, 1.67, 1.71 };//summer heat demand


        AvaPlot ElectricityPrice = this.Find<AvaPlot>("ElectricityPrice");

        ElectricityPrice.Plot.Add.Scatter(dataA, dataB);
        ElectricityPrice.Plot.Add.Scatter(dataA, dataX);

        ElectricityPrice.Refresh();

        AvaPlot HeatDemand = this.Find<AvaPlot>("HeatDemand");

        HeatDemand.Plot.Add.Scatter(dataA, dataY);
        HeatDemand.Plot.Add.Scatter(dataA, dataZ);

        HeatDemand.Refresh();

     
    }
    

}