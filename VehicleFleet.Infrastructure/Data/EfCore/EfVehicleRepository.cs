using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using VehicleFleet.Domain.Vehicles;

namespace VehicleFleet.Infrastructure.Data.EfCore
{
    public class EfVehicleRepository : EfBaseReporitory<Vehicle>, IVehicleRepository
    {
        public EfVehicleRepository(VehicleFleetContext context, ILogger<EfBaseReporitory<Vehicle>> logger) : base(context, logger)
        {

        }
    }
}
