using PlannerAssignment.Mvvm.ViewModels;
using PlannerAssignment.Mvvm.Views;
using PlannerAssignment.MVVM;
using PlannerAssignment.Utils;

namespace PlannerAssignment
{
    public partial class MainPage : ContentPage
    {
        RequestManager requestManager { get; set; }
        StationViewModel stationViewModel { get; set; }
        public MainPage()
        {
            InitializeComponent();
            requestManager = new RequestManager();
        }

        private void OnSearchClicked(object sender, EventArgs e)
        {
            if (stationEditor.Text == string.Empty)
            {
                Application.Current.MainPage.DisplayAlert("Error", "Station Field Is Empty!", "OK");
            }
            else if (locationSwitch.IsEnabled != true)
            {
                if (locationEditor.Text == string.Empty)
                {
                    Application.Current.MainPage.DisplayAlert("Error", "Location field is empty give location or enable switch to use current location!", "OK");
                }
            }
            else
            {
                stationViewModel = new StationViewModel(requestManager, stationEditor.Text);
                Navigation.PushAsync(new StationPage());
            }
        }
    }

}
