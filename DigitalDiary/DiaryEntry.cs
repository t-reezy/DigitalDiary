using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DigitalDiary
{
    [XmlType("DiaryEntry")]
    [XmlInclude(typeof(Event)), XmlInclude(typeof(OnlineEvent)), XmlInclude(typeof(Birthday)), XmlInclude(typeof(Task))]
    public abstract class DiaryEntry
    {
        public string Name { get; set; }
        public DateTime DateAndTime { get; set; }


        public virtual string ShortDetails()
        {
            return $"{DateAndTime.Day}. {DateAndTime.Month}. {DateAndTime.Year} - {Name}";
        }

        public virtual string PrintDetails() 
        {
            return $"The {Name} is taking place on {DateAndTime.Day}. {DateAndTime.Month}. {DateAndTime.Year}.";
        }       

        public virtual void Edit(string newName)
        {
                Name = newName;
        }

        public virtual void Edit(DateTime newDateTime)
        {
            DateAndTime = newDateTime;
        }

    }
}
