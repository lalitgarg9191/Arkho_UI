using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace DFS.Views
{
    public partial class StripeWebPage : ContentPage
    {
        public StripeWebPage()
        {
            InitializeComponent();

            webView.Source = App.TrainerStripeUrl;
            App.TrainerStripeUrl = "";
            webView.Navigated += WebView_Navigated;
        }

        async void WebView_Navigated(object sender, WebNavigatedEventArgs e)
        {
            if (e.Url.StartsWith("http://104.238.81.169:4080/FitnessApp/manageservices/v1/members/stripe/account/creation", StringComparison.CurrentCultureIgnoreCase))
            {
                await DisplayAlert("Alert", "Account Creation successfull!", "OK");
                App.Current.MainPage = new RootPage(App.SelectedView);

            }
        }
    }
}