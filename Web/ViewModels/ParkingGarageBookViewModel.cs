using AplicationCore.Entities;

namespace Web.ViewModels
{
    public class ParkingGarageBookViewModel
    {
        public ParkingGarageDetailsViewModel Details { get; set; } = new();
        public ParkingSpotViewModel ParkingSpot { get; set; } = new();
    }
}
