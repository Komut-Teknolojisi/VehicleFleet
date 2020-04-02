using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleFleet.Domain.Entities
{
    public class Vehicle : BaseEntity
    {
        public string PlateNumber { get; set; }
        public Guid BrandId { get; set; }
        public Guid ModelId { get; set; }
        public int Age { get; set; }

        public virtual Brand Brand { get; set; }
        public virtual Model Model { get; set; }
    }
}
