using VehicleFleet.Domain.interfaces;
using VehicleFleet.Domain.Vehicles;

namespace VehicleFleet.Infrastructure.Data
{
    public interface IVehicleRepository : IRepository<Vehicle>
    {
    }
}