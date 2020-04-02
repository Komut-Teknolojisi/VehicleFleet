using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using VehicleFleet.Domain.Entities;

namespace VehicleFleet.Infrastructure.Data.EfCore
{
    public class EfBrandRepository : EfBaseReporitory<Brand>
    {
        private readonly VehicleFleetContext _context;
        private readonly ILogger<EfBrandRepository> _logger;

        public EfBrandRepository(VehicleFleetContext vehicleFleetContext, ILogger<EfBrandRepository> logger) : base(vehicleFleetContext, logger)
        {

        }
    }
}
