using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using CalendarWithBaseASPMVC.Models;

namespace CalendarWithBaseASPMVC.DiskInputOutput
{
    class DiskManager
    {
        private static DiskManager instance;
        private string filePath = @"C:\Users\Pawel\Documents\Visual Studio 2010\Projects\CalendarWithBaseASPMVC\CalendarWithBaseASPMVC\Zdarzenia.txt";

        public static DiskManager getInstance()
        {
            if (instance == null)
                instance = new DiskManager();
            return instance;
        }

        public void WriteEventsToFile()
        {
            String outputString = null;
            byte[] outputLine;

            if (File.Exists(filePath))
                File.Delete(filePath);

            using (FileStream fileStream = File.Create(filePath))
            {
                for (int i = 0; i < Models.Calendar.getInstance().dayEventsList.Count; i++)
                {
                    outputString =
                        (((DayEvent)Models.Calendar.getInstance().dayEventsList[i]).GetStartTime().Day.ToString().Length == 1 ? "0" + ((DayEvent)Models.Calendar.getInstance().dayEventsList[i]).GetStartTime().Day.ToString() : ((DayEvent)Models.Calendar.getInstance().dayEventsList[i]).GetStartTime().Day.ToString()) + "-" +
                        (((DayEvent)Models.Calendar.getInstance().dayEventsList[i]).GetStartTime().Month.ToString().Length == 1 ? "0" + ((DayEvent)Models.Calendar.getInstance().dayEventsList[i]).GetStartTime().Month.ToString() : ((DayEvent)Models.Calendar.getInstance().dayEventsList[i]).GetStartTime().Month.ToString()) + "-" +
                        ((DayEvent)Models.Calendar.getInstance().dayEventsList[i]).GetStartTime().Year + " " +
                        (((DayEvent)Models.Calendar.getInstance().dayEventsList[i]).GetStartTime().Hour.ToString().Length == 1? "0" + ((DayEvent)Models.Calendar.getInstance().dayEventsList[i]).GetStartTime().Hour.ToString() : ((DayEvent)Models.Calendar.getInstance().dayEventsList[i]).GetStartTime().Hour.ToString()) + ":" +
                        (((DayEvent)Models.Calendar.getInstance().dayEventsList[i]).GetStartTime().Minute.ToString().Length == 1? "0" + ((DayEvent)Models.Calendar.getInstance().dayEventsList[i]).GetStartTime().Minute.ToString() : ((DayEvent)Models.Calendar.getInstance().dayEventsList[i]).GetStartTime().Minute.ToString()) + "-" +
                        (((DayEvent)Models.Calendar.getInstance().dayEventsList[i]).GetEndTime().Hour.ToString().Length == 1? "0" + ((DayEvent)Models.Calendar.getInstance().dayEventsList[i]).GetEndTime().Hour.ToString() : ((DayEvent)Models.Calendar.getInstance().dayEventsList[i]).GetEndTime().Hour.ToString()) + ":" +
                        (((DayEvent)Models.Calendar.getInstance().dayEventsList[i]).GetEndTime().Minute.ToString().Length == 1 ? "0" + ((DayEvent)Models.Calendar.getInstance().dayEventsList[i]).GetEndTime().Minute.ToString() : ((DayEvent)Models.Calendar.getInstance().dayEventsList[i]).GetEndTime().Minute.ToString()) + " " +
                        ((DayEvent)Models.Calendar.getInstance().dayEventsList[i]).GetDescription() + "\r\n";

                    outputLine = new UTF8Encoding(true).GetBytes(outputString);
                    fileStream.Write(outputLine, 0, outputLine.Length);
                }
            }
        }

        public void ReadEventsFromFile()
        {
            String inputString = "";
            int character;

            using (FileStream fileStream = File.OpenRead(filePath))
            {
                while ((character = fileStream.ReadByte()) != -1)
                {
                    //Console.WriteLine(character);
                    if (character != '\r')
                    {
                        inputString += (char)character;
                    }
                    else // character == '\r'
                    {// uwaga na to ze jest dodatkowy argument - tytul zdarzenia
                        fileStream.ReadByte();
                        DayEvent newDayEvent = new DayEvent("",
                            inputString.Substring(23, inputString.Length - 23),
                            new DateTime(Int32.Parse(inputString.Substring(6, 4)), Int32.Parse(inputString.Substring(3, 2)), Int32.Parse(inputString.Substring(0, 2)), Int32.Parse(inputString.Substring(11, 2)), Int32.Parse(inputString.Substring(14, 2)), 1),
                            new DateTime(Int32.Parse(inputString.Substring(6, 4)), Int32.Parse(inputString.Substring(3, 2)), Int32.Parse(inputString.Substring(0, 2)), Int32.Parse(inputString.Substring(17, 2)), Int32.Parse(inputString.Substring(20, 2)), 1)
                            );
                        Models.Calendar.getInstance().dayEventsList.Add(newDayEvent);
                        inputString = "";
                    }
                }
            }
        }
    }
}
