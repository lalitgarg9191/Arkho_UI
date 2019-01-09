using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace DFS.Views
{
    public partial class PaymentPage : ContentPage
    {
        public PaymentPage(string url)
        {
            InitializeComponent();
            webView.Source = url;
            webView.Navigated+=WebView_Navigated;
        }

        async void WebView_Navigated(object sender, WebNavigatedEventArgs e)
        {
            if (e.Url.StartsWith("http://104.238.81.169:4080",StringComparison.CurrentCultureIgnoreCase)) {
                var arr = e.Url.Split('?');
                var paymentData = arr[1];
                var arr1 = paymentData.Split('=');
                var paymentId = arr1[1].Split('&')[0];
                var tokenId = arr1[2].Split('&')[0];
                if (!string.IsNullOrEmpty(paymentId)) {
                    await DisplayAlert("Alert", "Payment successfull!", "OK");
                    App.Current.MainPage = new RootPage(App.SelectedView);
                }
            }
        }

    }
}
