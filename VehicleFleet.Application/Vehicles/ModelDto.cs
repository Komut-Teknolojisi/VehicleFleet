using System;

namespace VehicleFleet.Application.Vehicles
{
    public class ModelDto
    {
        public Guid Id { get; set; }
        public string ModelName { get; set; }
        public Guid BrandId { get; set; }
        public string BrandName { get; set; }
    }
}