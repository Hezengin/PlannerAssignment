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
    RequestManager requestManager;

    public ResultPage(TrainsListViewModel trainsViewModel)
	{
		InitializeComponent();
        requestManager = new RequestManager();
        BindingContext = trainsViewModel;
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
}