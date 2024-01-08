using PlannerAssignment.Mvvm.Models;
using PlannerAssignment.Utils;
using System.Collections.ObjectModel;
using System.Diagnostics;
using static PlannerAssignment.Mvvm.Models.ArrivalTrainModel;
using static PlannerAssignment.Mvvm.Models.DepartureTrainModel;


namespace PlannerAssignment.Mvvm.ViewModels
{
    public class TrainsListViewModel : BaseViewModel
    {
        private readonly RequestManager _requestManager;

        //private ObservableCollection<ArrivalTrain> _arrivals;
        //public ObservableCollection<ArrivalTrain> Arrivals
        //{
        //    get { return _arrivals; }
        //    set
        //    {
        //        if (_arrivals != value)
        //        {
        //            _arrivals = value;
        //            OnPropertyChanged(nameof(Arrivals));
        //        }
        //    }
        //}

        private ObservableCollection<DepartureTrain> _departures;
        public ObservableCollection<DepartureTrain> Departures
        {
            get { return _departures; }
            set
            {
                if (_departures != value)
                {
                    _departures = value;
                    OnPropertyChanged(nameof(Departures));
                }
            }
        }

        private bool _depHasItems;
        public bool DepHasItems
        {
            get { return _depHasItems; }
            set
            {
                _depHasItems = value;
                OnPropertyChanged(nameof(DepHasItems));
            }
        }

        //private bool _arrHasItems;
        //public bool ArrHasItems
        //{
        //    get { return _arrHasItems; }
        //    set
        //    {
        //        _arrHasItems = value;
        //        OnPropertyChanged(nameof(ArrHasItems));
        //    }
        //}

        public TrainsListViewModel(RequestManager requestManager) : base(requestManager) 
        {
            _requestManager = requestManager;
           // Arrivals = new ObservableCollection<ArrivalTrain>();
            Departures = new ObservableCollection<DepartureTrain>();

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
                //var task = _requestManager.GetArrivingTrainsListAsync();
                var task2 = _requestManager.GetDeparturingTrainsListAsync();

                //if (await Task.WhenAny(task, timeout) == timeout)
                //{
                //    throw new Exception("Server did not respond within the specified time limit.");
                //}else
                if (await Task.WhenAny(task2, timeout) == timeout)
                {
                    throw new Exception("Server did not respond within the specified time limit.");
                }
                else
                {
                    //var arrivingTrainData = await task;
                    var departingTrainData = await task2;
                    
                    //if (arrivingTrainData != null || departingTrainData != null)
                    if(departingTrainData != null)
                    {
                       // Arrivals.Clear();
                        Departures.Clear();

                        //foreach (var train in arrivingTrainData.payload.arrivals)
                        //{
                        //    Arrivals.Add(train);
                        //}
                        foreach(var train in departingTrainData.payload.departures)
                        {
                            Departures.Add(train);
                        }
                    }
                    //ArrHasItems = Arrivals.Count > 0;
                    DepHasItems = Departures.Count > 0;
                }
            }
            catch (Exception ex)
            {
                //if (Arrivals.Count > 0 || Departures.Count > 0)
                if(Departures.Count > 0)
                {
                    //Arrivals.Clear();
                    Departures.Clear();
                    //ArrHasItems = false;
                    DepHasItems = false;
                }
                Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }
    }
}