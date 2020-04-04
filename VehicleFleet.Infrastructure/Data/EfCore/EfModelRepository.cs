using Microsoft.Extensions.Logging;
using VehicleFleet.Domain.Vehicles;

namespace VehicleFleet.Infrastructure.Data.EfCore
{
    public class EfModelRepository : EfBaseReporitory<Model>, IModelRepository
    {
        public EfModelRepository(VehicleFleetContext context, ILogger<EfBaseReporitory<Model>> logger) : base(context, logger)
        {
        }
    }
}