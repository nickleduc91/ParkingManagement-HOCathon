using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicationCore.Entities
{
    public class ParkingGarage : BaseEntity, IAggregateRoot
    {
        public string Name { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string Address { get; set; }
        public int NumParkingSpots { get; set; }
        public int NumAvailableParkingSpots { get; set; }
        public int NumLevels { get; set; }
        public string OwnerId { get; set; }
        public bool IsOutdoor { get; set; }
        public string PhotoUrl { get; set; }

        private readonly List<ParkingSpot> _parkingSpots = new List<ParkingSpot>();
        public IReadOnlyCollection<ParkingSpot> ParkingSpots => _parkingSpots.AsReadOnly();

        public ParkingGarage()
        {
            
        }
        public ParkingGarage(string name, double longitude, double latitude, string address, int numParkingSpots, int numAvailableParkingSpots, int numLevels, bool isOutdoor, string ownerId, string photoURL)
        {
            Name = name;
            Longitude = longitude; 
            Latitude = latitude;
            Address = address;
            NumParkingSpots = numParkingSpots;
            NumAvailableParkingSpots = numAvailableParkingSpots;
            NumLevels = numLevels;
            IsOutdoor = isOutdoor;
            OwnerId = ownerId;
            PhotoUrl = photoURL;
        }

        public void AddParkingSpot(string directions, int parkingSpotNumber)
        {
            var parkingSpot = new ParkingSpot(directions, parkingSpotNumber);
            _parkingSpots.Add(parkingSpot);
        }

    }
}
