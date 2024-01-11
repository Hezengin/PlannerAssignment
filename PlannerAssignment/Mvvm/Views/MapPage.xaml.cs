using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;
using PlannerAssignment.Mvvm.ViewModels;
using PlannerAssignment.Utils;
using System.Diagnostics;
using System.Net;
using System.Net.NetworkInformation;

namespace PlannerAssignment.MVVM;

public partial class MapPage : ContentPage
{
	private RequestManager _requestManager;
	private Location userLocation;
	public MapPage(RequestManager requestManager)
	{
		InitializeComponent();
		_requestManager = requestManager;
		Debug.WriteLine(_requestManager);


		userLocation = GetUserLocation().GetAwaiter().GetResult();

		Pin stationPin = new Pin
		{
			Label = _requestManager.GetCurrentStation().Namen.Long,
			Address = _requestManager.GetCurrentStation().Land,
			Type = PinType.Place,
			Location = new Location(requestManager.GetCurrentStation().Lat, _requestManager.GetCurrentStation().Lng)
		};

		MapSpan userMapSpan = new MapSpan(userLocation, 0.1, 0.1);

		Polyline polyline = new Polyline();
		polyline.StrokeWidth = 7;
        List<Location> polylinePoints = _requestManager.GetRoutePolylineLocations(userLocation).GetAwaiter().GetResult();
        foreach (var polylinePoint in polylinePoints)
        {
            polyline.Geopath.Add(polylinePoint);
        }

        map.MapElements.Add(polyline);
		map.Pins.Add(stationPin);


        map.IsShowingUser = true;
        map.MoveToRegion(userMapSpan);
    }

	public async Task<Location> GetUserLocation()
	{
		Location location = new Location();
		try
		{
			location = await Geolocation.Default.GetLastKnownLocationAsync();
		}
		catch(Exception ex)
		{
			Debug.WriteLine("Unable to get user location");
		}

		return location;
    }

	public async Task GetDistanceFromStation()
	{
		try
		{
			Location location = new Location();
			GeolocationRequest request = new GeolocationRequest(GeolocationAccuracy.Best, TimeSpan.FromSeconds(10));
			location = await Geolocation.Default.GetLocationAsync(request);
			if (Location.CalculateDistance(location, userLocation, DistanceUnits.Kilometers) < 0.01)
			{

			}

		}
		catch (Exception e)
		{
			Debug.WriteLine("Couldn't get current geolocation");
		}
	}
}
