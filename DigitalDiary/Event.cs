using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalDiary
{
    public class Event : DiaryEntry
    {
        public string Place { get; set; }

        public Event() { }
        public Event(string eventName, DateTime dateAndTime, string place )
        {
            Name = eventName;
            DateAndTime = dateAndTime;
            Place = place;
        }

        
    }
}
