using PlannerAssignment.Database;
using PlannerAssignment.Mvvm.Models;
using PlannerAssignment.Mvvm.ViewModels;
using PlannerAssignment.MVVM;
using PlannerAssignment.Utils;
using System.Diagnostics;
namespace PlannerAssignment.Mvvm.Views;

public partial class FavoritePage : ContentPage
{
    private StationDatabase _stationDatabase;
    FavoriteViewModel _favoriteViewModel;
    RequestManager _requestManager;

    public FavoritePage(StationDatabase stationDatabase, RequestManager requestManager)
    {
        InitializeComponent();
        _requestManager = requestManager;
        _stationDatabase = stationDatabase;
        _favoriteViewModel = new FavoriteViewModel(_requestManager, stationDatabase);
        BindingContext = _favoriteViewModel;
    }

    private async Task<List<Names>> LoadDataAsync()
    {
        try
        {
            List<Names> names = new List<Names>(); 
            var vars = await _stationDatabase.GetAllStationsAsync();

            foreach (var name in vars)
            {
                Debug.WriteLine($"Name: Long: {name.Long}");
                names.Add( name );
            }
            return names;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error loading data: {ex.Message}");
        }
        return null;
    }

    public async void collectionView_SelectionChanged(object o, EventArgs e)
    {
        if (o is Frame frame && frame.BindingContext is Names selectedStation)
        {
            Debug.WriteLine($"Selected Station: {selectedStation.Long}");
            collectionView.SelectedItem = selectedStation;
            Station station = await _requestManager.GetStation(selectedStation.Long);
            _requestManager.SetCurrentStation(station);
            Debug.WriteLine("station.UICCode: " + station.UICCode);
            Debug.WriteLine("station: " + station);

            DeparturesViewModel trainsListViewModel = new DeparturesViewModel(_requestManager);
            await Navigation.PushAsync(new DeparturesPage(trainsListViewModel, _requestManager, _stationDatabase));
            collectionView.SelectedItem = null;
        }
    }
}