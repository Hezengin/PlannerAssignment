using PlannerAssignment.Mvvm.Models;
using System.Diagnostics;

namespace PlannerAssignment.Utils
{
    public class RequestManager
    {
        private static string _arrivalUrl = "https://gateway.apiportal.ns.nl/reisinformatie-api/api/v2/arrivals?uicCode=";
        private static string _stationUrl = "https://gateway.apiportal.ns.nl/reisinformatie-api/api/v2/stations?countryCodes=nl&q=";
        private static HttpClient _client = new HttpClient();
        private Station _currentStation;

        public RequestManager()
        {

        }

        public async Task<List<TrainModel>> GetTrainsListAsync()
        {
            try
            {

                string jsonString = await _client.GetStringAsync($"{_arrivalUrl}/{_currentStation.UICCode}");// TODO moet UIC code getten
                List<TrainModel> trains = JsonUtil.ParseTrainListJson(jsonString);
                if (trains == null)
                {
                    Application.Current.MainPage.DisplayAlert("Error", "Json is Empty", "OK");
                }
                return trains ?? new List<TrainModel>();
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
                StationModel stationModel = JsonUtil.ParseStationList(jsonString);// this is my object that contains the list the list is basically a jsonarray with stations inside of it.

                Debug.WriteLine("json: " + jsonString);

                if (stationModel != null && stationModel.Stations != null && stationModel.Stations.Count > 0)
                {
                    return stationModel;
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
