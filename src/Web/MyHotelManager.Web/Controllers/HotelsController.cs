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
        private readonly IApplicationUsersService usersService;

        public HotelsController(
            UserManager<ApplicationUser> userManager,
            IHotelsService hotelsService,
            ICitiesService citiesService,
            IStarsService starsService,
            ICompaniesService companiesService,
            IApplicationUsersService usersService)
        {
            this.userManager = userManager;
            this.hotelsService = hotelsService;
            this.citiesService = citiesService;
            this.starsService = starsService;
            this.companiesService = companiesService;
            this.usersService = usersService;
        }

        [Authorize]
        public async Task<IActionResult> ById(int id)
        {
            var hotelViewModel = this.hotelsService.GetById<HotelViewModel>(id);

            var user = await this.userManager.GetUserAsync(this.User);

            if (hotelViewModel == null)
            {
                return this.NotFound();
            }

            if (user.UsersHotels.Any(x => x.HotelId == id))
            {
                await this.usersService.UpdateSelectedHotel(user.Id, id);
                return this.View(hotelViewModel);
            }

            return this.NotFound();
        }

        [Authorize]
        public IActionResult Index()
        {
            var userId = this.userManager.GetUserId(this.User);

            var hotelViewModel = this.hotelsService.GetByUserId<HotelViewModel>(userId);

            return this.View(hotelViewModel);
        }

        [Authorize]
        public IActionResult Create()
        {
            var userId = this.userManager.GetUserId(this.User);

            var companies = this.companiesService.GetAllByUserId<CompanyDropDownViewModel>(userId);

            if (!companies.Any())
            {
                return this.Redirect("https://localhost:44319/Companies/Create");
            }

            var cities = this.citiesService.GetAll<CityDropDownViewModel>();
            var stars = this.starsService.GetAll<StarsDropDownViewModel>();

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

            var hotelId = await this.hotelsService.CreateAsync(
                input.Name,
                input.CityId,
                input.Address,
                input.StarsId,
                input.CompanyId,
                user,
                input.ImgUrl);

            await this.usersService.UpdateSelectedHotel(user.Id, hotelId);

            return this.RedirectToAction("Index", "Home");
        }
    }
}
