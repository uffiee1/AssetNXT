namespace AssetNXT.Models
{
    public struct Point
    {
        public double X { get; set; }

        public double Y { get; set; }

        public static implicit operator Point((double X, double Y)point)
        {
            return new Point { X = point.X, Y = point.Y };
        }
    }
}
