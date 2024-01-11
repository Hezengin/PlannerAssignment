using PlannerAssignment.Database;
namespace PlannerAssignment.Mvvm.Views;

public partial class FavoritePage : ContentPage
{
    private readonly StationDatabase _stationDatabase;

    public FavoritePage(StationDatabase stationDatabase)
	{
		InitializeComponent();
        _stationDatabase = stationDatabase;
        BindingContext = _stationDatabase.GetAllStationsAsync();
	}

    public async void collectionView_SelectionChanged(object o, EventArgs e)
    {
        //if (o is Frame frame && frame.BindingContext is Station selectedStation)
        //{
        //    Debug.WriteLine($"Selected Station: {selectedStation.Namen.Long}");
        //    collectionView.SelectedItem = selectedStation;
        //    requestManager.SetCurrentStation(selectedStation);

        //    await Navigation.PushAsync(new DeparturesPage(trainsListViewModel, requestManager, _stationDatabase));
        //    collectionView.SelectedItem = null;
        //}
    }
}