using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;
using VehicleFleet.Application.Vehicles;
using VehicleFleet.WebApi.Model;

namespace VehicleFleet.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("allowCors")]
    public class BrandsController : ControllerBase
    {
        private readonly IVehicleService _vehicleService;
        private readonly IMemoryCache _memoryCache;

        public BrandsController(IVehicleService vehicleService, IMemoryCache memoryCache)
        {
            _vehicleService = vehicleService;
            _memoryCache = memoryCache;
        }

        [EnableQuery()]
        [HttpGet]
        public IEnumerable<BrandDto> Get()
        {
            IEnumerable<BrandDto> brandDto = null;

            if (!_memoryCache.TryGetValue(CacheNames.Brands.ToString(), out brandDto))
            {
                brandDto = _vehicleService.GetBrands();
                _memoryCache.Set(CacheNames.Brands.ToString(), brandDto);
            }

            return brandDto;
        }
    }
}