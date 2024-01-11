using PlannerAssignment.Database;
using PlannerAssignment.Mvvm.ViewModels;
using PlannerAssignment.Mvvm.ViewModels.DetailViewModels;
using PlannerAssignment.Mvvm.Views.DetailPages;
using PlannerAssignment.Utils;
using System.Diagnostics;
using static PlannerAssignment.Mvvm.Models.ArrivalTrainModel;

namespace PlannerAssignment.Mvvm.Views;

public partial class ArrivalsPage : ContentPage
{
    RequestManager _requestManager;
    ArrivalsViewModel _arrivalsViewModel;
    private readonly StationDatabase _stationDatabase;

    public ArrivalsPage(ArrivalsViewModel arrivalsViewModel, RequestManager requestManager, StationDatabase stationDatabase)
	{
		InitializeComponent();
        BindingContext = arrivalsViewModel;
        _arrivalsViewModel = arrivalsViewModel;
        _requestManager = requestManager;
        _stationDatabase = stationDatabase;
    }

    public async void collectionView_SelectionChanged(object o, EventArgs e)
    {
        if (o is Frame frame && frame.BindingContext is ArrivalTrain selectedTrain)
        {
            Debug.WriteLine($"Selected train: {selectedTrain.origin}");
            collectionView.SelectedItem = selectedTrain;
            ArrivalDetailVM arrivalDetailVM = new ArrivalDetailVM(_requestManager, selectedTrain);

            await Navigation.PushAsync(new ArrivalsDetail(arrivalDetailVM, _requestManager));
            collectionView.SelectedItem = null;
        }
    }

    public async void OnDepBtnClicked(object o, EventArgs e)
    {
        await Navigation.PopAsync();
    }

    public async void OnArrBtnClicked(object o, EventArgs e)
    {
        await _arrivalsViewModel.FetchData();
    }

    public async void OnFavoriteClicked(object o, EventArgs e)
    {
        Debug.WriteLine("Save button Clicked");

        if (_stationDatabase == null)
        {
            Debug.WriteLine($"_stationDatabase is null: {_stationDatabase}");
        }

        var currentStation = _requestManager.GetCurrentStation();
        var existingStation = await _stationDatabase.GetStationByNameAsync(currentStation.Namen.Long);

        if (existingStation == null)
        {
            await _stationDatabase.SaveStationNameAsync(currentStation);
            Debug.WriteLine($"{currentStation.Namen.Long} is saved to favorites");
        }
        Debug.WriteLine("Save button finished");

    }
}