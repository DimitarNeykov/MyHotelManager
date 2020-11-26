namespace MyHotelManager.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using MyHotelManager.Common;
    using MyHotelManager.Data.Models;
    using MyHotelManager.Services.Data;
    using MyHotelManager.Web.Infrastructure.Attributes;
    using MyHotelManager.Web.ViewModels.Hotels;

    [AuthorizeRoles(new[] { GlobalConstants.ManagerRoleName, GlobalConstants.AdministratorRoleName, GlobalConstants.ReceptionistRoleName })]
    public class HotelsController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IHotelsService hotelsService;
        private readonly ICitiesService citiesService;
        private readonly IStarsService starsService;
        private readonly ICompaniesService companiesService;

        public HotelsController(
            UserManager<ApplicationUser> userManager,
            IHotelsService hotelsService,
            ICitiesService citiesService,
            IStarsService starsService,
            ICompaniesService companiesService)
        {
            this.userManager = userManager;
            this.hotelsService = hotelsService;
            this.citiesService = citiesService;
            this.starsService = starsService;
            this.companiesService = companiesService;
        }

        public async Task<IActionResult> Index()
        {
            var user = await this.userManager.GetUserAsync(this.User);

            if (user.HotelId != null)
            {
                var hotelViewModel = this.hotelsService.GetById<HotelViewModel>((int)user.HotelId);
                return this.View(hotelViewModel);
            }

            return this.RedirectToAction("Create", "Hotels");
        }

        public async Task<IActionResult> Create()
        {
            var user = await this.userManager.GetUserAsync(this.User);

            var cities = this.citiesService.GetAll<CityDropDownViewModel>();
            var stars = this.starsService.GetAll<StarsDropDownViewModel>();

            if (user.HotelId != null)
            {
                return this.RedirectToAction("Index");
            }

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
                user,
                input.ImgUrl);

            await this.companiesService.CreateAsync(
                input.Company.Name,
                input.Company.Bulstat,
                input.Company.PhoneNumber,
                input.Company.Email,
                input.Company.CityId,
                input.Company.Address,
                (int)user.HotelId);

            await this.userManager.AddToRoleAsync(user, GlobalConstants.ManagerRoleName);

            return this.RedirectToAction("Index");
        }

        public async Task<IActionResult> Manager()
        {
            var user = await this.userManager.GetUserAsync(this.User);

            if (user.HotelId == null)
            {
                return this.NotFound();
            }

            var viewModel = this.hotelsService.GetById<HotelViewModel>((int)user.HotelId);

            return this.View(viewModel);
        }

        public async Task<IActionResult> Edit()
        {
            var user = await this.userManager.GetUserAsync(this.User);

            if (user.HotelId == null)
            {
                return this.NotFound();
            }

            var hotel = this.hotelsService.GetById<HotelEditViewModel>((int)user.HotelId);

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
