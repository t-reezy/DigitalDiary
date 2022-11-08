using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalDiary
{
    internal class Anniversary : IDiaryEntry
    {
        
        public string Name { get; set; }
        public DateTime DateAndTime { get; set; }

        public Anniversary(string name, DateTime dateAndTime)
        {
            Name = "Anniversary of "  + name;
            DateAndTime = dateAndTime; 
        }
    }
}
