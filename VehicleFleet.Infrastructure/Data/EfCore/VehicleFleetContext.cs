using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleFleet.Infrastructure.Data.EfCore
{
    public class VehicleFleetContext : DbContext
    {
        public VehicleFleetContext(DbContextOptions options) : base(options)
        {

        }

    }
}
