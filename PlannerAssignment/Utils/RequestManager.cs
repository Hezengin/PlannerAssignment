using Newtonsoft.Json;
using PlannerAssignment.Database;
using PlannerAssignment.Mvvm.Models;
using SQLite;
using System.Diagnostics;

namespace PlannerAssignment.Utils
{
    public class RequestManager
    {
        private static string url = "https://gateway.apiportal.ns.nl/reisinformatie-api/api/v2/arrivals?uicCode=";
        private static HttpClient client = new HttpClient();
        private readonly SQLiteAsyncConnection _database;

        public RequestManager(SQLiteAsyncConnection database)
        {
            _database = database;
        }
        public async Task<List<TrainModel>> GetTrainsListAsync()
        {
            try
            {
                string jsonString = await client.GetStringAsync($"{url}8400621");
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
    }
}
