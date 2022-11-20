using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Xml.Serialization;

namespace DigitalDiary
{
    internal class Program
    {
        public static void Main(string[] args)
        {

            bool run = true;
            XmlSerializer serializer = new XmlSerializer(typeof(List<DiaryEntry>));
            string appFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "DigitalDiary");
            string appFile = Path.Combine(appFolder, "MyDiary.xml");
            if (!Directory.Exists(appFolder))
            {
                Directory.CreateDirectory(appFolder);
            }

            List<DiaryEntry> diaryEntries = ReadingDiary(serializer, appFile);

            Console.Write("Press Enter to open your diary!");
            Console.ReadLine();
            Console.Clear();

            PrintToday(diaryEntries);

            while (run)
            {
                SavingDiary(diaryEntries, serializer, appFile);

                Console.WriteLine("____________________________");
                Console.WriteLine("What do you want to do?");
                Console.WriteLine("(1) - Print all my plans");
                Console.WriteLine("(2) - Insert new event");
                Console.WriteLine("(3) - Insert new online event");
                Console.WriteLine("(4) - Insert someone's birthday");
                Console.WriteLine("(5) - Insert task");
                Console.WriteLine("(0) - Close the diary");
                Console.WriteLine("____________________________");

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
                        InsertTask(diaryEntries);
                        break;
                    case "0":
                        run = false;
                        Console.WriteLine("Closing the diary!");
                        SavingDiary(diaryEntries, serializer, appFile);
                        Thread.Sleep(1000);
                        break;
                    default:
                        Console.WriteLine("Your choice does not match any menu item, please try again!");
                        break;
                }

            }
        }

        private static List<DiaryEntry> ReadingDiary(XmlSerializer serializer, string appFile)
        {
            List<DiaryEntry> diaryEntries;
            if (File.Exists(appFile))
            {
                try
                {
                    Console.WriteLine("Getting your diary with your plans!");

                    using (StreamReader eventReader = new StreamReader(appFile))
                    {
                        diaryEntries = serializer.Deserialize(eventReader) as List<DiaryEntry>;
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Cannot get your saved diary :-(. The file may be corrupted or already in use.");
                    Console.WriteLine("We need to start new diary.");
                    diaryEntries = new List<DiaryEntry>();
                }

            }
            else
            {
                Console.WriteLine("You do not have yet your diary! Happy to change that!");
                diaryEntries = new List<DiaryEntry>();
            }

            return diaryEntries;
        }

        private static void SavingDiary(List<DiaryEntry> diaryEntries, XmlSerializer serializer, string appFile)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(appFile))
                {
                    serializer.Serialize(writer, diaryEntries);
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Something went wrong. Your diary was not saved");
            }

        }

        private static void PrintToday(List<DiaryEntry> diaryEntries)
        {
            DateTime today = DateTime.Now;
            Console.WriteLine($"Today is  {today.DayOfWeek} {today.Day}. {today.Month}. {today.Year}");
            var todayLinq = diaryEntries.Where(e => e.DateAndTime.Day == today.Day && e.DateAndTime.Month == today.Month && e.DateAndTime.Year == today.Year);
            var todayTasks = todayLinq.Where(t => t.GetType() == typeof(Task)).ToList();
            var todayBirthday = todayLinq.Where(b => b.GetType() == typeof(Birthday)).ToList();
            var todayEvents = todayLinq.Where(e => e.GetType() == typeof(Event) || e.GetType() == typeof(OnlineEvent)).ToList();

            if (todayBirthday.Count() > 0)
            {
                Console.WriteLine("You should congratulate to:");
                foreach (var person in todayBirthday)
                {
                    Console.WriteLine(person.Name);
                }
            }

            if (todayTasks.Count() > 0)
            {
                Console.WriteLine("You have unfinished tasks with todays due date:");
                foreach (var task in todayTasks)
                {
                    Console.WriteLine(task.Name);
                }
            }

            if (todayEvents.Count() > 0)
            {
                Console.WriteLine("You have those planned events:");
                foreach (var tEvent in todayEvents)
                {
                    Console.WriteLine(tEvent.Name);
                }
            }
        }

        private static void InsertTask(List<DiaryEntry> entries)
        {
            string eventName = InputEntryName("What do you need to do? ");
            DateTime eventDate = InputEntryDate(false);

            Task newTask = new Task(eventName, eventDate);
            entries.Add(newTask);
            Console.WriteLine("Your new task was added.");
        }


        private static void InsertOnlineEvent(List<DiaryEntry> entries)
        {

            string eventName = InputEntryName("What online event do you want to add?");
            DateTime eventDate = InputEntryDate(true);
            string eventUrl = InputEntryName("Put here a link, so you can connect to event.");
            OnlineEvent newEvent = new OnlineEvent(eventName, eventDate, eventUrl);
            entries.Add(newEvent);
            Console.WriteLine("Your new online event was added.");
        }

        private static void InsertBirthday(List<DiaryEntry> entries)
        {
            string eventName = InputEntryName("Whose birthday do you want to remember?");
            DateTime eventDate = InputEntryDate(false);
            Birthday newEvent = new Birthday(eventName, eventDate);
            entries.Add(newEvent);
            Console.WriteLine("You will never forget this birthday.");
        }

        public static void InsertEvent(List<DiaryEntry> entries)
        {
            string eventName = InputEntryName("Whats happening?");
            DateTime eventDate = InputEntryDate(true);
            Console.WriteLine("write a place where it happens");
            string eventPlace = Console.ReadLine();
            Event newEvent = new Event(eventName, eventDate, eventPlace);
            entries.Add(newEvent);
            Console.WriteLine("Your new event was added.");
        }

        public static void PrintMyPlans(List<DiaryEntry> entries)
        {
            DateTime now = DateTime.Now;
            DateTime today = new DateTime(now.Year, now.Month, now.Day);

            Console.Clear();

            if (entries.Count < 1)
            {
                Console.WriteLine("You have no plans!");
                Console.WriteLine("__________________");
                Console.WriteLine();
            }
            else
            {
                bool myPlanRun = true;
                while (myPlanRun)
                {
                    bool myChoiceRun = true;
                    while (myChoiceRun)
                    {
                        List<DiaryEntry> allPlan = entries.Where(e => e.DateAndTime >= today).OrderBy(x => x.DateAndTime).ToList();

                        Console.WriteLine("Those are your plans:");
                        Console.WriteLine("_____________________");
                        Console.WriteLine();

                        for (int i = 0; i < allPlan.Count; i++)
                        {
                            DiaryEntry workingEntry = allPlan[i];

                            Console.WriteLine($"({i + 1}) " + workingEntry.ShortDetails());
                        }

                        Console.WriteLine("Choose event by its number");
                        Console.WriteLine("or go back to Main menu - 0 ");
                        int.TryParse(Console.ReadLine(), out int which);
                        Console.Clear();
                        if (which == 0)
                        {
                            myPlanRun = false;
                            myChoiceRun = false;
                        }
                        else
                        {
                            DiaryEntry currentEntry = allPlan[which - 1];
                            Console.WriteLine(currentEntry.PrintDetails());
                            Console.WriteLine("What do you want to do with this event?");

                            string choicePrompt;

                            if (currentEntry.GetType() == typeof(Task))
                            {
                                choicePrompt = "1 - Delete, 2 - Edit, 3 - Mark as done ";
                            }
                            else
                            {
                                choicePrompt = "1 - Delete, 2 - Edit";
                            }
                            choicePrompt += ", 0 - Going back to My Plan";

                            Console.WriteLine(choicePrompt);

                            string choiceString = Console.ReadLine();
                            while (!int.TryParse(choiceString, out int choice) | choice > 3)
                            {
                                Console.WriteLine("Invalid choice, please try it again.");
                                Console.WriteLine(choicePrompt);
                                choiceString = Console.ReadLine();

                            }

                            switch (choiceString)
                            {
                                case "0":
                                    myChoiceRun = false;
                                    break;
                                case "1":
                                    entries.Remove(currentEntry);
                                    break;
                                case "2":
                                    ChangeEntry(currentEntry);

                                    break;
                                case "3":
                                    if (currentEntry.GetType() == typeof(Task))
                                    {
                                        Task currentTask = currentEntry as Task;
                                        currentTask.MarkAsDone();
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid choice, please try it again.");
                                    }
                                    break;
                                default:
                                    Console.WriteLine("Invalid choice, please try it again.");
                                    break;
                            }
                        }
                    }
                }
            }
        }

        private static void ChangeEntry(DiaryEntry currentEntry)
        {
            Console.WriteLine("Do you want to change the name of the entry? y/n");
            string ynInput = Console.ReadLine();
            while (!(ynInput.ToLower() == "y" || ynInput.ToLower() == "n"))
            {
                switch (ynInput.ToLower())
                {
                    case "y":
                        string nameToChange = InputEntryName("How should be named now?");
                        currentEntry.Edit(nameToChange);
                        break;
                    case "n":
                        break;
                    default:
                        Console.WriteLine("I did not catch that, once more please");
                        ynInput = Console.ReadLine();
                        break;
                }
            }

            Console.WriteLine("Do you want to change the Date of the entry? y/n");
            ynInput = Console.ReadLine();
            while (!(ynInput.ToLower() == "y" || ynInput.ToLower() == "n"))
            {
                switch (ynInput.ToLower())
                {
                    case "y":
                        DateTime dateTimeToChange;
                        if (currentEntry.GetType() == typeof(Event) || currentEntry.GetType() == typeof(OnlineEvent))
                        {
                            dateTimeToChange = InputEntryDate(true);
                        }
                        else
                        {
                            dateTimeToChange = InputEntryDate(false);
                        }
                        currentEntry.Edit(dateTimeToChange);
                        break;
                    case "n":
                        break;
                    default:
                        Console.WriteLine("I did not catch that, once more please");
                        break;
                }
            }

            if (currentEntry.GetType() == typeof(OnlineEvent))
            {
                Console.WriteLine("Do you want to change the Link of the event? y/n");
                ynInput = Console.ReadLine();
                while (!(ynInput.ToLower() == "y" || ynInput.ToLower() == "n"))
                {
                    switch (ynInput.ToLower())
                    {
                        case "y":
                            string urlToChange = InputEntryName("Put here new link to the event.");
                            OnlineEvent currentOnlineEvent = (OnlineEvent)currentEntry;
                            currentOnlineEvent.EditUrl(urlToChange);
                            break;
                        case "n":
                            break;
                        default:
                            Console.WriteLine("I did not catch that, once more please");
                            ynInput = Console.ReadLine();
                            break;
                    }

                }

            }

            if (currentEntry.GetType() == typeof(Event))
            {
                Console.WriteLine("Do you want to change the Place of the event? y/n");
                ynInput = Console.ReadLine();
                while (!(ynInput.ToLower() == "y" || ynInput.ToLower() == "n"))
                {
                    switch (ynInput.ToLower())
                    {
                        case "y":
                            string placeToChange = InputEntryName("Put here new place to the event.");
                            Event currentEvent = (Event)currentEntry;
                            currentEvent.EditPlace(placeToChange);
                            break;
                        case "n":
                            break;
                        default:
                            Console.WriteLine("I did not catch that, once more please");
                            break;
                    }
                }
            }
        }

        private static string InputEntryName(string prompt)
        {
            Console.WriteLine(prompt);
            string entryName = Console.ReadLine();
            while (String.IsNullOrEmpty(entryName.Trim()))
            {
                Console.WriteLine("You did not wrote anything, try it again!");
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
                while (!int.TryParse(minutesTimeString, out minuteTime) | minuteTime > 59)
                {
                    Console.WriteLine("Try it once more:");
                    minutesTimeString = Console.ReadLine();

                }

            }
            try
            {
                DateTime entryDateTime = new DateTime(yearDate, monthDate, dayDate, hourTime, minuteTime, 0);
                return entryDateTime;
            }
            catch (ArgumentOutOfRangeException)
            {
                string printedString = "You wrote a date which is not in calendar. The date was set for today";
                if (Time)
                {
                    printedString += " and time for now";
                }
                printedString += ".";
                Console.WriteLine(printedString);
                return DateTime.Now;
            }
        }
    }
}