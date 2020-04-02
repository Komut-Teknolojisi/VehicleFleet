using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using VehicleFleet.Application.Vehicles;

namespace VehicleFleet.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IVehicleService _vehicleService;

        const string cacheKey = "catalogKey";
        private readonly IMemoryCache _memCache;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IMemoryCache memCache, IVehicleService vehicleService)
        {
            _logger = logger;
            _memCache = memCache;
            _vehicleService = vehicleService;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {

            _logger.LogInformation("liste getirildi");


            VehicleDto vehicleDto = new VehicleDto
            {
                Age = 2020,
                BrandName = "Volvo",
                ModelName = "S40",
                PlateNumber = "34AA34"
            };

            _vehicleService.Add(vehicleDto);

            var brands = _vehicleService.GetBrands();

            var rng = new Random();
            WeatherForecast[] model = null;


            //Burada değerin belirtilen key ile cache'de kontrolünü yapıyoruz
            if (!_memCache.TryGetValue(cacheKey, out model))
            {
                model = Enumerable.Range(1, 5).Select(index => new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = rng.Next(-20, 55),
                    Summary = Summaries[rng.Next(Summaries.Length)]
                })
            .ToArray();

                //Burada cache için belirli ayarlamaları yapıyoruz.Cache süresi,önem derecesi gibi
                var cacheExpOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTime.Now.AddMinutes(30),
                    Priority = CacheItemPriority.Normal
                };
                //Bu satırda belirlediğimiz key'e göre ve ayarladığımız cache özelliklerine göre kategorilerimizi in-memory olarak cache'liyoruz.
                _memCache.Set(cacheKey, model, cacheExpOptions);
            }


            _logger.LogInformation("liste", model);

            return model;

        }
    }
}
