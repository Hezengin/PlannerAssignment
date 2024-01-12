using PlannerAssignment.Database;
using PlannerAssignment.Mvvm.Models;
using PlannerAssignment.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlannerAssignment.Mvvm.ViewModels
{
    public class FavoriteViewModel : BaseViewModel
    {
        private readonly RequestManager _requestManager;
        private ObservableCollection<Names> _stations;
        private readonly StationDatabase _stationDatabase;

        public ObservableCollection<Names> Stations
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

        public FavoriteViewModel(RequestManager requestManager,StationDatabase stationDatabase ) : base(requestManager)
        {
            _stationDatabase = stationDatabase;
            _requestManager = requestManager;
            Stations = new ObservableCollection<Names>();
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
                var task = _stationDatabase.GetAllStationsAsync();

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
                        foreach (var station in stationsData)
                        {
                            Stations.Add(station);
                            Debug.WriteLine(station.Long + " is added to the list");
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

