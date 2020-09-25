namespace MyHotelManager.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using MyHotelManager.Data.Models;
    using MyHotelManager.Services.Data;
    using MyHotelManager.Web.ViewModels.Companies;

    public class CompaniesController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ICompanyService companyService;

        public CompaniesController(UserManager<ApplicationUser> userManager, ICompanyService companyService)
        {
            this.userManager = userManager;
            this.companyService = companyService;
        }

        [Authorize]
        public IActionResult Create()
        {
            var viewModel = new CompanyCreateInputModel();
            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CompanyCreateInputModel input)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.companyService.CreateAsync(
                    input.Name,
                    input.Bulstat,
                    input.PhoneNumber,
                    input.Email,
                    input.Address,
                    user.Id);

            return this.Redirect("https://localhost:44319");
        }
    }
}
