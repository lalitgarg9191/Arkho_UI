﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using DFS.Views;
using Xamarin.Forms;

namespace DFS.ViewModels
{
    public class TraineeProfileViewModel : INotifyPropertyChanged
    {
        private String _traineeName;
        public String TraineeName
        {
            get { return _traineeName; }
            set
            {
                _traineeName = value;
                RaisePropertyChanged(nameof(TraineeName));
            }
        }

        private String _traineeInterest;
        public String TraineeInterest
        {
            get { return _traineeInterest; }
            set
            {
                _traineeInterest = value;
                RaisePropertyChanged(nameof(TraineeInterest));
            }
        }

        public String TraineeGoals
        {
            get { return _traineeGoals; }
            set
            {
                _traineeGoals = value;
                RaisePropertyChanged(nameof(TraineeGoals));
            }
        }

        private String _traineeGoals;

        private string _imageSource { get; set; } // = App.FacebookUser != null ? App.FacebookUser.Picture : App.InstagramUser != null ? App.InstagramUser.data.profile_picture : "profile1.jpeg";
        public string ImageSource
        {
            get
            {
                return _imageSource;
            }
            set
            {
                _imageSource = value;
                RaisePropertyChanged(nameof(ImageSource));
            }
        }

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

        private bool galeryVisible;
        public bool GalleryVisible
        {
            get
            {
                return galeryVisible;
            }
            set {
                galeryVisible = value;
                RaisePropertyChanged(nameof(GalleryVisible));
            }
        }

        private bool instaVisible;
        public bool InstaVisible
        {
            get
            {
                return instaVisible;
            }
            set
            {
                instaVisible = value;
                RaisePropertyChanged(nameof(InstaVisible));
            }
        }

        public ObservableCollection<string> Gallery { get; set; }

        public ICommand CalanderCommand
        {
            get { return new Command(CalanderHandler); }
        }

        public TraineeProfileViewModel()
        {
            //Models.LoginResponse.SyncLoginResponse syncLoginResponse = App.DatabaseManager.SyncLoginResponse("Trainee");

            TraineeName = App.LoginResponse.basicInfo.Name;
            TraineeGoals = App.LoginResponse.basicInfo.SportsInterest;
            TraineeInterest = App.LoginResponse.basicInfo.SportsInterest;
            ImageSource = App.LoginResponse.basicInfo.ImageUrl != null ? App.LoginResponse.basicInfo.ImageUrl : "defaultIcon.png";

            if(App.InstagramMedia!=null && App.InstagramMedia.data != null) {
                GalleryVisible = true;
                InstaVisible = false;

                var list = new List<string>();
                foreach(var item in App.InstagramMedia.data) {
                    var media = item.images.standard_resolution.url;
                    list.Add(media);
                }
                Gallery = new ObservableCollection<string>(list);
            }
            else {
                GalleryVisible = false;
                InstaVisible = true;
            }
        }

        void CalanderHandler(object obj)
        {
            //await Application.Current.MainPage.Navigation.PushAsync(new TraineeCalanderPage());
            MessagingCenter.Send<TraineeProfileViewModel>(this, "CalenderPage");
        }


        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        protected void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
