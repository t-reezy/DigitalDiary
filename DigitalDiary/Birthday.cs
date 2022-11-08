using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalDiary
{
    internal class Birthday : IDiaryEntry
    {
        public string Name { get; set; }
        public DateTime DateAndTime { get; set; }

        public Birthday(string name, DateTime dateAndTime)
        {
            Name = "Birthday of" + name;
            DateAndTime = dateAndTime;
        }
    }
}
