using AplicationCore.Entities;
using ApplicationCore.Interfaces;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Web.Areas.Identity.Pages.Account.Manage;
using Web.ViewModels;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICarRegisterFormService _carRegisterFormService;
        private readonly UserManager<ParkingUser> _userManager;


        public HomeController(ILogger<HomeController> logger, UserManager<ParkingUser> userManager, ICarRegisterFormService carRegisterFormService)
        {
            _logger = logger;
            _userManager = userManager;
            _carRegisterFormService = carRegisterFormService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult CarRegisterForm()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string licensePlates, string carModel, string carMake, string carColour)
        {

            var user = await _userManager.GetUserAsync(User);
            var userName = await _userManager.GetUserNameAsync(user);
            await _carRegisterFormService.addCar(licensePlates, userName,carModel, carMake, carColour);

            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        

    }
}
