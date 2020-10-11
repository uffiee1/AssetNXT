namespace AssetNXT.Models.Geometry.Shapes
{
    public class Triangle
    {
        public Point A { get; set; }

        public Point B { get; set; }

        public Point C { get; set; }

        public bool IntersectsWith(Point point)
        {
            var a = Sign(point, A, B);
            var b = Sign(point, B, C);
            var c = Sign(point, C, A);

            var hasNeg = (a < 0) || (b < 0) || (c < 0);
            var hasPos = (a > 0) || (b > 0) || (c > 0);

            return !(hasNeg && hasPos);
        }

        public static double Sign(Point a, Point b, Point c)
        {
            return ((a.X - c.X) * (b.Y - c.Y)) - ((b.X - c.X) * (a.Y - c.Y));
        }
    }
}
