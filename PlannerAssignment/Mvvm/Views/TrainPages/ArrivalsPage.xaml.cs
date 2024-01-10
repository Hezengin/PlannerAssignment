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

    public ArrivalsPage(ArrivalsViewModel arrivalsViewModel, RequestManager requestManager)
	{
		InitializeComponent();
        BindingContext = arrivalsViewModel;
        _arrivalsViewModel = arrivalsViewModel;
        _requestManager = requestManager;
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
}