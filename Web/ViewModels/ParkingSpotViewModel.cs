namespace Web.ViewModels
{
    public class ParkingSpotViewModel
    {
        public string Directions { get; set; } = null!;
        public bool IsAvailable { get; set; }
        public string UserId { get; set; } = null!;
        public int ParkingSpotNumber { get; set; }
        public int Id { get; set; }
    }
}
