using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalDiary
{
    internal class Event
    {

        //public string EventID { get; set; }
        public string EventName { get; set; }
        public DateTime Date { get; set; }

        public Event(string eventName, DateTime date)
        {
            EventName = eventName;
            Date = date; 
        }
        public Event(string eventName, DateTime date, string note, string withWhom)
        {

        }


        public void EditEventName(string newName)
        {
            EventName = newName;
        }

        public void Cancel()
        {

        }

        public void Remove()
        {

        }

    }
}
