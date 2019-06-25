using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIConsumer
{

        public class Value
        {
            public long date { get; set; }
            public string value { get; set; }
            public string quality { get; set; }
        }

        public class Parameter
        {
            public string key { get; set; }
            public string name { get; set; }
            public string summary { get; set; }
            public string unit { get; set; }
        }

        public class TempStation
        {
            public string key { get; set; }
            public string name { get; set; }
            public string owner { get; set; }
            public double height { get; set; }
        }

        public class Period
        {
        public string key { get; set; }
        public long from { get; set; }
        public long to { get; set; }
        public string summary { get; set; }
        public string sampling { get; set; }
    }

        public class Position
        {
        public object from { get; set; }
        public object to { get; set; }
        public double height { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
    }

        public class TempLink
        {
        public string rel { get; set; }
        public string type { get; set; }
        public string href { get; set; }
    
}

        public class TempRootObject
        {
        public List<Value> value { get; set; }
        public long updated { get; set; }
        public Parameter parameter { get; set; }
        public Station station { get; set; }
        public Period period { get; set; }
        public List<Position> position { get; set; }
        public List<Link> link { get; set; }
    }
    
}
