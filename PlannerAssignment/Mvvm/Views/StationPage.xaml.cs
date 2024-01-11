using PlannerAssignment.Database;
using PlannerAssignment.Mvvm.Models;
using PlannerAssignment.Mvvm.ViewModels;
using PlannerAssignment.MVVM;
using PlannerAssignment.Utils;
using System.Diagnostics;

namespace PlannerAssignment.Mvvm.Views;

public partial class StationPage : ContentPage
{
    DeparturesViewModel trainsListViewModel;
    RequestManager requestManager;
    private readonly StationDatabase _stationDatabase;

    public StationPage(StationViewModel stationViewModel, StationDatabase stationDatabase)
	{
		InitializeComponent();
		requestManager = new RequestManager();
        BindingContext = stationViewModel;
        trainsListViewModel = new DeparturesViewModel(requestManager);
        _stationDatabase = stationDatabase;
    }

    public async void collectionView_SelectionChanged(object o, EventArgs e)
    {
        if (o is Frame frame && frame.BindingContext is Station selectedStation)
        {
            Debug.WriteLine($"Selected Station: {selectedStation.Namen.Long}");
            collectionView.SelectedItem = selectedStation;
            requestManager.SetCurrentStation(selectedStation);
            
            await Navigation.PushAsync(new DeparturesPage(trainsListViewModel, requestManager,_stationDatabase));
            collectionView.SelectedItem = null;
        }
    }
}