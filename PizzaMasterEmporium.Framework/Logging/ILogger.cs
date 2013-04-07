using System;

namespace PizzaMasterEmporium.Framework.Logging
{
    public interface ILogger
    {
        void LogDebugMessage(Type source, string message, params object[] args);
        void LogDebugMessage(Type source, Exception exception, string message, params object[] args);
        void LogInfoMessage(Type source, string message, params object[] args);
        void LogInfoMessage(Type source, Exception exception, string message, params object[] args);
        void LogWarnMessage(Type source, string message, params object[] args);
        void LogWarnMessage(Type source, Exception exception, string message, params object[] args);
        void LogErrorMessage(Type source, string message, params object[] args);
        void LogErrorMessage(Type source, Exception exception, string message, params object[] args);
        void LogFatalMessage(Type source, string message, params object[] args);
        void LogFatalMessage(Type source, Exception exception, string message, params object[] args);
    }
}