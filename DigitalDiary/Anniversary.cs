using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalDiary
{
    public class Anniversary : DiaryEntry
    {
        public Anniversary() { }

        public Anniversary(string name, DateTime dateAndTime)
        {
            Name = name;
            DateAndTime = dateAndTime; 
        }
    }
}
