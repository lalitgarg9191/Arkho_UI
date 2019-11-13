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

        private ObservableCollection<Models.TrainerListModel.Trainee> _listViewData;
        public ObservableCollection<Models.TrainerListModel.Trainee> ListViewData
        {
            get { return _listViewData; }
            set
            {
                _listViewData = value;
                RaisePropertyChanged(nameof(ListViewData));
            }
        }

        private ObservableCollection<Models.TrainerListModel.Trainee> _mainListData;
        public ObservableCollection<Models.TrainerListModel.Trainee> MainListData
        {
            get { return _mainListData; }
            set
            {
                _mainListData = value;
                RaisePropertyChanged(nameof(MainListData));
            }
        }

        private String _searchText;
        public String SearchText
        {
            get
            {
                return _searchText;
            }
            set
            {
                _searchText = value;
                RaisePropertyChanged(nameof(SearchText));

                FilterList();
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

            ListViewData = new ObservableCollection<Models.TrainerListModel.Trainee>();
            MainListData = response.trainee;
            FilterList();
            IsServiceInProgress = false;
        }

        private void FilterList()
        {
            if (SearchText == "" || SearchText == null)
            {
                ListViewData = MainListData;
            }
            else
            {
                ListViewData = new ObservableCollection<Models.TrainerListModel.Trainee>();
                foreach (var item in MainListData)
                {
                    if (item.SportsInterest.ToUpper().Contains(SearchText.ToUpper()))
                    {
                        ListViewData.Add(item);
                    }
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        protected void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
