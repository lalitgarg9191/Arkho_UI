using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DFS.Models
{
    public class CustomSignupModel : ObservableCollection<SignupData>
    {
        public String HeaderName { get; set; }

    }

    public class SignupData
    {
        public String InputType { get; set; }
        public bool IsAdditionAvailable { get; set; }
        public String PlaceholderText { get; set; }
        public ObservableCollection<String> SelectionData { get; set; }
        public int SelectedIndex { get; set; }
        public DateTime SelectedDate { get; set; }

        public String MainSelectedData { get; set; }
        public String SessionAmount { get; set; }
        public String SessionDesc { get; set; }
        public String SessionLocation { get; set; }
        public String SessionTeam { get; set; }


    }
}
