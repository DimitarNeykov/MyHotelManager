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
                user);

            await this.userManager.AddToRoleAsync(user, GlobalConstants.ManagerRoleName);

            return this.RedirectToAction("Index");
        }
    }
}
