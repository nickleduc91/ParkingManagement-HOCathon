namespace Web.ViewModels
{
    public class BookedSpotViewModel
    {
        public string ParkingGarageName{ get; set; }
        public int ParkingGarageId { get; set; }
        public ParkingSpotViewModel ParkingSpot { get; set; } = new ParkingSpotViewModel();
    }
}
