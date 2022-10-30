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

        public Anniversary(string name)
        {
            Name = "Anniversary of "  + name;
        }
    }
}
