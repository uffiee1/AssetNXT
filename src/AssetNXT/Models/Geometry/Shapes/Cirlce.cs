using System;

namespace AssetNXT.Models.Geometry.Shapes
{
    public class Cirlce
    {
        public double Radius { get; set; }

        public Point Position { get; set; }

        public bool IntersectsWith(Point point)
        {
            var a = Math.Pow(point.X - Position.X, 2);
            var b = Math.Pow(point.Y - Position.Y, 2);

            return Math.Sqrt(a + b) < Radius;
        }
    }
}
