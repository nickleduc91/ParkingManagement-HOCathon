using AplicationCore.Entities;

namespace Web.ViewModels
{
    public class ParkingGarageDetailsViewModel
    {
        public ParkingGarageViewModel ParkingGarage { get; set; } = new();
        public List<ParkingSpot> ParkingSpots { get; set; } = new();
    }
}
