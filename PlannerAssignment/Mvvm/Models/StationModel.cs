using Newtonsoft.Json;

namespace PlannerAssignment.Mvvm.Models
{
    [Serializable]
    public class StationModel
    {
        [JsonProperty("payload")]
        public List<Station> Stations { get; set; }
    }

    public class Names
    {
        [JsonProperty("lang")]
        public string Long { get; set; }

        [JsonProperty("middel")]
        public string Mid{ get; set; }

        [JsonProperty("kort")]
        public string Short{ get; set; }
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

        [JsonProperty("stationType")]
        public string StationType { get; set; }

        [JsonProperty("EVACode")]
        public string EVACode { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("cdCode")]
        public int CDCode { get; set; }

        [JsonProperty("sporen")]
        public List<Track> Sporen { get; set; }

        [JsonProperty("synoniemen")]
        public List<object> Synoniemen { get; set; }

        [JsonProperty("heeftFaciliteiten")]
        public bool HeeftFaciliteiten { get; set; }

        [JsonProperty("heeftVertrektijden")]
        public bool HeeftVertrektijden { get; set; }

        [JsonProperty("heeftReisassistentie")]
        public bool HeeftReisassistentie { get; set; }

        [JsonProperty("namen")]
        public Names Namen { get; set; }

        [JsonProperty("land")]
        public string Land { get; set; }

        [JsonProperty("lat")]
        public double Lat { get; set; }

        [JsonProperty("lng")]
        public double Lng { get; set; }

        [JsonProperty("radius")]
        public int Radius { get; set; }

        [JsonProperty("naderenRadius")]
        public int NaderenRadius { get; set; }

        [JsonProperty("ingangsDatum")]
        public string IngangsDatum { get; set; }

        [JsonProperty("nearbyMeLocationId")]
        public NearbyMeLocationId NearbyMeLocationId { get; set; }
    }

    public class Track
    {
        [JsonProperty("spoorNummer")]
        public string SpoorNummer { get; set; }
    }
}
