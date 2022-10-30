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

        public Birthday(string name)
        {
            Name = name + " has birthday";
        }
    }
}
