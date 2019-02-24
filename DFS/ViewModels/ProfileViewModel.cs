using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;
using static DFS.Models.LoginResponse;

namespace DFS.ViewModels
{
    public class ProfileViewModel : INotifyPropertyChanged
    {
        private String _trainerName;
        public String TrainerName
        {
            get
            {
                return _trainerName;
            }
            set
            {
                _trainerName = value;
                RaisePropertyChanged(nameof(TrainerName));
            }
        }

        private String _serviceDesc;
        public String ServiceDesc
        {
            get
            {
                return _serviceDesc;
            }
            set
            {
                _serviceDesc = value;
                RaisePropertyChanged(nameof(ServiceDesc));
            }
        }

        private String _trainerSpeciality;
        public String TrainerSpeciality
        {
            get
            {
                return _trainerSpeciality;
            }
            set
            {
                _trainerSpeciality = value;
                RaisePropertyChanged(nameof(TrainerSpeciality));
            }
        }

        private String _trainingPlace;
        public String TrainingPlace
        {
            get
            {
                return _trainingPlace;
            }
            set
            {
                _trainingPlace = value;
                RaisePropertyChanged(nameof(TrainingPlace));
            }
        }

        private String _trainerExperience;
        public String TrainerExperience
        {
            get
            {
                return _trainerExperience;
            }
            set
            {
                _trainerExperience = value;
                RaisePropertyChanged(nameof(TrainerExperience));
            }
        }

        private String _trainerAccolades;
        public String TrainerAccolades
        {
            get
            {
                return _trainerAccolades;
            }
            set
            {
                _trainerAccolades = value;
                RaisePropertyChanged(nameof(TrainerAccolades));
            }
        }

        private String _trainerCert;
        public String TrainerCert
        {
            get
            {
                return _trainerCert;
            }
            set
            {
                _trainerCert = value;
                RaisePropertyChanged(nameof(TrainerCert));
            }
        }

        private Boolean _isReviewsVisible;
        public Boolean IsReviewsVisible
        {
            get
            {
                return _isReviewsVisible;
            }
            set
            {
                _isReviewsVisible = value;
                RaisePropertyChanged(nameof(IsReviewsVisible));
            }
        }

        private Boolean _isServiceVisible;
        public Boolean IsServiceVisible
        {
            get
            {
                return _isServiceVisible;
            }
            set
            {
                _isServiceVisible = value;
                RaisePropertyChanged(nameof(IsServiceVisible));
            }
        }

        private Boolean _isProfileVisible;
        public Boolean IsProfileVisible
        {
            get
            {
                return _isProfileVisible;
            }
            set
            {
                _isProfileVisible = value;
                RaisePropertyChanged(nameof(IsProfileVisible));
            }
        }

        private Boolean _isEditable;
        public Boolean IsEditable
        {
            get
            {
                return _isEditable;
            }
            set
            {
                _isEditable = value;
                RaisePropertyChanged(nameof(IsEditable));
            }
        }


        private Color _profileColor;
        public Color ProfileColor
        {
            get
            {
                return _profileColor;
            }
            set
            {
                _profileColor = value;
                RaisePropertyChanged(nameof(ProfileColor));
            }
        }

        private Color _serviceColor;
        public Color ServiceColor
        {
            get
            {
                return _serviceColor;
            }
            set
            {
                _serviceColor = value;
                RaisePropertyChanged(nameof(ServiceColor));
            }
        }

        private Color _reviewColor;
        public Color ReviewColor
        {
            get
            {
                return _reviewColor;
            }
            set
            {
                _reviewColor = value;
                RaisePropertyChanged(nameof(ReviewColor));
            }
        }

        private int _selectedIndex;
        public int SelectedIndex
        {
            get
            {
                return _selectedIndex;
            }
            set
            {
                _selectedIndex = value;
                RaisePropertyChanged(nameof(SelectedIndex));
            }
        }

        private ObservableCollection<Models.LoginResponse.Services> _serviceListData;
        public ObservableCollection<Models.LoginResponse.Services> ServiceListData
        {
            get { return _serviceListData; }
            set
            {
                _serviceListData = value;
                RaisePropertyChanged(nameof(ServiceListData));
            }
        }

        public ICommand ServiceCommand { get; set; }
        public ICommand ReviewCommand { get; set; }
        public ICommand ProfileCommand { get; set; }


        private UriImageSource _imageSource { get; set; } // = App.FacebookUser != null ? App.FacebookUser.Picture : App.InstagramUser != null ? App.InstagramUser.data.profile_picture : "profile1.jpeg";
        public UriImageSource ImageSource
        {
            get
            {
                return _imageSource;
            }
            set
            {
                _imageSource = value;
            }
        }

        //private string _placeHolderImageSource = App.FacebookUser != null ? App.FacebookUser.Picture : App.InstagramUser != null ? App.InstagramUser.data.profile_picture : "profile1.jpeg";
        //public string PlaceHolderImageSource
        //{
        //    get
        //    {
        //        return _placeHolderImageSource;
        //    }
        //    set
        //    {
        //        _placeHolderImageSource = value;
        //        RaisePropertyChanged(nameof(PlaceHolderImageSource));
        //    }
        //}

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
            set
            {
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

        public ProfileViewModel()
        {
            ServiceListData = new ObservableCollection<Models.LoginResponse.Services>();

            //Models.LoginResponse.SyncLoginResponse syncLoginResponse = App.DatabaseManager.SyncLoginResponse(App.SelectedView);


            if (App.SelectedView == "Trainer")
            {
                IsEditable = true;
                TrainerName = App.LoginResponse.basicInfo.Name;
                TrainingPlace = App.LoginResponse.basicInfo.Address;
                TrainerAccolades = App.LoginResponse.professionalInfo.Accolades;
                TrainerExperience = App.LoginResponse.professionalInfo.Experience;
                TrainerSpeciality = App.LoginResponse.professionalInfo.Speciality;

                if (App.LoginResponse.basicInfo.ImageUrl != null)
                {
                    String url = App.LoginResponse.basicInfo.ImageUrl != null ? App.LoginResponse.basicInfo.ImageUrl : "defaultIcon.png";
                    string[] values = url.Split(new string[] { "http" }, StringSplitOptions.None);

                    String finalUrl;

                    if (values.Length > 2)
                    {
                        String tempUrl = "http" + values[values.Length - 1];

                        finalUrl = tempUrl.Substring(0, tempUrl.Length - 5);
                    }
                    else
                    {
                        finalUrl = "http" + values[values.Length - 1];
                    }
                    ImageSource = new UriImageSource { CachingEnabled = true, Uri = new System.Uri(finalUrl) };
                }
                //PlaceHolderImageSource = "defaultIcon.png";
                ServiceListData = App.LoginResponse.professionalInfo.services;

                TrainerCert = "";

                foreach (var item in App.LoginResponse.professionalInfo.certifications)
                {
                    TrainerCert += item.Certification + " | ";
                }

                if (App.InstagramMedia != null && App.InstagramMedia.data != null)
                {
                    GalleryVisible = true;
                    InstaVisible = false;

                    var list = new List<string>();
                    foreach (var item in App.InstagramMedia.data)
                    {
                        var media = item.images.standard_resolution.url;
                        list.Add(media);
                    }
                    Gallery = new ObservableCollection<string>(list);
                }
                else
                {
                    GalleryVisible = false;
                    InstaVisible = true;
                }
            }
            else
            {
                IsEditable = false;
                TrainerName = App.TrainerData.basicInfo.Name;
                TrainingPlace = App.TrainerData.basicInfo.Address;
                TrainerAccolades = App.TrainerData.professionalInfo.Accolades;
                TrainerExperience = App.TrainerData.professionalInfo.Experience;
                TrainerSpeciality = App.TrainerData.professionalInfo.Speciality;

                if (App.TrainerData.basicInfo.ImageUrl != null)
                {

                    String url = App.TrainerData.basicInfo.ImageUrl != null ? App.TrainerData.basicInfo.ImageUrl : "defaultIcon.png";

                    string[] values = url.Split(new string[] { "http" }, StringSplitOptions.None);

                    String finalUrl;

                    if (values.Length > 2)
                    {
                        String tempUrl = "http" + values[values.Length - 1];

                        finalUrl = tempUrl.Substring(0, tempUrl.Length - 5);
                    }
                    else
                    {
                        finalUrl = "http" + values[values.Length - 1];
                    }

                    ImageSource = new UriImageSource { CachingEnabled = true, Uri = new System.Uri(finalUrl) };
                }

                if (App.TrainerData.professionalInfo.services.Any())
                    ServiceListData = App.TrainerData.professionalInfo.services;



                TrainerCert = "";

                foreach (var item in App.TrainerData.professionalInfo.certifications)
                {
                    TrainerCert += item.Certification + " | ";
                }
            }

            SelectedIndex = 0;

            HandleSwipe();

            ServiceCommand = new Command(() => HandleServiceSelection());
            ReviewCommand = new Command(() => HandleReviewSelection());
            ProfileCommand = new Command(() => HandleProfileSelection());
        }

        public void HandleSwipe()
        {
            if (SelectedIndex == 0)
            {
                HandleProfileSelection();
            }
            else if (SelectedIndex == 1)
            {
                HandleServiceSelection();
            }
            else
            {
                HandleReviewSelection();
            }
        }

        public void HandleServiceSelection()
        {
            SelectedIndex = 1;
            IsServiceVisible = true;
            IsReviewsVisible = false;
            IsProfileVisible = false;

            ServiceColor = Color.LimeGreen;
            ReviewColor = Color.White;
            ProfileColor = Color.White;
        }

        public void HandleReviewSelection()
        {
            SelectedIndex = 2;
            IsServiceVisible = false;
            IsReviewsVisible = true;
            IsProfileVisible = false;

            ServiceColor = Color.White;
            ReviewColor = Color.LimeGreen;
            ProfileColor = Color.White;
        }

        public void HandleProfileSelection()
        {
            SelectedIndex = 0;
            IsServiceVisible = false;
            IsReviewsVisible = false;
            IsProfileVisible = true;

            ServiceColor = Color.White;
            ReviewColor = Color.White;
            ProfileColor = Color.LimeGreen;
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        protected void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
