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
        public CalenderPage(LoginResponse.Services schedules)
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

        async void Handle_Calender(object sender, System.EventArgs e)
        {

            int count = 0;

            for (int index = 0; index < calenderViewModel.Attendances.Count; index++)
            {
                if (calenderViewModel.Attendances[index].BackgroundColor == Color.Green)
                {
                    count = count + 1;

                }
            }

            if (count == 0)
            {
                await DisplayAlert("Alert", "Please select minimum 1 day.", "Ok");
            }
            else
            {
                var result = await DisplayAlert("Alert", "Are you sure, you want to pay for " + count + " days?", "Accept", "Cancel");

                if (result)
                {
                    var paymentRequest = new PaymentRequest
                    {
                        BusinessTransaction = new BusinessTransaction
                        {
                            PayeeId = App.TrainerData.Email,
                            PayerId = App.LoginResponse.Email,
                            Amount = (count * Convert.ToDouble(calenderViewModel.Charges)) + "",
                            EventId = "1"
                        }
                    };
                    var paymentResponse = await App.TodoManager.StartPayment(paymentRequest);
                    if (paymentResponse != null && paymentResponse.TransactionStatus != null && !string.IsNullOrEmpty(paymentResponse.TransactionStatus.processURL))
                    {
                        await this.Navigation.PushAsync(new PaymentPage(paymentResponse.TransactionStatus.processURL));
                    }
                }


            }
        }
    }


    //void Hide_Calender(object sender, System.EventArgs e)
    //{
    //    TimeView.IsVisible = false;
    //    OpaqueView.IsVisible = false;
    //}


}
