using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalDiary
{
    internal class OnlineEvent : IDiaryEntry
    {
        public string Name { get; set; }
        public DateTime DateAndTime { get; set; }
        public OnlineEvent(string name, DateTime dateAndTime)
        {
            Name = name;
            DateAndTime = dateAndTime;
        }
    }
}
