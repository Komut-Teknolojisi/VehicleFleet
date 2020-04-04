using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using VehicleFleet.Application.Vehicles;
using VehicleFleet.WebApi.Model;

namespace VehicleFleet.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("allowCors")]
    public class VehiclesController : ControllerBase
    {
        private readonly IVehicleService _vehicleService;
        private readonly IMemoryCache _memoryCache;

        public VehiclesController(IVehicleService vehicleService, IMemoryCache memoryCache)
        {
            _vehicleService = vehicleService;
            _memoryCache = memoryCache;
        }

        [HttpGet]
        [EnableQuery()]
        //[Queryable]
        public IEnumerable<VehicleDto> Get()
        {
            //test();

            IEnumerable<VehicleDto> vehicles = null;

            if (!_memoryCache.TryGetValue(CacheNames.Vehicles.ToString(), out vehicles))
            {
                vehicles = _vehicleService.GetVehicles();

                _memoryCache.Set(CacheNames.Vehicles.ToString(), vehicles);
            }

            return vehicles;
        }

        public VehicleDto Post(VehicleDto vehicleDto)
        {
            _vehicleService.Add(vehicleDto);

            _memoryCache.Remove(CacheNames.Vehicles.ToString());
            _memoryCache.Remove(CacheNames.Brands.ToString());
            _memoryCache.Remove(CacheNames.Models.ToString());

            return vehicleDto;
        }

        [HttpPut]
        public VehicleDto Put(VehicleDto vehicleDto)
        {
            _vehicleService.Update(vehicleDto);

            _memoryCache.Remove(CacheNames.Vehicles.ToString());

            return vehicleDto;
        }

        [HttpDelete("{key}")]
        public bool Delete([FromODataUri]Guid key)
        {
            bool result = _vehicleService.Delete(key);
            _memoryCache.Remove(CacheNames.Vehicles.ToString());

            return result;
        }
    }
}