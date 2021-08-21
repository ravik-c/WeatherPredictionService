using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherPredictionService.Manager
{
    public interface IWeatherManager
    {
        WeatherForecast GetWeatherForecast(DateTime requestedDate);
    }
}
