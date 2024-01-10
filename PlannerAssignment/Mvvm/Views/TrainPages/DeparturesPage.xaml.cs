using PlannerAssignment.Mvvm.ViewModels;
using PlannerAssignment.Mvvm.ViewModels.DetailViewModels;
using PlannerAssignment.Mvvm.Views;
using PlannerAssignment.Mvvm.Views.DetailPages;
using PlannerAssignment.Utils;
using System.Diagnostics;
using static PlannerAssignment.Mvvm.Models.DepartureTrainModel;


namespace PlannerAssignment.MVVM;

public partial class DeparturesPage : ContentPage
{
    RequestManager _requestManager;
    DeparturesViewModel _departuresViewModel;
    ArrivalsViewModel _arrivalsViewModel;
    public DeparturesPage(DeparturesViewModel trainsViewModel, RequestManager requestManager)
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
        await Navigation.PushAsync(new ArrivalsPage(_arrivalsViewModel, _requestManager));
    }
}