using System;
using System.Collections.Generic;
using System.Text;
using VehicleFleet.Domain.interfaces;
using VehicleFleet.Domain.Vehicles;

namespace VehicleFleet.Infrastructure.Data
{
    public interface IModelRepository : IRepository<Model>
    {
    }
}
