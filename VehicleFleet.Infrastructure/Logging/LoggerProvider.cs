using Microsoft.Extensions.Logging;
using System;

namespace VehicleFleet.Infrastructure.Logging
{
    public class LoggerProvider : ILoggerProvider
    {
        public ILogger CreateLogger(string categoryName) => new Logger();

        public void Dispose() => throw new NotImplementedException();
    }
}