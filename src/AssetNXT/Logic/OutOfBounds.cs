using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using AssetNXT.Model;
using Microsoft.AspNetCore.Routing.Constraints;

namespace AssetNXT.Logic
{
    public class OutOfBounds
    {
        private int _radius = 250;
        private float _long_circle = 51.111110F;
        private float _lat_circle = 51.121110F;
        private float _long_marker = 51.233333F;
        private float _lat_marker = 51.233331F;
        private AssetModel _assetModel;

        public OutOfBounds()
        {
            _assetModel = new AssetModel();
        }

        public AssetModel Circle_OutOfBounds()
        {
            // (MarkerX - MiddlepointX)^2 + (MarkerY - MiddlepointY)^2 = radius^2
            double result = Math.Sqrt(Math.Pow(_long_marker - _long_circle, 2) + Math.Pow(_lat_marker - _lat_circle, 2));

            // Uses Pythagorean theorem to calculate the distance from the middle of the circle.
            // == Outer of the circle (returns false)
            // <  Inside the circle (returns false)
            // >  Outside of the circle (returns true)
            if (result <= _radius)
            {
                _assetModel.OutOfBounds = true;
                return _assetModel;
            }
            else
            {
                _assetModel.OutOfBounds = false;
                return _assetModel;
            }
        }
    }
}
