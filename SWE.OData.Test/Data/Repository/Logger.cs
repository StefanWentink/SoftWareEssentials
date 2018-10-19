namespace SWE.OData.Test.Data.Repository
{
    using Microsoft.Extensions.Logging;
    using System;

    public class Logger<T> : Logger, ILogger<T>
    {
    }

    public class Logger : ILogger
    {
        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            switch (logLevel)
            {
                case LogLevel.Critical:
                    break;

                case LogLevel.Debug:
                    break;

                case LogLevel.Error:
                    break;

                case LogLevel.Warning:
                    break;

                case LogLevel.Information:
                case LogLevel.None:
                case LogLevel.Trace:
                    break;
            }
        }
    }
}