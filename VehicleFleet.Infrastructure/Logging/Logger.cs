using Microsoft.Extensions.Logging;
using System;
using System.IO;

namespace VehicleFleet.Infrastructure.Logging
{
    public class Logger : ILogger
    {
        public IDisposable BeginScope<TState>(TState state) => null;

        public bool IsEnabled(LogLevel logLevel) => true;

        public async void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            using (StreamWriter streamWriter = new StreamWriter($"{Directory.GetCurrentDirectory()}/log.txt", true))
            {
                await streamWriter.WriteLineAsync($"Log Level : {logLevel.ToString()} | Event ID : {eventId.Id} | Event Name : {eventId.Name} | Formatter : {formatter(state, exception)}");
                streamWriter.Close();
            }
        }
    }
}