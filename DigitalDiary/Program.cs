using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace DigitalDiary
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            //TO DO
            //change the location of storages file(s)
            // printing plans - day, 7 day, 30 days, 1 year 
            // handling of past entries (another list?) 
            // implement removing and editing of entries (remove and create new?)
            // inizialization -
            // IAnual - handling of birthdays and aniversaries

            //NICE TO HAVE 
            // setting file
           
            bool run = true;
            List<DiaryEntry> diaryEntries; 
            XmlSerializer serializer = new XmlSerializer(typeof(List<DiaryEntry>));
            XmlSerializer settingSerializer = new XmlSerializer(typeof(DiarySettings));

            if (File.Exists("Settings.xml"))
            {
                Console.WriteLine("Getting your settings");

                using (StreamReader settingsReader = new StreamReader("Settings.xml"))
                {
                    DiarySettings settings = settingSerializer.Deserialize(settingsReader) as DiarySettings;
                }
            }
            else
            {
                Console.WriteLine("We need to set your settings");
                //default settings
                
            }


            if (File.Exists("SecondAttempt.xml"))
            {
                Console.WriteLine("Opening your digital diary!");
                
                using (StreamReader eventReader = new StreamReader("SecondAttempt.xml"))
                {
                    diaryEntries = serializer.Deserialize(eventReader) as List<DiaryEntry>;
                }
            } else
            {
                Console.WriteLine("You do not have yet your diary! Happy to change that!");
                diaryEntries = new List<DiaryEntry>();
            }
            
            while (run)
            {
                Console.WriteLine("____________________________");
                Console.WriteLine("What do you want to do?");
                Console.WriteLine("(1) - Print my plans");
                Console.WriteLine("(2) - Insert new event");
                Console.WriteLine("(3) - Insert new online event");
                Console.WriteLine("(4) - Insert someone's birthday");
                Console.WriteLine("(5) - Insert aniversary");
                Console.WriteLine("(6) - Insert task");
                Console.WriteLine("(0) - Close the diary");
                Console.WriteLine("____________________________");

                string diaryInput = Console.ReadLine();

                switch (diaryInput)
                {
                    case "1":
                        PrintMyPlans(diaryEntries, settings);
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
                        
                        using (StreamWriter writer = new StreamWriter("SecondAttempt.xml"))
                        {
                            serializer.Serialize(writer, diaryEntries);
                        }
                        Thread.Sleep(3000);
                        break;
                    default:
                        Console.WriteLine("Your choice does not match any menu item, please try again!");
                        break;
                }

            }
        }

        private static void InsertTask(List<DiaryEntry> entries)
        {
            string eventName = InputEntryName("What do you need to do? ");
            DateTime eventDate = InputEntryDate(false);

            Task newTask = new Task(eventName, eventDate);
            entries.Add(newTask);
        }


        private static void InsertOnlineEvent(List<DiaryEntry> entries)
        {

            string eventName = InputEntryName("What online event do you want to add?");
            DateTime eventDate = InputEntryDate(true);
            Console.WriteLine("Url to online event");
            string eventUrl = Console.ReadLine();

            OnlineEvent newEvent = new OnlineEvent(eventName,eventDate, eventUrl);
            entries.Add(newEvent);
        }

        private static void InsertAniversary(List<DiaryEntry> entries)
        {
            string eventName = InputEntryName("What amazing aniversary you need to remember?");
            DateTime eventDate = InputEntryDate(false);

            Anniversary newEvent = new Anniversary(eventName, eventDate);
            entries.Add(newEvent);
        }

        private static void InsertBirthday(List<DiaryEntry> entries)
        {
            string eventName = InputEntryName("Whose birthday do you want to remember?");
            DateTime eventDate = InputEntryDate(false);

            Birthday newEvent = new Birthday(eventName,eventDate);
            entries.Add(newEvent);
        }

        public static void InsertEvent(List<DiaryEntry> entries)
        {
            string eventName = InputEntryName("Whats happening?");
            DateTime eventDate = InputEntryDate(true);
            Console.WriteLine("write a place where it happens");
            string eventPlace = Console.ReadLine();

            Event newEvent = new Event(eventName, eventDate, eventPlace);
            entries.Add(newEvent);
        }

        public static void PrintMyPlans(List<DiaryEntry> entries, DiarySettings settings)
        {
            

            Console.Clear(); 


            
            if (entries.Count < 1)
            {
                Console.WriteLine("You have no plans!");
                Console.WriteLine("__________________");
                Console.WriteLine();
            }
            else
            {
                //entries.Where(e => e.GetType() == typeof(Task)).ToList().ForEach(e => Console.WriteLine(e.Name)); 
                var taskEntries = entries.Where(e => e.GetType() == typeof(Task));
                var eventEntries = entries.Where(e => e.GetType() == typeof(Event));
                var onlineEventEntries = entries.Where(e => e.GetType() == typeof(OnlineEvent));
                var brirhdayEntries = entries.Where(e => e.GetType() == typeof(Birthday));
                var aniversaryEntries = entries.Where(e => e.GetType() == typeof(Anniversary));


                List<DiaryEntry> thisMonth = entries.Where(e => e.DateAndTime.Month == DateTime.Now.Month && e.DateAndTime.Year == DateTime.Now.Year).OrderBy(x => x.DateAndTime).ToList();
                List<DiaryEntry> today = thisMonth.Where(e => e.DateAndTime.Day == DateTime.Now.Day).OrderBy(x => x.DateAndTime).ToList();


                Console.WriteLine("Those are your plans:");
                Console.WriteLine("_____________________");
                Console.WriteLine();
                Console.WriteLine("Plans for today: ");
                Console.WriteLine("Tasks: ");
                List<DiaryEntry> todaysTasks = taskEntries.Where(e => e.DateAndTime.Month == DateTime.Now.Month && e.DateAndTime.Year == DateTime.Now.Year).ToList();
                foreach (DiaryEntry entry in todaysTasks)
                {
                    Console.WriteLine($"{entry.Name}");
                }

                Console.WriteLine("Events: ");
                foreach (DiaryEntry entry in today)
                {
                    Console.WriteLine($"{entry.Name}");
                }

                Console.WriteLine();
                Console.WriteLine("Plan for this month: ");
                foreach (DiaryEntry entry in thisMonth)
                {
                    Console.WriteLine($"{entry.DateAndTime.Day}. {entry.DateAndTime.Month}. {entry.DateAndTime.Year} - {entry.Name}");
                }
            }

        }
        private static void RemoveEntry(List<DiaryEntry> entries, string eventName) 
        {
            var toRemove = entries.Where(e => e.Name == eventName).ToList();
            entries.Remove(toRemove[0]);
        }

        private static string InputEntryName(string prompt)
        {
            Console.WriteLine(prompt);
            string entryName = Console.ReadLine();
            while (!String.IsNullOrEmpty(entryName.Trim()))
            {
                Console.WriteLine("Empty string, we do not like that!");
                entryName = Console.ReadLine();
            }

            return entryName;
        }

        private static DateTime InputEntryDate(bool Time)
        {
            int dayDate;
            int monthDate;
            int yearDate;
            int hourTime = 0;
            int minuteTime = 0;
            Console.WriteLine("When? ");

            Console.WriteLine("Day:");
            string dayDateString = Console.ReadLine();
            while (!int.TryParse(dayDateString, out dayDate) | dayDate > 31)
            {
                Console.WriteLine("Try it once more, it has to be number, not greater than 31.");
                dayDateString = Console.ReadLine();

            }
            

            Console.WriteLine("Month:");
            string monthDateString = Console.ReadLine();
            while (!int.TryParse(monthDateString, out monthDate) | monthDate > 12)
            {
                Console.WriteLine("Try it once more, it has to be number, not greater than 12.");
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
                Console.WriteLine("What time?");
                Console.WriteLine("Hour:");
                string hourTimeString = Console.ReadLine();
                while (!int.TryParse(hourTimeString, out hourTime) | hourTime > 23)
                {
                    Console.WriteLine("Try it once more:");
                    hourTimeString = Console.ReadLine();

                }

                Console.WriteLine("Minutes:");
                string minutesTimeString = Console.ReadLine();
                while (!int.TryParse(minutesTimeString, out  minuteTime) | minuteTime > 59)
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