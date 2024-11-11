using AplicationCore.Entities;
using ApplicationCore.Interfaces;
using NuGet.ContentModel;
using Web.Areas.Identity.Pages.Account.Manage;
using Web.Interfaces;
using Web.ViewModels;

namespace Web.Services
{
    public class ParkingGarageViewModelService : IParkingGarageViewModelService
    {
        private readonly IParkingGarageService _parkingGarageService;
        public ParkingGarageViewModelService(IParkingGarageService parkingGarageService)
        {
            _parkingGarageService = parkingGarageService;
        }

        public ParkingGarageResultsViewModel Map(List<ParkingGarage> parkingGarages, List<Double> distances)
        {
            var viewModels = new ParkingGarageResultsViewModel();

            for (int i = 0; i < parkingGarages.Count; i++)
            {
                var garage = parkingGarages[i];

                var viewModel = new ParkingGarageViewModel
                {
                    Id = garage.Id,
                    Address = garage.Address,
                    NumParkingSpots = garage.NumParkingSpots,
                    NumAvailableParkingSpots = garage.NumAvailableParkingSpots,
                    NumLevels = garage.NumLevels,
                    IsOutdoor = garage.IsOutdoor,
                    Name = garage.Name,
                    UserDistance = distances.Count > 0 ? distances[i] : 0,
                    Longitude = garage.Longitude,
                    Latitude = garage.Latitude
                };

                viewModels.ParkingGarages.Add(viewModel);
            }

            return viewModels;
        }

        public async Task<ParkingGarageBookViewModel> MapBook(ParkingSpot parkingSpot, ParkingGarage garage)
        {
            var viewModel = new ParkingGarageBookViewModel
            {
                ParkingSpot = new ParkingSpotViewModel()
                {
                    Directions = parkingSpot.Directions,
                    IsAvailable = parkingSpot.IsAvailable,
                    UserId = parkingSpot.UserId,
                    ParkingSpotNumber = parkingSpot.ParkingSpotNumber,
                    Id = parkingSpot.Id
                },
                Details = MapDetails(garage)
              
            };
            return viewModel;
        }

        public ParkingGarageDetailsViewModel MapDetails(ParkingGarage garage)
        {
            var viewModel = new ParkingGarageDetailsViewModel
            {
                ParkingGarage = new ParkingGarageViewModel()
                {
                    Id = garage.Id,
                    Address = garage.Address,
                    NumParkingSpots = garage.NumParkingSpots,
                    NumAvailableParkingSpots = garage.NumAvailableParkingSpots,
                    NumLevels = garage.NumLevels,
                    IsOutdoor = garage.IsOutdoor,
                    Name = garage.Name,
                    PhotoURL = garage.PhotoUrl,
                    Longitude = garage.Longitude,
                    Latitude = garage.Latitude,
                },
                ParkingSpots = garage.ParkingSpots.ToList(),
            };

            return viewModel;
        }

        public async Task<MyBookingsViewModel> MapMyBookings(List<ParkingSpot> parkingSpots)
        {
            var viewModel = new MyBookingsViewModel();
            foreach (var p in parkingSpots)
            {
                ParkingGarage garage = await _parkingGarageService.getParkingGarage(p.ParkingGarageId);
                var bookedSpotViewModel = new BookedSpotViewModel
                {
                    ParkingGarageName = garage.Name,
                    ParkingGarageId = p.ParkingGarageId,
                    ParkingSpot = new ParkingSpotViewModel
                    {
                        Id = p.Id,
                        Directions = p.Directions,
                        IsAvailable = p.IsAvailable,
                        UserId = p.UserId,
                        ParkingSpotNumber = p.ParkingSpotNumber
                    }
                };
                viewModel.MyBookings.Add(bookedSpotViewModel);
            }
            return viewModel;
        }
    }
}
