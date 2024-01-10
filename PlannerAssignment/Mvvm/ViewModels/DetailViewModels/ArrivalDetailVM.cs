using PlannerAssignment.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PlannerAssignment.Mvvm.Models.ArrivalTrainModel;

namespace PlannerAssignment.Mvvm.ViewModels.DetailViewModels
{

    public class ArrivalDetailVM : BaseViewModel
    {
        private readonly RequestManager _requestManager;

        public ArrivalTrain _selectedTrain;
        public ArrivalTrain SelectedTrain
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

        public ArrivalDetailVM(RequestManager requestManager, ArrivalTrain arrivalTrain) : base(requestManager)
        {
            _requestManager = requestManager;
            SelectedTrain = arrivalTrain;
        }

        protected override async Task FetchDataInternal()
        {
            try
            {
                await _requestManager.GetArrivingTrainsListAsync();
            }
            catch (Exception ex)
            {
                Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }
    }

}
