using AplicationCore.Entities;
using ApplicationCore.Interfaces;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.Interfaces;

namespace Web.Areas.Identity.Pages.Account.Manage
{
    public class MyBookingsModel : PageModel
    {
        private readonly IParkingSpotService _parkingSpotService;
        private readonly IParkingGarageViewModelService _parkingGarageViewModelService;
        private readonly UserManager<ParkingUser> _userManager;
        public MyBookingsModel(IParkingSpotService parkingSpotService, UserManager<ParkingUser> userManager, IParkingGarageService parkingGarageService, IParkingGarageViewModelService parkingGarageViewModelService)
        {
            _parkingSpotService = parkingSpotService;
            _userManager = userManager;
            _parkingGarageViewModelService = parkingGarageViewModelService;

        }

        public MyBookingsViewModel BookingsModel { get; set; } = new MyBookingsViewModel();
        public async Task OnGet()
        {
            var user = await _userManager.GetUserAsync(User);
            var userName = await _userManager.GetUserNameAsync(user);
            List<ParkingSpot> parkingSpots = await _parkingSpotService.getBookedParkingSpots(userName);
            BookingsModel = await _parkingGarageViewModelService.MapMyBookings(parkingSpots);
        }

        public async Task<IActionResult> OnPost(int id)
        {
            await _parkingSpotService.CancelBookedParkingSpot(id);
            return RedirectToPage();
        }
    }
}
