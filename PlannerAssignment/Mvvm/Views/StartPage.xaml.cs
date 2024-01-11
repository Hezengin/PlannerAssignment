using PlannerAssignment.Mvvm.ViewModels;
using PlannerAssignment.Mvvm.Views;
using PlannerAssignment.MVVM;
using PlannerAssignment.Utils;
using PlannerAssignment.Database;
using System.Diagnostics;

namespace PlannerAssignment
{
    public partial class MainPage : ContentPage
    {
        RequestManager requestManager { get; set; }
        StationViewModel stationViewModel { get; set; }
        private StationDatabase _stationDatabase;

        public MainPage()
        {
            InitializeComponent();
            requestManager = new RequestManager();

            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "StationsDatabase.db3");

            try
            {
                _stationDatabase = new StationDatabase(dbPath);
            }
            catch (AggregateException ex)
            {
                foreach (var innerEx in ex.InnerExceptions)
                {
                    Console.WriteLine($"Error: {innerEx.Message}");
                }
            }
        }

        private void OnSearchClicked(object sender, EventArgs e)
        {
            string stationText = stationEditor.Text;
            string locationText = locationEditor.Text;
            bool switchIsOn = locationSwitch.IsToggled;


            if (string.IsNullOrEmpty(stationText))
            {
                ShowAlert("Error", "Station Field Is Empty!");
                return;
            }

            if (string.IsNullOrEmpty(locationText) && switchIsOn == false)
            {
                ShowAlert("Error", "Location field is empty. Provide a location or enable the switch to use the current location!");
                return;
            }

            ClearInputs();

            stationViewModel = new StationViewModel(requestManager, stationText);
            Navigation.PushAsync(new StationPage(stationViewModel,_stationDatabase));
        }

        private void OnLocationSwitchToggled(object sender, ToggledEventArgs e)
        {
            if (locationSwitch.IsToggled)
            {
                //TODO Use Current Location
                locationEditor.IsEnabled = false;
                locationEditor.Text = null;
                locationSwitch.IsToggled = true;
            }
            else
            {
                locationEditor.IsEnabled = true;
                locationSwitch.IsToggled = false;
            }
        }

        private async void OnFavStationBtnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new FavoritePage(_stationDatabase, requestManager));
        }

        private void ClearInputs()
        {
            stationEditor.Text = null;
            locationEditor.Text = null;
            locationSwitch.IsToggled = false;
        }

        private void ShowAlert(string title, string message)
        {
            Application.Current.MainPage.DisplayAlert(title, message, "OK");
        }
    }
}
