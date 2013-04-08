using System;
using System.Collections.Generic;

namespace PizzaMasterEmporium.Framework.Web.Tracking
{
    public class RequestStatus
    {
        public Guid CallerGuid { get; set; }

        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public string[] Parameters { get; set; }

        public IList<TimeSpanHolder> TimeSpanHolders { get; set; }


        public RequestStatus()
        {
            TimeSpanHolders = new List<TimeSpanHolder>();
        }

        public void AddTimeSpanHolder(string name, TimeSpan timeSpan)
        {
            TimeSpanHolders.Add(new TimeSpanHolder { Name = name, TimeSpan = timeSpan });
        }
    }

    public class TimeSpanHolder
    {
        public string Name { get; set; }
        public TimeSpan TimeSpan { get; set; }
    }
}