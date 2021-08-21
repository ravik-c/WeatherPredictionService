# WeatherPredictionService

# Assumptions

The date prediction is for the future, so when the date is a past date, it won't be allowed.
The date format we want is "yyyy/mm/dd". So the API path is "weatherforecast/yyyy/mm/dd".
Invalid date will return a bad request error
# Prerequisites This project uses .net core 3.1. That needs to be installed to run this project

# Running the project To run the project,

Download the solution to a directory.
Go to project root folder in command terminal.
Go down one level to the WeatherPredictionService folder, which houses the WeatherPredictionService.csproj file, and use the command,
` dotnet build `

Run using the command,
` dotnet run `

The project launches using the port 5001 by default. Below is the usual localhost address https://localhost:5001/weatherforecast
