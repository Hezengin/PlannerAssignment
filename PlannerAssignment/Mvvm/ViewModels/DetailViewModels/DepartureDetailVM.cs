using PlannerAssignment.Utils;
using static PlannerAssignment.Mvvm.Models.DepartureTrainModel;

namespace PlannerAssignment.Mvvm.ViewModels.DetailViewModels
{
    public class DepartureDetailVM : BaseViewModel
    {
        private readonly RequestManager _requestManager;

        public DepartureTrain _selectedTrain;
        public DepartureTrain SelectedTrain
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

        public DepartureDetailVM(RequestManager requestManager, DepartureTrain departureTrain) : base(requestManager)
        {
            _requestManager = requestManager;
            SelectedTrain = departureTrain;
        }

        protected override async Task FetchDataInternal()
        {
            try
            {
                await _requestManager.GetDeparturingTrainsListAsync();
            }
            catch (Exception ex)
            {
                Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }
    }
}
