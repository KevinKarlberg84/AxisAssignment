using APIConsumer;
using AxisProgrammingAssignment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AxisProgrammingAssignment
{
    class WeatherLogic
    {
        IWeatherService _weatherService;
        RootObject baseInfoResponse;
        List<TempRootObject> weatherStationInfoResponse;
        PercepRootObject percipitationResponse;
        public WeatherLogic(IWeatherService weatherService)
        {
            _weatherService = weatherService;
            baseInfoResponse = new RootObject();
            weatherStationInfoResponse = new List<TempRootObject>();
            percipitationResponse = new PercepRootObject();
        }
        // A list of the resources required for this TaskFactory to communicate with, and construct the methods for the API communication and BusinessLogic
        /// <summary>
        /// The method used to update the weather information in memory
        /// </summary>
        /// <returns></returns>
        public async Task UpdateWeatherInformation()
        {
            baseInfoResponse = await _weatherService.GetBaseKeyInfoAboutWeatherLocations();
            weatherStationInfoResponse = await _weatherService.GetFullListOfTemperatures(baseInfoResponse);
            percipitationResponse = await _weatherService.GetLundPercipitation(baseInfoResponse);
        }
        /// <summary>
        /// Calculates the average temperature for all weather stations and returns a double with the answer
        /// </summary>
        /// <returns></returns>
        public double CalculateAverageTemperature()
        {
            int counter = 0;
            double totalTemp = 0;
            foreach (var valueList in weatherStationInfoResponse)
            {
                if (valueList.value != null)
                {
                    foreach (var value in valueList.value)
                    {
                        counter++;
                        // Since the measurements are listed in a Swedish way with a comma and Visual Studio has an american way with a dot for the double values
                        //, the dot [.] needed to be replaced with a comma [,] to be able to read in the values
                        totalTemp += double.Parse(value.value.Replace('.', ','));
                    }

                }
            }
            return totalTemp / counter;
            
        }
        /// <summary>
        ///  Calculates the total percipitation in Lund during the last few months and returns a Model containing the information
        /// </summary>
        /// <returns></returns>
        public TotalPercipitationModel CalculatePercipitationInLund()
        {
            double rainInMilimeters = 0;
            foreach (var item in percipitationResponse.value)
            {
                rainInMilimeters += double.Parse(item.value.Replace('.', ','));
            }
            return new TotalPercipitationModel(percipitationResponse.value[0].@ref, percipitationResponse.value[percipitationResponse.value.Count - 1].@ref, rainInMilimeters);
        }

        /// <summary>
        /// Returns a full list of models that contain all values for all currently active stations.
        /// </summary>
        public List<WeatherStationModel> GetFullListOfWeatherReports()
        {
            List<WeatherStationModel> weatherStations = new List<WeatherStationModel>();
            foreach (var item in weatherStationInfoResponse)
            {
                if (item.value != null)
                {
                    foreach (var item2 in item.value)
                    {
                        DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
                        dtDateTime = dtDateTime.AddSeconds(item2.date / 1000);
                        weatherStations.Add(new WeatherStationModel(dtDateTime, item.station.name, item2.value));      
                    }
                }
            }
            return weatherStations;
        }
    }
}
