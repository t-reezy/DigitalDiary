using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DigitalDiary
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            bool run = true;

            Console.WriteLine("My Digital Diary");

            List<IDiaryEntry> diaryEntries = new List<IDiaryEntry>();



            while (run)
            {
                Console.WriteLine("What do you want to do?");
                Console.WriteLine("(1) - Print my plans");
                Console.WriteLine("(2) - Insert new event");
                Console.WriteLine("(3) - Insert new online event");
                Console.WriteLine("(4) - Insert someone's birthday");
                Console.WriteLine("(5) - Insert aniversary");
                Console.WriteLine("(6) - Insert task");
                Console.WriteLine("(0) - Close the diary");

                string diaryInput = Console.ReadLine();

                switch (diaryInput)
                {
                    case "1":
                        PrintMyPlans(diaryEntries);
                        break;
                    case "2":
                        InsertEvent(diaryEntries);
                        break;
                    case "3":
                        InsertOnlineEvent(diaryEntries);
                        break;
                    case "4":
                        InsertBirthday(diaryEntries);
                        break;
                    case "5":
                        InsertAniversary(diaryEntries);
                        break;
                    case "6":
                        InsertTask(diaryEntries);
                        break;
                    case "0":
                        run = false;
                        Console.WriteLine("Closing the diary!");
                        Thread.Sleep(3000);
                        break;
                    default:
                        Console.WriteLine("Your choice does not match any menu item, please try again!");
                        break;
                }

            }



        }

        private static void InsertTask(List<IDiaryEntry> entries)
        {
            Console.WriteLine("What do you need to do?");
            string eventName = InputEntryName();
            DateTime eventDate = InputEntryDate(false);

            Task newTask = new Task(eventName, eventDate);
            entries.Add(newTask);
        }


        private static void InsertOnlineEvent(List<IDiaryEntry> entries)
        {
            Console.WriteLine("What online event do you want to add?");
            string eventName = InputEntryName();
            DateTime eventDate = InputEntryDate(true);

            OnlineEvent newEvent = new OnlineEvent(eventName,eventDate);
            entries.Add(newEvent);
        }

        private static void InsertAniversary(List<IDiaryEntry> entries)
        {
            Console.WriteLine("What amazing aniversary you need to remember?");
            string eventName = InputEntryName();
            DateTime eventDate = InputEntryDate(false);

            Anniversary newEvent = new Anniversary(eventName, eventDate);
            entries.Add(newEvent);
        }

        private static void InsertBirthday(List<IDiaryEntry> entries)
        {
            Console.WriteLine("Whos birthday do you want to add?");
            string eventName = InputEntryName();
            DateTime eventDate = InputEntryDate(false);

            Birthday newEvent = new Birthday(eventName,eventDate);
            entries.Add(newEvent);
        }

        public static void InsertEvent(List<IDiaryEntry> entries)
        {
            Console.WriteLine("New Event Name:");
            string eventName = InputEntryName();
            DateTime eventDate = InputEntryDate(true);

            Event newEvent = new Event(eventName, eventDate);
            entries.Add(newEvent);
        }

        public static void PrintMyPlans(List<IDiaryEntry> entries)
        {

            if (entries.Count < 1)
            {
                Console.WriteLine("You have no plans!");
            }
            else
            {
                Console.WriteLine("Those are your plans:");
                foreach (IDiaryEntry entry in entries)
                {
                    Console.WriteLine($"{entry.DateAndTime.Day}. {entry.DateAndTime.Month}. {entry.DateAndTime.Year} - {entry.Name}");
                }
            }

        }

        private static string InputEntryName()
        {
            Console.WriteLine("How you would like to name your event?");
            return Console.ReadLine();
        }

        private static DateTime InputEntryDate(bool Time)
        {
            int dayDate;
            int monthDate;
            int yearDate;
            int hourTime = 0;
            int minuteTime = 0;

            Console.WriteLine("Day:");
            string dayDateString = Console.ReadLine();
            while (!int.TryParse(dayDateString, out dayDate) && dayDate < 32)
            {
                Console.WriteLine("Try it once more:");
                dayDateString = Console.ReadLine();

            }
            

            Console.WriteLine("Month:");
            string monthDateString = Console.ReadLine();
            while (!int.TryParse(monthDateString, out monthDate) && monthDate < 12)
            {
                Console.WriteLine("Try it once more:");
                monthDateString = Console.ReadLine();

            }

            Console.WriteLine("Year:");
            string yearDateString = Console.ReadLine();
            while (!int.TryParse(yearDateString, out yearDate))
            {
                Console.WriteLine("Try it once more:");
                yearDateString = Console.ReadLine();

            }

            if (Time)
            {
                Console.WriteLine("Hour:");
                string hourTimeString = Console.ReadLine();
                while (!int.TryParse(hourTimeString, out hourTime) && hourTime < 24)
                {
                    Console.WriteLine("Try it once more:");
                    hourTimeString = Console.ReadLine();

                }

                Console.WriteLine("Minutes:");
                string minutesTimeString = Console.ReadLine();
                while (!int.TryParse(minutesTimeString, out  minuteTime) && minuteTime < 60)
                {
                    Console.WriteLine("Try it once more:");
                    minutesTimeString = Console.ReadLine();

                }
               
            }
            DateTime entryDateTime = new DateTime(yearDate, monthDate, dayDate, hourTime, minuteTime, 0);
            return entryDateTime;

        }
    }
}