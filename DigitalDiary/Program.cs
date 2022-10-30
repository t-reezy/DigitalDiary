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
                Console.WriteLine("1 - Print my plans");
                Console.WriteLine("2 - Insert new event");
                Console.WriteLine("3 - Insert new online event");
                Console.WriteLine("4 - Insert someone's birthday");
                Console.WriteLine("5 - Insert aniversary");
                Console.WriteLine("6 - Insert task");
                Console.WriteLine("0 - Close the diary");

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
            string eventName = Console.ReadLine();

            Task newTask = new Task(eventName);
            entries.Add(newTask);
        }

        private static void InsertOnlineEvent(List<IDiaryEntry> entries)
        {
            Console.WriteLine("What online event do you want to add?");
            string eventName = Console.ReadLine();

            OnlineEvent newEvent = new OnlineEvent(eventName);
            entries.Add(newEvent);
        }

        private static void InsertAniversary(List<IDiaryEntry> entries)
        {
            Console.WriteLine("What amazing aniversary you need to remember?");
            string eventName = Console.ReadLine();

            Anniversary newEvent = new Anniversary(eventName);
            entries.Add(newEvent);
        }

        private static void InsertBirthday(List<IDiaryEntry> entries)
        {
            Console.WriteLine("Whos birthday do you want to add?");
            string eventName = Console.ReadLine();

            Birthday newEvent = new Birthday(eventName);
            entries.Add(newEvent);
        }

        public static void InsertEvent(List<IDiaryEntry> entries)
        {
            Console.WriteLine("New Event Name:");
            string eventName = Console.ReadLine();

            Event newEvent = new Event(eventName, DateTime.Now);
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
                    Console.WriteLine(entry.Name);
                }
            }

        }
    }
}
