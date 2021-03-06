﻿using System;
using System.Collections.Generic;
using AssetNXT.Models.Core;
using AssetNXT.Models.Core.ServiceAgreement;
using AssetNXT.Models.Data;

namespace AssetNXT.Dtos
{
    public class RuuviStationReadDto
    {
        public string Id { get; set; }

        public List<Tag> Tags { get; set; }

        public int BatteryLevel { get; set; }

        public string DeviceId { get; set; }

        public string EventId { get; set; }

        public Location Location { get; set; }

        public List<ServiceAgreement> ServiceAgreements { get; set; }

        public List<ServiceGeometric> ServiceGeometrics { get; set; }

        public DateTime Time { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
