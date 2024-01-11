using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;
using PlannerAssignment.Mvvm.ViewModels;
using PlannerAssignment.Utils;
using System.Diagnostics;

namespace PlannerAssignment.MVVM;

public partial class MapPage : ContentPage
{
	private RequestManager _requestManager;
	private Location userLocation;
	public MapPage(RequestManager requestManager)
	{
		InitializeComponent();
		_requestManager = requestManager;

		GetUserLocation();
		MapSpan userMapSpan = new MapSpan(userLocation, 0.1, 0.1);
		Location e = new Location(51.5955543518066, 4.78000020980835);

		Polyline polyline = new Polyline();
		polyline.StrokeWidth = 7;
        List<Location> polylinePoints = _requestManager.GetRoutePolylineLocations(userLocation, e).GetAwaiter().GetResult();
        
        foreach (var polylinePoint in polylinePoints)
        {
            polyline.Geopath.Add(polylinePoint);
        }

        map.MapElements.Add(polyline);

        map.MoveToRegion(userMapSpan);
    }

	public async void GetUserLocation()
	{
		try
		{
			userLocation = await Geolocation.Default.GetLastKnownLocationAsync();
		}
		catch(Exception ex)
		{
			Debug.WriteLine("Unable to get user location");
		}
    }



}