using Newtonsoft.Json;
using PlannerAssignment.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject.JsonUtilTests
{
    public class JsonUtilUnhappyTests
    {
        [Fact]
        public void TestParseArrivingTrainListJson_UnhappyPath()
        {
            string jsonString = "{ \"trains\": [{ \"invalidField\":\"1234\" }] }";
            Assert.Throws<JsonSerializationException>(() => JsonUtil.ParseArrivingTrainListJson(jsonString));
        }

        [Fact]
        public void TestParseDeparturingTrainListJson_UnhappyPath()
        {
            string jsonString = "{ \"trains\": [{ \"invalidField\":\"1234\" }] }";
            Assert.Throws<JsonSerializationException>(() => JsonUtil.ParseDeparturingTrainListJson(jsonString));
        }

        [Fact]
        public void TestParseStationList_UnhappyPath()
        {
            string jsonString = "{ \"invalidField\": [{ \"UICCode\":\"8400131\" }] }";
            Assert.Throws<JsonSerializationException>(() => JsonUtil.ParseStationList(jsonString));
        }

        [Fact]
        public void TestParseStation_UnhappyPath()
        {
            string jsonString = "{ \"payload\": [] }";
            Assert.Throws<JsonSerializationException>(() => JsonUtil.ParseStation(jsonString));
        }
    }
}
