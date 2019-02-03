using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using DFS.Models;
using Xamarin.Forms;
using XamForms.Controls;

namespace DFS.Views
{
    public partial class CalenderPage : ContentPage
    {
        ViewModels.CalenderViewModel calenderViewModel;
        //DateTime selectedDate;
        public CalenderPage(List<LoginResponse.Schedule> schedules)
        {
            InitializeComponent();

            BindingContext = calenderViewModel = new ViewModels.CalenderViewModel(schedules);

            calendar.MinDate = DateTime.Now; 

            //MessagingCenter.Subscribe<ViewModels.CalenderViewModel, DateTime>(this, "DateSelected", (sender, _selectedDate) =>
            //{
            //    //await this.Navigation.PushAsync(new TimeSelectionPopup());
            //    TimeView.IsVisible = true;
            //    OpaqueView.IsVisible = true;

            //    selectedDate = _selectedDate;

            //});

        }

        //void Handle_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        //{
        //    // don't do anything if we just de-selected the row.
        //    if (e.SelectedItem == null) return;

        //    // Deselect the item.
        //    if (sender is ListView lv) lv.SelectedItem = null;

        //}
        /*
        async void Handle_Calender(object sender, System.EventArgs e)
        {
            if (calenderViewModel.StartingIndex == calenderViewModel.EndIndex)
                await Application.Current.MainPage.DisplayAlert("Alert", "Please select end date", "OK");
            else
            {
                TimeView.IsVisible = false;
                OpaqueView.IsVisible = false;

                var result = await DisplayAlert("Alert", "Are you sure, you want to pay for " + (calenderViewModel.EndIndex - calenderViewModel.StartingIndex) + " hours?", "Accept", "Cancel");

                if (result)
                {
                    Models.SetTimeSlotsRequestModel setTimeSlots = new Models.SetTimeSlotsRequestModel();
                    setTimeSlots.emailID = App.TrainerData.Email;
                    setTimeSlots.addByEmailID = App.LoginResponse.Email;

                    List<Models.SetTimeSlotsRequestModel.TimeSlot> timeSlots = new List<Models.SetTimeSlotsRequestModel.TimeSlot>();

                    Models.SetTimeSlotsRequestModel.TimeSlot timeSlot = new Models.SetTimeSlotsRequestModel.TimeSlot();
                    timeSlot.day = selectedDate.Day.ToString();
                    timeSlot.month = selectedDate.Month.ToString();
                    timeSlot.year = selectedDate.Year.ToString();
                    timeSlot.startTime = calenderViewModel.ListViewData[calenderViewModel.StartingIndex].LabelName;
                    timeSlot.endTime = calenderViewModel.ListViewData[calenderViewModel.EndIndex].LabelName;
                    timeSlot.remarks = "Appointment";

                    timeSlots.Add(timeSlot);

                    setTimeSlots.timeSlot = timeSlots;

                    calenderViewModel.RefreshData();

                    calenderViewModel.IsServiceInProgress = true;

                    var slotResponseModel = await App.TodoManager.SetTimeSlot(setTimeSlots);
                    if (slotResponseModel != null && !string.IsNullOrEmpty(slotResponseModel.status.status) && slotResponseModel.status.status.ToLower().Equals("success"))
                    {
                        var paymentRequest = new PaymentRequest {
                            BusinessTransaction=new BusinessTransaction { 
                                PayeeId=App.TrainerData.Email,
                                PayerId=App.LoginResponse.Email,
                                Amount="20",
                                EventId="1"
                            }
                        };
                        var paymentResponse = await App.TodoManager.StartPayment(paymentRequest);
                        if(paymentResponse!=null && paymentResponse.TransactionStatus!=null && !string.IsNullOrEmpty(paymentResponse.TransactionStatus.processURL)) {
                            await this.Navigation.PushAsync(new PaymentPage(paymentResponse.TransactionStatus.processURL));
                        }
                    }

                    calenderViewModel.IsServiceInProgress = false;
                }
            }
        }*/

        //void Hide_Calender(object sender, System.EventArgs e)
        //{
        //    TimeView.IsVisible = false;
        //    OpaqueView.IsVisible = false;
        //}

        protected override void OnAppearing()
        {
            base.OnAppearing();
            //calenderViewModel.IsServiceInProgress = true;
            //var email = App.SelectedView.Equals("Trainer") ? App.TrainerData.Email : App.LoginResponse.Email;
            //var request = new GetTimeSlotRequest { emailID = email, month = DateTime.Now.Month.ToString(), year = DateTime.Now.Year.ToString() };
            //var response = await App.TodoManager.GetTimeSlots(request);
            //if (response != null && response.TimeSlots.timeSlot.Any())
            //{
            //    var bookedDates = new List<SpecialDate>();
            //    foreach (var item in response.TimeSlots.timeSlot)
            //    {
            //        var date = new DateTime(Convert.ToInt32(item.year), Convert.ToInt32(item.month), Convert.ToInt32(item.day));
            //        bookedDates.Add(new SpecialDate(date) { BackgroundColor = Color.Red, Selectable = true });
            //    }
            //    calenderViewModel.Attendances = new ObservableCollection<SpecialDate>(bookedDates);
            //}
            //calenderViewModel.IsServiceInProgress = false;
        }
    }
}
