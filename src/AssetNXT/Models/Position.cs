using AssetNXT.Models.Geometry;

namespace AssetNXT.Models
{
    public class Position
    {
        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public static implicit operator Position(Point point)
        {
            return new Position { Latitude = point.X, Longitude = point.Y };
        }

        public static implicit operator Position((double X, double Y) point)
        {
            return new Position { Latitude = point.X, Longitude = point.Y };
        }
    }
}
