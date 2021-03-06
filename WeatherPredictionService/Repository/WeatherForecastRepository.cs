using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;
using WeatherPredictionService.Models;

namespace WeatherPredictionService.Repository
{
    /// <summary>
    /// This is the data retrieval layer. It only performs CRUD operations. No business logic to go here. 
    /// </summary>
    public class WeatherForecastRepository : IWeatherForecastRepository
    {
        IEnumerable<WeatherForecastDTO> weatherForecasts;
        public WeatherForecastRepository()
        {
            using (var reader = new StreamReader(@"./Resources/RduWeatherDataDump.csv"))
            using(var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                weatherForecasts = csv.GetRecords<WeatherForecastDTO>().ToList();
            }
        }
        /// <summary>
        /// Method that gets the weather forecast DTO based on the key requested by the manager layer. 
        /// </summary>
        /// <param name="julianDay"></param>
        /// <returns></returns>
        public WeatherForecastDTO GetWeatherForecast(int julianDay)
        {
            return weatherForecasts.Single(weatherForecast => weatherForecast.JulianDay == julianDay);  
        }
    }
}
