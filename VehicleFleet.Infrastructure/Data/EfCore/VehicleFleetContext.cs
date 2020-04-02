using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleFleet.Infrastructure.Data.EfCore
{
    public class VehicleFleetContext : DbContext
    {
        private readonly ILoggerFactory _loggerFactory;
        public VehicleFleetContext(DbContextOptions options, ILoggerFactory loggerFactory) : base(options)
        {
            _loggerFactory = loggerFactory;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(_loggerFactory)
                .EnableSensitiveDataLogging();
        }

    }
}
