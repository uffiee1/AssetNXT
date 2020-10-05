using System;

using AssetNXT;
using AssetNXT.Logic;

namespace AssetNXT.Models
{
    public struct Circle : IIntersector
    {
        public double Radius { get; set; }

        public Point Position { get; set; }

        public Circle(Point position, double radius)
        {
            Radius = radius;

            Position = position;
        }

        public bool IntersectsWith(Point point)
        {
            var a = Math.Pow(point.X - Position.X, 2);
            var b = Math.Pow(point.Y - Position.Y, 2);

            var distance = Math.Sqrt(a + b);
            return distance <= Radius;
        }
    }
}
