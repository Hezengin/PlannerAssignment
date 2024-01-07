using PlannerAssignment.Mvvm.Models;
using PlannerAssignment.Utils;
using System.Diagnostics;
using System.Windows.Input;

namespace PlannerAssignment.Mvvm.ViewModels
{
    public class DetailViewModel : BaseViewModel
    {
        private readonly RequestManager _requestManager;
        public ICommand UpdateHueCommand { get; set; }
        public ICommand UpdateSaturationCommand { get; set; }
        public ICommand UpdateBrightnessCommand { get; set; }
        public ICommand SwitchToggleCommand { get; set; }

        public TrainModel _selectedTrain;
        public TrainModel SelectedTrain
        {
            get => _selectedTrain;
            set
            {
                if (_selectedTrain != value)
                {
                    _selectedTrain = value;
                    OnPropertyChanged(nameof(SelectedTrain));
                }
            }
        }

        public DetailViewModel(RequestManager requestManager, TrainModel selectedTrain) : base(requestManager)
        {
            _requestManager = requestManager;
            SelectedTrain = selectedTrain;
            UpdateHueCommand = new Command<int>(UpdateHue);
            UpdateSaturationCommand = new Command<int>(UpdateSaturation);
            UpdateBrightnessCommand = new Command<int>(UpdateBrightness);
            SwitchToggleCommand = new Command<bool>(SwitchToggle);
        }

        private async Task SendData(bool isOn, int hue, int saturation, int brightness)
        {
            Debug.WriteLine("SendingData started");
            //await _requestManager.PostMessageAsync(SelectedTrain, isOn, hue, saturation, brightness);
            await FetchDataInternal().ConfigureAwait(false);
            Debug.WriteLine("SendingData finished");
        }

        private void UpdateHue(int value)
        {
            //SendData(SelectedTrain.State.IsOn, value, SelectedTrain.State.Saturation, SelectedTrain.State.Brightness);
        }

        private void UpdateSaturation(int value)
        {
            //SendData(SelectedTrain.State.IsOn, SelectedTrain.State.Hue, value, SelectedTrain.State.Brightness);
        }

        private void UpdateBrightness(int value)
        {
           // SendData(SelectedTrain.State.IsOn, SelectedTrain.State.Hue, SelectedTrain.State.Saturation, value);
        }

        private void SwitchToggle(bool value)
        {
            //SendData(value, SelectedTrain.State.Hue, SelectedTrain.State.Saturation, SelectedTrain.State.Brightness);
        }


        protected override async Task FetchDataInternal()
        {
            try
            {
                var timeout = Task.Delay(TimeSpan.FromSeconds(4));
               // var task = _requestManager.GetLightsListAsync();
                //Debug.WriteLine($"Selectedlight.Index: {SelectedTrain.}");

                //if (await Task.WhenAny(task, timeout) == timeout)
                //{
                //    throw new Exception("Server did not respond within the specified time limit.");
                //}
                //else
                //{
                //    var lightData = await task;
                //    if (lightData != null)
                //    {
                //        foreach (var light in lightData)
                //        {
                //            if (light.UniqueID == SelectedTrain.UniqueID)
                //            {
                //                Device.BeginInvokeOnMainThread(() =>
                //                {
                //                    SelectedTrain = light;
                //                });
                //            }
                //        }
                //    }
                //}
            }
            catch (Exception ex)
            {
                Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }
    }
}
