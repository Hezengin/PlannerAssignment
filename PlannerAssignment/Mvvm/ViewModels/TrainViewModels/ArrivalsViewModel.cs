using PlannerAssignment.Utils;
using System.Collections.ObjectModel;
using System.Diagnostics;
using static PlannerAssignment.Mvvm.Models.ArrivalTrainModel;

namespace PlannerAssignment.Mvvm.ViewModels
{
    public class ArrivalsViewModel : BaseViewModel
    {
        private readonly RequestManager _requestManager;

        private ObservableCollection<ArrivalTrain> _arrivals;

        public ObservableCollection<ArrivalTrain> Arrivals
        {
            get { return _arrivals; }
            set
            {
                if (_arrivals != value)
                {
                    _arrivals = value;
                    OnPropertyChanged(nameof(Arrivals));
                }
            }
        }

        private bool _arrHasItems;
        public bool ArrHasItems
        {
            get { return _arrHasItems; }
            set
            {
                _arrHasItems = value;
                OnPropertyChanged(nameof(ArrHasItems));
            }
        }

        public ArrivalsViewModel(RequestManager requestManager) : base(requestManager)
        {
            _requestManager = requestManager;

            Arrivals = new ObservableCollection<ArrivalTrain>();

            StartPollingAsync();
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
                var task = _requestManager.GetArrivingTrainsListAsync();

                if (await Task.WhenAny(task, timeout) == timeout)
                {
                    throw new Exception("Server did not respond within the specified time limit.");
                }
                else
                {
                    var arrivingTrainData = await task;

                    if (arrivingTrainData != null)
                    {
                        Arrivals.Clear();
                        foreach (var train in arrivingTrainData.payload.arrivals)
                        {
                            Arrivals.Add(train);
                        }
                    }
                    ArrHasItems = Arrivals.Count > 0;
                }
            }
            catch (Exception ex)
            {
                if (Arrivals.Count > 0)
                {
                    Arrivals.Clear();
                    ArrHasItems = false;
                }
                Debug.WriteLine(ex.ToString());
                Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }
    }
}
