using System;
using System.Collections.Generic;
using System.Linq;
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

    }
}
