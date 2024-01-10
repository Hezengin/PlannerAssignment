using PlannerAssignment.Mvvm.ViewModels;
using PlannerAssignment.Mvvm.ViewModels.DetailViewModels;
using PlannerAssignment.Utils;

namespace PlannerAssignment.Mvvm.Views.DetailPages;

public partial class ArrivalsDetail : ContentPage
{
	private ArrivalDetailVM _arrivalDetailVM;
	private RequestManager _requestManager;

	public ArrivalsDetail(ArrivalDetailVM arrivalDetailVM, RequestManager requestManager)
	{
		InitializeComponent();
		BindingContext = arrivalDetailVM;
        _arrivalDetailVM = arrivalDetailVM;
		_requestManager = requestManager;
	}

    private void refreshButtonClicked(object sender, EventArgs e)
    {

    }
}