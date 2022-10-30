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

        public Task(string name)
        {
            Name = name;
        }
    }
}
