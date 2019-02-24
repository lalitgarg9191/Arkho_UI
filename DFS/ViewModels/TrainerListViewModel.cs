using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DFS.ViewModels
{
    public class TrainerListViewModel : INotifyPropertyChanged
    {
        private Boolean _isServiceInProgress;
        public Boolean IsServiceInProgress
        {
            get { return _isServiceInProgress; }
            set
            {
                _isServiceInProgress = value;
                RaisePropertyChanged(nameof(IsServiceInProgress));
            }
        }

        private ObservableCollection<Models.TrainerListModel.TraineeList> _listViewData;
        public ObservableCollection<Models.TrainerListModel.TraineeList> ListViewData
        {
            get { return _listViewData; }
            set
            {
                _listViewData = value;
                RaisePropertyChanged(nameof(ListViewData));
            }
        }


        public Task Initialization { get; private set; }

        public TrainerListViewModel()
        {
            _isServiceInProgress = true;

            Initialization = InitializeAsync();
        }

        private async Task InitializeAsync()
        {
            var response = await App.TodoManager.FetchTrainerList();

            ListViewData = new ObservableCollection<Models.TrainerListModel.TraineeList>();

            foreach(var item in response.trainee)
            {
                Models.TrainerListModel.TraineeList trainee = new Models.TrainerListModel.TraineeList();
                trainee.Address = item.Address;
                trainee.Country = item.Country;
                trainee.Email = item.Email;
                trainee.Name = item.Name;
                trainee.SportsInterest = item.SportsInterest;
                trainee.State = item.State;
                trainee.Status = item.Status;

                String imageUrl = item.ImageUrL;
                trainee.ImageUrL = new UriImageSource { CachingEnabled = true, Uri = new System.Uri(imageUrl) };

                ListViewData.Add(trainee);

            }



            IsServiceInProgress = false;
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        protected void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
