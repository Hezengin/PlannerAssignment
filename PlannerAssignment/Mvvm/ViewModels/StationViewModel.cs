using PlannerAssignment.Mvvm.Models;
using PlannerAssignment.Utils;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace PlannerAssignment.Mvvm.ViewModels
{
    public class StationViewModel : BaseViewModel
    {
        private readonly RequestManager _requestManager;
        private ObservableCollection<Station> _stations;
        private string _wantedStation;

        public ObservableCollection<Station> Stations
        {
            get { return _stations; }
            set
            {
                if (_stations != value)
                {
                    _stations = value;
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
            Stations = new ObservableCollection<Station>();
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
                    var stationsData = await task;
                    if (stationsData != null)
                    {
                        Stations.Clear();
                        foreach (var station in stationsData.Stations)
                        {
                            Stations.Add(station);
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

