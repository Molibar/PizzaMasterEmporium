using System;
using System.Collections.Generic;
using log4net;

namespace PizzaMasterEmporium.Framework.Logging
{
    public class Log
    {
        internal static string ApplicationName { get; set; }

        private static ILog _overridingLogger;
        private static Dictionary<Type, ILog> _loggers = new Dictionary<Type, ILog>();
        private static readonly object _lock = new object();


        /// <summary>
        /// This constructor should ONLY be called if you want to override the
        /// standard behaviour of this logging class. For instance if you want
        /// to override the config file in case of testing or such.
        /// </summary>
        /// <param name="overridingLogger">
        /// A implementation or the ILog interface. MemoryLoggerForTest could
        /// be sent in here. That way you can verify everything that is logged.
        /// </param>
        public Log(ILog overridingLogger)
        {
            _overridingLogger = overridingLogger;
        }

        private static ILog GetLogger(Type source)
        {
            lock (_lock)
            {
                if (_loggers.ContainsKey(source))
                {
                    return _loggers[source];
                }
                // If there is an overriding logger set by the constructor that
                // will be returned regardless off the 
                if (_overridingLogger != null)
                {
                    return _overridingLogger;
                }
                var logger = LogManager.GetLogger(source);
                _loggers.Add(source, logger);
                return logger;
            }
        }

        public static void DebugMessage(Type source, string message, params object[] args)
        {
            DebugMessage(source, null, message, args);
        }

        public static void DebugMessage(Type source, Exception exception, string message, params object[] args)
        {
            var log = GetLogger(source);
            if (log.IsDebugEnabled)
            {
                ProcessLogEntry(log, LogLevel.Debug, exception, message, args);
            }
        }

        public static void InfoMessage(Type source, string message, params object[] args)
        {
            InfoMessage(source, null, message, args);
        }

        public static void InfoMessage(Type source, Exception exception, string message, params object[] args)
        {
            var log = GetLogger(source);
            if (log.IsInfoEnabled)
            {
                ProcessLogEntry(log, LogLevel.Info, exception, message, args);
            }
        }

        public static void WarnMessage(Type source, string message, params object[] args)
        {
            WarnMessage(source, null, message, args);
        }

        public static void WarnMessage(Type source, Exception exception, string message, params object[] args)
        {
            var log = GetLogger(source);
            if (log.IsWarnEnabled)
            {
                ProcessLogEntry(log, LogLevel.Warn, exception, message, args);
            }
        }

        public static void ErrorMessage(Type source, string message, params object[] args)
        {
            ErrorMessage(source, null, message, args);
        }

        public static void ErrorMessage(Type source, Exception exception, string message, params object[] args)
        {
            var log = GetLogger(source);
            if (log.IsErrorEnabled)
            {
                ProcessLogEntry(log, LogLevel.Error, exception, message, args);
            }
        }

        public static void FatalMessage(Type source, string message, params object[] args)
        {
            FatalMessage(source, null, message, args);
        }

        public static void FatalMessage(Type source, Exception exception, string message, params object[] args)
        {
            var log = GetLogger(source);
            if (log.IsFatalEnabled)
            {
                ProcessLogEntry(log, LogLevel.Fatal, exception, message, args);
            }
        }

        public delegate void LoggerCall(object message, Exception exception);

        private static void ProcessLogEntry(ILog log, LogLevel logLevel, Exception exception, string message, params object[] args)
        {
            PreProcessLogEntry(logLevel, exception);
            var formattedMessage = message;
            try
            {
                formattedMessage = string.Format(message, args);
            }
            catch (Exception ex)
            {
                WarnMessage(typeof(Log), ex, "Unable to format message \"{0}\" with args {1}", message, args);
            }
            switch (logLevel)
            {
                case LogLevel.Debug:
                    log.Debug(formattedMessage, exception);
                    break;
                case LogLevel.Info:
                    log.Info(formattedMessage, exception);
                    break;
                case LogLevel.Warn:
                    log.Warn(formattedMessage, exception);
                    break;
                case LogLevel.Error:
                    log.Error(formattedMessage, exception);
                    break;
                case LogLevel.Fatal:
                    log.Fatal(formattedMessage, exception);
                    break;
            }
            PostProcessLogEntry();
        }

        private static void PreProcessLogEntry(LogLevel logLevel, Exception exception)
        {
            NDC.Push(ApplicationName);
        }

        private static void PostProcessLogEntry()
        {
            NDC.Pop();
        }

        public enum LogLevel
        {
            Debug = 1,
            Info = 2,
            Warn = 3,
            Error = 4,
            Fatal = 5
        }
    }
}