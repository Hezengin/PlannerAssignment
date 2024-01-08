using PlannerAssignment.Mvvm.Models;
using System.Diagnostics;

namespace PlannerAssignment.Utils
{
    public class RequestManager
    {
        private static string _arrivalUrl = "https://gateway.apiportal.ns.nl/reisinformatie-api/api/v2/arrivals?uicCode=";
        private static string _departureUrl = "https://gateway.apiportal.ns.nl/reisinformatie-api/api/v2/departures?uicCode=";
        private static string _stationUrl = "https://gateway.apiportal.ns.nl/reisinformatie-api/api/v2/stations?countryCodes=nl&q=";
        private static HttpClient _client = new HttpClient();
        private Station _currentStation;

        public RequestManager()
        {

        }

        public async Task<DepartureTrainModel> GetDeparturingTrainsListAsync()
        {
            try
            {
                string neededUrl = _departureUrl + _currentStation.UICCode;
                Debug.WriteLine(neededUrl);

                _client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "cfba573a8d014708aa013c32453eca8e");

                HttpResponseMessage response = await _client.GetAsync(neededUrl);
                response.EnsureSuccessStatusCode();

                string jsonString = await response.Content.ReadAsStringAsync();
                DepartureTrainModel trains = JsonUtil.ParseDeparturingTrainListJson(jsonString);

                if (trains != null && trains.payload != null && trains.payload.departures.Count > 0)
                {
                    return trains;
                }
                else
                {
                    Application.Current.MainPage.DisplayAlert("Error", $"{trains} is not found", "OK");
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

                if (trains != null && trains.payload != null && trains.payload.arrivals.Count > 0)
                {
                    return trains;
                }
                else
                {
                    Application.Current.MainPage.DisplayAlert("Error", $"{trains} is not found", "OK");
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
                StationModel stations = JsonUtil.ParseStationList(jsonString);// this is my object that contains the list the list is basically a jsonarray with stations inside of it.

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



    }
}
