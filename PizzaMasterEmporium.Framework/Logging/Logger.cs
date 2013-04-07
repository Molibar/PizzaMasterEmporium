using System;

namespace PizzaMasterEmporium.Framework.Logging
{
    public class Logger : ILogger
    {

        public void LogDebugMessage(Type source, string message, params object[] args)
        {
            Log.DebugMessage(source, message, args);
        }

        public void LogDebugMessage(Type source, Exception exception, string message, params object[] args)
        {
            Log.DebugMessage(source, exception, message, args);
        }

        public void LogInfoMessage(Type source, string message, params object[] args)
        {
            Log.InfoMessage(source, message, args);
        }

        public void LogInfoMessage(Type source, Exception exception, string message, params object[] args)
        {
            Log.InfoMessage(source, exception, message, args);
        }

        public void LogWarnMessage(Type source, string message, params object[] args)
        {
            Log.WarnMessage(source, message, args);
        }

        public void LogWarnMessage(Type source, Exception exception, string message, params object[] args)
        {
            Log.WarnMessage(source, exception, message, args);
        }

        public void LogErrorMessage(Type source, string message, params object[] args)
        {
            Log.ErrorMessage(source, message, args);
        }

        public void LogErrorMessage(Type source, Exception exception, string message, params object[] args)
        {
            Log.ErrorMessage(source, exception, message, args);
        }

        public void LogFatalMessage(Type source, string message, params object[] args)
        {
            Log.FatalMessage(source, message, args);
        }

        public void LogFatalMessage(Type source, Exception exception, string message, params object[] args)
        {
            Log.FatalMessage(source, exception, message, args);
        }
    }
}