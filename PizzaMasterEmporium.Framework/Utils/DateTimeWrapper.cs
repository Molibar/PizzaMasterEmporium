using System;

namespace PizzaMasterEmporium.Framework.Utils
{
    public interface IDateTimeWrapper
    {
        DateTime Now { get; }
        DateTime UtcNow { get; }
    }

    public class DateTimeWrapper : IDateTimeWrapper
    {
        public DateTime Now { get { return DateTime.Now; } }
        public DateTime UtcNow { get { return DateTime.UtcNow; } }
    }
}
