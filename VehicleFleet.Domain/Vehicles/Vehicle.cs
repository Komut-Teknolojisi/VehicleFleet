using System;
using System.Collections.Generic;
using System.Text;
using VehicleFleet.Domain.Entities;

namespace VehicleFleet.Domain.Vehicles
{
    public class Vehicle : BaseEntity
    {

        public string PlateNumber { get; set; }
        public Guid BrandId { get; set; }
        public Guid ModelId { get; set; }
        public int Age { get; set; }


        public virtual Brand Brand { get; set; }
        public virtual Model Model { get; set; }

        public static Vehicle Create(string plateNumber, Brand brand, Model model, int age)
        {
            Vehicle vehicle = new Vehicle
            {
                Id = Guid.NewGuid(),
                PlateNumber = plateNumber,
                BrandId = brand.Id,
                Brand = brand,
                ModelId = model.Id,
                Model = model,
                Age = age,
                Created = DateTime.Now
            };

            return vehicle;
        }

    }
}
