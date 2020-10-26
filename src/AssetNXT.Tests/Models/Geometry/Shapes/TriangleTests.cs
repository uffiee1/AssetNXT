using Xunit;

namespace AssetNXT.Models.Geometry.Shapes
{
    public class TriangleTests
    {
        [Theory]
        [InlineData(0.0, 0.0)]
        [InlineData(-1.0, 1.0)]
        [InlineData(1.0, 1.0)]
        public void Should_IntersectWith_Triangle(double x, double y)
        {
            // Arrange
            var point = new Point { X = x, Y = y };
            var triangle = new Triangle
            {
                A = (-5, -3),
                B = (0, 3),
                C = (3, -3)
            };

            // Act
            var result = triangle.IntersectsWith(point);

            // Assert
            Assert.True(result);
        }

        [Theory]
        [InlineData(-1.0, 2.0)]
        [InlineData(1.0, 2.0)]
        public void Should_NotIntersectWith_Triangle(double x, double y)
        {
            // Arrange
            var point = new Point { X = x, Y = y };
            var triangle = new Triangle
            {
                A = (-5, -3),
                B = (0, 3),
                C = (3, -3)
            };

            // Act
            var result = triangle.IntersectsWith(point);

            // Assert
            Assert.False(result);
        }
    }
}
