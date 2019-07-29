using System;
using System.Collections.Generic;
using DFS.Utils;
using Xamarin.Forms;

namespace DFS.Views
{
    public partial class TrainerProfilePage : ContentPage
    {
        double xTransition;
        ViewModels.ProfileViewModel profileViewModel;
        public TrainerProfilePage()
        {
            InitializeComponent();

            BindingContext = profileViewModel = new ViewModels.ProfileViewModel();

            
        }
        void Handle_Tapped(object sender, System.EventArgs e)
        {
            //this.Navigation.PushAsync(new UserInformationPage(new ViewModels.SignupViewModel()));
            App.Current.MainPage = new HanselmanNavigationPage(new UserInformationPage(new ViewModels.SignupViewModel()));
        }

        async void Handle_Calender(object sender, System.EventArgs e)
        {
            await this.Navigation.PushAsync(new TraineeCalanderPage());
        }

        async void Handle_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            // don't do anything if we just de-selected the row.
            if (e.SelectedItem == null) return;

            // Deselect the item.
            if (sender is ListView lv)
            {
                Models.LoginResponse.Services service = (Models.LoginResponse.Services)e.SelectedItem;
                profileViewModel.ServiceDesc = service.ChargingPeriod;
                ServiceLabel.IsVisible = true;
                lv.SelectedItem = null;

                await DisplayAlert("Service Description", service.ChargingPeriod, "OK");
                await this.Navigation.PushAsync(new CalenderPage(service));

            }

        }

        void Handle_PanUpdated(object sender, Xamarin.Forms.PanUpdatedEventArgs e)
        {
            if (e.StatusType == GestureStatus.Completed)
            {
                if (xTransition > 50)
                {
                    if(profileViewModel.SelectedIndex != 0)
                    {
                        profileViewModel.SelectedIndex -= 1;
                        profileViewModel.HandleSwipe();
                    }

                }
                else if (xTransition < -50)
                {
                    if (profileViewModel.SelectedIndex < 2)
                    {
                        profileViewModel.SelectedIndex += 1;
                        profileViewModel.HandleSwipe();
                    }

                }

            }
            else
            {
                xTransition = e.TotalX;
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            MessagingCenter.Subscribe<Object, string>(this, "InstagramMedia", async (arg1, arg2) =>
            {
                try
                {
                    if (!string.IsNullOrEmpty(arg2))
                    {
                        var result = await App.TodoManager.GetInstagramMedia(arg2);

                        if (result.Equals("Success"))
                        {
                            CredentialsService.SaveCredentials(instagramMedia: App.InstagramMedia);
                            Application.Current.MainPage = new RootPage(App.SelectedView);
                        }
                        else
                        {
                            await DisplayAlert("Alert", result, "Ok");
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.ToString());
                    await DisplayAlert("Alert", "something went wrong", "Ok");
                }
            });

        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<InstagramLoginPage, string>(this, "InstagramMedia");
        }

        async void Handle_Tapped_1(object sender, System.EventArgs e)
        {
            await this.Navigation.PushAsync(new InstagramLoginPage(false));
        }
    }
}
