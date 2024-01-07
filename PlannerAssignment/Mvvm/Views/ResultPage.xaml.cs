using PlannerAssignment.Mvvm.Models;
using PlannerAssignment.Mvvm.ViewModels;
using PlannerAssignment.Mvvm.Views;
using PlannerAssignment.Utils;

namespace PlannerAssignment.MVVM;

public partial class ResultPage : ContentPage
{
    RequestManager requestManager;
    LightsViewModel lightViewModel;

    public ResultPage()
	{
		InitializeComponent();
	}

    public async void collectionView_SelectionChanged(object o, EventArgs e)
    {
        if (o is Border border && border.BindingContext is TrainModel selectedLight)
        {
            //Debug.WriteLine($"Selected Light: {selectedLight.Name} {selectedLight.Index}");
            collectionView.SelectedItem = selectedLight;
            DetailViewModel viewModel = new DetailViewModel(requestManager, selectedLight);

            await Navigation.PushAsync(new DetailPage(viewModel));
            collectionView.SelectedItem = null;
        }
    }
}