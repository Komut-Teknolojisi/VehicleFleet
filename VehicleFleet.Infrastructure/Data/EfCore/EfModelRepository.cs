using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using VehicleFleet.Domain.Entities;

namespace VehicleFleet.Infrastructure.Data.EfCore
{
    public class EfModelRepository : EfBaseReporitory<Model>
    {
        private readonly VehicleFleetContext _context;
        private readonly ILogger<EfModelRepository> _logger;

        public EfModelRepository(VehicleFleetContext context, ILogger<EfModelRepository> logger) : base(context, logger)
        {
            _context = context;
            _logger = logger;
        }
    }
}
