using System;

namespace VehicleFleet.Application.Vehicles
{
    public class VehicleDto
    {
        public Guid Id { get; set; }
        public string PlateNumber { get; set; }
        public Guid BrandId { get; set; }
        public string BrandName { get; set; }
        public Guid ModelId { get; set; }
        public string ModelName { get; set; }
        public int Age { get; set; }
    }
}