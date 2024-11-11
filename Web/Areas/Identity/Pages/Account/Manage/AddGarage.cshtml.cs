using AplicationCore.Entities;
using ApplicationCore.Interfaces;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Areas.Identity.Pages.Account.Manage
{
    [Authorize(Roles = "Owner")]
    public class AddGarageModel : PageModel
    {
        private readonly UserManager<ParkingUser> _userManager;
        private readonly IParkingGarageService _parkingGarageService;
        public AddGarageModel(UserManager<ParkingUser> userManager, IParkingGarageService parkingGarageService)
        {
            _userManager = userManager;
            _parkingGarageService = parkingGarageService;
        }
        public async Task<IActionResult> OnPostAsync(string name, string address, double longitude, double latitude, int numSpots, string environment)
        {

            var user = await _userManager.GetUserAsync(User);
            var userName = await _userManager.GetUserNameAsync(user);
            var key = "KEY";
            var photoURL = $"https://maps.googleapis.com/maps/api/streetview?size=600x300&location={address}&key={key}";

            ParkingGarage garage = await _parkingGarageService.AddParkingGarage(name, address, longitude, latitude, numSpots, environment, userName, photoURL);

            return RedirectToAction("Details", "ParkingGarage", new { garageId = garage.Id });
        }
    }
}

