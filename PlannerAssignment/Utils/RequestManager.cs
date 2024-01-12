using PlannerAssignment.Mvvm.Models;
using PolylineEncoder.Net.Utility;
using System.Diagnostics;
using System.Net.Http.Json;
using System.Text.Json.Nodes;

namespace PlannerAssignment.Utils
{
    public class RequestManager
    {
        private static string _arrivalUrl = "https://gateway.apiportal.ns.nl/reisinformatie-api/api/v2/arrivals?uicCode=";
        private static string _departureUrl = "https://gateway.apiportal.ns.nl/reisinformatie-api/api/v2/departures?uicCode=";
        private static string _stationUrl = "https://gateway.apiportal.ns.nl/reisinformatie-api/api/v2/stations?countryCodes=nl&q=";
        private static string _googleApiKey = "AIzaSyBXG_XrA3JRTL58osjxd0DbqH563e2t84o";
        private static HttpClient _client = new HttpClient();
        private Station _currentStation;
        JsonObject? _jsonResponse;

        public RequestManager()
        {

        }

        public void SetCurrentStation(Station station)
        {
            _currentStation = station;
        }
        public Station GetCurrentStation()
        {
            return _currentStation;
    }

        public async Task<DepartureTrainModel> GetDeparturingTrainsListAsync()
        {
            try
            {
                Debug.WriteLine("_currentStation.UICCode:" + _currentStation.UICCode);
                string neededUrl = _departureUrl + _currentStation.UICCode;

                _client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "cfba573a8d014708aa013c32453eca8e");

                HttpResponseMessage response = await _client.GetAsync(neededUrl);
                response.EnsureSuccessStatusCode();

                string jsonString = await response.Content.ReadAsStringAsync();
                DepartureTrainModel trains = JsonUtil.ParseDeparturingTrainListJson(jsonString);
                Debug.WriteLine("jsonDepartures: " + jsonString);

                if (trains != null && trains.payload != null && trains.payload.departures.Count > 0)
                {
                    return trains;
                }
                else
                {
                    Application.Current.MainPage.DisplayAlert("Error", "No Trains at the moment", "OK");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error getting message: {ex}");
                return null;
            }
        }

        public async Task<ArrivalTrainModel> GetArrivingTrainsListAsync()
        {
            try
            {
                string neededUrl = _arrivalUrl + _currentStation.UICCode;

                _client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "cfba573a8d014708aa013c32453eca8e");

                HttpResponseMessage response = await _client.GetAsync(neededUrl);
                response.EnsureSuccessStatusCode();

                string jsonString = await response.Content.ReadAsStringAsync();
                ArrivalTrainModel trains = JsonUtil.ParseArrivingTrainListJson(jsonString);
                Debug.WriteLine("jsonArrival: " + jsonString);

                if (trains != null && trains.payload != null && trains.payload.arrivals.Count > 0)
                {
                    return trains;
                }
                else
                {
                    Application.Current.MainPage.DisplayAlert("Error", "No Trains at the moment", "OK");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error getting message: {ex}");
                return null;
            }
        }

        public async Task<Station> GetStation(string stationName)
        {
            try
            {
                string neededUrl = _stationUrl + stationName + "&limit=1";

                _client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "cfba573a8d014708aa013c32453eca8e");

                HttpResponseMessage response = await _client.GetAsync(neededUrl);
                response.EnsureSuccessStatusCode();
                Debug.WriteLine("Response was: " + response.EnsureSuccessStatusCode());

                string jsonString = await response.Content.ReadAsStringAsync();
                Station station = JsonUtil.ParseStation(jsonString);

                Debug.WriteLine("json GetStation: " + jsonString);
                Debug.WriteLine("Parsed Station from method: " + $"UICCode: {station.UICCode}, StationType: {station.StationType}, Lat: {station.Lat}, Lng: {station.Lng}");


                if (station != null)
                {
                    return station;
                }
                else
                {
                    Application.Current.MainPage.DisplayAlert("Error", $"{stationName} is not found", "OK");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error getting message: {ex}");
                return null;
            }
        }

        public async Task<StationModel> GetUICCodeAsync(string stationName)
        {
            try
            {
                string neededUrl = _stationUrl + stationName;

                _client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "cfba573a8d014708aa013c32453eca8e");

                HttpResponseMessage response = await _client.GetAsync(neededUrl);
                response.EnsureSuccessStatusCode();

                string jsonString = await response.Content.ReadAsStringAsync();
                StationModel stations = JsonUtil.ParseStationList(jsonString);

                Debug.WriteLine("json: " + jsonString);

                if (stations != null && stations.Stations != null && stations.Stations.Count > 0)
                {
                    return stations;
                }
                else
                {
                    Application.Current.MainPage.DisplayAlert("Error", $"{stationName} is not found", "OK");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error getting message: {ex}");
                return null;
            }
        }

        public List<Location> GetRoutePolylineLocations()
        {
            Debug.WriteLine(_jsonResponse.ToJsonString());
            List<Location> locations = [];
            string encodedPolyline =
                _jsonResponse!["routes"]!.AsArray()
                [0]!.AsObject()
                ["overview_polyline"]!.AsObject()
                ["points"]!.ToString();

            PolylineUtility decoder = new();
            var coordinates = decoder.Decode(encodedPolyline);

            foreach (var coordinate in coordinates)
            {
                locations.Add(new Location(coordinate.Latitude, coordinate.Longitude));
            }

            return locations;
        }

        public string GetRouteWalkDuration()
        {
            string walkDuration = "";
            
            walkDuration = _jsonResponse!["routes"]!.AsArray()
                [0]!.AsObject()
                ["legs"]!.AsArray()
                [0]!.AsObject()
                ["duration"]!.AsObject()
                ["text"]!.ToString();

            return walkDuration;
        }

        public async Task SendRequestGAPI(Location userLocation)
        {
            //Nederlandse coordinaten zijn met comma. Google gebruikt punt.
            string userLocationURLString = $"{userLocation.Latitude.ToString().Replace(',', '.')}%2C{userLocation.Longitude.ToString().Replace(',', '.')}";
            string landmarkLocationURLString = $"{_currentStation.Lat.ToString().Replace(',', '.')}%2C{_currentStation.Lng.ToString().Replace(',', '.')}";

            string requestURL = $"https://maps.googleapis.com/maps/api/directions/json?origin={userLocationURLString}&destination={landmarkLocationURLString}&mode=walking&key={_googleApiKey}";

            var response = _client.GetAsync(requestURL).GetAwaiter().GetResult();

            if (!response.IsSuccessStatusCode)
            {
                Debug.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
            }

            
            _jsonResponse = await response.Content.ReadFromJsonAsync<JsonObject>();


            if (_jsonResponse!["status"]!.ToString() == "ZERO_RESULTS")
               throw new ApplicationException("Invalid JSON");

        }
        public JsonObject GetJsonResponse()
        {
            return _jsonResponse;
        }

    }

    
}
