using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace APIConsumer
{
    public delegate int UpdateWeatherInformationHandler(int stationsCounted);
    public class SMHIService
    {
        public event UpdateWeatherInformationHandler UpdateWeatherInformationTracker;
        /// <summary>
        /// Gets the baseline info from SMHI that contains all the individual stations keys for further data requests
        /// </summary>
        /// <returns></returns>
        public async Task<RootObject> GetBaseKeyInfoAboutWeatherLocations()
        {
            RootObject obj = new RootObject();
            string str = "";
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://opendata-download-metobs.smhi.se/api");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
               new MediaTypeWithQualityHeaderValue("application/json"));
            var response = await client.GetAsync("/api/version/latest/parameter/1.json");
            str = await response.Content.ReadAsStringAsync();

            obj = JsonConvert.DeserializeObject<RootObject>(str);

            return obj;

        }

        /// <summary>
        /// Gets all the individual stations and their measurements
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public async Task<List<TempRootObject>> GetFullListOfTemperatures(RootObject obj)
        {
            TempRootObject tempObj = new TempRootObject();
            List<TempRootObject> listTempObj = new List<TempRootObject>();
            string str = "";
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://opendata-download-metobs.smhi.se/api");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
               new MediaTypeWithQualityHeaderValue("application/json"));
            foreach (var item in obj.station)
            {
                if (item.active != false)
                {
                    var response = await client.GetAsync($"/api/version/1.0/parameter/1/station/{item.key}/period/latest-hour/data.json");
                    str = await response.Content.ReadAsStringAsync();
                    tempObj = JsonConvert.DeserializeObject<TempRootObject>(str);
                    listTempObj.Add(tempObj);
                    OnGetFullListOfTemperatures(listTempObj.Count);
                }
              
            }
            return listTempObj;
            

            
        }
        protected virtual void OnGetFullListOfTemperatures(int stationsCounted)
        {
            UpdateWeatherInformationHandler del = UpdateWeatherInformationTracker as UpdateWeatherInformationHandler;
            stationsCounted++;
            if (del != null)
            {
                del(stationsCounted);
            }
        }
        /// <summary>
        /// Gets the percipitation for Lund only during the last few months
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public async Task<PercepRootObject> GetLundPercipitation(RootObject obj)
        {
            PercepRootObject tempObj = new PercepRootObject();
            string str = "";
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://opendata-download-metobs.smhi.se/api");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
               new MediaTypeWithQualityHeaderValue("application/json"));
            foreach (var item in obj.station)
            {
                if (item.active != false)
                {
                    if (item.name == "Lund")
                    {
                        var response = await client.GetAsync($"/api/version/1.0/parameter/5/station/{item.key}/period/latest-months/data.json");
                        str = await response.Content.ReadAsStringAsync();
                        tempObj = JsonConvert.DeserializeObject<PercepRootObject>(str);
                    }
                   
                }
            }
            return tempObj;
        }
    }
}
