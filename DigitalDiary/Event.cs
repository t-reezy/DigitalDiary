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

        public override string ShortDetails()
        {
            string details = $"{DateAndTime.Day}. {DateAndTime.Month}. {DateAndTime.Year} - {Name}";
            return details;
        }

        public override string PrintDetails()
        {
            string details = $"{Name}\n{DateAndTime.Day}. {DateAndTime.Month}. {DateAndTime.Year} Time: {DateAndTime.Hour}: {DateAndTime.Minute} \nPlace: {Place}";
            return details;
        }

        public void EditPlace(string place)
        {
            Place = place;
        }
        public override void Edit(DateTime newDateTime)
        {
            base.Edit(newDateTime);
        }
        public override void Edit(string newName)
        {
            base.Edit(newName);
        }
    }
}
