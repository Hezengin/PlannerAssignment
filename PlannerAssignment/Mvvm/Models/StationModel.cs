using Newtonsoft.Json;
using SQLite;


namespace PlannerAssignment.Mvvm.Models
{
    [Serializable]
    public class StationModel
    {
        [JsonProperty("payload")]
        public List<Station> Stations { get; set; }
    }

    [Table("names")]
    public class Names
    {
        [Column("name")]
        [JsonProperty("lang")]
        public string Long { get; set; }

        [Ignore]
        [JsonProperty("middel")]
        public string Mid { get; set; }
        
        [Ignore]
        [JsonProperty("kort")]
        public string Short { get; set; }
    }

    public class NearbyMeLocationId
    {
        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }

    public class Station
    {
        [JsonProperty("UICCode")]
        public string UICCode { get; set; }

        [Ignore]
        [JsonProperty("stationType")]
        public string StationType { get; set; }

        [Ignore]
        [JsonProperty("EVACode")]
        public string EVACode { get; set; }
        
        [Ignore]
        [JsonProperty("code")]
        public string Code { get; set; }

        [Ignore]
        [JsonProperty("cdCode")]
        public int CDCode { get; set; }

        [Ignore]
        [JsonProperty("sporen")]
        public List<Track> Sporen { get; set; }

        [Ignore]
        [JsonProperty("synoniemen")]
        public List<object> Synoniemen { get; set; }

        [Ignore]
        [JsonProperty("heeftFaciliteiten")]
        public bool HeeftFaciliteiten { get; set; }

        [Ignore]
        [JsonProperty("heeftVertrektijden")]
        public bool HeeftVertrektijden { get; set; }

        [Ignore]
        [JsonProperty("heeftReisassistentie")]
        public bool HeeftReisassistentie { get; set; }

        [Ignore]
        [JsonProperty("namen")]
        public Names Namen { get; set; }

        [Ignore]
        [JsonProperty("land")]
        public string Land { get; set; }

        [Ignore]
        [JsonProperty("lat")]
        public double Lat { get; set; }

        [Ignore]
        [JsonProperty("lng")]
        public double Lng { get; set; }
        
        [Ignore]
        [JsonProperty("radius")]
        public int Radius { get; set; }

        [Ignore]
        [JsonProperty("naderenRadius")]
        public int NaderenRadius { get; set; }

        [Ignore]
        [JsonProperty("ingangsDatum")]
        public string IngangsDatum { get; set; }

        [Ignore]
        [JsonProperty("nearbyMeLocationId")]
        public NearbyMeLocationId NearbyMeLocationId { get; set; }
    }

    public class Track
    {
        [JsonProperty("spoorNummer")]
        public string SpoorNummer { get; set; }
    }
}
