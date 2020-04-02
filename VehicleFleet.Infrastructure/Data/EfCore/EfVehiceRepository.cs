using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using VehicleFleet.Domain.Entities;

namespace VehicleFleet.Infrastructure.Data.EfCore
{
    public class EfVehiceRepository : EfBaseReporitory<Vehicle>
    {
        private readonly VehicleFleetContext _context;
        private ILogger<EfVehiceRepository> _logger;

        public EfVehiceRepository(VehicleFleetContext vehicleFleetContext, ILogger<EfVehiceRepository> logger) : base(vehicleFleetContext, logger)
        {
            _context = vehicleFleetContext;
            _logger = logger;
        }


    }
}
