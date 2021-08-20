using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherPredictionService.Models
{
    public class WeatherForecastDTO
    {
        [Index(0)]
        public string Month { get; set; }
        [Index(1)]
        public string DayOfMonth { get; set; }
        [Index(2)]
        public int JulianDay { get; set; }
        [Index(3)]
        public double MaxTemp { get; set; }
        [Index(4)]
        public double MinTemp { get; set; }
        [Index(5)]
        public string Precipitation { get; set; }
    }
}
