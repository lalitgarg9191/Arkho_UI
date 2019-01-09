using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using DFS.Models;
using Xamarin.Forms;
using XamForms.Controls;

namespace DFS.ViewModels
{
    public class CalenderViewModel : INotifyPropertyChanged
    {

        private bool _isSubmitVisible;
        public bool IsSubmitVisible
        {
            get
            {
                return _isSubmitVisible;
            }
            set
            {
                _isSubmitVisible = value;
                RaisePropertyChanged(nameof(IsSubmitVisible));
            }
        }

        private bool _isServiceInProgress;
        public bool IsServiceInProgress
        {
            get
            {
                return _isServiceInProgress;
            }
            set
            {
                _isServiceInProgress = value;
                RaisePropertyChanged(nameof(IsServiceInProgress));
            }
        }

        private int _startingIndex;
        public int StartingIndex
        {
            get
            {
                return _startingIndex;
            }
            set
            {
                _startingIndex = value;
                RaisePropertyChanged(nameof(StartingIndex));
            }
        }

        private int _endIndex;
        public int EndIndex
        {
            get
            {
                return _endIndex;
            }
            set
            {
                _endIndex = value;
                RaisePropertyChanged(nameof(EndIndex));
            }
        }

        private ObservableCollection<Models.CalenerListModal> _listViewData;
        public ObservableCollection<Models.CalenerListModal> ListViewData
        {
            get
            {
                return _listViewData;
            }
            set
            {
                _listViewData = value;

                RaisePropertyChanged(nameof(ListViewData));
            }
        }

        private ObservableCollection<XamForms.Controls.SpecialDate> attendances;
        public ObservableCollection<XamForms.Controls.SpecialDate> Attendances
        {
            get { return attendances; }
            set { attendances = value; RaisePropertyChanged(nameof(Attendances)); }
        }

        private Models.CalenerListModal _recentlySelectedItem;
        public Models.CalenerListModal RecentlySelectedItem
        {
            get
            {
                return _recentlySelectedItem;
            }
            set
            {
                _recentlySelectedItem = value;

                if (_recentlySelectedItem == null) return;

                int index = ListViewData.IndexOf(_recentlySelectedItem);

                UpdateListState(index);

                RaisePropertyChanged(nameof(RecentlySelectedItem));
            }
        }

        private void UpdateListState(int index)
        {
            if (StartingIndex == 0 && EndIndex == 0 && index == 0)
            {
                StartingIndex = index;
                ListViewData[index] = new Models.CalenerListModal(ListViewData[index].LabelName, Color.Lime);
                return;
            }
            else if (StartingIndex == 0 && EndIndex == 0 && index != 0)
            {

                if (ListViewData[StartingIndex].ListItemColor != Color.Lime)
                {
                    StartingIndex = index;
                    ListViewData[index] = new Models.CalenerListModal(ListViewData[index].LabelName, Color.Lime);
                    return;
                }

            }

            if (StartingIndex < index)
            {
                EndIndex = index;

                for (var tempIndex = 0; tempIndex < ListViewData.Count; tempIndex++)
                {
                    if (tempIndex >= StartingIndex && tempIndex <= EndIndex)
                    {
                        ListViewData[tempIndex] = new Models.CalenerListModal(ListViewData[tempIndex].LabelName, Color.Lime);
                    }
                    else
                    {
                        ListViewData[tempIndex] = new Models.CalenerListModal(ListViewData[tempIndex].LabelName, Color.White);
                    }
                }

            }
            else
            {
                StartingIndex = index;
                EndIndex = 0;

                for (var tempIndex = 0; tempIndex < ListViewData.Count; tempIndex++)
                {
                    if (tempIndex == StartingIndex)
                    {
                        ListViewData[tempIndex] = new Models.CalenerListModal(ListViewData[tempIndex].LabelName, Color.Lime);
                    }
                    else
                    {
                        ListViewData[tempIndex] = new Models.CalenerListModal(ListViewData[tempIndex].LabelName, Color.White);
                    }
                }
            }

        }

        public CalenderViewModel()
        {
            RefreshData();

            _isServiceInProgress = false;

            if(App.SelectedView == "Trainer")
            {
                IsSubmitVisible = false;
            }
            else
            {
                IsSubmitVisible = true;
            }

            /*var dt = DateTime.Now;
            var list = new List<SpecialDate> { new SpecialDate(dt) { BackgroundColor = Color.Red, Selectable = true }, new SpecialDate(dt.AddDays(1)) { BackgroundColor = Color.Red, Selectable = true } };
            Attendances = new ObservableCollection<SpecialDate>(list);*/
        }

        public void RefreshData()
        {
            StartingIndex = 0;
            EndIndex = 0;

            ListViewData = new ObservableCollection<Models.CalenerListModal>();

            ListViewData.Add(new Models.CalenerListModal("9:00 AM", Color.White));
            ListViewData.Add(new Models.CalenerListModal("10:00 AM", Color.White));
            ListViewData.Add(new Models.CalenerListModal("11:00 AM", Color.White));
            ListViewData.Add(new Models.CalenerListModal("12:00 AM", Color.White));
            ListViewData.Add(new Models.CalenerListModal("1:00 PM", Color.White));
            ListViewData.Add(new Models.CalenerListModal("2:00 PM", Color.White));
            ListViewData.Add(new Models.CalenerListModal("3:00 PM", Color.White));
            ListViewData.Add(new Models.CalenerListModal("4:00 PM", Color.White));
            ListViewData.Add(new Models.CalenerListModal("5:00 PM", Color.White));
        }

        public ICommand DateChosen
        {
            get
            {
                return new Command(async(obj) => {
                    System.Diagnostics.Debug.WriteLine(obj as DateTime?);

                    DateTime dateTime = (DateTime)obj;

                    var isExist =Attendances!=null? Attendances.Where(x => x.Date.Date == dateTime.Date).Any():false;
                    if (isExist)
                        await Application.Current.MainPage.DisplayAlert("Alert", "Date already selected", "OK");
                    else
                        MessagingCenter.Send<CalenderViewModel, DateTime>(this, "DateSelected", dateTime);
                });
            }
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        protected void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        DateTime currentDate = DateTime.Now;

        public ICommand LeftNavigateCommand {
            get
            {
                return new Command(HandleAction);
            }
        }

        public ICommand RightNavigateCommand
        {
            get
            {
                return new Command(HandleAction1);
            }
        }

        void HandleAction(object obj)
        {
            currentDate=currentDate.AddMonths(-1);
            GetTimeSlots();
        }

        private void HandleAction1(object obj)
        {
            currentDate=currentDate.AddMonths(1);
            GetTimeSlots();
        }

        private async void GetTimeSlots() {
            try
            {
                if (currentDate.Date >= DateTime.Now.Date)
                {
                    IsServiceInProgress = true;
                    var email = App.SelectedView.Equals("Trainer") ? App.TrainerData.Email : App.LoginResponse.Email;
                    var request = new GetTimeSlotRequest { emailID = App.TrainerData.Email, month = currentDate.Month.ToString(), year = currentDate.Year.ToString() };
                    var response = await App.TodoManager.GetTimeSlots(request);
                    if (response != null && response.TimeSlots.timeSlot.Any())
                    {
                        var bookedDates = new List<SpecialDate>();
                        foreach (var item in response.TimeSlots.timeSlot)
                        {
                            var date = new DateTime(Convert.ToInt32(item.year), Convert.ToInt32(item.month), Convert.ToInt32(item.day));
                            bookedDates.Add(new SpecialDate(date) { BackgroundColor = Color.Red, Selectable = true });
                        }
                        Attendances = new ObservableCollection<SpecialDate>(bookedDates);
                    }
                }
            }
            catch (Exception ex)
            {
            }
            IsServiceInProgress = false;
        }
    }
}
