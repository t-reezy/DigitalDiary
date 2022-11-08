using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalDiary
{
    internal interface IDiaryEntry
    {
        string Name { get; set; }
        DateTime DateAndTime { get; set; }

    }
}
