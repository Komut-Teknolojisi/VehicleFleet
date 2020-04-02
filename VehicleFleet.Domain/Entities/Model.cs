using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleFleet.Domain.Entities
{
    public class Model : BaseEntity
    {
        public Guid BrandId { get; set; }
        public string ModelName { get; set; }

        public virtual Brand Brand { get; set; }
    }
}
