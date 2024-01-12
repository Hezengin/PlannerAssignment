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

        public static Station ParseStation(string jsonString)
        {
            try
            {
                // Parse the JSON string into a StationModel object
                StationModel stationModel = JsonConvert.DeserializeObject<StationModel>(jsonString);
                if (stationModel == null || stationModel.Stations == null || stationModel.Stations.Count == 0)
                {
                    Debug.WriteLine($"StationModel = {stationModel}");
                    return null;
                }
                else
                {
                    // Select the first Station object from the Stations list
                    Station station = stationModel.Stations[0];

                    Debug.WriteLine("Parsed Station: " + $"UICCode: {station.UICCode}, StationType: {station.StationType}, Lat: {station.Lat}, Lng: {station.Lng}");
                    return station;
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
