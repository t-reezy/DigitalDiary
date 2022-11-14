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

        public override string PrintDetails()
        {
            return base.PrintDetails();
        }

        public void MarkAsDone()
        {
            IsDone = true;
        }
    }
}
