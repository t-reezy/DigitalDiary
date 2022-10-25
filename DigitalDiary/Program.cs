using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DigitalDiary
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool run = true;

            Console.WriteLine("My Digital Diary");


            while (run)
            {
                Console.WriteLine("What do you want to do?");
                Console.WriteLine("1 - Insert new event");
                Console.WriteLine("2 - Print my plans!");
                Console.WriteLine("0 - Close the diary");
                string diaryInput = Console.ReadLine();

                switch (diaryInput)
                {
                    case "1":
                        InsertEvent();
                        break;
                    case "2":
                        PrintMyPlans();
                        break;
                    case "0":
                        run = false;
                        Console.WriteLine("Closing the diary!");
                        Thread.Sleep(3000);
                        break;
                       
                }
                
            }


            
        }
        public static void InsertEvent()
        {
            Console.WriteLine("Event Inserted");
        }

        public static void PrintMyPlans()
        {
            Console.WriteLine("Those are your plans:");
        }
    }
}
