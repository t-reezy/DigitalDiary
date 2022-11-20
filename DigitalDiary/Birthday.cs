using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DigitalDiary
{
    public class Birthday : DiaryEntry
    {
        public Birthday() { }
        public Birthday(string name, DateTime dateAndTime)
        {
            Name = name;
            DateAndTime = dateAndTime;
        }

        public override string ShortDetails()
        {
            string details = $"{DateAndTime.Day}. {DateAndTime.Month}. {DateAndTime.Year} - Birthday of: {Name}";
            return details;
        }
        public override string PrintDetails()
        {
            string details = $"The {DateAndTime.Day}. {DateAndTime.Month}. {DateAndTime.Year} {Name} has birthday.";
           
            return details;
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
