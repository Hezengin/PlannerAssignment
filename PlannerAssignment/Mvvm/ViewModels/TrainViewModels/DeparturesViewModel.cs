using PlannerAssignment.Utils;
using System.Collections.ObjectModel;
using System.Diagnostics;
using static PlannerAssignment.Mvvm.Models.DepartureTrainModel;


namespace PlannerAssignment.Mvvm.ViewModels
{
    public class DeparturesViewModel : BaseViewModel
    {
        private readonly RequestManager _requestManager;

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

        public DeparturesViewModel(RequestManager requestManager) : base(requestManager) 
        {
            _requestManager = requestManager;
           
            Departures = new ObservableCollection<DepartureTrain>();

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
                var task = _requestManager.GetDeparturingTrainsListAsync();

                if (await Task.WhenAny(task, timeout) == timeout)
                {
                    throw new Exception("Server did not respond within the specified time limit.");
                }
                else
                {
                    var departingTrainData = await task;
                    
                    if(departingTrainData != null)
                    {
                        Departures.Clear();

                        foreach(var train in departingTrainData.payload.departures)
                        {
                            Departures.Add(train);
                        }
                    }
                    DepHasItems = Departures.Count > 0;
                }
            }
            catch (Exception ex)
            {
                if(Departures.Count > 0)
                {
                    Departures.Clear();
                    DepHasItems = false;
                }
                Debug.WriteLine(ex.ToString());
                Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }
    }
}