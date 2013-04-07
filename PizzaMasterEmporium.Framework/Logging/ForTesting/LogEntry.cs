using System;

namespace PizzaMasterEmporium.Framework.Logging.ForTesting
{
    public class LogEntry
    {
        public string Message { get; set; }
        public Exception Exception { get; set; }

        public LogEntry(string message, Exception exception)
        {
            Message = message;
            Exception = exception;
        }
    }
}