using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIConsumer
{
    public class RootObject
    {
        public string key { get; set; }
        public long updated { get; set; }
        public string title { get; set; }
        public string summary { get; set; }
        public string valueType { get; set; }
        public List<Link> link { get; set; }
        public List<StationSet> stationSet { get; set; }
        public List<Station> station { get; set; }
    }
    public class Link
    {
        public string rel { get; set; }
        public string type { get; set; }
        public string href { get; set; }
    }

    public class Link2
    {
        public string rel { get; set; }
        public string type { get; set; }
        public string href { get; set; }
    }

    public class StationSet
    {
        public string key { get; set; }
        public object updated { get; set; }
        public string title { get; set; }
        public string summary { get; set; }
        public List<Link2> link { get; set; }
    }

    public class Link3
    {
        public string rel { get; set; }
        public string type { get; set; }
        public string href { get; set; }
    }

    public class Station
    {
        public string name { get; set; }
        public string owner { get; set; }
        public int id { get; set; }
        public double height { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public bool active { get; set; }
        public object from { get; set; }
        public object to { get; set; }
        public string key { get; set; }
        public object updated { get; set; }
        public string title { get; set; }
        public string summary { get; set; }
        public List<Link3> link { get; set; }
    }
}
