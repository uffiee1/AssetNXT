using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssetNXT.Model
{
    public class PointModel
    {
        public double Latitude { get; set; } // x

        public double Longtitude { get; set; } // y

        public PointModel(double latitude, double longtitude )
        {
            Latitude = latitude;
            Longtitude = longtitude;
        }
    }
}
