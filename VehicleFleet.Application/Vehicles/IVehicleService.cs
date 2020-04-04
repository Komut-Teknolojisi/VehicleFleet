using System;
using System.Collections.Generic;

namespace VehicleFleet.Application.Vehicles
{
    public interface IVehicleService
    {
        VehicleDto GetById(Guid id);

        List<VehicleDto> GetVehicles();

        List<BrandDto> GetBrands();

        List<ModelDto> GetModels();

        VehicleDto Add(VehicleDto vehicle);

        bool Update(VehicleDto vehicle);

        bool Delete(Guid id);
    }
}