using PlannerAssignment.Mvvm.Models;
using PlannerAssignment.Utils;
using System.Collections.ObjectModel;

namespace PlannerAssignment.Mvvm.ViewModels
{
    public class StationViewModel : BaseViewModel
    {
        private readonly RequestManager _requestManager;
        private ObservableCollection<StationModel> _trains;
        private string _wantedStation;

        public ObservableCollection<StationModel> Stations
        {
            get { return _trains; }
            set
            {
                if (_trains != value)
                {
                    _trains = value;
                    OnPropertyChanged(nameof(Stations));
                }
            }
        }

        private bool _hasItems;
        public bool HasItems
        {
            get { return _hasItems; }
            set
            {
                _hasItems = value;
                OnPropertyChanged(nameof(HasItems));
            }
        }

        public StationViewModel(RequestManager requestManager, string stationName) : base(requestManager)
        {
            _wantedStation = stationName;
            _requestManager = requestManager;
            Stations = new ObservableCollection<StationModel>();
            // StartPollingAsync();
        }

        private async Task StartPollingAsync()
        {
            await Task.Run(async () =>
            {
                while (true)
                {
                    await FetchDataInternal();
                    await Task.Delay(4000);
                }
            });
        }

        protected override async Task FetchDataInternal()
        {
            try
            {
                var timeout = Task.Delay(TimeSpan.FromSeconds(4));
                var task = _requestManager.GetUICCodeAsync(_wantedStation);

                if (await Task.WhenAny(task, timeout) == timeout)
                {
                    throw new Exception("Server did not respond within the specified time limit.");
                }
                else
                {
                    var lightsData = await task;
                    if (lightsData != null)
                    {
                        Stations.Clear();
                        foreach (var light in lightsData)
                        {
                            //Debug.WriteLine($"{light.Name} {light.Index}");
                            Stations.Add(light);
                        }
                    }
                    HasItems = Stations.Count > 0;
                }
            }
            catch (Exception ex)
            {
                if (Stations.Count > 0)
                {
                    Stations.Clear();
                    HasItems = false;
                }
                Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }
    }
}

