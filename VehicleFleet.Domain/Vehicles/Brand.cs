using System;
using System.Collections.Generic;
using System.Text;
using VehicleFleet.Domain.Entities;

namespace VehicleFleet.Domain.Vehicles
{
    public class Brand : BaseEntity
    {
        public string BrandName { get; set; }

        public virtual ICollection<Vehicle> Vehicles { get; set; }
        public virtual ICollection<Model> Models { get; set; }

        public static Brand Create(string name)
        {
            Brand brand = new Brand
            {
                Id = Guid.NewGuid(),
                BrandName = name,
                Created = DateTime.Now
            };

            return brand;
        }
    }
}
