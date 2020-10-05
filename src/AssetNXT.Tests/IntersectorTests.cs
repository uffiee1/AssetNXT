using System;
using System.Collections;
using System.Collections.Generic;

using AssetNXT;
using AssetNXT.Logic;
using AssetNXT.Models;

using Xunit;

namespace AssetNXT.Tests
{
    public class IntersectorTests
    {
        [Theory]
        [InlineData(0.0, 0.0)]
        [InlineData(15.3, 13.8)]
        [InlineData(-18.4, -12.6)]
        public void Should_IntersectWith_Circle(double x, double y)
        {
            // Arrange
            var circle = new Circle((0, 0), 25);
            var point = new Point { X = x, Y = y };

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
            var circle = new Circle((0, 0), 25);
            var point = new Point { X = x, Y = y };

            // Act
            var result = circle.IntersectsWith(point);

            // Assert
            Assert.False(result);
        }

        [Theory]
        [InlineData(0.0, 0.0)]
        [InlineData(-1.0, 1.0)]
        [InlineData(1.0, 1.0)]
        public void Should_IntersectWith_Triangle(double x, double y)
        {
            // Arrange
            var point = new Point { X = x, Y = y };
            var triangle = new Triangle { A = (-5, -3), B = (0, 3), C = (3, -3) };

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
            var triangle = new Triangle { A = (-5, -3), B = (0, 3), C = (3, -3) };

            // Act
            var result = triangle.IntersectsWith(point);

            // Assert
            Assert.False(result);
        }
    }
}
