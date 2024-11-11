using AplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.ApplicationCore.Entities.ParkingSpotsTests
{
    public class ParkingSpotTests
    {
        private readonly string _userId = "test user id";
        private readonly string _directions = "test directions";
        private readonly int _parkingSpotNumber = 3;

        [Fact]
        public void ShouldBookParkingSpot()
        {
            var parkingSpot = new ParkingSpot(_directions, _parkingSpotNumber);
            parkingSpot.Book(_userId);
            Assert.Equal(parkingSpot.IsAvailable, false);
        }
        [Fact]
        public void ShouldCancelParkingSpot()
        {
            var parkingSpot = new ParkingSpot(_directions, _parkingSpotNumber);
            parkingSpot.Cancel();
            Assert.Equal(parkingSpot.IsAvailable, true);

        }
    }
}
