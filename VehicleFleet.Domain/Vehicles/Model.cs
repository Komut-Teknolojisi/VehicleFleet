using System;
using System.Collections.Generic;
using System.Text;
using VehicleFleet.Domain.Entities;

namespace VehicleFleet.Domain.Vehicles
{
    public class Model : BaseEntity
    {
        public string ModelName { get; set; }
        public Guid BrandId { get; set; }

        public virtual Brand Brand { get; set; }
        public virtual ICollection<Vehicle> Vehicles { get; set; }
        public static Model Create(Brand brand, string modelName)
        {
            Model model = new Model
            {
                BrandId = brand.Id,
                Brand = brand,
                ModelName = modelName
            };

            return model;
        }
    }
}
