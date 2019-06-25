using Newtonsoft.Json;
using OutputTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace OutputTest.Controllers
{
    public class HomeController : Controller
    {
        public async Task<ActionResult> Index()
        {
            string str = "";

            RootObject objects = new RootObject();
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://opendata-download-metobs.smhi.se/api");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
               new MediaTypeWithQualityHeaderValue("application/json"));
            var response = await client.GetAsync("/api/version/latest/parameter/1.json");
            str = await response.Content.ReadAsStringAsync();

            objects = JsonConvert.DeserializeObject<RootObject>(str);
    

            return View(objects);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}