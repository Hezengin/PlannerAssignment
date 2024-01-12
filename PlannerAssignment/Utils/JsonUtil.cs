using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PlannerAssignment.Mvvm.Models;
using System.Diagnostics;
using static System.Collections.Specialized.BitVector32;

namespace PlannerAssignment.Utils
{
    public static class JsonUtil
    {
        public static ArrivalTrainModel ParseArrivingTrainListJson(string jsonString)
        {
            try
            {
                var trains = JsonConvert.DeserializeObject<ArrivalTrainModel>(jsonString);
                if (trains == null || trains.payload == null || trains.payload.arrivals.Count == 0)
                {
                    Debug.WriteLine($"Invalid TrainsModel: {trains}");
                    throw new JsonSerializationException();
                }
                else
                {
                    return trains;
                }
            }
            catch (JsonSerializationException ex)
            {
                Debug.WriteLine($"Error parsing JSON for ArrivalTrainModel: {ex.Message}. Stack Trace: {ex.StackTrace}");
                throw; // Re-throw the exception to allow it to be handled higher up the call stack
            }
        }

        public static DepartureTrainModel ParseDeparturingTrainListJson(string jsonString)
        {
            try
            {
                var trains = JsonConvert.DeserializeObject<DepartureTrainModel>(jsonString);
                if (trains == null || trains.payload == null || trains.payload.departures.Count == 0)
                {
                    Debug.WriteLine($"Invalid TrainsModel: {trains}");
                    throw new JsonSerializationException();
                }
                else
                {
                    return trains;
                }
            }
            catch (JsonSerializationException ex)
            {
                Debug.WriteLine($"Error parsing JSON for DepartureTrainModel: {ex.Message}");
                throw;
            }
        }

        public static StationModel ParseStationList(string jsonString)
        {
            try
            {
                var stationModel = JsonConvert.DeserializeObject<StationModel>(jsonString);
                if (stationModel == null || stationModel.Stations == null || stationModel.Stations.Count == 0)
                {
                    Debug.WriteLine($"Invalid StationModel: {stationModel}");
                    throw new JsonSerializationException();
                }
                else
                {
                    return stationModel;
                }
            }
            catch (JsonSerializationException ex)
            {
                Debug.WriteLine($"Error parsing JSON for StationModel: {ex.Message}");
                throw;
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
                    Debug.WriteLine($"Invalid StationModel: {stationModel}");
                    throw new JsonSerializationException();
                }
                else
                {
                    // Select the first Station object from the Stations list
                    Station station = stationModel.Stations[0];

                    Debug.WriteLine($"Parsed Station: UICCode: {station.UICCode}, StationType: {station.StationType}, Lat: {station.Lat}, Lng: {station.Lng}");
                    return station;
                }
            }
            catch (JsonSerializationException ex)
            {
                Debug.WriteLine($"Error parsing JSON for StationModel: {ex.Message}");
                throw;
            }
        }
    }
}
