using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;
using PlannerAssignment.Mvvm.ViewModels;
using PlannerAssignment.Notification;
using PlannerAssignment.Utils;
using System.Diagnostics;
using System.Net;
using System.Net.NetworkInformation;

namespace PlannerAssignment.MVVM;

public partial class MapPage : ContentPage
{
	private RequestManager _requestManager;
	private Location userLocation;
	private Location stationLocation;
	private NotificationManager notificationManager;

    public MapPage(RequestManager requestManager)
	{
		InitializeComponent();
        MapViewModel viewModel = new MapViewModel(requestManager,map);
        viewModel.InitializeAsync().GetAwaiter().GetResult();
    }
}
