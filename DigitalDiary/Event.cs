using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalDiary
{
    internal class Event : IDiaryEntry
    {

        //public string EventID { get; set; }
        public string Name { get; set; }
        public DateTime DateAndTime { get; set; }
        public DateTime Date { get; set; }

        public Event(string eventName, DateTime dateAndTime)
        {
            Name = "Event: " + eventName;
            DateAndTime = dateAndTime;
        }
    }
}
