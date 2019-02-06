using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DFS.Views
{
    public partial class ScheduleList : ContentPage
    {
        public ScheduleList()
        {
            InitializeComponent();

            OpaqueView.IsVisible = true;
            IndicatorView.IsVisible = true;

            Initialization = InitializeAsync();

        }

        public Task Initialization { get; private set; }

        private async Task InitializeAsync()
        {
            var response = await App.TodoManager.FetchTrainerList();

            ItemsListView.ItemsSource = response.trainee;

            OpaqueView.IsVisible = false;
            IndicatorView.IsVisible = false;
        }
    }
}
