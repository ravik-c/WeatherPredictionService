##Weather Prediction App

#Assumptions
1. The date prediction is for the future, so when the date is a past date, it won't be allowed.
2. The date format we want is "yyyy/mm/dd". So the API path is "weatherforecast/yyyy/mm/dd".
3. Invalid date will return a bad request error

#Prerequisites
This project uses .net core 3.1. That needs to be installed to run this project

#Running the project
To run the project,
1. Download the solution to a directory. 
2. Go to solution root in command terminal and use the command,

`` dotnet build ``

3. Run using the command,

`` dotnet run ``

The project launches using the port 5001 by default. Below is the usual localhost address
https://localhost:5001/weatherforecast
