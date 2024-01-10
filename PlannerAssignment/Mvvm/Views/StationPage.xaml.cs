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

    public StationPage(StationViewModel stationViewModel)
	{
		InitializeComponent();
		requestManager = new RequestManager();
        BindingContext = stationViewModel;
        trainsListViewModel = new DeparturesViewModel(requestManager);
	}

    public async void collectionView_SelectionChanged(object o, EventArgs e)
    {
        if (o is Frame frame && frame.BindingContext is Station selectedStation)
        {
            Debug.WriteLine($"Selected Station: {selectedStation.Namen.Long}");
            collectionView.SelectedItem = selectedStation;
            requestManager.SetCurrentStation(selectedStation);
            
            await Navigation.PushAsync(new DeparturesPage(trainsListViewModel, requestManager));
            collectionView.SelectedItem = null;
        }
    }
}