namespace MyHotelManager.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using MyHotelManager.Common;
    using MyHotelManager.Data.Models;
    using MyHotelManager.Services.Data;
    using MyHotelManager.Web.Infrastructure.Attributes;
    using MyHotelManager.Web.ViewModels.Hotels;
    using MyHotelManager.Web.ViewModels.Reservations;

    public class HotelsController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IHotelsService hotelsService;
        private readonly ICitiesService citiesService;
        private readonly IStarsService starsService;
        private readonly IRoomsService roomsService;

        public HotelsController(
            UserManager<ApplicationUser> userManager,
            IHotelsService hotelsService,
            ICitiesService citiesService,
            IStarsService starsService,
            IRoomsService roomsService)
        {
            this.userManager = userManager;
            this.hotelsService = hotelsService;
            this.citiesService = citiesService;
            this.starsService = starsService;
            this.roomsService = roomsService;
        }

        [AuthorizeRoles(new[] { GlobalConstants.ManagerRoleName, GlobalConstants.AdministratorRoleName, GlobalConstants.ReceptionistRoleName })]
        public async Task<IActionResult> Index()
        {
            var user = await this.userManager.GetUserAsync(this.User);

            if (user.HotelId != null)
            {
                var hotelViewModel = await this.hotelsService.GetByIdWithDeletedAsync<HotelDashboardViewModel>((int)user.HotelId);

                var availableRoomsCount =
                    this.roomsService.AvailableRooms<RoomViewModel>((int)user.HotelId, DateTime.UtcNow.Date, DateTime.UtcNow.Date.AddDays(1)).Count();

                hotelViewModel.AvailableRoomsCount = availableRoomsCount;
                return this.View(hotelViewModel);
            }

            return this.RedirectToAction("Create", "Hotels");
        }

        public async Task<IActionResult> Create()
        {
            var user = await this.userManager.GetUserAsync(this.User);

            if (user.HotelId != null)
            {
                return this.RedirectToAction("Index");
            }

            var cities = this.citiesService.GetAll<CityDropDownViewModel>();
            var stars = this.starsService.GetAll<StarsDropDownViewModel>();

            var viewModel = new HotelCreateInputModel
            {
                Cities = cities,
                Stars = stars,
            };

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(HotelCreateInputModel input)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            if (user.HotelId != null)
            {
                return this.RedirectToAction("Index");
            }

            if (!this.ModelState.IsValid)
            {
                input.Cities = this.citiesService.GetAll<CityDropDownViewModel>();
                input.Stars = this.starsService.GetAll<StarsDropDownViewModel>();
                return this.View(input);
            }

            await this.hotelsService.CreateAsync(
                input.Name,
                input.CityId,
                input.Address,
                input.StarsId,
                input.CleaningPerDays,
                user);

            await this.userManager.AddToRoleAsync(user, GlobalConstants.ManagerRoleName);

            return this.RedirectToAction("Index");
        }

        [AuthorizeRoles(new[] { GlobalConstants.ManagerRoleName, GlobalConstants.AdministratorRoleName })]
        public async Task<IActionResult> Manager()
        {
            var user = await this.userManager.GetUserAsync(this.User);

            if (user.HotelId == null)
            {
                return this.NotFound();
            }

            var viewModel = await this.hotelsService.GetByIdAsync<HotelViewModel>((int)user.HotelId);

            return this.View(viewModel);
        }

        [AuthorizeRoles(new[] { GlobalConstants.ManagerRoleName, GlobalConstants.AdministratorRoleName })]
        public async Task<IActionResult> Edit()
        {
            var user = await this.userManager.GetUserAsync(this.User);

            if (user.HotelId == null)
            {
                return this.NotFound();
            }

            var hotel = await this.hotelsService.GetByIdAsync<HotelEditViewModel>((int)user.HotelId);

            var cities = this.citiesService.GetAll<CityDropDownViewModel>();
            var stars = this.starsService.GetAll<StarsDropDownViewModel>();

            var viewModel = new HotelEditInputModel
            {
                Name = hotel.Name,
                CityId = hotel.CityId,
                Address = hotel.Address,
                StarsId = hotel.StarsId,
                CleaningPerDays = hotel.CleaningPerDays,
                Cities = cities,
                Stars = stars,
            };

            return this.View(viewModel);
        }

        [HttpPost]
        [AuthorizeRoles(new[] { GlobalConstants.ManagerRoleName, GlobalConstants.AdministratorRoleName })]
        public async Task<IActionResult> Edit(HotelEditInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                var cities = this.citiesService.GetAll<CityDropDownViewModel>();
                var stars = this.starsService.GetAll<StarsDropDownViewModel>();

                input.Cities = cities;
                input.Stars = stars;

                return this.View(input);
            }

            var user = await this.userManager.GetUserAsync(this.User);

            if (user.HotelId == null)
            {
                return this.NotFound();
            }

            await this.hotelsService.UpdateAsync(
                (int)user.HotelId,
                input.Name,
                input.CityId,
                input.Address,
                input.StarsId,
                input.CleaningPerDays);

            return this.RedirectToAction("Manager");
        }
    }
}
