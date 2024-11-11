using AplicationCore.Entities;
using ApplicationCore.Interfaces;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Web.Interfaces;
using Web.ViewModels;



namespace Web.Controllers
{
    public class ParkingGarageController : Controller
    {
        private readonly IParkingGarageService _parkingGarageService;
        private readonly IParkingSpotService _parkingSpotService;
        private readonly IParkingGarageViewModelService _parkingGarageViewModelService;
        private readonly UserManager<ParkingUser> _userManager;
        private readonly ICarRegisterFormService _carRegisterFormService;
        public ParkingGarageController(IParkingGarageService parkingGarageService, IParkingGarageViewModelService parkingGarageViewModelService, IParkingSpotService parkingSpotService, UserManager<ParkingUser> userManager, ICarRegisterFormService carRegisterFormService)
        {
            _parkingGarageService = parkingGarageService;
            _parkingGarageViewModelService = parkingGarageViewModelService;
            _parkingSpotService = parkingSpotService;
            _userManager = userManager;
            _carRegisterFormService = carRegisterFormService;
        }
        public async Task<IActionResult> Results()
        {
            double latitude = Convert.ToDouble(Request.Query["latitude"]);
            double longitude = Convert.ToDouble(Request.Query["longitude"]);
            int range= Convert.ToInt32(Request.Query["range"]);
            string address = Request.Query["address"];

            List<ParkingGarage> allGarages = await _parkingGarageService.getClosestParkingGarages(latitude, longitude);
            List<double> distances = _parkingGarageService.getDistancesFromUser(latitude, longitude, allGarages);
            var filteredDistances = distances.Where(distance => distance <= range).ToList();
            var filteredGarages = allGarages
                .Where((garage, index) => distances[index] <= range)
                .ToList();
            //Mapping the data to the view model
            ParkingGarageResultsViewModel viewModel = _parkingGarageViewModelService.Map(filteredGarages, filteredDistances);
            viewModel.SearchLat = latitude;
            viewModel.SearchLong = longitude;
            viewModel.Range = range;
            viewModel.Address = address;
            return View(viewModel);
        }

        [HttpGet("/ParkingGarage/Details/{garageId}")]
        public async Task<IActionResult> Details(int garageId)
        {
            ParkingGarage garage = await _parkingGarageService.getParkingGarage(garageId);
            if(garage == null)
            {
                return NotFound();
            }
            ParkingGarageDetailsViewModel viewModel = _parkingGarageViewModelService.MapDetails(garage);
            return View(viewModel);
        }

        [HttpGet("/ParkingGarage/Book/{spotId}")]
        public async Task<IActionResult> Book(int spotId)
        {
            ParkingSpot parkingSpot = await _parkingSpotService.getParkingSpot(spotId);
            if (parkingSpot == null)
            {
                return NotFound();
            }
            ParkingGarage garage = await _parkingGarageService.getParkingGarage(parkingSpot.ParkingGarageId);
            ParkingGarageBookViewModel viewModel = await _parkingGarageViewModelService.MapBook(parkingSpot, garage);
            return View(viewModel);
        }

        [HttpPost("/ParkingGarage/ConfirmBook/{spotId}")]
        public async Task<IActionResult> ConfirmBook(int spotId)
        {
            var user = await _userManager.GetUserAsync(User);
            var userName = await _userManager.GetUserNameAsync(user);
            var hasCar = await _carRegisterFormService.HasCar(userName);
            if (!hasCar)
            {
                return LocalRedirect("/Identity/Account/Manage/ChangeCarDetails");
            }
            await _parkingSpotService.BookParkingSpot(spotId, userName);
            return LocalRedirect("/Identity/Account/Manage/MyBookings");
        }

    }

}
