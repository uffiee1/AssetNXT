using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using AssetNXT.Model;
using AssetNXT.Logic;

namespace XUnitTest
{
    public class TestOutOfBounds
    {
        [Theory]
        [InlineData(2.0, 1.0, 5.0, 1.0, 3.0, false)]
        [InlineData(0.0, 0.0, 10.0, 25.0, 25.0, true)]
        public void TestPointInCircle(double latitudeMiddleCircle, double longtitudeCircle, double radius, 
                                      double latitudeMarker, double longtitudeMarker, bool expected)
        {
            //Arrange
            AssetModel asset = new AssetModel();
            PointModel middleCircle = new PointModel(latitudeMiddleCircle, longtitudeCircle);
            PointModel markerPoint = new PointModel(latitudeMarker, longtitudeMarker);
            OutOfBounds outOfBounds = new OutOfBounds(asset, middleCircle, radius, markerPoint);
            //Act
            AssetModel result = outOfBounds.PointInCircle();
            //Assert
            Assert.Equal(expected, result.OutOfBounds);
        }

        [Theory]
        [InlineData(0.0, 0.0, 10.0, 30.0, 20.0, 0.0, 10.0, 15.0, false)]
        [InlineData(0.0, 0.0, 10.0, 30.0, 20.0, 0.0, 30.0, 15.0, true)]
        public void TestPointInTriangle(double pointALatitude, double pointALongtitude,
                                        double pointBLatitude, double pointBLongtitude, 
                                        double pointCLatitude, double pointCLongtitude,
                                        double latitudeMarker, double longtitudeMarker, bool expected)
        {
            //Arrange
            AssetModel asset = new AssetModel();
            PointModel markerPoint = new PointModel(latitudeMarker, longtitudeMarker);
            List<PointModel> points = new List<PointModel>
            {
                //Point A
                new PointModel(pointALatitude, pointALongtitude),
                //Point B
                new PointModel(pointBLatitude, pointBLongtitude),
                //Point C
                new PointModel(pointCLatitude, pointCLongtitude)
            };
            OutOfBounds outOfBounds = new OutOfBounds(asset, markerPoint, points);
            //Act
            AssetModel result = outOfBounds.PointInTriangle();
            //Assert
            Assert.Equal(expected, result.OutOfBounds);
        }
    }
}
