namespace Web.ViewModels
{
    public class ParkingGarageResultsViewModel
    {
        public List<ParkingGarageViewModel> ParkingGarages { get; set; } = new();
        public double SearchLong { get; set; }
        public double SearchLat { get; set; }
        public int Range { get; set; }
        public string Address { get; set; }

    }
}
