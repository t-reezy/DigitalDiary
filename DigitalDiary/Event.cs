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
        public DateTime Date { get; set; }

        public Event(string eventName, DateTime date)
        {
            Name = "Event: " + eventName;
            Date = date; 
        }
        public Event(string eventName, DateTime date, string note, string withWhom)
        {

        }


        public void EditEventName(string newName)
        {
            Name = newName;
        }

        public void Cancel()
        {

        }

        public void Remove()
        {

        }

    }
}
