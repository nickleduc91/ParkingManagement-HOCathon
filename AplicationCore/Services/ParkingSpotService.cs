using AplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ParkingSpotService : IParkingSpotService
    {
        private readonly IRepository<ParkingSpot> _parkingSpotRepository;
        private readonly IParkingGarageService _parkingGarageService;

        public ParkingSpotService(IRepository<ParkingSpot> parkingSpotRepository, IParkingGarageService parkingGarageService)
        {
            _parkingSpotRepository = parkingSpotRepository;
            _parkingGarageService = parkingGarageService;
        }

        public async Task<ParkingSpot?> getParkingSpot(int id)
        {
            var spec = new ParkingSpotSpecification(id);
            var parkingSpot = await _parkingSpotRepository.FirstOrDefaultAsync(spec);
            if (parkingSpot == null)
            {
                Console.WriteLine($"Parking Spot with ID {id} not found.");
                return null;
            }
            return parkingSpot;
        }

        public async Task BookParkingSpot(int parkingSpotId, string userId)
        {
            // Retrieve the parking spot from the database
            var parkingSpot = await _parkingSpotRepository.GetByIdAsync(parkingSpotId);

            if (parkingSpot != null && parkingSpot.IsAvailable)
            {
                parkingSpot.Book(userId);
                await _parkingSpotRepository.UpdateAsync(parkingSpot);
                await _parkingGarageService.UpdateParkingSpotCount(parkingSpot.ParkingGarageId, true);
            }
        }

        public async Task<List<ParkingSpot>> getBookedParkingSpots(string userId)
        {
            var spec = new ParkingSpotSpecification(userId);
            List<ParkingSpot> parkingSpots = await _parkingSpotRepository.ListAsync(spec);
            return parkingSpots; 
        }

        public async Task CancelBookedParkingSpot(int parkingSpotId)
        {
            // Retrieve the parking spot from the database
            var parkingSpot = await _parkingSpotRepository.GetByIdAsync(parkingSpotId);

            if (parkingSpot != null && !parkingSpot.IsAvailable)
            {
                parkingSpot.Cancel();
                await _parkingSpotRepository.UpdateAsync(parkingSpot);
                await _parkingGarageService.UpdateParkingSpotCount(parkingSpot.ParkingGarageId, false);
            }
        }
    }
}