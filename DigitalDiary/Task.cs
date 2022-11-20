using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalDiary
{
    public class Task : DiaryEntry
    {
        public bool IsDone = false;
        public Task() { }
        public Task(string name, DateTime dateAndTime)
        {
            Name = name;
            DateAndTime = dateAndTime;
        }
        public void MarkAsDone()
                {
                    IsDone = true;
                }
        public override string ShortDetails()
        {
            string details =  $"{DateAndTime.Day}. {DateAndTime.Month}. {DateAndTime.Year} - Task: {Name}";
            if  (IsDone)
                {
                    details += " - DONE!";
                }
            return details;
        }
        public override string PrintDetails()
        {
            string details = $"Task with due date: {DateAndTime.Day}. {DateAndTime.Month}. {DateAndTime.Year}, - {Name}";
            if (IsDone)
            {
                details += " is DONE!";
            }
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
