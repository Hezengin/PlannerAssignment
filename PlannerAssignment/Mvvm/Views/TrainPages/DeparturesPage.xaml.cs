using PlannerAssignment.Database;
using PlannerAssignment.Mvvm.ViewModels;
using PlannerAssignment.Mvvm.ViewModels.DetailViewModels;
using PlannerAssignment.Mvvm.Views;
using PlannerAssignment.Mvvm.Views.DetailPages;
using PlannerAssignment.Mvvm.Models;
using PlannerAssignment.Utils;
using System.Diagnostics;
using static PlannerAssignment.Mvvm.Models.DepartureTrainModel;
using System.ComponentModel.Design;


namespace PlannerAssignment.MVVM;

public partial class DeparturesPage : ContentPage
{
    RequestManager _requestManager;
    DeparturesViewModel _departuresViewModel;
    ArrivalsViewModel _arrivalsViewModel;
    private readonly StationDatabase _stationDatabase;

    public DeparturesPage(DeparturesViewModel trainsViewModel, RequestManager requestManager, StationDatabase stationDatabase)
	{
		InitializeComponent();
        _requestManager = requestManager;
        BindingContext = trainsViewModel;
        _departuresViewModel = trainsViewModel;
        _arrivalsViewModel = new ArrivalsViewModel(requestManager);
        _stationDatabase = stationDatabase;
    }

    public async void collectionView_SelectionChanged(object o, EventArgs e)
    {
        if (o is Frame frame && frame.BindingContext is DepartureTrain selectedTrain)
        {
            Debug.WriteLine($"Selected train: {selectedTrain.direction}");
            collectionView.SelectedItem = selectedTrain;
            DepartureDetailVM departureDetail = new DepartureDetailVM(_requestManager, selectedTrain);

            await Navigation.PushAsync(new DeparturesDetail(departureDetail, _requestManager));
            collectionView.SelectedItem = null;
        }
    }

    public async void OnDepBtnClicked(object o, EventArgs e)
    {
        await _departuresViewModel.FetchData();
    }
    public async void OnArrBtnClicked(object o, EventArgs e)
    {
        await Navigation.PushAsync(new ArrivalsPage(_arrivalsViewModel, _requestManager, _stationDatabase));
    }

    public async void OnFavoriteClicked(object o, EventArgs e)
    {
        Debug.WriteLine("Save button Clicked");
        if (_stationDatabase == null)
        {
            Debug.WriteLine($"_stationDatabase is null: {_stationDatabase}");
        }
        else
        {
            var currentStation = _requestManager.GetCurrentStation();

            Names existingStation = await _stationDatabase.GetStationByNameAsync(currentStation.Namen.Long);

            if (existingStation == null)
            {
                await _stationDatabase.SaveStationNameAsync(currentStation);
                Debug.WriteLine($"{currentStation.Namen.Long} is saved to favorites");
            }
            else
            {
                Debug.WriteLine($"{currentStation.Namen.Long} is not saved to favorites");
                Debug.WriteLine($"Current Station: {existingStation.Long}");
            }
            Debug.WriteLine("Save button Finished");
        }
    }
}