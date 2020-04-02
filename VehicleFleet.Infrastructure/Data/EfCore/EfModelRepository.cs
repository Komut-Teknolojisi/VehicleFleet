﻿using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using VehicleFleet.Domain.Vehicles;

namespace VehicleFleet.Infrastructure.Data.EfCore
{
    public class EfModelRepository : EfBaseReporitory<Model>, IModelRepository
    {
        public EfModelRepository(VehicleFleetContext context, ILogger<EfBaseReporitory<Model>> logger) : base(context, logger)
        {

        }
    }
}
