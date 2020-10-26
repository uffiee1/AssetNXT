using Xunit;

namespace AssetNXT.Models.Geometry.Shapes
{
    public class CircleTests
    {
        [Theory]
        [InlineData(0.0, 0.0)]
        [InlineData(15.3, 13.8)]
        [InlineData(-18.4, -12.6)]
        public void Should_IntersectWith_Circle(double x, double y)
        {
            // Arrange
            var point = new Point { X = x, Y = y };
            var circle = new Cirlce { Radius = 25 };

            // Act
            var result = circle.IntersectsWith(point);

            // Assert
            Assert.True(result);
        }

        [Theory]
        [InlineData(25.01, 0.0)]
        [InlineData(18.0, 36.6)]
        public void Should_NotIntersectWith_Circle(double x, double y)
        {
            // Arrange
            var point = new Point { X = x, Y = y };
            var circle = new Cirlce { Radius = 25 };

            // Act
            var result = circle.IntersectsWith(point);

            // Assert
            Assert.False(result);
        }
    }
}
