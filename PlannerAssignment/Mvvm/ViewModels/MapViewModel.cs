using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;
using PlannerAssignment.Notification;
using PlannerAssignment.Utils;
using System.Diagnostics;
using Map = Microsoft.Maui.Controls.Maps.Map;

namespace PlannerAssignment.Mvvm.ViewModels
{
    public class MapViewModel : BaseViewModel
    {
        private RequestManager _requestManager;
        private Location userLocation;
        private Location stationLocation;
        private NotificationManager notificationManager;
        private bool notificationSent;

        private Map map;
        private Label travelLabel;

        public MapViewModel(RequestManager requestManager, Map map, Label travelLabel) : base(requestManager)
        {
            _requestManager = requestManager;
            notificationManager = new NotificationManager();
            this.map = map;
            this.notificationSent = false;
            this.travelLabel = travelLabel;
            StartPollingAsync();// is needed so the map updates the user position to get notification
        }

        private async Task StartPollingAsync()
        {
            await Task.Run(async () =>
            {
                while (true)
                {
                    await GetDistanceFromStation();
                    await Task.Delay(1000);
                }
            });
        }

        public async Task InitializeAsync()
        {
            userLocation = await GetUserLocation();
            stationLocation = new Location(_requestManager.GetCurrentStation().Lat, _requestManager.GetCurrentStation().Lng);

            Pin stationPin = new Pin
            {
                Label = _requestManager.GetCurrentStation().Namen.Long,
                Address = _requestManager.GetCurrentStation().Land,
                Type = PinType.Place,
                Location = stationLocation
            };

            MapSpan userMapSpan = new MapSpan(userLocation, 0.1, 0.1);

            Polyline polyline = new Polyline();
            polyline.StrokeWidth = 7;

            await _requestManager.SendRequestGAPI(userLocation);

            List<Location> polylinePoints = _requestManager.GetRoutePolylineLocations();
            foreach (var polylinePoint in polylinePoints)
            {
                polyline.Geopath.Add(polylinePoint);
            }

            map.MapElements.Add(polyline);
            map.Pins.Add(stationPin);

            map.IsShowingUser = true;
            map.MoveToRegion(userMapSpan);

            travelLabel.Text = _requestManager.GetRouteWalkDuration();

            GetDistanceFromStation();
        }


        protected override async Task FetchDataInternal()
        {
            await StartPollingAsync();
        }

        public async Task<Location> GetUserLocation()
        {
            Location location = new Location();
            try
            {
                location = await Geolocation.Default.GetLastKnownLocationAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Unable to get user location");
            }

            return location;
        }

        public async Task GetDistanceFromStation()
        {
            try
            {
                Debug.WriteLine(notificationSent);

                Location location = new Location();
                GeolocationRequest request = new GeolocationRequest(GeolocationAccuracy.Best, TimeSpan.FromSeconds(10));
                location = await Geolocation.Default.GetLocationAsync(request);
                if (!notificationSent && Location.CalculateDistance(location, stationLocation, DistanceUnits.Kilometers) < 0.05)
                {
                    Debug.WriteLine("value " + notificationManager);
                    notificationManager.SendNotification("", "Approaching station!");
                    notificationSent = true;
                    Debug.WriteLine("should be true" + notificationSent);
                }
                else if (notificationSent && Location.CalculateDistance(location, stationLocation, DistanceUnits.Kilometers) >= 0.05)
                {
                    notificationSent = false;
                    Debug.WriteLine("should be false" + notificationSent);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error getting current geolocation: {e.Message}");
            }
        }
    }
}
