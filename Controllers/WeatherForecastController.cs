using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WeatherPredictionService.Manager;

namespace WeatherPredictionService.Controllers
{
    /// <summary>
    /// Api controller that houses the endpoints we have
    /// </summary>
    [ApiController]
    [Route("[controller]/{year?}/{month?}/{day?}")]
    public class WeatherForecastController : ControllerBase
    {
        private const string invalidError = "Invalid Date detected. Please enter a valid date(check if month is less than 12 and date is proper). We expect the format \"yyyy/mm/dd\"";
        private const string pastDateError = "Past date detected. This is a prediction app that makes predictions. We expect a future date in the format \"yyyy/mm/dd\"";

        private readonly IWeatherManager _weatherManager;

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IWeatherManager weatherManager)
        {
            _logger = logger;
            _weatherManager = weatherManager;
        }

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
                
                DateTime.TryParse(String.Format("{0}-{1}-{2}", year.Value, month.Value, day.Value), out requestForDateTime);

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
