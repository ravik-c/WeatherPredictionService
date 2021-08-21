using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using WeatherPredictionService.Manager;
using WeatherPredictionService.Repository;
using WeatherPredictionService.Models;

namespace WeatherPredictionService.Tests
{
    public class WeatherManagerTest
    {
        WeatherManager _manager;

        [SetUp]
        public void Setup()
        {
            //Arrange
            var weatherRepo = new Mock<IWeatherForecastRepository>();
            weatherRepo.Setup(wr => wr.GetWeatherForecast(61)).Returns(new WeatherForecastDTO()
            {
                JulianDay = 61,
                DayOfMonth = "1",
                Month = "March",
                MaxTemp = 70.1,
                MinTemp = 45.4,
                Precipitation = "1.9"
            });
            _manager = new WeatherManager(weatherRepo.Object);
        }

        [Test]
        public void WeatherManager_ShouldReturnSuccessWhenDateWithNonLeapYearPassed()
        {
            //Act
            var weatherForecastDTO = _manager.GetWeatherForecast(new DateTime(2021, 3, 1));

            //Assert
            Assert.AreEqual("March-1-2021", weatherForecastDTO.Date);
            Assert.AreEqual(70.1, weatherForecastDTO.MaxTemp);
        }
        [Test]
        public void WeatherManager_ShouldReturnSuccessWhenDateWithLeapYearPassed()
        {
            //Act
            var weatherForecastDTO = _manager.GetWeatherForecast(new DateTime(2024, 3, 1));

            //Assert
            Assert.AreEqual("March-1-2024", weatherForecastDTO.Date);
            Assert.AreEqual(70.1, weatherForecastDTO.MaxTemp);
        }

    }
}
