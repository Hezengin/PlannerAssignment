using PlannerAssignment.Utils;
using PlannerAssignment.Mvvm.Models;
using System.Text.Json.Nodes;
using Xunit.Abstractions;


namespace UnitTestProject.Happy
{
    public class RequestManagerTest
    {
        [Fact]
        public async void GoogleAPIJsonTest()
        {
            RequestManager requestManager = new RequestManager();
            StationModel station = await requestManager.GetUICCodeAsync("Rotterdam Centraal");

            requestManager.SetCurrentStation(station.Stations[0]);

            await requestManager.SendRequestGAPI(new Location(51.91391161801332, 4.481820066452984));
            
            JsonObject jObject = requestManager.GetJsonResponse();
            Assert.NotNull(jObject);

            string addressStartLocation = jObject!["routes"]!.AsArray()
                [0]!.AsObject()
                ["legs"]!.AsArray()
                [0]!.AsObject()
                ["start_address"]!.ToString();

            Assert.Equal("Ketelaarstraat 1, 3011 CM Rotterdam, Netherlands", addressStartLocation);
        }

        [Fact]
        public async void GetStationFromAPITest()
        {
            RequestManager requestManager = new RequestManager();
            StationModel stationModelData = await requestManager.GetUICCodeAsync("Rotterdam Centraal");
            Assert.NotNull(stationModelData);

            Station station = stationModelData.Stations[0];

            Assert.NotNull(station);
            Assert.Equal("Rotterdam Centraal", station.Namen.Long);
        }

        [Fact]
        public async void GetDeparturesFromAPITest()
        {
            RequestManager requestManager = new RequestManager();
            StationModel stationModelData = await requestManager.GetUICCodeAsync("Rotterdam Centraal");
            Station station = stationModelData.Stations[0];
            requestManager.SetCurrentStation(station);

            DepartureTrainModel dTM = await requestManager.GetDeparturingTrainsListAsync();

            Assert.True(dTM.payload.departures.Count > 0);
        }

        [Fact]
        public async void GetArrivalsFromAPITest()
        {
            RequestManager requestManager = new RequestManager();
            StationModel stationModelData = await requestManager.GetUICCodeAsync("Rotterdam Centraal");
            Station station = stationModelData.Stations[0];
            requestManager.SetCurrentStation(station);

            ArrivalTrainModel aTM = await requestManager.GetArrivingTrainsListAsync();

            Assert.True(aTM.payload.arrivals.Count > 0);
        }

        [Fact]
        public async void GetWalkDurationTest()
        {
            RequestManager requestManager = new RequestManager();
            StationModel station = await requestManager.GetUICCodeAsync("Rotterdam Centraal");

            requestManager.SetCurrentStation(station.Stations[0]);

            await requestManager.SendRequestGAPI(new Location(51.91391161801332, 4.481820066452984));
            string walkDuration = requestManager.GetRouteWalkDuration();

            Assert.Equal("26 mins", walkDuration);
        }

        [Fact]
        public async void GetRoutePolylineTest()
        {
            RequestManager requestManager = new RequestManager();
            StationModel station = await requestManager.GetUICCodeAsync("Rotterdam Centraal");

            requestManager.SetCurrentStation(station.Stations[0]);

            await requestManager.SendRequestGAPI(new Location(51.91391161801332, 4.481820066452984));
            List<Location> location = requestManager.GetRoutePolylineLocations();

            Assert.True(location.Count > 0);
        }
    }
}