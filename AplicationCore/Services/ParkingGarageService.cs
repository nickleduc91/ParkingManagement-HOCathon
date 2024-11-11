using AplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeoCoordinatePortable;
using System.Runtime.ConstrainedExecution;

namespace ApplicationCore.Services
{
    public class ParkingGarageService : IParkingGarageService
    {
        private readonly IRepository<ParkingGarage> _parkingGarageRepository;

        public ParkingGarageService(IRepository<ParkingGarage> parkingGarageRepository)
        {
            _parkingGarageRepository = parkingGarageRepository;
        }

        public async Task<ParkingGarage> AddParkingGarage(string name, string address, double longitude, double latitude, int numSpots, string environment, string ownerId, string photoURL)
        {
            bool isOutside = environment == "outdoor" ? true : false;
            ParkingGarage garage = new(name, longitude, latitude, address, numSpots, numSpots, 5, isOutside, ownerId, photoURL);
            await _parkingGarageRepository.AddAsync(garage);

            for (int i = 0; i < garage.NumParkingSpots; i++)
            {
                garage.AddParkingSpot("Default Directions", i + 1);
            }
            await _parkingGarageRepository.UpdateAsync(garage);
            return garage;
        }

        public async Task<List<ParkingGarage>> getClosestParkingGarages(double latitude, double longitude)
        {
            List<ParkingGarage> allGarages = await _parkingGarageRepository.GetAllAsync(); 

            var coord = new GeoCoordinate(latitude, longitude);
            // Bubble Sort
            for (int i = 0; i < allGarages.Count - 1; i++)
            {
                for (int j = 0; j < allGarages.Count - 1 - i; j++)
                {
                    var distance1 = new GeoCoordinate(allGarages[j].Latitude, allGarages[j].Longitude).GetDistanceTo(coord);
                    var distance2 = new GeoCoordinate(allGarages[j + 1].Latitude, allGarages[j + 1].Longitude).GetDistanceTo(coord);

                    if (distance1 > distance2)
                    {
                        var temp = allGarages[j];
                        allGarages[j] = allGarages[j + 1];
                        allGarages[j + 1] = temp;
                    }
                }
            }

            Console.WriteLine("Done");
            return allGarages; 
        }

        public List<double> getDistancesFromUser(double userLatitude, double userLongitude, List<ParkingGarage> allGarages)
        {
            var userCoord = new GeoCoordinate(userLatitude, userLongitude);

            var distancesInMeters = allGarages
                .Select(garage => new GeoCoordinate(garage.Latitude, garage.Longitude).GetDistanceTo(userCoord))
                .ToList();

            // Convert distances from meters to kilometers
            var distancesInKilometers = distancesInMeters.Select(distance => Math.Round(distance / 1000, 2)).ToList();

            return distancesInKilometers;
        }

        public async Task<ParkingGarage?> getParkingGarage(int garageId)
        {
            var spec = new ParkingGarageSpecification(garageId);
            var garage = await _parkingGarageRepository.FirstOrDefaultAsync(spec);
            if(garage == null)
            {
                Console.WriteLine($"Garage with ID {garageId} not found.");
                return null;
            }
            return garage;
        }

        public async Task<List<ParkingGarage>> getOwnedParkingGarages(string userId)
        {
            var spec = new ParkingGarageSpecification(userId);
            List<ParkingGarage> parkingGarages = await _parkingGarageRepository.ListAsync(spec);
            return parkingGarages;
        }

        public async Task RemoveGarage(int garageId)
        {
            // Retrieve the parking spot from the database
            var garage = await _parkingGarageRepository.GetByIdAsync(garageId);

            if (garage != null)
            {
                await _parkingGarageRepository.DeleteAsync(garage);
            }
        }

        public async Task UpdateParkingSpotCount(int garageId, bool isBook)
        {
            // Retrieve the parking spot from the database
            var garage = await _parkingGarageRepository.GetByIdAsync(garageId);
            if (garage.NumAvailableParkingSpots > 0)
            {
                if(isBook)
                {
                    garage.NumAvailableParkingSpots -= 1;
                }
                else
                {
                    garage.NumAvailableParkingSpots += 1;
                }
            }
            await _parkingGarageRepository.UpdateAsync(garage);
        }
    }
}

