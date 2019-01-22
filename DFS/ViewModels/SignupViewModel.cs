using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using Plugin.Media;
using System.Collections.Generic;
using Plugin.Geolocator;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using DFS.Utils;

namespace DFS.ViewModels
{
    public class SignupViewModel : INotifyPropertyChanged
    {
        private Plugin.Geolocator.Abstractions.Position position = new Plugin.Geolocator.Abstractions.Position();

        private Boolean _isTrainerView { get; set; }

        public Boolean IsTrainerView
        {
            get
            {
                return _isTrainerView;
            }
            set
            {
                _isTrainerView = value;

                RaisePropertyChanged(nameof(IsTrainerView));
            }
        }

        private ObservableCollection<String> _titleList { get; set; }

        public ObservableCollection<String> TitleList
        {
            get
            {
                return _titleList;
            }
            set {
                _titleList = value;

                RaisePropertyChanged(nameof(TitleList));
            }
        }

        private int _titleIndex { get; set; }

        public int TitleIndex
        {
            get
            {
                return _titleIndex;
            }
            set
            {
                _titleIndex = value;

                RaisePropertyChanged(nameof(TitleIndex));
            }
        }

        private ObservableCollection<String> _genderList { get; set; }

        public ObservableCollection<String> GenderList
        {
            get
            {
                return _genderList;
            }
            set
            {
                _genderList = value;

                RaisePropertyChanged(nameof(GenderList));
            }
        }

        private int _genderIndex { get; set; }

        public int GenderIndex
        {
            get
            {
                return _genderIndex;
            }
            set
            {
                _genderIndex = value;

                RaisePropertyChanged(nameof(GenderIndex));
            }
        }

        private String _selectedView { get; set; }

        public String SelectedView
        {
            get
            {
                return _selectedView;
            }
            set
            {
                _selectedView = value;

                RaisePropertyChanged(nameof(SelectedView));
            }
        }

        private String _emailAddress { get; set; }

        public String EmailAddress
        {
            get
            {
                return _emailAddress;
            }
            set
            {
                _emailAddress = value;

                RaisePropertyChanged(nameof(EmailAddress));
            }
        }

        private String _password { get; set; }

        public String Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;

                RaisePropertyChanged(nameof(Password));
            }
        }

        private String _confirmPassword { get; set; }

        public String ConfirmPassword
        {
            get
            {
                return _confirmPassword;
            }
            set
            {
                _confirmPassword = value;

                RaisePropertyChanged(nameof(ConfirmPassword));
            }
        }


        private DateTime _dateOfBirth { get; set; }

        public DateTime DateOfBirth 
        {
            get
            {
                return _dateOfBirth;
            }
            set
            {
                _dateOfBirth = value;
                RaisePropertyChanged(nameof(DateOfBirth));
            }
        }


        private ObservableCollection<String> _sportsList { get; set; }

        public ObservableCollection<String> SportsList
        {
            get
            {
                return _sportsList;
            }
            set
            {
                _sportsList = value;

                RaisePropertyChanged(nameof(SportsList));
            }
        }

        private int _sportsIndex { get; set; }

        public int SportsIndex
        {
            get
            {
                return _sportsIndex;
            }
            set
            {
                _sportsIndex = value;

                RaisePropertyChanged(nameof(SportsIndex));
            }
        }

        private ObservableCollection<String> _specialityList { get; set; }

        public ObservableCollection<String> SpecialityList
        {
            get
            {
                return _specialityList;
            }
            set
            {
                _specialityList = value;

                RaisePropertyChanged(nameof(SpecialityList));
            }
        }

        private int _specialityIndex { get; set; }

        public int SpecialityIndex
        {
            get
            {
                return _specialityIndex;
            }
            set
            {
                _specialityIndex = value;

                RaisePropertyChanged(nameof(SpecialityIndex));
            }
        }


        private String _userIcon { get; set; }

        public String UserIcon
        {
            get
            {
                return _userIcon;
            }
            set
            {
                _userIcon = value;

                RaisePropertyChanged(nameof(UserIcon));
            }
        }

        private String _user64String { get; set; }

        public String User64String
        {
            get
            {
                return _user64String;
            }
            set
            {
                _user64String = value;

                RaisePropertyChanged(nameof(User64String));
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

        private Boolean _timeSelectionVisible;
        public Boolean TimeSelectionVisible
        {
            get { return _timeSelectionVisible; }
            set
            {
                _timeSelectionVisible = value;
                RaisePropertyChanged(nameof(TimeSelectionVisible));
            }
        }


        private ObservableCollection<Models.CustomSignupModel> _staticListData;

        public ObservableCollection<Models.CustomSignupModel> StaticListData
        {
            get { return _staticListData; }
            set
            {
                _staticListData = value;
                RaisePropertyChanged(nameof(StaticListData));
            }
        }

        private int _selectedCalenderIndex { get; set; }

        public int SelectedCalenderIndex
        {
            get
            {
                return _selectedCalenderIndex;
            }
            set
            {
                _selectedCalenderIndex = value;

                RaisePropertyChanged(nameof(SelectedCalenderIndex));
            }
        }

        private ObservableCollection<String> _listViewData;
        public ObservableCollection<String> ListViewData
        {
            get
            {
                return _listViewData;
            }
            set
            {
                _listViewData = value;

                RaisePropertyChanged(nameof(ListViewData));
            }
        }

        private String _recentlySelectedItem { get; set; }

        public String RecentlySelectedItem
        {
            get
            {
                return _recentlySelectedItem;
            }
            set
            {
                if (value == null)
                    return;

                _recentlySelectedItem = value;
                RaisePropertyChanged(nameof(RecentlySelectedItem));
                if (TimeHeader == "Please Select Starting Time")
                {
                    StaticListData[1][SelectedCalenderIndex].StartTime = value;
                    TimeHeader = "Please Select End Time";
                }
                else
                {
                    TimeSelectionVisible = false;
                    StaticListData[1][SelectedCalenderIndex].EndTime = value;
                    TimeHeader = "Please Select Starting Time";

                    MessagingCenter.Send<SignupViewModel>(this, "MoveBack");
                }


            }
        }

        private String _timeHeader { get; set; }

        public String TimeHeader
        {
            get
            {
                return _timeHeader;
            }
            set
            {
                _timeHeader = value;

                RaisePropertyChanged(nameof(TimeHeader));
            }
        }

        public ICommand SaveCommand { get; set; }
        public ICommand PictureCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand CalendarCommand { get; set; }

        void SelectImage()
        {

        }

        public ICommand DateChosen
        {
            get
            {
                return new Command( (obj) => {
                    System.Diagnostics.Debug.WriteLine(obj as DateTime?);

                    DateTime dateTime = (DateTime)obj;


                    StaticListData[1][SelectedCalenderIndex].Day = dateTime.Day + "";
                    StaticListData[1][SelectedCalenderIndex].Month = dateTime.Month + "";
                    StaticListData[1][SelectedCalenderIndex].Year = dateTime.Year + "";
                    StaticListData[1][SelectedCalenderIndex].WeekDay = dateTime.DayOfWeek.ToString();



                    TimeSelectionVisible = true;

                });
            }
        }



        public Task Initialization { get; private set; }

        public SignupViewModel()
        {
            ListViewData = new ObservableCollection<String>();

            ListViewData.Add("9:00 AM");
            ListViewData.Add("9:15 AM");
            ListViewData.Add("9:30 AM");
            ListViewData.Add("9:45 AM");
            ListViewData.Add("10:00 AM");
            ListViewData.Add("10:15 AM");
            ListViewData.Add("10:30 AM");
            ListViewData.Add("10:45 AM");
            ListViewData.Add("11:00 AM");
            ListViewData.Add("11:15 AM");
            ListViewData.Add("11:30 AM");
            ListViewData.Add("11:45 AM");
            ListViewData.Add("12:00 AM");
            ListViewData.Add("12:15 AM");
            ListViewData.Add("12:30 AM");
            ListViewData.Add("12:45 AM");
            ListViewData.Add("1:00 PM");
            ListViewData.Add("1:15 PM");
            ListViewData.Add("1:30 PM");
            ListViewData.Add("1:45 PM");
            ListViewData.Add("2:00 PM");
            ListViewData.Add("2:15 PM");
            ListViewData.Add("2:30 PM");
            ListViewData.Add("2:45 PM");
            ListViewData.Add("3:00 PM");
            ListViewData.Add("3:15 PM");
            ListViewData.Add("3:30 PM");
            ListViewData.Add("3:45 PM");
            ListViewData.Add("4:00 PM");
            ListViewData.Add("4:15 PM");
            ListViewData.Add("4:30 PM");
            ListViewData.Add("4:45 PM");
            ListViewData.Add("5:00 PM");

            GenderList = new ObservableCollection<String>();
            GenderList.Add("Male");
            GenderList.Add("Female");

            SportsList = new ObservableCollection<String>();
            SportsList.Add("Cricket");
            SportsList.Add("Baseball");
            SportsList.Add("Football");
            SportsList.Add("Tennis");
            SportsList.Add("Table Tennis");
            SportsList.Add("Basketball");
            SportsList.Add("Swimming");
            SportsList.Add("Athletics");
            SportsList.Add("Others");

            SpecialityList = new ObservableCollection<String>();
            SpecialityList.Add("Cricket");
            SpecialityList.Add("Baseball");
            SpecialityList.Add("Football");
            SpecialityList.Add("Tennis");
            SpecialityList.Add("Table Tennis");
            SpecialityList.Add("Basketball");
            SpecialityList.Add("Swimming");
            SpecialityList.Add("Athletics");
            SpecialityList.Add("Others");

            DateOfBirth = new DateTime(2000, 1, 1);

            TimeSelectionVisible = false;

            _timeHeader = "Please Select Starting Time";

            StaticListData = new ObservableCollection<Models.CustomSignupModel>();

            Models.CustomSignupModel basicSignUpModel = new Models.CustomSignupModel { HeaderName = "Basic Information" };

            basicSignUpModel.Add(new Models.SignupData { InputType = "Label", PlaceholderText = "Name", IsAdditionAvailable = false });
            // Index Number 1 (Name)
            basicSignUpModel.Add(new Models.SignupData { InputType = "Entry", PlaceholderText = "Enter Name", IsAdditionAvailable = false });

            basicSignUpModel.Add(new Models.SignupData { InputType = "Label", PlaceholderText = "Gender", IsAdditionAvailable = false });
            // Index Number 3 (Gender)
            basicSignUpModel.Add(new Models.SignupData { InputType = "Picker", PlaceholderText = "Select Gender", IsAdditionAvailable = false, SelectionData = GenderList });

            basicSignUpModel.Add(new Models.SignupData { InputType = "Label", PlaceholderText = "Date of Birth", IsAdditionAvailable = false });
            // Index Number 5 (DOB)
            basicSignUpModel.Add(new Models.SignupData { InputType = "Picker", PlaceholderText = "Date of Birth", SelectedDate = DateOfBirth, IsAdditionAvailable = true });

            basicSignUpModel.Add(new Models.SignupData { InputType = "Label", PlaceholderText = "Phone Number", IsAdditionAvailable = false });
            // Index Number 7 (Phone)
            basicSignUpModel.Add(new Models.SignupData { InputType = "Entry", IsAdditionAvailable = true, PlaceholderText = "Enter Phone Number" });

            basicSignUpModel.Add(new Models.SignupData { InputType = "Label", PlaceholderText = "Height (in cm)", IsAdditionAvailable = false });
            // Index Number 9 (Height)
            basicSignUpModel.Add(new Models.SignupData { InputType = "Entry", IsAdditionAvailable = true, PlaceholderText = "Enter Height" });

            basicSignUpModel.Add(new Models.SignupData { InputType = "Label", PlaceholderText = "Weight (in kg)", IsAdditionAvailable = false });
            // Index Number 11 (Weight)
            basicSignUpModel.Add(new Models.SignupData { InputType = "Entry", IsAdditionAvailable = true, PlaceholderText = "Enter Weight" });

            basicSignUpModel.Add(new Models.SignupData { InputType = "Label", PlaceholderText = "Sports Interest", IsAdditionAvailable = false });
            // Index Number 13 (Sports Interest)
            basicSignUpModel.Add(new Models.SignupData { InputType = "Picker", PlaceholderText = "Select Sport", IsAdditionAvailable = false, SelectionData = SportsList });

            basicSignUpModel.Add(new Models.SignupData { InputType = "Label", PlaceholderText = "Medical Information", IsAdditionAvailable = false });
            // Index Number 15 (Medical Info)
            basicSignUpModel.Add(new Models.SignupData { InputType = "Editor", PlaceholderText = "Enter Medical Information" });


            StaticListData.Add(basicSignUpModel);

            if (App.SelectedView == "Trainer")
            {

                Models.CustomSignupModel serviceSignUpModel = new Models.CustomSignupModel { HeaderName = "Services" };

                serviceSignUpModel.Add(new Models.SignupData { InputType = "Label", PlaceholderText = "Specialty", IsAdditionAvailable = false });
                // Index Number 1 (Specialty)
                serviceSignUpModel.Add(new Models.SignupData { InputType = "Picker", PlaceholderText = "Select Specialty", IsAdditionAvailable = false, SelectionData = SpecialityList });

                serviceSignUpModel.Add(new Models.SignupData { InputType = "Label", PlaceholderText = "Experience", IsAdditionAvailable = false });
                // Index Number 1 (Experiance)
                serviceSignUpModel.Add(new Models.SignupData { InputType = "Entry", PlaceholderText = "Enter Experience", IsAdditionAvailable = true });

                serviceSignUpModel.Add(new Models.SignupData { InputType = "Label", PlaceholderText = "Awards", IsAdditionAvailable = true });
                // Index Number 1 (Awards)
                serviceSignUpModel.Add(new Models.SignupData { InputType = "Entry", PlaceholderText = "Enter Awards", IsAdditionAvailable = false });

                serviceSignUpModel.Add(new Models.SignupData { InputType = "Label", PlaceholderText = "Certification", IsAdditionAvailable = true });
                // Index Number 1 (Certification)
                serviceSignUpModel.Add(new Models.SignupData { InputType = "Entry", PlaceholderText = "Enter Certification", IsAdditionAvailable = false });

                serviceSignUpModel.Add(new Models.SignupData { InputType = "Label", PlaceholderText = "Service", IsAdditionAvailable = true });
                // Index Number 1 (Service)
                serviceSignUpModel.Add(new Models.SignupData { InputType = "Service" });


                StaticListData.Add(serviceSignUpModel);
            }

            UserIcon = "defaultIcon.png";

            User64String = "NA";

            // Intialize commands
            SaveCommand = new Command(() => SaveClicked());
            PictureCommand = new Command(() => SelectImage());
            AddCommand = new Command(AddRow);
            CalendarCommand = new Command(CalenderSelction);
            IsServiceInProgress = false;


            Initialization = InitializeAsync();

        }

        private void CalenderSelction(object obj)
        {
            var item = obj as Models.SignupData;

            SelectedCalenderIndex = StaticListData[1].IndexOf(item);

            MessagingCenter.Send<SignupViewModel>(this, "SignUpCalenderPage");
        }

        private void AddRow(object obj)
        {
            var item = obj as Models.SignupData;

            int index = StaticListData[1].IndexOf(item);

            if(item.PlaceholderText == "Awards")
            {
                StaticListData[1].Insert(index + 1, new Models.SignupData { InputType = "Entry", IsAdditionAvailable = false, PlaceholderText = "Enter Awards"});
            }
            else if (item.PlaceholderText == "Certification")
            {
                StaticListData[1].Insert(index + 1, new Models.SignupData { InputType = "Entry", PlaceholderText = "Enter Certification", IsAdditionAvailable = false });
            }
            else if (item.PlaceholderText == "Service")
            {
                StaticListData[1].Insert(index + 1, new Models.SignupData { InputType = "Service" });
            }


        }

        private async void SaveClicked()
        {
            IsServiceInProgress = true;

            Models.TraineeSignupModel signupModel = new Models.TraineeSignupModel();

            signupModel.email = EmailAddress;
            signupModel.password = Password;
            signupModel.profile = SelectedView;
            signupModel.signUpMetod = (Password == "fb@trainme") ? "FB" : "App";
            signupModel.imagePayload = User64String;


            Models.TraineeSignupModel.BasicInfo basicInfo = new Models.TraineeSignupModel.BasicInfo();
            basicInfo.address = position.Accuracy + "";
            basicInfo.country = position.Altitude + "";
            basicInfo.id = 1;
            basicInfo.imageUrl = "NA";
            basicInfo.instaGramId = "NA";
            basicInfo.latitude = position.Latitude + "";
            basicInfo.longitude = position.Longitude + "";
            basicInfo.state = "NA";
            basicInfo.valueAdded = "NA";

            if (StaticListData[0][1].MainSelectedData == null || StaticListData[0][7].MainSelectedData == null || StaticListData[0][9].MainSelectedData == null || StaticListData[0][11].MainSelectedData == null)
            {
                MessagingCenter.Send<SignupViewModel, String>(this, "SignUpFailure", "Please enter all manadatory fields.");
                IsServiceInProgress = false;
                return;
            }


            basicInfo.name = StaticListData[0][1].MainSelectedData;
            basicInfo.gender = StaticListData[0][3].SelectionData[StaticListData[0][3].SelectedIndex];
            basicInfo.dateOfBirth = StaticListData[0][5].SelectedDate.ToString();
            basicInfo.mobileNumber = StaticListData[0][7].MainSelectedData;
            basicInfo.phoneNumber = StaticListData[0][7].MainSelectedData;
            basicInfo.height = StaticListData[0][9].MainSelectedData;
            basicInfo.weight = StaticListData[0][11].MainSelectedData;
            basicInfo.sportsInterest = StaticListData[0][13].SelectionData[StaticListData[0][13].SelectedIndex];
            basicInfo.anyMedicalCondition = StaticListData[0][15].MainSelectedData;


            Models.TraineeSignupModel.ProfessionalInfo professionalInfo = new Models.TraineeSignupModel.ProfessionalInfo();


            if (App.SelectedView == "Trainer")
            {
                // Add certificates
                List<Models.TraineeSignupModel.Certifications> certifications = new List<Models.TraineeSignupModel.Certifications>();

                // Add Services
                List<Models.TraineeSignupModel.Services> services = new List<Models.TraineeSignupModel.Services>();


                var enteredProfessionalInfo = StaticListData[1];
                foreach (var item in enteredProfessionalInfo)
                {
                    if (item.PlaceholderText == "Select Specialty" && item.InputType == "Picker")
                    {
                        professionalInfo.speciality = item.SelectionData[item.SelectedIndex];
                    }
                    else if (item.PlaceholderText == "Enter Experience" && item.InputType == "Entry")
                    {
                        professionalInfo.experience = item.MainSelectedData;
                    }
                    else if (item.PlaceholderText == "Enter Awards")
                    {
                        professionalInfo.accolades = item.MainSelectedData;
                    }
                    else if (item.PlaceholderText == "Enter Certification")
                    {
                        Models.TraineeSignupModel.Certifications certification = new Models.TraineeSignupModel.Certifications();
                        certification.certification = item.MainSelectedData;

                        certifications.Add(certification);
                    }
                    else if (item.InputType == "Service")
                    {
                        Models.TraineeSignupModel.Services service = new Models.TraineeSignupModel.Services();
                        service.chargingPeriod = item.SessionDesc;
                        service.serviceName = item.MainSelectedData;
                        service.charges = Convert.ToDouble(item.SessionAmount);
                        service.workLocaton = item.SessionLocation;
                        service.teamSize = item.SessionTeam;

                        if(item.StartTime == null)
                        {
                            MessagingCenter.Send<SignupViewModel, String>(this, "SignUpFailure", "Please Select Time.");
                            IsServiceInProgress = false;
                            return;
                        }

                        List<Models.TraineeSignupModel.Schedule> schedules = new List<Models.TraineeSignupModel.Schedule>();
                        Models.TraineeSignupModel.Schedule schedule = new Models.TraineeSignupModel.Schedule();

                        schedule.day = item.Day;
                        schedule.month = item.Month;
                        schedule.year = item.Year;
                        schedule.scheduleType = "Week";
                        schedule.startTime = item.StartTime;
                        schedule.endTime = item.EndTime;
                        schedule.weekDay = item.WeekDay;

                        schedules.Add(schedule);

                        service.schedule = schedules;


                        services.Add(service);
                    }


                }

                professionalInfo.certifications = certifications;

                professionalInfo.services = services;

                if (professionalInfo.accolades == null || professionalInfo.experience == null || professionalInfo.speciality == null || professionalInfo.certifications.Count < 1 || professionalInfo.services.Count < 1)
                {
                    MessagingCenter.Send<SignupViewModel, String>(this, "SignUpFailure", "Please enter all manadatory fields.");
                    IsServiceInProgress = false;
                    return;
                }
            }

            // Creating the final object
            signupModel.basicInfo = basicInfo;
            signupModel.professionalInfo = professionalInfo;

            var message = await App.TodoManager.SignUp(signupModel);

            if (message == "Success")
            {
                Application.Current.MainPage = new RootPage(App.SelectedView);
                //MessagingCenter.Send<SignupViewModel>(this, "SignUpSuccess");
            }
            else
            {
                MessagingCenter.Send<SignupViewModel, String>(this, "SignUpFailure", "Internal Issue. Please Try Again.");
            }

            IsServiceInProgress = false;

        }

        private async Task InitializeAsync()
        {

            try
            {
                CheckUpdateProfile();

                var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Location);
                if (status != PermissionStatus.Granted)
                {
                    var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Location);
                    //Best practice to always check that the key exists
                    if (results.ContainsKey(Permission.Location))
                        status = results[Permission.Location];
                }

                if (status == PermissionStatus.Granted)
                {
                    GetCurrentPosition();

                }
                else if (status != PermissionStatus.Unknown)
                {

                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Unable to get location: " + ex);

            }


        }

        private void CheckUpdateProfile()
        {
            if (App.LoginResponse == null || App.LoginResponse.Email == "" || App.LoginResponse.Email == null)
            {
                // Do nothing if no data is available
            }
            else
            {
                StaticListData = new ObservableCollection<Models.CustomSignupModel>();

                Models.CustomSignupModel basicSignUpModel = new Models.CustomSignupModel { HeaderName = "Basic Information" };

                basicSignUpModel.Add(new Models.SignupData { InputType = "Label", PlaceholderText = "Name", IsAdditionAvailable = false });
                // Index Number 1 (Name)
                basicSignUpModel.Add(new Models.SignupData { MainSelectedData= App.LoginResponse.basicInfo.Name, InputType = "Entry", PlaceholderText = "Enter Name", IsAdditionAvailable = false });

                basicSignUpModel.Add(new Models.SignupData { InputType = "Label", PlaceholderText = "Gender", IsAdditionAvailable = false });
                // Index Number 3 (Gender)
                basicSignUpModel.Add(new Models.SignupData { InputType = "Picker", PlaceholderText = "Select Gender", IsAdditionAvailable = false, SelectionData = GenderList, SelectedIndex = GenderList.IndexOf(App.LoginResponse.basicInfo.Gender) });

                basicSignUpModel.Add(new Models.SignupData { InputType = "Label", PlaceholderText = "Date of Birth", IsAdditionAvailable = false });
                // Index Number 5 (DOB)
                basicSignUpModel.Add(new Models.SignupData {  InputType = "Picker", PlaceholderText = "Date of Birth", SelectedDate = DateTime.Parse(App.LoginResponse.basicInfo.DateOfBirth), IsAdditionAvailable = true });

                basicSignUpModel.Add(new Models.SignupData { InputType = "Label", PlaceholderText = "Phone Number", IsAdditionAvailable = false });
                // Index Number 7 (Phone)
                basicSignUpModel.Add(new Models.SignupData { MainSelectedData= App.LoginResponse.basicInfo.PhoneNumber, InputType = "Entry", IsAdditionAvailable = true, PlaceholderText = "Enter Phone Number" });

                basicSignUpModel.Add(new Models.SignupData { InputType = "Label", PlaceholderText = "Height (in cm)", IsAdditionAvailable = false });
                // Index Number 9 (Height)
                basicSignUpModel.Add(new Models.SignupData { MainSelectedData= App.LoginResponse.basicInfo.Height, InputType = "Entry", IsAdditionAvailable = true, PlaceholderText = "Enter Height" });

                basicSignUpModel.Add(new Models.SignupData { InputType = "Label", PlaceholderText = "Weight (in kg)", IsAdditionAvailable = false });
                // Index Number 11 (Weight)
                basicSignUpModel.Add(new Models.SignupData { MainSelectedData= App.LoginResponse.basicInfo.Weight, InputType = "Entry", IsAdditionAvailable = true, PlaceholderText = "Enter Weight" });

                basicSignUpModel.Add(new Models.SignupData { InputType = "Label", PlaceholderText = "Sports Interest", IsAdditionAvailable = false });
                // Index Number 13 (Sports Interest)
                basicSignUpModel.Add(new Models.SignupData { InputType = "Picker", PlaceholderText = "Select Sport", IsAdditionAvailable = false, SelectionData = SportsList , SelectedIndex = SportsList.IndexOf(App.LoginResponse.basicInfo.SportsInterest) });

                basicSignUpModel.Add(new Models.SignupData { InputType = "Label", PlaceholderText = "Medical Information", IsAdditionAvailable = false });
                // Index Number 15 (Medical Info)
                basicSignUpModel.Add(new Models.SignupData { MainSelectedData= App.LoginResponse.basicInfo.AnyMedicalCondition, InputType = "Editor", PlaceholderText = "Enter Medical Information" });


                StaticListData.Add(basicSignUpModel);

                if (App.SelectedView == "Trainer")
                {

                    Models.CustomSignupModel serviceSignUpModel = new Models.CustomSignupModel { HeaderName = "Services" };

                    serviceSignUpModel.Add(new Models.SignupData { InputType = "Label", PlaceholderText = "Specialty", IsAdditionAvailable = false });
                    // Index Number 1 (Specialty)
                    serviceSignUpModel.Add(new Models.SignupData { InputType = "Picker", PlaceholderText = "Select Specialty", IsAdditionAvailable = false, SelectionData = SpecialityList, SelectedIndex = SpecialityList.IndexOf(App.LoginResponse.professionalInfo.Speciality) });

                    serviceSignUpModel.Add(new Models.SignupData { InputType = "Label", PlaceholderText = "Experience", IsAdditionAvailable = false });
                    // Index Number 1 (Experiance)
                    serviceSignUpModel.Add(new Models.SignupData { MainSelectedData= App.LoginResponse.professionalInfo.Experience, InputType = "Entry", PlaceholderText = "Enter Experience", IsAdditionAvailable = true });

                    serviceSignUpModel.Add(new Models.SignupData { InputType = "Label", PlaceholderText = "Awards", IsAdditionAvailable = true });
                    // Index Number 1 (Awards)
                    serviceSignUpModel.Add(new Models.SignupData { MainSelectedData= App.LoginResponse.professionalInfo.Accolades, InputType = "Entry", PlaceholderText = "Enter Awards", IsAdditionAvailable = false });

                    serviceSignUpModel.Add(new Models.SignupData { InputType = "Label", PlaceholderText = "Certification", IsAdditionAvailable = true });
                    // Index Number 1 (Certification)

                    foreach(var certItem in App.LoginResponse.professionalInfo.certifications)
                    {
                        serviceSignUpModel.Add(new Models.SignupData { MainSelectedData=certItem.Certification, InputType = "Entry", PlaceholderText = "Enter Certification", IsAdditionAvailable = false });
                    }



                    serviceSignUpModel.Add(new Models.SignupData { InputType = "Label", PlaceholderText = "Service", IsAdditionAvailable = true });
                    // Index Number 1 (Service)
                    foreach(var serviceItem in App.LoginResponse.professionalInfo.services)
                    {
                        serviceSignUpModel.Add(new Models.SignupData { MainSelectedData = serviceItem.ServiceName, SessionDesc = serviceItem.ChargingPeriod, SessionAmount = serviceItem.Charges, InputType = "Service" });
                    }



                    StaticListData.Add(serviceSignUpModel);
                }




                EmailAddress = App.LoginResponse.Email;
                Password = App.LoginResponse.Password;
                User64String = "NA";
                SelectedView = App.SelectedView;

            }
            
        }


        private async void GetCurrentPosition()
        {

            try
            {
                var locator = CrossGeolocator.Current;
                locator.DesiredAccuracy = 100;


                position = await locator.GetPositionAsync(TimeSpan.FromSeconds(20), null, true);
                if (position == null)
                {
                    position = await locator.GetLastKnownLocationAsync();

                }


            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Unable to get location: " + ex);
            }

        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        protected void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
