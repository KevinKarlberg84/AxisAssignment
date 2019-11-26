using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using APIConsumer;
using System.Threading;

namespace AxisProgrammingAssignment
{
    class Program
    {
        static DateTime lastUpdateTime;
        static void Main(string[] args)
        {
            WeatherLogic weather = new WeatherLogic(new SMHIService());
            Menu(weather);
        }
        static void Menu(WeatherLogic weather)
        {

            bool keepRunning = true;
            while (keepRunning)
            {
                CancellationTokenSource cts = new CancellationTokenSource();
                // An addition to make sure unecessary calls to the API doesn't happen when no new hour has passed
                if (DateTime.Now.Hour > lastUpdateTime.Hour + 1 || lastUpdateTime == null)
                {
                    lastUpdateTime = DateTime.Now;
                    weather.UpdateWeatherInformation().Wait();
                }
                Console.WriteLine("Welcome to Kevins version of the Axis Assignment");
                Console.WriteLine("Which operation do you wish to see?");
                Console.WriteLine("[1]: Average temperature for Sweden the last hour");
                Console.WriteLine("[2]: The percipitation for Lund for the last few months");
                Console.WriteLine("[3]: Listing of [Date, Location , Temperature] for all locations in Sweden");
                Console.WriteLine("[4]: Exit");
                string answer = Console.ReadLine();
                switch (answer)
                {
                    case "1":
                        AverageTemperatureForSweden(weather);
                        break;
                    case "2":
                        TotalPercipitationForLund(weather);
                        break;
                    case "3":
                        ThreadingAllLocations(weather, cts);
                        break;
                    case "4":
                        Console.WriteLine("Thank you for using this application. Have a nice day");
                        Thread.Sleep(3000);
                        keepRunning = false;
                        break;
                    default:
                        Console.WriteLine("I'm sorry, that was not an acceptable menue choice. Please select a menue option using the numbers 1-4 and press enter");
                        break;
                }
                if (keepRunning == true)
                {
                    Console.WriteLine("Returning you to main menue");
                    Thread.Sleep(5000);
                    Console.Clear();
                }

            }

        }
        static void ThreadingAllLocations(WeatherLogic weather, CancellationTokenSource cts)
        {
            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                Console.ReadKey(true);
                cts.Cancel();
            }).Start();
            WriteAllLocationsWithDelay(weather, cts.Token);
        }
        static void WriteAllLocationsWithDelay(WeatherLogic weather, CancellationToken ct)
        {
            var weatherStationList = weather.GetFullListOfWeatherReports();

            foreach (var weatherStation in weatherStationList)
            {
                Console.WriteLine($"Date: {weatherStation.DateOfMeasurement}. Location: {weatherStation.City}. Temperature: {weatherStation.Measurement}");
                Thread.Sleep(100);
                if (ct.IsCancellationRequested)
                {
                    Console.WriteLine("Cancel was requested, returning you to main menu");
                    return;
                }
            }
        }
        static void TotalPercipitationForLund(WeatherLogic weather)
        {
            var lundPercipitationObject = weather.CalculatePercipitationInLund();
            Console.WriteLine($"From {lundPercipitationObject.MeasurementStartDate} to {lundPercipitationObject.MeasurementEndDate} a total of {lundPercipitationObject.TotalAmountOfWaterInMilimeters} milimeters of water fell");
        }
        static void AverageTemperatureForSweden(WeatherLogic weather)
        {

            double avgTemp = weather.CalculateAverageTemperature();
            Console.WriteLine($"The average temperature for all measured weather stations in Sweden is currently: {avgTemp}");
        }


    }
}
