using System;
using Microsoft.AspNetCore.Mvc;
using WeatherPredictionService.Manager;

namespace WeatherPredictionService.Controllers
{
    /// <summary>
    /// Api controller that houses the endpoints we have. This only does the request validations. No other business logic will exist here.
    /// All business logic will be handled at the manager layer.
    /// </summary>
    [ApiController]
    [Route("[controller]/{year?}/{month?}/{day?}")]
    public class WeatherForecastController : ControllerBase
    {
        private const string invalidError = "Invalid Date detected. Please enter a valid date(check if month is less than 12 and date is proper). We expect the format \"yyyy/mm/dd\"";
        private const string pastDateError = "Past date detected. This is a prediction app that makes predictions. We expect a future date in the format \"yyyy/mm/dd\"";

        private readonly IWeatherManager _weatherManager;

        public WeatherForecastController(IWeatherManager weatherManager)
        {
            _weatherManager = weatherManager;
        }

        /// <summary>
        /// Get Endpoint that returns the forecast data as a JSON object. If nothing is passed, returns data for the current date.
        /// </summary>
        /// <param name="year">year as number</param>
        /// <param name="month">month as number</param>
        /// <param name="day">day as number</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get(int? year, int? month, int? day)
        {
            //Fallback case. Set to current date
            DateTime requestForDateTime;

            if (year == null && month == null && day == null)
            {
                //Valid case. set the variable to current date
                requestForDateTime = DateTime.Today;
            } else
            {
                
                DateTime.TryParse(String.Format("{0}-{1}-{2}", year != null ? year.Value:0, month != null? month.Value : 0, day != null ?day.Value:0), out requestForDateTime);

                if (requestForDateTime == DateTime.MinValue)
                {
                    return BadRequest(invalidError);
                }
                //Since this is a prediction app, lets restrict the user from looking up past values.
                if(requestForDateTime < DateTime.Today)
                {
                    return BadRequest(pastDateError);
                }
                
            }
            
            return Ok(_weatherManager.GetWeatherForecast(requestForDateTime));
        }
    }
}
