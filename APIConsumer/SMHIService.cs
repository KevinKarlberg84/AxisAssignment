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
    public class SMHIService : IWeatherService
    {
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
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("https://opendata-download-metobs.smhi.se/api");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                   new MediaTypeWithQualityHeaderValue("application/json"));
                foreach (var station in obj.station)
                {
                    if (station.active != false)
                    {
                        var response = await client.GetAsync($"/api/version/1.0/parameter/1/station/{station.key}/period/latest-hour/data.json");
                        var str = await response.Content.ReadAsStringAsync();
                        tempObj = JsonConvert.DeserializeObject<TempRootObject>(str);
                        listTempObj.Add(tempObj);
                    }

                }
               
            }
            catch (Exception)
            {
                // This is where i would handle the error, log it or whatever the application would require
                throw;
            }
            return listTempObj;
        }
        /// <summary>
        /// Gets the percipitation for Lund only during the last few months
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public async Task<PercepRootObject> GetLundPercipitation(RootObject obj)
        {

            PercepRootObject tempObj = new PercepRootObject();
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("https://opendata-download-metobs.smhi.se/api");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                   new MediaTypeWithQualityHeaderValue("application/json"));
                foreach (var station in obj.station)
                {
                    if (station.active != false)
                    {
                        if (station.name == "Lund")
                        {
                            var response = await client.GetAsync($"/api/version/1.0/parameter/5/station/{station.key}/period/latest-months/data.json");
                            var str = await response.Content.ReadAsStringAsync();
                            tempObj = JsonConvert.DeserializeObject<PercepRootObject>(str);
                        }

                    }
                }
               
            }
            catch (Exception)
            {
                // This is where i would handle the error, log it or whatever the application would require
                throw;
            }
            return tempObj;
        }
    }
}
