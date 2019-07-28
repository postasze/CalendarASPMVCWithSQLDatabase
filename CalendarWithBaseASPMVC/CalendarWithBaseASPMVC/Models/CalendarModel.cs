using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Globalization;
using System.Collections;

namespace CalendarWithBaseASPMVC.Models
{
    public class Calendar
    {
        private static Calendar instance;
        public static bool firstTimeDisplay;
        public GregorianCalendar gregorianCalendar;
        public DateTime dateTime;
        public DateTime currentDateTime;
        public ArrayList dayEventsList;
        public int chosenDayNumber;
        public StorageContext databaseContext;
        public static Person currentPerson;

        private String[] weekLabelsArray = new String[4];
        public Array WeekLabelsArray { get { return weekLabelsArray; } }

        private String[] dayLabelsArray = new String[28];
        public Array DayLabelsArray { get { return dayLabelsArray; } }

        public ArrayList[] dayEventLabelsLists = new ArrayList[28];

        public String[] Months = new String[] 
        { "January", 
          "February", 
          "March", 
          "April", 
          "May", 
          "June", 
          "July", 
          "August", 
          "September", 
          "October", 
          "November", 
          "December" 
        };

        public Calendar()
        {
            //Console.WriteLine("Calendar constructor");
            gregorianCalendar = new GregorianCalendar();
            currentDateTime = DateTime.Now;
            currentDateTime = currentDateTime.AddHours(-currentDateTime.Hour);
            currentDateTime = currentDateTime.AddMinutes(-currentDateTime.Minute);
            currentDateTime = currentDateTime.AddSeconds(-currentDateTime.Second);
            currentDateTime = currentDateTime.AddMilliseconds(-currentDateTime.Millisecond);
            dateTime = currentDateTime;
            this.dayEventsList = new ArrayList();
            for (int i = 0; i < 28; i++)
                dayEventLabelsLists[i] = new ArrayList();
            firstTimeDisplay = true;
            this.databaseContext = new StorageContext();
        }

        public static Calendar getInstance()
        {
            if (instance == null)
                instance = new Calendar();
            return instance;
        }

        public void NormalizeWeek()
        {
            switch (dateTime.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    break;
                case DayOfWeek.Tuesday:
                    dateTime = dateTime.AddDays(-1);
                    break;
                case DayOfWeek.Wednesday:
                    dateTime = dateTime.AddDays(-2);
                    break;
                case DayOfWeek.Thursday:
                    dateTime = dateTime.AddDays(-3);
                    break;
                case DayOfWeek.Friday:
                    dateTime = dateTime.AddDays(-4);
                    break;
                case DayOfWeek.Saturday:
                    dateTime = dateTime.AddDays(-5);
                    break;
                case DayOfWeek.Sunday:
                    dateTime = dateTime.AddDays(-6);
                    break;
            }
        }
    }

    public enum Gender
    {
        Male,
        Female
    }

    public class DayEvent
    {
        private String title;
        private String description;
        private DateTime startTime;
        private DateTime endTime;

        public DayEvent(String title, String description, DateTime startTime, DateTime endTime)
        {
            this.title = title;
            this.description = description;
            this.startTime = startTime;
            this.endTime = endTime;
        }

        public DayEvent()
        {
        }

        public void SetTitle(String title)
        {
            this.title = title;
        }

        public String GetTitle()
        {
            return this.title;
        }

        public void SetDescription(String description)
        {
            this.description = description;
        }

        public String GetDescription()
        {
            return this.description;
        }

        public DateTime GetStartTime()
        {
            return this.startTime;
        }

        public void SetStartTime(DateTime startTime)
        {
            this.startTime = startTime;
        }

        public DateTime GetEndTime()
        {
            return this.endTime;
        }

        public void SetEndTime(DateTime endTime)
        {
            this.endTime = endTime;
        }
    }

    public class DayEventsComparer : IComparer
    {
        int IComparer.Compare(Object x, Object y)
        {
            DayEvent DayEvent1 = (DayEvent)x;
            DayEvent DayEvent2 = (DayEvent)y;

            return DateTime.Compare(DayEvent1.GetStartTime(), DayEvent2.GetStartTime());
        }
    }

    public class DayEventForm
    {
        private static DayEventForm instance;

        [Required]
        [DisplayName("Event title")]
        public string Title { get; set; }

        [Required]
        [DisplayName("Event description")]
        public string Description { get; set; }

        [Required]
        [DisplayName("Start hour")]
        public int StartHour { get; set; }

        [Required]
        [DisplayName("End hour")]
        public int EndHour { get; set; }

        [Required]
        [DisplayName("Start minute")]
        public int StartMinute { get; set; }

        [Required]
        [DisplayName("End minute")]
        public int EndMinute { get; set; }

        public int chosenDayNumber { get; set; }
        public int chosenEventNumber { get; set; }

        public DayEventForm()
        { 
        }

        public static DayEventForm getInstance()
        {
            if (instance == null)
                instance = new DayEventForm();
            return instance;
        }
    }

    public class LoginForm
    {
        [DisplayName("First name")]
        public string FirstName { get; set; }

        [DisplayName("Last name")]
        public string LastName { get; set; }

        public int ChosenPersonIndex { get; set; }

        public Person[] PeopleArray;
        public LoginForm()
        {
            PeopleArray = Models.Calendar.getInstance().databaseContext.People.ToArray();
        }
    }
}