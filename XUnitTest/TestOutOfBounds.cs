using AssetNXT.Model;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using AssetNXT.Logic;

namespace XUnitTest
{
    public class TestOutOfBounds
    {
        [Fact]
        public void Test()
        {
            //Arrange
            bool expected = false;
            bool actual = true;
            //Act 
            //Do something here.....
            actual = false;

            //Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(15.0, 4.4, 4.5, false)]
        [InlineData(16.0, 4.4, 4.6, true)]
        public void Test2(double radius, double Latitude, double Longtitude, bool expected)
        {
            //Arrange
            OutOfBounds outOfBounds = new OutOfBounds();
            //Act
            outOfBounds.PointInCircle();
            outOfBounds.PointInTriangle();
            //Assert
            Assert.Equal(expected, false);
        }
    }
}
