using NUnit.Framework;
using WeatherPredictionService.Controllers;
using WeatherPredictionService.Manager;
using Moq;
using System;
using Microsoft.AspNetCore.Mvc;

namespace WeatherPredictionService.Tests
{
    public class WeatherForecastControllerTests
    {
        private WeatherForecastController _controller;

        [SetUp]
        public void Setup()
        {
            //Arrange
            var weatherManager = new Mock<IWeatherManager>();
            weatherManager.Setup(wm => wm.GetWeatherForecast(It.IsAny<DateTime>())).Returns(new WeatherForecast
            {
                Date = "January-1-2020",
                MinTemp = 60.9,
                MaxTemp = 70.8,
                Precipitation = "0.9"
            });

            this._controller = new WeatherForecastController(weatherManager.Object);
        }

        [Test]
        public void WeatherForecastController_ShouldReturnSuccess()
        {
            //Act
            var today = DateTime.Today;
            var result = _controller.Get(today.Year, today.Month, today.Day);
            var okResult = result as OkObjectResult;

            //Assert
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);

            var okValue = (WeatherForecast)(okResult.Value);
            Assert.AreEqual(60.9, okValue.MinTemp);
            Assert.AreEqual("0.9", okValue.Precipitation);
        }

        [Test]
        public void WeatherForecastController_ShouldReturnSuccessWhenNoInput()
        {
            //Act
            var result = _controller.Get(null, null, null);
            var okResult = result as OkObjectResult;

            //Assert
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);

            var okValue = (WeatherForecast)(okResult.Value);
            Assert.AreEqual(60.9, okValue.MinTemp);
            Assert.AreEqual("0.9", okValue.Precipitation);
        }

        [Test]
        public void WeatherForecastController_ShouldReturnBadRequestForInvalidYearInput()
        {
            //Act
            var result = _controller.Get(null, 1, 1);
            var badRequestResult = result as BadRequestObjectResult;

            //Assert
            Assert.IsNotNull(badRequestResult);
            Assert.AreEqual(400, badRequestResult.StatusCode);
        }

        [Test]
        public void WeatherForecastController_ShouldReturnBadRequestForInvalidMonthInput()
        {
            //Act
            var result = _controller.Get(2021, null, 1);
            var badRequestResult = result as BadRequestObjectResult;

            //Assert
            Assert.IsNotNull(badRequestResult);
            Assert.AreEqual(400, badRequestResult.StatusCode);
        }

        [Test]
        public void WeatherForecastController_ShouldReturnBadRequestForInvalidDayInput()
        {
            //Act
            var result = _controller.Get(2021, 1, 42);
            var badRequestResult = result as BadRequestObjectResult;

            //Assert
            Assert.IsNotNull(badRequestResult);
            Assert.AreEqual(400, badRequestResult.StatusCode);
        }

        [Test]
        public void WeatherForecastController_ShouldReturnBadRequestForPastDate()
        {
            //Act
            var pastDate = DateTime.Today.Subtract(TimeSpan.FromDays(2));
            var result = _controller.Get(pastDate.Year, pastDate.Month, pastDate.Day);
            var badRequestResult = result as BadRequestObjectResult;

            //Assert
            Assert.IsNotNull(badRequestResult);
            Assert.AreEqual(400, badRequestResult.StatusCode);
        }
    }
}