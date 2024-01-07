﻿using PlannerAssignment.Mvvm.ViewModels;
using PlannerAssignment.Mvvm.Views;
using PlannerAssignment.MVVM;
using PlannerAssignment.Utils;
using System.Diagnostics;

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
            string stationText = stationEditor.Text?.Trim();
            string locationText = locationEditor.Text?.Trim();
            ClearInputs();

            if (string.IsNullOrEmpty(stationText))
            {
                ShowAlert("Error", "Station Field Is Empty!");
                return;
            }

            if (string.IsNullOrEmpty(locationText))
            {
                ShowAlert("Error", "Location field is empty. Provide a location or enable the switch to use the current location!");
                return;
            }

            stationViewModel = new StationViewModel(requestManager, stationText);
            Navigation.PushAsync(new StationPage(stationViewModel));
        }

        private void OnLocationSwitchToggled(object sender, ToggledEventArgs e)
        {
            if (locationSwitch.IsToggled)
            {
                //TODO Use Current Location
                locationEditor.IsEnabled = false;
                locationEditor.Text = null;
            }
            else
            {
                locationEditor.IsEnabled = true;
            }
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
