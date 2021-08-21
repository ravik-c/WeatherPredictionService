using System;
using WeatherPredictionService.Models;
using WeatherPredictionService.Repository;

namespace WeatherPredictionService.Manager
{
    /// <summary>
    /// This is the orchestration layer. All business logic will go to this layer. 
    /// </summary>
    public class WeatherManager : IWeatherManager
    {
        IWeatherForecastRepository _weatherForecastRepository;

        public WeatherManager(IWeatherForecastRepository weatherForecastRepository)
        {
            _weatherForecastRepository = weatherForecastRepository;
        }

        /// <summary>
        /// This method requests the weather forecast DTO and maps it to a WeatherForecast object. This also houses the logic to convert the datetime to the Julian day for easier retrieval of data
        /// Any other business logic can be added at this layer.
        /// </summary>
        /// <param name="requestedDate"></param>
        /// <returns></returns>
        public WeatherForecast GetWeatherForecast(DateTime requestedDate)
        {
            var dayOfYear = requestedDate.DayOfYear;
            //day of year will vary based on the leap year in the data. So this method will add + 1 if the month is 3 or greater if the year is a leap year. This will adjust the key to the data.
            if(requestedDate.Month >= 3)
            {
                if(requestedDate.Year % 4 != 0)
                {
                    dayOfYear++;
                }
            }

            WeatherForecastDTO requestedWeatherForecastDTO = _weatherForecastRepository.GetWeatherForecast(dayOfYear);

            return new WeatherForecast
            {
                MaxTemp = requestedWeatherForecastDTO.MaxTemp,
                MinTemp = requestedWeatherForecastDTO.MinTemp,
                Precipitation = requestedWeatherForecastDTO.Precipitation,
                Date = String.Format("{0}-{1}-{2}", requestedWeatherForecastDTO.Month, requestedWeatherForecastDTO.DayOfMonth, requestedDate.Year)
            };
        }
    }
}
