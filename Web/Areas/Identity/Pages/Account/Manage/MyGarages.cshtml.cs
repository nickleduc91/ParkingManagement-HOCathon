using AplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Services;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.Interfaces;
using Web.ViewModels;

namespace Web.Areas.Identity.Pages.Account.Manage
{
    [Authorize(Roles = "Owner")]
    public class MyGaragesModel : PageModel
    {
        private readonly IParkingGarageService _parkingGarageService;
        private readonly IParkingGarageViewModelService _parkingGarageViewModelService;
        private readonly UserManager<ParkingUser> _userManager;

        public MyGaragesModel(IParkingGarageService parkingGarageService, IParkingGarageViewModelService parkingGarageViewModelService, UserManager<ParkingUser> userManager)
        {
            _parkingGarageService = parkingGarageService;
            _parkingGarageViewModelService = parkingGarageViewModelService;
            _userManager = userManager;
        }

        public ParkingGarageResultsViewModel GaragesModel { get; set; } = new ParkingGarageResultsViewModel();
        public async Task OnGet()
        {
            var user = await _userManager.GetUserAsync(User);
            var userName = await _userManager.GetUserNameAsync(user);
            List<ParkingGarage> garages = await _parkingGarageService.getOwnedParkingGarages(userName);
            GaragesModel = _parkingGarageViewModelService.Map(garages, []);
        }

        public async Task<IActionResult> OnPost(int id)
        {
            await _parkingGarageService.RemoveGarage(id);
            return RedirectToPage();
        }
    }
}
