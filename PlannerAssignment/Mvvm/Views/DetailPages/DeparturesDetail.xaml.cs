using PlannerAssignment.Mvvm.ViewModels;
using PlannerAssignment.Mvvm.ViewModels.DetailViewModels;
using PlannerAssignment.Utils;
using System.Diagnostics;

namespace PlannerAssignment.Mvvm.Views.DetailPages;

public partial class DeparturesDetail : ContentPage
{
    RequestManager _requestManager;
    DepartureDetailVM _departuresDetailVM;

    public DeparturesDetail(DepartureDetailVM departuresDetailVM, RequestManager requestManager)
    {
        InitializeComponent();
        BindingContext = departuresDetailVM;
        _departuresDetailVM = departuresDetailVM;
        _requestManager = requestManager;
    }

    private void lampTapped(object sender, TappedEventArgs e)
    {

    }

    private void refreshButtonClicked(object sender, EventArgs e)
    {

    }
}