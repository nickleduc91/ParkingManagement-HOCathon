using Web.ViewModels;

namespace Web.Areas.Identity.Pages.Account.Manage
{
    public class MyBookingsViewModel
    {
        public List<BookedSpotViewModel> MyBookings { get; set; } = new List<BookedSpotViewModel>();
    }
}
