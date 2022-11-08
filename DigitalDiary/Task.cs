using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalDiary
{
    internal class Task : IDiaryEntry
    {
        public string Name { get; set; }
        public DateTime DateAndTime { get; set; }

        public Task(string name, DateTime dateAndTime)
        {
            Name = "Task: " + name;
            DateAndTime = dateAndTime;
        }
    }
}
