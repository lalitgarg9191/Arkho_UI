﻿using System;
using System.Collections.Generic;

using Xamarin.Forms;
//using XLabs.Forms.Controls;

namespace DFS.Views
{
    public partial class Calender : ContentPage
    {

        ///// <summary>
        ///// The _calendar view
        ///// </summary>
        //readonly CalendarView _calendarView;
        ///// <summary>
        ///// The _relative layout
        ///// </summary>
        //readonly RelativeLayout _relativeLayout;
        ///// <summary>
        ///// The _stacker
        ///// </summary>
        //readonly StackLayout _stacker;


        public Calender()
        {

            InitializeComponent();
            /*
            calenderView.MinDate = CalendarView.FirstDayOfMonth(DateTime.Now);
            calenderView.MaxDate = CalendarView.LastDayOfMonth(DateTime.Now.AddMonths(3));
            calenderView.HighlightedDateBackgroundColor = Color.FromRgb(227, 227, 227);
            calenderView.ShouldHighlightDaysOfWeekLabels = false;
            calenderView.SelectionBackgroundStyle = CalendarView.BackgroundStyle.CircleFill;
            calenderView.TodayBackgroundStyle = CalendarView.BackgroundStyle.CircleOutline;
            calenderView.HighlightedDaysOfWeek = new DayOfWeek[] { DayOfWeek.Saturday, DayOfWeek.Sunday };
            calenderView.ShowNavigationArrows = true;
            calenderView.MonthTitleFont = Font.OfSize("Open 24 Display St", NamedSize.Medium);


            _relativeLayout = new RelativeLayout()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand
            };
            Content = _relativeLayout;



            _calendarView = new CalendarView()
            {
                //BackgroundColor = Color.Blue
                MinDate = CalendarView.FirstDayOfMonth(DateTime.Now),
                MaxDate = CalendarView.LastDayOfMonth(DateTime.Now.AddMonths(3)),
                HighlightedDateBackgroundColor = Color.FromRgb(227, 227, 227),
                ShouldHighlightDaysOfWeekLabels = false,
                SelectionBackgroundStyle = CalendarView.BackgroundStyle.CircleFill,
                TodayBackgroundStyle = CalendarView.BackgroundStyle.CircleOutline,
                HighlightedDaysOfWeek = new DayOfWeek[] { DayOfWeek.Saturday, DayOfWeek.Sunday },
                ShowNavigationArrows = true,
                MonthTitleFont = Font.OfSize("Open 24 Display St", NamedSize.Medium)

            };

            _relativeLayout.Children.Add(_calendarView,
                Constraint.Constant(0),
                Constraint.Constant(0),
                Constraint.RelativeToParent(p => p.Width),
                Constraint.RelativeToParent(p => p.Height * 2 / 3));

            _stacker = new StackLayout()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.StartAndExpand
            };
            _relativeLayout.Children.Add(_stacker,
                Constraint.Constant(0),
                Constraint.RelativeToParent(p => p.Height * 2 / 3),
                Constraint.RelativeToParent(p => p.Width),
                Constraint.RelativeToParent(p => p.Height * 1 / 3)
            );
            _calendarView.DateSelected += (object sender, DateTime e) =>
            {
                _stacker.Children.Add(new Label()
                {
                    Text = "Date Was Selected" + e.ToString("d"),
                    VerticalOptions = LayoutOptions.Start,
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                });
            };
*/

        }
    }
}
