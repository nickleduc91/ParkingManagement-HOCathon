using AplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IParkingGarageService
    {
        Task<List<ParkingGarage>> getClosestParkingGarages(double latitude, double longitude);
        List<double> getDistancesFromUser(double userLatitude, double userLongitude, List<ParkingGarage> allGarages);
        Task<ParkingGarage?> getParkingGarage(int garageId);
        Task<ParkingGarage> AddParkingGarage(string name, string address, double longitude, double latitude, int numSpots, string environment, string ownerId, string photoURL);
        Task<List<ParkingGarage>> getOwnedParkingGarages(string userId);
        Task RemoveGarage(int garageId);
        Task UpdateParkingSpotCount(int garageId, bool isBook);
    }
}
