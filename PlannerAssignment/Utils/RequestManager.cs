using PlannerAssignment.Mvvm.Models;
using System.Diagnostics;

namespace PlannerAssignment.Utils
{
    public class RequestManager
    {
        private static string _arrivalUrl = "https://gateway.apiportal.ns.nl/reisinformatie-api/api/v2/arrivals?uicCode=";
        private static string _stationUrl = "https://gateway.apiportal.ns.nl/reisinformatie-api/api/v2/stations?countryCodes=nl&q=";
        private static HttpClient _client = new HttpClient();
        private StationModel _currentStation;
        private string _nextStation;

        public RequestManager()
        {

        }

        public async Task<List<TrainModel>> GetTrainsListAsync()
        {
            try
            {

                string jsonString = await _client.GetStringAsync($"{_arrivalUrl}/{_currentStation.Uic}");// TODO moet UIC code getten
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

        public async Task<List<StationModel>> GetUICCodeAsync(string stationName)
        {
            try
            {
                string jsonString = await _client.GetStringAsync($"{_stationUrl}/{stationName}");
                List<StationModel> stations = JsonUtil.ParseStationList<List<StationModel>>(jsonString);
                if(stations != null)
                {
                    //checken if the requested stations name are correct if so we can use it in the GetTrainsListAsync method 
                    foreach (StationModel station in stations)
                    {
                        if (station.NameMedium == stationName)
                        {
                            _currentStation.NameMedium = stationName;
                        }
                    }
                }
                else
                {
                    Application.Current.MainPage.DisplayAlert("Error", $"{stationName} is not found", "OK");
                    return null;
                }

                return stations;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error getting message: {ex}");
                return null;
            }
        }

    }
}
