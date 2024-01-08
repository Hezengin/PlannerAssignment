using PlannerAssignment.Mvvm.Models;
using PlannerAssignment.Mvvm.ViewModels;
using PlannerAssignment.MVVM;
using PlannerAssignment.Utils;
using System.Diagnostics;

namespace PlannerAssignment.Mvvm.Views;

public partial class StationPage : ContentPage
{
    TrainsListViewModel trainsListViewModel;
    RequestManager requestManager;

    public StationPage(StationViewModel stationViewModel)
	{
		InitializeComponent();
		requestManager = new RequestManager();
        BindingContext = stationViewModel;

        trainsListViewModel = new TrainsListViewModel(requestManager);
	}

    public async void collectionView_SelectionChanged(object o, EventArgs e)
    {
        if (o is Frame frame && frame.BindingContext is Station selectedStation)
        {
            Debug.WriteLine($"Selected Light: {selectedStation.Namen.Long}");
            collectionView.SelectedItem = selectedStation;
            
            await Navigation.PushAsync(new ResultPage(trainsListViewModel));
            collectionView.SelectedItem = null;
        }
    }
}