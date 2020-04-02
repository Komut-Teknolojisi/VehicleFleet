﻿using System;

namespace VehicleFleet.Domain.Entities
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
    }
}