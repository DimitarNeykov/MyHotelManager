namespace MyHotelManager.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using MyHotelManager.Common;
    using MyHotelManager.Data.Models;
    using MyHotelManager.Services.Data;
    using MyHotelManager.Web.Infrastructure.Attributes;
    using MyHotelManager.Web.ViewModels.Companies;

    [AuthorizeRoles(new[] { GlobalConstants.AdministratorRoleName, GlobalConstants.ManagerRoleName })]
    public class CompaniesController : Controller
    {
        private readonly ICompaniesService companiesService;
        private readonly ICitiesService citiesService;
        private readonly UserManager<ApplicationUser> userManager;

        public CompaniesController(
            ICompaniesService companiesService,
            ICitiesService citiesService,
            UserManager<ApplicationUser> userManager)
        {
            this.companiesService = companiesService;
            this.citiesService = citiesService;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Create()
        {
            var userId = this.userManager.GetUserId(this.User);

            var user = await this.userManager.Users
                .Include(u => u.Hotel)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user.Hotel.CompanyId != null)
            {
                return this.NotFound();
            }

            var cities = this.citiesService.GetAll<CityDropDownViewModel>();

            var viewModel = new CompanyCreateInputModel
            {
                Cities = cities,
            };

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CompanyCreateInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                var cities = this.citiesService.GetAll<CityDropDownViewModel>();
                input.Cities = cities;
                return this.View(input);
            }

            var userId = this.userManager.GetUserId(this.User);

            var user = await this.userManager.Users
                .Include(u => u.Hotel)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user.HotelId == null || user.Hotel.CompanyId != null)
            {
                return this.NotFound();
            }

            await this.companiesService.CreateAsync(
                input.Name,
                input.Bulstat,
                input.PhoneNumber,
                input.Email,
                input.CityId,
                input.Address,
                (int)user.HotelId);

            return this.RedirectToAction("Manager", "Hotels");
        }

        public async Task<IActionResult> Edit()
        {
            var userId = this.userManager.GetUserId(this.User);

            var user = await this.userManager.Users
                .Include(u => u.Hotel)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user.Hotel.CompanyId == null)
            {
                return this.NotFound();
            }

            var company = await this.companiesService.GetByIdAsync<CompanyEditViewModel>((int)user.Hotel.CompanyId);

            var cities = this.citiesService.GetAll<CityDropDownViewModel>();

            var viewModel = new CompanyEditInputModel
            {
                Name = company.Name,
                Cities = cities,
                Address = company.Address,
                Bulstat = company.Bulstat,
                CityId = company.CityId,
                Email = company.Email,
                PhoneNumber = company.PhoneNumber,
            };

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CompanyEditInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                var cities = this.citiesService.GetAll<CityDropDownViewModel>();
                input.Cities = cities;
                return this.View(input);
            }

            var userId = this.userManager.GetUserId(this.User);

            var user = await this.userManager.Users
                .Include(u => u.Hotel)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user.HotelId == null || user.Hotel.CompanyId == null)
            {
                return this.NotFound();
            }

            await this.companiesService.EditAsync(
                (int)user.Hotel.CompanyId,
                (int)user.HotelId,
                input.Name,
                input.Bulstat,
                input.PhoneNumber,
                input.Email,
                input.CityId,
                input.Address);

            return this.RedirectToAction("Manager", "Hotels");
        }
    }
}
