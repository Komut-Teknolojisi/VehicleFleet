using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;
using VehicleFleet.Application.Vehicles;
using VehicleFleet.WebApi.Model;

namespace VehicleFleet.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModelsController : ControllerBase
    {
        private readonly IVehicleService _vehicleService;
        private readonly IMemoryCache _memoryCache;

        public ModelsController(IVehicleService vehicleService, IMemoryCache memoryCache)
        {
            _vehicleService = vehicleService;
            _memoryCache = memoryCache;
        }

        [EnableQuery()]
        [HttpGet]
        public IEnumerable<ModelDto> Get()
        {
            IEnumerable<ModelDto> modelDtos = null;

            if (!_memoryCache.TryGetValue(CacheNames.Models.ToString(), out modelDtos))
            {
                modelDtos = _vehicleService.GetModels();
                _memoryCache.Set(CacheNames.Models.ToString(), modelDtos);
            }

            return modelDtos;
        }
    }
}