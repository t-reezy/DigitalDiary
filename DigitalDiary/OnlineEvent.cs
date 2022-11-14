using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalDiary
{
    public class OnlineEvent : DiaryEntry
    {
        public string UrlString;
        public OnlineEvent() { }
        public OnlineEvent(string name, DateTime dateAndTime, string url)
        {
            Name = name;
            DateAndTime = dateAndTime;
            UrlString = url; 
        }
    }
}
