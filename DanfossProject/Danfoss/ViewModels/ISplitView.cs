using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Danfoss.ViewModels;
using Danfoss.ViewModels.Charts;
using static Danfoss.ViewModels.Charts.WeatherForecast;


namespace Danfoss.ViewModels
{
    public interface ISplitView
    {
        public class ChartsPageViewModel : ViewModelBase, ISplitView
        {
            public string IconName => "PollRegular";
            public LineChartViewModel LineChartViewModel { get; } = new();



        }
    }

}