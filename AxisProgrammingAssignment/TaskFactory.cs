using APIConsumer;
using AxisProgrammingAssignment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AxisProgrammingAssignment
{
    public delegate int UpdateWeatherInformationHandler(int stationsCounted);
    class TaskFactory
    {
        public event UpdateWeatherInformationHandler UpdateWeatherInformationTracker;
        // A list of the resources required for this TaskFactory to communicate with, and construct the methods for the API communication and BusinessLogic
        SMHIService api = new SMHIService();
        RootObject baseInfoResponse = new RootObject();
        List<TempRootObject> percipitationResponse = new List<TempRootObject>();
        PercepRootObject weatherStationInfoResponse = new PercepRootObject();
        /// <summary>
        /// The method used to update the weather information in memory
        /// </summary>
        /// <returns></returns>
        public async Task UpdateWeatherInformation()
        {
            SMHIService api = new SMHIService();
            baseInfoResponse = await api.GetBaseKeyInfoAboutWeatherLocations();
            percipitationResponse = await api.GetFullListOfTemperatures(baseInfoResponse);
            weatherStationInfoResponse = await api.GetLundPercipitation(baseInfoResponse);
        }
        /// <summary>
        /// Calculates the average temperature for all weather stations and returns a double with the answer
        /// </summary>
        /// <returns></returns>
        public double CalculateAverageTemperature()
        {
            int counter = 0;
            double totalTemp = 0;
            foreach (var valueList in percipitationResponse)
            {
                if (valueList.value != null)
                {
                    foreach (var item2 in valueList.value)
                    {
                        counter++;
                        // Since the measurements are listed in a Swedish way with a comma and Visual Studio has an american way with a dot for the double values
                        //, the dot [.] needed to be replaced with a comma [,] to be able to read in the values
                        totalTemp += double.Parse(item2.value.Replace('.', ','));
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
            foreach (var item in weatherStationInfoResponse.value)
            {
                rainInMilimeters += double.Parse(item.value.Replace('.', ','));
            }
            return new TotalPercipitationModel(weatherStationInfoResponse.value[0].@ref, weatherStationInfoResponse.value[weatherStationInfoResponse.value.Count - 1].@ref, rainInMilimeters);
        }

        /// <summary>
        /// Returns a full list of models that contain all values for all currently active stations.
        /// </summary>
        public List<WeatherStationModel> GetFullListOfWeatherReports()
        {
            List<WeatherStationModel> weatherStations = new List<WeatherStationModel>();
            foreach (var item in percipitationResponse)
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
