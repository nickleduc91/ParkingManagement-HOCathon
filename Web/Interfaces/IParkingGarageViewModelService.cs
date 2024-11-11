using AplicationCore.Entities;
using NuGet.ContentModel;
using Web.Areas.Identity.Pages.Account.Manage;
using Web.ViewModels;

namespace Web.Interfaces
{
    public interface IParkingGarageViewModelService
    {
        ParkingGarageResultsViewModel Map(List<ParkingGarage> parkingGarages, List<Double> distances);
        ParkingGarageDetailsViewModel MapDetails(ParkingGarage garage);
        Task<ParkingGarageBookViewModel> MapBook(ParkingSpot parkingSpot, ParkingGarage garage);

        Task<MyBookingsViewModel> MapMyBookings(List<ParkingSpot> parkingSpots);
    }
}
