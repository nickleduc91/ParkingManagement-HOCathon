using AplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.ApplicationCore.Entities.ParkingGarageTests
{
    public class ParkingGarageAddParkingSpot
    {
        private readonly string _name = "test name";
        private readonly double _longitude; 
        private readonly double _latitude;
        private readonly string _address = "test address";
        private readonly int _numParkingSpots = 10;
        private readonly int _numAvailableParkingSpots = 6;
        private readonly int _numLevels = 1;
        private readonly bool _isOutdoor = false;
        private readonly string _ownerId = "test owner";
        private readonly string _photoUrl = "test photo url";

        [Fact]
        public void CantAddNegativeParkingSpot()
        {
            var parkingGarage = new ParkingGarage(_name, _longitude, _latitude, _address, _numParkingSpots, _numAvailableParkingSpots, _numLevels, _isOutdoor, _ownerId, _photoUrl);

            Assert.Throws<ArgumentOutOfRangeException>(() => parkingGarage.AddParkingSpot(_ownerId,  -5));
        }

    }
}
