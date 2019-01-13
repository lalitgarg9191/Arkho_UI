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

        public TraineeCalanderViewModel()
        {

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

        private async void GetTimeSlots()
        {
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
