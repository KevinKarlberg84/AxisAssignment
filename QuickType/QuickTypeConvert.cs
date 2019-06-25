using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickType
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class Welcome
    {
        [JsonProperty("key")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Key { get; set; }

        [JsonProperty("updated")]
        public long Updated { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("summary")]
        public string Summary { get; set; }

        [JsonProperty("valueType")]
        public string ValueType { get; set; }

        [JsonProperty("link")]
        public List<Link> Link { get; set; }

        [JsonProperty("stationSet")]
        public List<StationSet> StationSet { get; set; }

        [JsonProperty("station")]
        public List<Station> Station { get; set; }
    }

    public partial class Link
    {
        [JsonProperty("rel")]
        public Rel Rel { get; set; }

        [JsonProperty("type")]
        public TypeEnum Type { get; set; }

        [JsonProperty("href")]
        public Uri Href { get; set; }
    }

    public partial class Station
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("owner")]
        public Owner Owner { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("height")]
        public double Height { get; set; }

        [JsonProperty("latitude")]
        public double Latitude { get; set; }

        [JsonProperty("longitude")]
        public double Longitude { get; set; }

        [JsonProperty("active")]
        public bool Active { get; set; }

        [JsonProperty("from")]
        public long From { get; set; }

        [JsonProperty("to")]
        public long To { get; set; }

        [JsonProperty("key")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Key { get; set; }

        [JsonProperty("updated")]
        public long Updated { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("summary")]
        public string Summary { get; set; }

        [JsonProperty("link")]
        public List<Link> Link { get; set; }
    }

    public partial class StationSet
    {
        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("updated")]
        public object Updated { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("summary")]
        public string Summary { get; set; }

        [JsonProperty("link")]
        public List<Link> Link { get; set; }
    }

    public enum Rel { Metadata, Parameter, Station, StationSet, Version };

    public enum TypeEnum { ApplicationAtomXml, ApplicationJson, ApplicationXml };

    public enum Owner { FlygplatsbolagKommunLandsting, Försvarsmakten, IckeNamngivenÄgare, Smhi, Swedavia, Trafikverket };

    public partial class Welcome
    {
        public static Welcome FromJson(string json) => JsonConvert.DeserializeObject<Welcome>(json, QuickType.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this Welcome self) => JsonConvert.SerializeObject(self, QuickType.Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                RelConverter.Singleton,
                TypeEnumConverter.Singleton,
                OwnerConverter.Singleton,
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class ParseStringConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(long) || t == typeof(long?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            long l;
            if (Int64.TryParse(value, out l))
            {
                return l;
            }
            throw new Exception("Cannot unmarshal type long");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (long)untypedValue;
            serializer.Serialize(writer, value.ToString());
            return;
        }

        public static readonly ParseStringConverter Singleton = new ParseStringConverter();
    }

    internal class RelConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Rel) || t == typeof(Rel?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "metadata":
                    return Rel.Metadata;
                case "parameter":
                    return Rel.Parameter;
                case "station":
                    return Rel.Station;
                case "stationSet":
                    return Rel.StationSet;
                case "version":
                    return Rel.Version;
            }
            throw new Exception("Cannot unmarshal type Rel");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (Rel)untypedValue;
            switch (value)
            {
                case Rel.Metadata:
                    serializer.Serialize(writer, "metadata");
                    return;
                case Rel.Parameter:
                    serializer.Serialize(writer, "parameter");
                    return;
                case Rel.Station:
                    serializer.Serialize(writer, "station");
                    return;
                case Rel.StationSet:
                    serializer.Serialize(writer, "stationSet");
                    return;
                case Rel.Version:
                    serializer.Serialize(writer, "version");
                    return;
            }
            throw new Exception("Cannot marshal type Rel");
        }

        public static readonly RelConverter Singleton = new RelConverter();
    }

    internal class TypeEnumConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(TypeEnum) || t == typeof(TypeEnum?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "application/atom+xml":
                    return TypeEnum.ApplicationAtomXml;
                case "application/json":
                    return TypeEnum.ApplicationJson;
                case "application/xml":
                    return TypeEnum.ApplicationXml;
            }
            throw new Exception("Cannot unmarshal type TypeEnum");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (TypeEnum)untypedValue;
            switch (value)
            {
                case TypeEnum.ApplicationAtomXml:
                    serializer.Serialize(writer, "application/atom+xml");
                    return;
                case TypeEnum.ApplicationJson:
                    serializer.Serialize(writer, "application/json");
                    return;
                case TypeEnum.ApplicationXml:
                    serializer.Serialize(writer, "application/xml");
                    return;
            }
            throw new Exception("Cannot marshal type TypeEnum");
        }

        public static readonly TypeEnumConverter Singleton = new TypeEnumConverter();
    }

    internal class OwnerConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Owner) || t == typeof(Owner?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "Flygplatsbolag (kommun/landsting)":
                    return Owner.FlygplatsbolagKommunLandsting;
                case "Försvarsmakten":
                    return Owner.Försvarsmakten;
                case "Icke namngiven ägare":
                    return Owner.IckeNamngivenÄgare;
                case "SMHI":
                    return Owner.Smhi;
                case "Swedavia":
                    return Owner.Swedavia;
                case "Trafikverket":
                    return Owner.Trafikverket;
            }
            throw new Exception("Cannot unmarshal type Owner");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (Owner)untypedValue;
            switch (value)
            {
                case Owner.FlygplatsbolagKommunLandsting:
                    serializer.Serialize(writer, "Flygplatsbolag (kommun/landsting)");
                    return;
                case Owner.Försvarsmakten:
                    serializer.Serialize(writer, "Försvarsmakten");
                    return;
                case Owner.IckeNamngivenÄgare:
                    serializer.Serialize(writer, "Icke namngiven ägare");
                    return;
                case Owner.Smhi:
                    serializer.Serialize(writer, "SMHI");
                    return;
                case Owner.Swedavia:
                    serializer.Serialize(writer, "Swedavia");
                    return;
                case Owner.Trafikverket:
                    serializer.Serialize(writer, "Trafikverket");
                    return;
            }
            throw new Exception("Cannot marshal type Owner");
        }

        public static readonly OwnerConverter Singleton = new OwnerConverter();
    }
}
