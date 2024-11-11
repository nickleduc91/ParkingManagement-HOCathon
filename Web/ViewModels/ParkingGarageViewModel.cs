namespace Web.ViewModels
{
    public class ParkingGarageViewModel
    {
        public int Id { get; set; }
        public string? Address { get; set; }
        public int NumParkingSpots { get; set; }
        public int NumAvailableParkingSpots { get; set; }
        public int NumLevels { get; set; }
        public bool IsOutdoor { get; set; }
        public string? Name { get; set; }
        public double UserDistance { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string PhotoURL { get; set; }
    }
}
