using PlannerAssignment.Mvvm.Models;
using PlannerAssignment.Utils;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace PlannerAssignment.Mvvm.ViewModels
{
    public class TrainsListViewModel : BaseViewModel
    {
        private readonly RequestManager _requestManager;
        private ObservableCollection<TrainModel> _trains;

        public ObservableCollection<TrainModel> Trains
        {
            get { return _trains; }
            set
            {
                if (_trains != value)
                {
                    _trains = value;
                    OnPropertyChanged(nameof(Trains));
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

        public TrainsListViewModel(RequestManager requestManager) : base(requestManager) 
        {
            _requestManager = requestManager;
            Trains = new ObservableCollection<TrainModel>();
            //StartPollingAsync();
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
                //var task = _requestManager.GetTrainsListAsync();

                //if (await Task.WhenAny(task, timeout) == timeout)
                //{
                //    throw new Exception("Server did not respond within the specified time limit.");
                //}
                //else
                //{
                //    var lightsData = await task;
                //    if (lightsData != null)
                //    {
                //        Trains.Clear();
                //        foreach (var light in lightsData)
                //        {
                //            //Debug.WriteLine($"{light.Name} {light.Index}");
                //            Trains.Add(light);
                //        }
                //    }
                //    HasItems = Trains.Count > 0;
                //}
            }
            catch (Exception ex)
            {
                if (Trains.Count > 0)
                {
                    Trains.Clear();
                    HasItems = false;
                }
                Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }
    }
}