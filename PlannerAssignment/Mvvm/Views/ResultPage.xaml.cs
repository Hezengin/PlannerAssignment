using PlannerAssignment.Mvvm.Models;
using PlannerAssignment.Mvvm.ViewModels;
using PlannerAssignment.Mvvm.Views;
using PlannerAssignment.Utils;

namespace PlannerAssignment.MVVM;

public partial class ResultPage : ContentPage
{
    RequestManager requestManager;
    TrainsListViewModel trainsViewModel;

    public ResultPage()
	{
		InitializeComponent();
	}

    public async void collectionView_SelectionChanged(object o, EventArgs e)
    {
        if (o is Border border && border.BindingContext is TrainModel selectedTrain)
        {
            //Debug.WriteLine($"Selected Light: {selectedLight.Name} {selectedLight.Index}");
            collectionView.SelectedItem = selectedTrain;
            DetailViewModel viewModel = new DetailViewModel(requestManager, selectedTrain);

            await Navigation.PushAsync(new DetailPage(viewModel));
            collectionView.SelectedItem = null;
        }
    }
}