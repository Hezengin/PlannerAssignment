using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PlannerAssignment.Mvvm.Models;
using System.Diagnostics;

namespace PlannerAssignment.Utils
{
    public static class JsonUtil
    {
        public static ArrivalTrainModel ParseArrivingTrainListJson(string jsonString)
        {
            try
            {
                var trains = JsonConvert.DeserializeObject<ArrivalTrainModel>(jsonString);
                return trains;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error parsing JSON: {ex.Message}");
                return null;
            }
        }

        public static DepartureTrainModel ParseDeparturingTrainListJson(string jsonString)
        {
            try
            {
                var trains = JsonConvert.DeserializeObject<DepartureTrainModel>(jsonString);
                return trains;
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
