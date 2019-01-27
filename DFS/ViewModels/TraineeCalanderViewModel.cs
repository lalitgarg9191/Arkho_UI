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
    public class TraineeCalanderViewModel: INotifyPropertyChanged
    {
        private ObservableCollection<XamForms.Controls.SpecialDate> attendances;

        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ObservableCollection<XamForms.Controls.SpecialDate> Attendances
        {
            get { return attendances; }
            set { attendances = value; RaisePropertyChanged(nameof(Attendances)); }
        }

        private ObservableCollection<String> _scheduleList { get; set; }
        public ObservableCollection<String> ScheduleList
        {
            get { return _scheduleList; }
            set { _scheduleList = value; RaisePropertyChanged(nameof(ScheduleList)); }
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

        private bool _isInfoVisible;
        public bool IsInfoVisible
        {
            get
            {
                return _isInfoVisible;
            }
            set
            {
                _isInfoVisible = value;
                RaisePropertyChanged(nameof(IsInfoVisible));
            }
        }

        public TraineeCalanderViewModel()
        {
            ScheduleList = new ObservableCollection<string>();
        }

        DateTime currentDate = DateTime.Now;

        public ICommand LeftNavigateCommand
        {
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

        public ICommand DateChosen
        {
            get
            {
                return new Command((obj) => {
                    IsInfoVisible = true;
                });
            }
        }

        public ICommand CalenderSubmitCommand
        {
            get
            {
                return new Command((obj) => {
                    IsInfoVisible = false;
                });
            }
        }


        void HandleAction(object obj)
        {
            currentDate = currentDate.AddMonths(-1);
            GetTimeSlots();
        }

        private void HandleAction1(object obj)
        {
            currentDate = currentDate.AddMonths(1);
            GetTimeSlots();
        }

        public async void GetTimeSlots()
        {
            try
            {
                IsServiceInProgress = true;
                var email = App.SelectedView.Equals("Trainee") ? (App.TrainerData == null ? App.LoginResponse.Email : App.TrainerData.Email) : App.LoginResponse.Email;

                String currentMonth = currentDate.Month > 9 ? currentDate.Month.ToString() : "0" + (currentDate.Month.ToString());

                var request = new GetTimeSlotRequest { emailID = App.LoginResponse.Email, month = currentMonth, year = currentDate.Year.ToString() };
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
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
            IsServiceInProgress = false;
        }
    }
}
