using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlannerAssignment.Mvvm.Models
{
    public class DepartureTrainModel
    {
        [JsonProperty("payload")]
        public Payload payload { get; set; }

        [JsonProperty("links")]
        public Links links { get; set; }

        [JsonProperty("meta")]
        public Meta meta { get; set; }

        public class DepartureTrain
        {
            [JsonProperty(nameof(direction))]
            public string direction { get; set; }

            [JsonProperty("name")]
            public string name { get; set; }

            [JsonProperty("plannedDateTime")]
            public DateTime plannedDateTime { get; set; }

            [JsonProperty("plannedTimeZoneOffset")]
            public int plannedTimeZoneOffset { get; set; }

            [JsonProperty("actualDateTime")]
            public DateTime actualDateTime { get; set; }

            [JsonProperty("actualTimeZoneOffset")]
            public int actualTimeZoneOffset { get; set; }

            [JsonProperty("plannedTrack")]
            public string plannedTrack { get; set; }

            [JsonProperty("actualTrack")]
            public string actualTrack { get; set; }

            [JsonProperty("product")]
            public Product product { get; set; }

            [JsonProperty("trainCategory")]
            public string trainCategory { get; set; }

            [JsonProperty("cancelled")]
            public bool cancelled { get; set; }

            [JsonProperty("routeStations")]
            public List<RouteStation> routeStations { get; set; }

            [JsonProperty("messages")]
            public List<Message> messages { get; set; }

            [JsonProperty("departureStatus")]
            public string departureStatus { get; set; }
        }

        public class Disruptions
        {
            [JsonProperty("uri")]
            public string uri { get; set; }
        }

        public class Links
        {
            [JsonProperty("disruptions")]
            public Disruptions disruptions { get; set; }
        }

        public class Message
        {
            [JsonProperty("message")]
            public string message { get; set; }

            [JsonProperty("style")]
            public string style { get; set; }
        }

        public class Meta
        {
            [JsonProperty("numberOfDisruptions")]
            public int numberOfDisruptions { get; set; }
        }

        public class Payload
        {
            [JsonProperty("source")]
            public string source { get; set; }

            [JsonProperty("departures")]
            public List<DepartureTrain> departures { get; set; }
        }

        public class Product
        {
            [JsonProperty("number")]
            public string number { get; set; }

            [JsonProperty("categoryCode")]
            public string categoryCode { get; set; }

            [JsonProperty("shortCategoryName")]
            public string shortCategoryName { get; set; }

            [JsonProperty("longCategoryName")]
            public string longCategoryName { get; set; }

            [JsonProperty("operatorCode")]
            public string operatorCode { get; set; }

            [JsonProperty("operatorName")]
            public string operatorName { get; set; }

            [JsonProperty("type")]
            public string type { get; set; }
        }

        public class RouteStation
        {
            [JsonProperty("uicCode")]
            public string uicCode { get; set; }

            [JsonProperty("mediumName")]
            public string mediumName { get; set; }
        }
    }
}
