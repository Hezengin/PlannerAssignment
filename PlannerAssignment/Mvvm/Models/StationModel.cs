namespace PlannerAssignment.Mvvm.Models
{
    public class StationModel
    {
        public string Code { get; set; }
        public int Uic { get; set; }
        public string NameShort { get; set; }
        public string NameMedium { get; set; }
        public string NameLong { get; set; }
        public string Slug { get; set; }
        public string Country { get; set; }
        public string Type { get; set; }
        public double GeoLat { get; set; }
        public double GeoLng { get; set; }
    }

}
