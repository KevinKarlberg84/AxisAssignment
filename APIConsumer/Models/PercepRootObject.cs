using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIConsumer
{
    public class PercepRootObject
    {
        public List<PercepValue> value { get; set; }
        public long updated { get; set; }
        public PercepParameter parameter { get; set; }
        public PercepStation station { get; set; }
        public PercepPeriod period { get; set; }
        public List<PercepPosition> position { get; set; }
        public List<PercepLink> link { get; set; }

    }
    public class PercepValue
    {
        public object from { get; set; }
        public object to { get; set; }
        public string @ref { get; set; }
        public string value { get; set; }
        public string quality { get; set; }
    }
    public class PercepParameter
    {
        public string key { get; set; }
        public string name { get; set; }
        public string summary { get; set; }
        public string unit { get; set; }
    }
    public class PercepStation
    {
        public string key { get; set; }
        public string name { get; set; }
        public string owner { get; set; }
        public double height { get; set; }
    }
    public class PercepPeriod
    {
        public string key { get; set; }
        public long from { get; set; }
        public long to { get; set; }
        public string summary { get; set; }
        public string sampling { get; set; }
    }
    public class PercepPosition
    {
        public object from { get; set; }
        public object to { get; set; }
        public double height { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
    }
    public class PercepLink
    {
        public string rel { get; set; }
        public string type { get; set; }
        public string href { get; set; }
    }
}
