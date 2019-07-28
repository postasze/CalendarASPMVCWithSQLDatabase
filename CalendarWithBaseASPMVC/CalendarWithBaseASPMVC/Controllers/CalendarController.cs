using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CalendarWithBaseASPMVC.Models;
using System.Data.SqlClient;
using System.Diagnostics;

namespace CalendarWithBaseASPMVC.Controllers
{
    public class CalendarController : Controller
    {
        // **************************************
        // GET: /Calendar/
        // URL: /Calendar/Calendar.aspx
        // **************************************

        public ActionResult Login(LoginForm loginForm)
        {
            return View("Login", loginForm);
        }

        [HttpPost]
        public ActionResult Login(LoginForm loginForm, String parameter)
        {
            Random randomValue = new Random();
            if (this.HttpContext.Request.Form["submitButton"] == "Choose person")
            {
                Models.Calendar.currentPerson =
                    Models.Calendar.getInstance().databaseContext.People.AsEnumerable().ElementAt(loginForm.ChosenPersonIndex);

               return RedirectToAction("Calendar", "Calendar");
            }
            else if (this.HttpContext.Request.Form["submitButton"] == "New person")
            {
                Person newPerson = new Person()
                {
                    PersonID = Guid.NewGuid(),
                    FirstName = loginForm.FirstName,
                    LastName = loginForm.LastName,
                    UserID = (loginForm.FirstName + loginForm.LastName).Substring(0, 10) //Guid.NewGuid().ToString().Substring(0, 10)
                };

                if (!Models.Calendar.getInstance().databaseContext.People.Any<Person>(
                    person => person.FirstName == newPerson.FirstName && person.LastName == newPerson.LastName))
                {
                    //while (Models.Calendar.getInstance().databaseContext.People.Any<Person>(person => person.PersonID == newPerson.PersonID))
                    //    newPerson.PersonID = new Guid(randomValue.Next(0, 10000).ToString());

                    Models.Calendar.getInstance().databaseContext.People.Add(newPerson);
                    Models.Calendar.getInstance().databaseContext.SaveChanges();

                    Models.Calendar.currentPerson = newPerson;

                    return RedirectToAction("Calendar", "Calendar");
                }
                else
                    return View("Login", loginForm);
            }
            else
                return View("Login", loginForm);
        }

        public ActionResult Calendar()
        {
            //Calendar calendar = new Calendar();
            Models.Calendar.getInstance();
            ViewData["Message"] = Models.Calendar.currentPerson.FirstName;
            ViewData["Message1"] = Models.Calendar.currentPerson.LastName;
            //ViewData["dateinfo"] = DateTime.Now.ToShortDateString();
            if (Models.Calendar.firstTimeDisplay)
            {
                Models.Calendar.firstTimeDisplay = false;
                Models.Calendar.getInstance().NormalizeWeek();

                /*if (!Models.Calendar.getInstance().databaseContext.DatabaseExists())
                {
                    CreateDatabase();
                    //this.databaseContext.CreateDatabase();
                }*/
                //DiskInputOutput.DiskManager.getInstance().ReadEventsFromFile();
            }

            //Models.Calendar.getInstance().dayEventsList.Clear();
            ClearDayEventsList();
            ReadPersonEventsFromDatabaseToList();
            DisplayWeekLabels();
            DisplayDayLabels();
            MarkCurrentDay();

            WriteDayEventsLabelsFromDayEventListToLabelsList();

            return View("Calendar", Models.Calendar.getInstance());
        }

        [HttpPost]
        public ViewResult Calendar(String parameter)
        {
            //this.HttpContext.Request.Form[""];
            //ViewData["dateinfo"] = DateTime.Now.ToShortDateString();

            ViewData["Message"] = Models.Calendar.currentPerson.FirstName;
            ViewData["Message1"] = Models.Calendar.currentPerson.LastName;
            //ViewData["Message"] = Models.Calendar.getInstance().dateTime;
            //ViewData["Message1"] = Models.Calendar.getInstance().currentDateTime;

            if (this.HttpContext.Request.Form["changeWeekButton"] == "prev")
            {
                //UnmarkCurrentDay();
                Models.Calendar.getInstance().dateTime = Models.Calendar.getInstance().dateTime.AddDays(-7);
                //MarkCurrentDay();
                //UpdateDayLabelBackgrounds();
            }
            else if (this.HttpContext.Request.Form["changeWeekButton"] == "next")
            {
                //UnmarkCurrentDay();
                Models.Calendar.getInstance().dateTime = Models.Calendar.getInstance().dateTime.AddDays(7);
                //MarkCurrentDay();
                //UpdateDayLabelBackgrounds();
            }
            DisplayWeekLabels();
            DisplayDayLabels();
            MarkCurrentDay();
            WriteDayEventsLabelsFromDayEventListToLabelsList();
            //ViewData["Message1"] = Models.Calendar.firstTimeDisplay;
            return View(Models.Calendar.getInstance());
        }

        // **************************************
        // URL: /Calendar/AddEvent.aspx
        // **************************************

        public ActionResult AddEvent(DayEventForm dayEventForm)
        {
            return View("AddEvent");
        }

        [HttpPost]
        public ActionResult AddEvent(DayEventForm dayEventForm, String parameter)
        {
            DateTime startTime = new DateTime(Models.Calendar.getInstance().dateTime.Year, Models.Calendar.getInstance().dateTime.Month, Models.Calendar.getInstance().dateTime.Day, dayEventForm.StartHour, dayEventForm.StartMinute, 1);
            DateTime endTime = new DateTime(Models.Calendar.getInstance().dateTime.Year, Models.Calendar.getInstance().dateTime.Month, Models.Calendar.getInstance().dateTime.Day, dayEventForm.EndHour, dayEventForm.EndMinute, 1);
            startTime = startTime.AddDays(dayEventForm.chosenDayNumber);
            endTime = endTime.AddDays(dayEventForm.chosenDayNumber);

            if (dayEventForm.StartHour > dayEventForm.EndHour)
                return View("AddEvent"); // zdarzenie ma wczesniejszy koniec niz rozpoczecie

            if ((dayEventForm.StartHour == dayEventForm.EndHour) && (dayEventForm.StartMinute > dayEventForm.EndMinute))
                return View("AddEvent"); // zdarzenie ma wczesniejszy koniec niz rozpoczecie

            DayEvent dayEvent = new DayEvent(dayEventForm.Title, dayEventForm.Description, startTime, endTime);

            if (Models.Calendar.getInstance().dayEventsList.BinarySearch(dayEvent, new DayEventsComparer()) >= 0)
                return View("AddEvent");

            Models.Calendar.getInstance().dayEventsList.Add(dayEvent);
            Models.Calendar.getInstance().dayEventsList.Sort(new DayEventsComparer());

            Appointment newAppointment = new Appointment()
            {
                AppointmentID = Guid.NewGuid(),
                Title = dayEventForm.Title,
                Description = dayEventForm.Description,
                AppointmentDate = new DateTime(startTime.Year, startTime.Month, startTime.Day),
                StartTime = new TimeSpan(dayEventForm.StartHour, dayEventForm.StartMinute, 1),
                EndTime = new TimeSpan(dayEventForm.EndHour, dayEventForm.EndMinute, 1)
            };

            Attendance newAttendance = new Attendance()
            {
                AppointmentID = newAppointment.AppointmentID,
                PersonID = Models.Calendar.currentPerson.PersonID,
                Accepted = true,
                AttendanceID = Guid.NewGuid(),
            };

            Models.Calendar.getInstance().databaseContext.Appointments.Add(newAppointment);
            Models.Calendar.getInstance().databaseContext.Attendances.Add(newAttendance);
            Models.Calendar.getInstance().databaseContext.SaveChanges();

            return RedirectToAction("Calendar", "Calendar");  //return View("Calendar", Models.Calendar.getInstance());
        }

        // **************************************
        // URL: /Calendar/EditEvent.aspx
        // **************************************

        public ActionResult EditEvent(DayEventForm dayEventForm)
        {
            //this.HttpContext.Request.Form["nextWeekButton"]
            //ViewData["message"] = dayEventForm.chosenEventNumber;
            //int chosenDayNumber = (int) RouteData.Values["id"];
            //ViewData["message"] = chosenDayNumber;
            char[] charSeparators = new char[] { ':', '-' };
            string[] separatedStrings;

            String chosenDayEventLabelString = (String)Models.Calendar.getInstance().dayEventLabelsLists[dayEventForm.chosenDayNumber][dayEventForm.chosenEventNumber];
            separatedStrings = chosenDayEventLabelString.Split(charSeparators, StringSplitOptions.None);

            Models.DayEventForm.getInstance().StartHour = Int32.Parse(separatedStrings[0]);
            Models.DayEventForm.getInstance().StartMinute = Int32.Parse(separatedStrings[1]);
            Models.DayEventForm.getInstance().EndHour = Int32.Parse(separatedStrings[2]);
            Models.DayEventForm.getInstance().EndMinute = Int32.Parse(separatedStrings[3]);
            Models.DayEventForm.getInstance().Title = separatedStrings[4];
            Models.DayEventForm.getInstance().Description = separatedStrings[5];
            return View("EditEvent", Models.DayEventForm.getInstance());
        }

        [HttpPost]
        public ActionResult EditEvent(DayEventForm dayEventForm, String parameter)
        {
            DateTime eventDate = new DateTime(Models.Calendar.getInstance().dateTime.Year, Models.Calendar.getInstance().dateTime.Month, Models.Calendar.getInstance().dateTime.Day);
            eventDate = eventDate.AddDays(dayEventForm.chosenDayNumber);
            DateTime startTime = new DateTime(eventDate.Year, eventDate.Month, eventDate.Day, Models.DayEventForm.getInstance().StartHour, Models.DayEventForm.getInstance().StartMinute, 1);
            DateTime endTime = new DateTime(eventDate.Year, eventDate.Month, eventDate.Day, Models.DayEventForm.getInstance().EndHour, Models.DayEventForm.getInstance().EndMinute, 1);
            TimeSpan previousStartTimeSpan = new TimeSpan(startTime.Hour, startTime.Minute, 1);
            TimeSpan previousEndTimeSpan = new TimeSpan(endTime.Hour, endTime.Minute, 1);
            TimeSpan newStartTimeSpan = new TimeSpan(dayEventForm.StartHour, dayEventForm.StartMinute, 1);
            TimeSpan newEndTimeSpan = new TimeSpan(dayEventForm.EndHour, dayEventForm.EndMinute, 1);
            String previousEventTitle = Models.DayEventForm.getInstance().Title;
            String previousEventDescription = Models.DayEventForm.getInstance().Description;
            String newEventDescription = dayEventForm.Description;
            String newEventTitle = dayEventForm.Title;

            if (dayEventForm.StartHour > dayEventForm.EndHour)
                return View("EditEvent"); // zdarzenie ma wczesniejszy koniec niz rozpoczecie

            if ((dayEventForm.StartHour == dayEventForm.EndHour) && (dayEventForm.StartMinute > dayEventForm.EndMinute))
                return View("EditEvent"); // zdarzenie ma wczesniejszy koniec niz rozpoczecie

            DayEvent chosenDayEvent = new DayEvent(previousEventTitle, previousEventDescription, startTime, endTime);

            if (this.HttpContext.Request.Form["submitButton"] == "Edit event")
            {
                int index = Models.Calendar.getInstance().dayEventsList.BinarySearch(chosenDayEvent, new DayEventsComparer());
                ((DayEvent)Models.Calendar.getInstance().dayEventsList[index]).SetTitle(newEventTitle);
                ((DayEvent)Models.Calendar.getInstance().dayEventsList[index]).SetDescription(newEventDescription);
                ((DayEvent)Models.Calendar.getInstance().dayEventsList[index]).SetStartTime(new DateTime(eventDate.Year, eventDate.Month, eventDate.Day, dayEventForm.StartHour, dayEventForm.StartMinute, 1));
                ((DayEvent)Models.Calendar.getInstance().dayEventsList[index]).SetEndTime(new DateTime(eventDate.Year, eventDate.Month, eventDate.Day, dayEventForm.EndHour, dayEventForm.EndMinute, 1));

                Appointment searchedAppointment;
                Guid appointmentID;

                for (int i = 0; i < Models.Calendar.getInstance().databaseContext.Attendances.Count(); i++)
                {
                    if (Models.Calendar.getInstance().databaseContext.Attendances.AsEnumerable().ElementAt(i).PersonID == Models.Calendar.currentPerson.PersonID)
                    {
                        appointmentID = Models.Calendar.getInstance().databaseContext.Attendances.AsEnumerable().ElementAt(i).AppointmentID;
                        searchedAppointment = Models.Calendar.getInstance().databaseContext.Appointments.FirstOrDefault<Appointment>(
                            appointment => appointment.AppointmentID == appointmentID && appointment.Title == previousEventTitle &&
                            appointment.Description == previousEventDescription && appointment.AppointmentDate == eventDate &&
                            appointment.StartTime.Hours == previousStartTimeSpan.Hours && appointment.StartTime.Minutes == previousStartTimeSpan.Minutes &&
                            appointment.EndTime.Hours == previousEndTimeSpan.Hours && appointment.EndTime.Minutes == previousEndTimeSpan.Minutes);

                        if (searchedAppointment != null)
                        {
                            searchedAppointment.Title = newEventTitle;
                            searchedAppointment.Description = newEventDescription;
                            searchedAppointment.StartTime = newStartTimeSpan;
                            searchedAppointment.EndTime = newEndTimeSpan;
                        }
                    }
                }
                Models.Calendar.getInstance().databaseContext.SaveChanges();
            }
            else if (this.HttpContext.Request.Form["submitButton"] == "Remove event")
            {
                //for(int i = 0; i < )

                int index = Models.Calendar.getInstance().dayEventsList.BinarySearch(chosenDayEvent, new DayEventsComparer());
                Models.Calendar.getInstance().dayEventsList.RemoveAt(index);

                Appointment appointmentToRemove = Models.Calendar.getInstance().databaseContext.Appointments.First<Appointment>(
                        appointment => appointment.Title == previousEventTitle &&
                        appointment.Description == previousEventDescription &&
                        appointment.AppointmentDate == eventDate &&
                        appointment.StartTime.Hours == previousStartTimeSpan.Hours &&
                        appointment.StartTime.Minutes == previousStartTimeSpan.Minutes &&
                        appointment.EndTime.Hours == previousEndTimeSpan.Hours &&
                        appointment.EndTime.Minutes == previousEndTimeSpan.Minutes);

                Attendance attendanceToRemove = Models.Calendar.getInstance().databaseContext.Attendances.First<Attendance>(
                        attendance => attendance.AppointmentID == appointmentToRemove.AppointmentID);

                Models.Calendar.getInstance().databaseContext.Attendances.Remove(attendanceToRemove);
                Models.Calendar.getInstance().databaseContext.Appointments.Remove(appointmentToRemove);
                Models.Calendar.getInstance().databaseContext.SaveChanges();
            }
            return RedirectToAction("Calendar", "Calendar");//return View("EditEvent", dayEventForm); //return View("Calendar", Models.Calendar.getInstance());
        }

        // **************************************
        // Funkcje pomocnicze
        // **************************************

        public void DisplayWeekLabels()
        {
            for (int i = 0; i < 4; i++)
            {
                Models.Calendar.getInstance().WeekLabelsArray.SetValue("W" +
                    ((Models.Calendar.getInstance().dateTime.DayOfYear / 7) + 1).ToString() +
                    "\n" + Models.Calendar.getInstance().dateTime.Year.ToString(), i);
                Models.Calendar.getInstance().dateTime = Models.Calendar.getInstance().dateTime.AddDays(7);
            }
            Models.Calendar.getInstance().dateTime = Models.Calendar.getInstance().dateTime.AddDays(-28);

            //PropertyChanged.Invoke(this, WeekLabelsArrayChangedEventArgs);
        }

        public void DisplayDayLabels()
        {
            for (int i = 0; i < 28; i++)
            {
                Models.Calendar.getInstance().DayLabelsArray.SetValue(Models.Calendar.getInstance().Months[Models.Calendar.getInstance().dateTime.Month - 1]
                     + " " + Models.Calendar.getInstance().dateTime.Day.ToString(), i);
                Models.Calendar.getInstance().dateTime = Models.Calendar.getInstance().dateTime.AddDays(1);
            }
            Models.Calendar.getInstance().dateTime = Models.Calendar.getInstance().dateTime.AddDays(-28);
        }

        public void MarkCurrentDay()
        {
            TimeSpan differenceTimeSpan = Models.Calendar.getInstance().currentDateTime -
                Models.Calendar.getInstance().dateTime;

            if (0 <= differenceTimeSpan.Days && differenceTimeSpan.Days < 28)
                Models.Calendar.getInstance().DayLabelsArray.SetValue(Models.Calendar.getInstance().DayLabelsArray.GetValue(differenceTimeSpan.Days) + " - dzisiaj", differenceTimeSpan.Days);
        }

        public void UnmarkCurrentDay()
        {
            TimeSpan differenceTimeSpan = Models.Calendar.getInstance().currentDateTime -
                Models.Calendar.getInstance().dateTime;

            if (0 <= differenceTimeSpan.Days && differenceTimeSpan.Days < 28)
                Models.Calendar.getInstance().DayLabelsArray.SetValue(Models.Calendar.getInstance().DayLabelsArray.GetValue(differenceTimeSpan.Days).ToString().Substring(0, Models.Calendar.getInstance().DayLabelsArray.GetValue(differenceTimeSpan.Days).ToString().Length - 10), differenceTimeSpan.Days);
        }

        public void ClearDayEventsLabels()
        {
            for (int i = 0; i < 28; i++)
                Models.Calendar.getInstance().dayEventLabelsLists[i].Clear();
        }

        private void ClearDayEventsList()
        {
            Models.Calendar.getInstance().dayEventsList.Clear();
        }

        public void WriteDayEventsLabelsFromDayEventListToLabelsList()
        {
            TimeSpan differenceTimeSpan;
            int dayNumber;

            ClearDayEventsLabels();

            foreach (DayEvent dayEvent in Models.Calendar.getInstance().dayEventsList)
            {
                differenceTimeSpan = dayEvent.GetStartTime() - Models.Calendar.getInstance().dateTime;

                if (0 <= differenceTimeSpan.Days && differenceTimeSpan.Days < 28)
                {
                    dayNumber = differenceTimeSpan.Days;
                    Models.Calendar.getInstance().dayEventLabelsLists[dayNumber].Add(
                        (dayEvent.GetStartTime().Hour.ToString().Length == 1 ? "0" + dayEvent.GetStartTime().Hour.ToString() : dayEvent.GetStartTime().Hour.ToString())
                        + ":" + (dayEvent.GetStartTime().Minute.ToString().Length == 1 ? "0" + dayEvent.GetStartTime().Minute.ToString() : dayEvent.GetStartTime().Minute.ToString())
                        + "-" + (dayEvent.GetEndTime().Hour.ToString().Length == 1 ? "0" + dayEvent.GetEndTime().Hour.ToString() : dayEvent.GetEndTime().Hour.ToString())
                        + ":" + (dayEvent.GetEndTime().Minute.ToString().Length == 1 ? "0" + dayEvent.GetEndTime().Minute.ToString() : dayEvent.GetEndTime().Minute.ToString())
                        + "-" + dayEvent.GetTitle() + "-" + dayEvent.GetDescription());
                    //stackPanelsArray[dayNumber].Children.Add(label);
                }
            }
        }

        private void ReadPersonEventsFromDatabaseToList()
        {
            //DayEvents[] dayEventsArray = databaseContext.DayEvents.ToArray();
            DayEvent dayEvent;
            Appointment searchedAppointment;
            Guid appointmentID;

            for (int i = 0; i < Models.Calendar.getInstance().databaseContext.Attendances.Count(); i++)
            {
                if (Models.Calendar.getInstance().databaseContext.Attendances.AsEnumerable().ElementAt(i).PersonID == Models.Calendar.currentPerson.PersonID)
                {
                    appointmentID = Models.Calendar.getInstance().databaseContext.Attendances.AsEnumerable().ElementAt(i).AppointmentID;
                    searchedAppointment = Models.Calendar.getInstance().databaseContext.Appointments.First<Appointment>(
                        appointment => appointment.AppointmentID == appointmentID);
                    
                    dayEvent = new DayEvent(searchedAppointment.Title, searchedAppointment.Description,
                    new DateTime(searchedAppointment.AppointmentDate.Year, searchedAppointment.AppointmentDate.Month, searchedAppointment.AppointmentDate.Day, searchedAppointment.StartTime.Hours, searchedAppointment.StartTime.Minutes, 1),
                    new DateTime(searchedAppointment.AppointmentDate.Year, searchedAppointment.AppointmentDate.Month, searchedAppointment.AppointmentDate.Day, searchedAppointment.EndTime.Hours, searchedAppointment.EndTime.Minutes, 1)
                    );

                    Models.Calendar.getInstance().dayEventsList.Add(dayEvent);
                }
            }
            Models.Calendar.getInstance().dayEventsList.Sort(new DayEventsComparer());
        }

        public void CreateDatabase()
        {
            SqlConnection connection = new SqlConnection(@"server=X51RL-PC;Trusted_Connection=Yes;");
            using (connection)
            {
                string sqlQuery = string.Format(@"
                    CREATE DATABASE
                        [NTR2017]
                    ON PRIMARY (
                       NAME=Calendar_data,
                       FILENAME = '{0}\Calendar_data.mdf'
                    )
                    LOG ON (
                        NAME=Calendar_log,
                        FILENAME = '{0}\Calendar_log.ldf'
                    )",
                    @"C:\Program Files\Microsoft SQL Server\MSSQL10.MSSQLSERVER\MSSQL\DATA"
                );

                SqlCommand command = new SqlCommand(sqlQuery, connection);
                command.Connection.Open();
                command.ExecuteNonQuery();
            }
        }

        //
        // GET: /Calendar/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Calendar/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Calendar/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Calendar/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Calendar/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Calendar/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Calendar/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
