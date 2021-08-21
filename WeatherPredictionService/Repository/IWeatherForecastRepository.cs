using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherPredictionService.Models;

namespace WeatherPredictionService.Repository
{
    public interface IWeatherForecastRepository
    {
        WeatherForecastDTO GetWeatherForecast(int julianDay);
    }
}
