using Avalonia.Media;
using LiveChartsCore;
using LiveChartsCore.Drawing;
using LiveChartsCore.Kernel;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView.VisualElements;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using static Danfoss.ViewModels.Charts.WeatherForecast;



namespace Danfoss.ViewModels.Charts
{
    public record WeatherForecast(DateOnly Date, int Temperature)
    {
        public static IEnumerable<WeatherForecast> TestData() =>
        [
        new WeatherForecast(new DateOnly(2024, 4, 21),  4),
        new WeatherForecast(new DateOnly(2024, 4, 22), 18),
        new WeatherForecast(new DateOnly(2024, 4, 23), 21),
        new WeatherForecast(new DateOnly(2024, 4, 24), 28),
        new WeatherForecast(new DateOnly(2024, 4, 25), 19),
        new WeatherForecast(new DateOnly(2024, 4, 26), 32),
        new WeatherForecast(new DateOnly(2024, 4, 27), 35),
        new WeatherForecast(new DateOnly(2024, 4, 28), 20),
        new WeatherForecast(new DateOnly(2024, 4, 29), 30),
        new WeatherForecast(new DateOnly(2024, 4, 30), 16)
        ];
    }

    public class LineChartViewModel
    {
        public LabelVisual Title { get; set; } =
            new()
            {
                Text = "My chart title",
                TextSize = 25,
                Padding = new Padding(15),
                Paint = new SolidColorPaint(SKColors.WhiteSmoke)
            };

        public ISeries[] Series { get; set; } =
        [
            new LineSeries<WeatherForecast>
        {
            Values = WeatherForecast.TestData(),
            Mapping = (sample, _) => new Coordinate(sample.Date.Day, sample.Temperature),
            Fill = null
        }
        ];

        public Axis[] XAxes { get; set; } = [new Axis { MinStep = 1, }];
    }
}
