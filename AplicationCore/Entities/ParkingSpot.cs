using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicationCore.Entities
{
    public class ParkingSpot: BaseEntity, IAggregateRoot
    {
        public string Directions { get; set; } = null!;
        public bool IsAvailable { get; set; }
        public string UserId { get; set; } = null!;
        public int ParkingSpotNumber { get; set; }
        public int ParkingGarageId { get; private set; }

        public ParkingSpot(string directions, int parkingSpotNumber)
        {
            Directions = directions;
            ParkingSpotNumber = parkingSpotNumber;
            IsAvailable = true;
            UserId = "";
        }

        public void Book(string userId)
        {
            IsAvailable = false;
            UserId = userId;
        }

        public void Cancel()
        {
            IsAvailable = true;
            UserId = "";
        }
    }

}
