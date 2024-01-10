using PlannerAssignment.Mvvm.Models;
using PlannerAssignment.Mvvm.ViewModels;
using PlannerAssignment.Mvvm.Views;
using PlannerAssignment.Utils;
using System.Diagnostics;
using static PlannerAssignment.Mvvm.Models.ArrivalTrainModel;
using static PlannerAssignment.Mvvm.Models.DepartureTrainModel;


namespace PlannerAssignment.MVVM;

public partial class ResultPage : ContentPage
{
    RequestManager _requestManager;
    DeparturesViewModel _departuresViewModel;
    ArrivalsViewModel _arrivalsViewModel;
    public ResultPage(DeparturesViewModel trainsViewModel, RequestManager requestManager)
	{
		InitializeComponent();
        _requestManager = requestManager;
        BindingContext = trainsViewModel;
        _departuresViewModel = trainsViewModel;
        _arrivalsViewModel = new ArrivalsViewModel(requestManager);
	}

    public async void collectionView_SelectionChanged(object o, EventArgs e)
    {
        if (o is Frame frame && frame.BindingContext is DepartureTrain selectedTrain)
        {
            Debug.WriteLine($"Selected train: {selectedTrain.direction}");
            collectionView.SelectedItem = selectedTrain;
            //DetailViewModel detailViewModel = new DetailViewModel(requestManager, selectedTrain);

            //await Navigation.PushAsync(new DetailPage(detailViewModel));
            collectionView.SelectedItem = null;
        }
    }

    public async void OnDepBtnClicked(object o, EventArgs e)
    {
        await _departuresViewModel.FetchData();
        collectionView.ItemsSource = _departuresViewModel.Departures;
    }

    public async void OnArrBtnClicked(object o, EventArgs e)
    {
        await _arrivalsViewModel.FetchData();
        collectionView.ItemsSource= _arrivalsViewModel.Arrivals;
    }
}