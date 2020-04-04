using Microsoft.Extensions.Logging;
using VehicleFleet.Domain.Vehicles;

namespace VehicleFleet.Infrastructure.Data.EfCore
{
    public class EfBrandRepository : EfBaseReporitory<Brand>, IBrandRepository
    {
        public EfBrandRepository(VehicleFleetContext context, ILogger<EfBaseReporitory<Brand>> logger) : base(context, logger)
        {
        }
    }
}