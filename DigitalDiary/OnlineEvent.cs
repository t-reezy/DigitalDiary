using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        public override string ShortDetails()
        {
            string details = $"{DateAndTime.Day}. {DateAndTime.Month}. {DateAndTime.Year} - {Name} (online)";
            return details;
        }

        public override string PrintDetails()
        {
            string details = $"{Name}\n{DateAndTime.Day}. {DateAndTime.Month}. {DateAndTime.Year} Time: {DateAndTime.Hour}: {DateAndTime.Minute} \nLink: {UrlString}";
            return details;
        }

        public void EditUrl(string url)
        {
            UrlString = url;
        }

        public override void Edit(DateTime newDateTime)
        {
            base.Edit(newDateTime);
        }
        public override void Edit(string newName)
        {
            base.Edit(newName);
        }
    }
}
