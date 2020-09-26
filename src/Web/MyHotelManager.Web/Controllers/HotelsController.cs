namespace MyHotelManager.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using MyHotelManager.Data.Models;
    using MyHotelManager.Services.Data;
    using MyHotelManager.Web.ViewModels.Hotels;

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

        [Authorize]
        public IActionResult Create()
        {
            var userId = this.userManager.GetUserId(this.User);

            var cities = this.citiesService.GetAll<CityDropDownViewModel>();
            var stars = this.starsService.GetAll<StarsDropDownViewModel>();
            var companies = this.companiesService.GetAllByUserId<CompanyDropDownViewModel>(userId);

            var viewModel = new HotelCreateInputModel
            {
                Cities = cities,
                Stars = stars,
                Companies = companies,
            };

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(HotelCreateInputModel input)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.hotelsService.CreateAsync(
                input.Name,
                input.CityId,
                input.Address,
                input.StarsId,
                input.CompanyId,
                user);

            return this.Redirect("https://localhost:44319");
        }
    }
}
