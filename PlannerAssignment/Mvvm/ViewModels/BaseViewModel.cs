using PlannerAssignment.Utils;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace PlannerAssignment.Mvvm.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        private readonly RequestManager _requestManager;

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private bool _isRefreshing;
        public bool IsRefreshing
        {
            get => _isRefreshing;
            set
            {
                _isRefreshing = value;
                OnPropertyChanged(nameof(IsRefreshing));
            }
        }

        public ICommand FetchDataCommand { get; set; }

        public BaseViewModel(RequestManager requestManager)
        {
            _requestManager = requestManager;
            FetchDataCommand = new Command(async () => await FetchData());
        }

        public async Task FetchData()
        {
            IsRefreshing = true;
            try
            {
                await FetchDataInternal().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                throw new Exception("Error fetching data");
                Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
            finally
            {
                IsRefreshing = false;
            }
        }

        protected abstract Task FetchDataInternal();
    }
}
