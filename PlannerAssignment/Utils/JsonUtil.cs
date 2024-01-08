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

        public static StationModel ParseStationList(string jsonString)
        {
            try
            {
                var stationModel = JsonConvert.DeserializeObject<StationModel>(jsonString);
                return stationModel;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error parsing JSON: {ex.Message}");
                return null;
            }
        }
    }
}
