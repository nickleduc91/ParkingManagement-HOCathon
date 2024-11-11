using ApplicationCore.Interfaces;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Areas.Identity.Pages.Account.Manage
{
    public class ChangeCarDetailsModel : PageModel
    {
        private readonly UserManager<ParkingUser> _userManager;
        private readonly ILogger<ChangeCarDetailsModel> _logger;
        private readonly ICarRegisterFormService _carRegisterFormService;
        
        public ChangeCarDetailsModel(
            UserManager<ParkingUser> userManager,
            ILogger<ChangeCarDetailsModel> logger, ICarRegisterFormService carRegisterFormService)
        {
            _userManager = userManager;
            _logger = logger;
            _carRegisterFormService = carRegisterFormService;
        }

        [BindProperty]
        public CarDetailsModel CarDetails { get; set; }


        public class CarDetailsModel
        {
            public string localCarMakeModel { get; set; }
            public string localCarModel { get; set; }
            public string localCarLicenseModel { get; set; }
            public string localCarColourModel { get; set; }


        }

        public async Task OnGetAsync()
        {
            
            var user = await _userManager.GetUserAsync(User);
           
            //user id
            var userName = await _userManager.GetUserNameAsync(user);

            //To prefill the form with this data
            var hasCar = await _carRegisterFormService.HasCar(userName);
            
            if (hasCar)
            {
                var car = await _carRegisterFormService.getCar(userName);

                //CarDetails.localCarLicenseModel = car.LicensePlateNumber;
                //CarDetails.localCarModel = car.Model;
                //CarDetails.localCarMakeModel = car.Make;
                //CarDetails.localCarColourModel = car.Colour;

                ViewData["CarMake"] = car.Make; 
                ViewData["CarModel"] = car.Model; 
                ViewData["LicensePlates"] = car.LicensePlateNumber; 
                ViewData["CarColour"] = car.Colour;

            }
            


        }


        public async Task<IActionResult> OnPostAsync(string licensePlates, string carModel, string carMake, string carColour)
        {
            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            //user id
            var userName = await _userManager.GetUserNameAsync(user);

            await _carRegisterFormService.addCar(licensePlates, userName, carModel, carMake, carColour);

            return RedirectToPage();
        }
    }

}
