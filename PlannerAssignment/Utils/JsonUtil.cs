using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PlannerAssignment.Mvvm.Models;
using System.Diagnostics;

namespace PlannerAssignment.Utils
{
    public static class JsonUtil
    {
        public static List<TrainModel> ParseTrainListJson(string jsonString)
        {
            try
            {
                var trains = JsonConvert.DeserializeObject<Dictionary<string, JObject>>(jsonString);
                List<TrainModel> Trains = new List<TrainModel>();

                foreach (var item in trains)
                {
                    TrainModel model = item.Value.ToObject<TrainModel>();
                    Trains.Add(model);
                }

                return Trains;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error parsing JSON: {ex.Message}");
                return null;
            }
        }

        public static T ParseStationList<T>(string jsonString) where T : class
        {
            try
            {
                if (typeof(T).IsGenericType && typeof(T).GetGenericTypeDefinition() == typeof(List<>))
                {
                    var list = JsonConvert.DeserializeObject<List<T>>(jsonString);
                    return list as T;
                }
                else
                {
                    var obj = JsonConvert.DeserializeObject<T>(jsonString);
                    return obj;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error parsing JSON: {ex.Message}");
                return null;
            }
        }
    }
}
