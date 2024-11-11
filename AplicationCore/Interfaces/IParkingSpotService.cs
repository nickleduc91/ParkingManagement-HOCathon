using AplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IParkingSpotService
    {
        Task<ParkingSpot?> getParkingSpot(int id);
        Task BookParkingSpot(int parkingSpotId, string userId);
        Task<List<ParkingSpot>> getBookedParkingSpots(string userId);
        Task CancelBookedParkingSpot(int parkingSpotId);
    }
}
