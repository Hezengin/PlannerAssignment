using PlannerAssignment.Mvvm.ViewModels;

namespace PlannerAssignment.Mvvm.Views;

public partial class StationPage : ContentPage
{
	public StationPage(StationViewModel stationViewModel)
	{
		InitializeComponent();
		BindingContext = stationViewModel;
	}

}