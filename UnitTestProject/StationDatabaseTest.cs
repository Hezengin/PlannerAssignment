using PlannerAssignment.Database;
using PlannerAssignment.Mvvm.Models;
using PlannerAssignment.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject.Happy
{
    public class StationDatabaseTest
    {
        [Fact]
        public async void StationDatabaseAddTest()
        {
            StationDatabase stationDB = new StationDatabase(Path.Combine(Directory.GetCurrentDirectory(), "StationsDatabase.db3"));

            RequestManager requestManager = new RequestManager();
            StationModel stationModelData = await requestManager.GetUICCodeAsync("Rotterdam Centraal");
            Station station = stationModelData.Stations[0];


            await stationDB.SaveStationNameAsync(station);
            Names stationNames = await stationDB.GetStationByNameAsync("Rotterdam Centraal");

            Assert.NotNull(stationNames);
            Assert.Equal("Rotterdam Centraal", stationNames.Long);
        }

        [Fact]
        public async void StationDatabaseGetListTest()
        {
            StationDatabase stationDB = new StationDatabase(Path.Combine(Directory.GetCurrentDirectory(), "StationsDatabase.db3"));

            RequestManager requestManager = new RequestManager();
            StationModel stationModelData = await requestManager.GetUICCodeAsync("Rotterdam Centraal");
            Station station = stationModelData.Stations[0];


            await stationDB.SaveStationNameAsync(station);
            List<Names> stationNames = await stationDB.GetAllStationsAsync();

            Assert.NotNull(stationNames);
            Assert.True(stationNames.Count > 0);
        }
    }
}
