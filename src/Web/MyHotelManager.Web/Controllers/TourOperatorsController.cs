namespace MyHotelManager.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using MyHotelManager.Common;
    using MyHotelManager.Data.Models;
    using MyHotelManager.Services.Data.DTOs.TourOperators;
    using MyHotelManager.Services.Data.Interfaces;
    using MyHotelManager.Web.Infrastructure.Attributes;
    using MyHotelManager.Web.ViewModels.TourOperators;

    public class TourOperatorsController : Controller
    {
        private readonly ITourOperatorsService tourOperatorsService;
        private readonly UserManager<ApplicationUser> userManager;

        public TourOperatorsController(ITourOperatorsService tourOperatorsService, UserManager<ApplicationUser> userManager)
        {
            this.tourOperatorsService = tourOperatorsService;
            this.userManager = userManager;
        }

        [AuthorizeRoles(new[] { GlobalConstants.ReceptionistRoleName, GlobalConstants.ManagerRoleName })]
        public async Task<IActionResult> Index()
        {
            var user = await this.userManager.GetUserAsync(this.User);

            var viewModel = this.tourOperatorsService.GetAllByHotelId<TourOperatorViewModel>((int)user.HotelId);

            return this.View(viewModel);
        }

        [AuthorizeRoles(GlobalConstants.ManagerRoleName)]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [AuthorizeRoles(GlobalConstants.ManagerRoleName)]
        public async Task<IActionResult> Create(TourOperatorCreateInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var user = await this.userManager.GetUserAsync(this.User);

            var dto = new TourOperatorCreateDto
            {
                Name = input.Name,
                Agent = new TourOperatorAgentCreateDto
                {
                    FirstName = input.Agent.FirstName,
                    LastName = input.Agent.LastName,
                    Email = input.Agent.Email,
                    PhoneNumber = input.Agent.PhoneNumber,
                },
                Company = new TourOperatorCompanyCreateDto
                {
                    Name = input.Company.Name,
                    Bulstat = input.Company.Bulstat,
                    Email = input.Company.Email,
                    PhoneNumber = input.Company.PhoneNumber,
                },
                HotelId = (int)user.HotelId,
            };

            await this.tourOperatorsService.CreateAsync(dto);

            return this.RedirectToAction("Index");
        }

        [AuthorizeRoles(GlobalConstants.ManagerRoleName)]
        public IActionResult Edit(int tourOperatorId)
        {
            var viewModel = this.tourOperatorsService.GetById<TourOperatorEditInputModel>(tourOperatorId);

            return this.View(viewModel);
        }

        [HttpPost]
        [AuthorizeRoles(GlobalConstants.ManagerRoleName)]
        public async Task<IActionResult> Edit(TourOperatorEditInputModel input)
        {
            var userId = this.userManager.GetUserId(this.User);
            var user = this.userManager.Users
                .Include(x => x.Hotel)
                .ThenInclude(x => x.TourOperators)
                .First(x => x.Id == userId);

            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            if (user.Hotel.TourOperators.All(x => x.Id != input.Id))
            {
                return this.RedirectToAction("Index");
            }

            var dto = new TourOperatorEditDto
            {
                Id = input.Id,
                Name = input.Name,
                Agent = new TourOperatorAgentEditDto
                {
                    FirstName = input.Agent.FirstName,
                    LastName = input.Agent.LastName,
                    Email = input.Agent.Email,
                    PhoneNumber = input.Agent.PhoneNumber,
                },
                Company = new TourOperatorCompanyEditDto
                {
                    Name = input.Company.Name,
                    Bulstat = input.Company.Bulstat,
                    Email = input.Company.Email,
                    PhoneNumber = input.Company.PhoneNumber,
                },
            };

            await this.tourOperatorsService.EditAsync(dto);

            return this.RedirectToAction("Index");
        }

        [AuthorizeRoles(GlobalConstants.ManagerRoleName)]
        public async Task<IActionResult> Delete(int tourOperatorId)
        {
            var userId = this.userManager.GetUserId(this.User);
            var user = this.userManager.Users
                .Include(x => x.Hotel)
                .ThenInclude(x => x.TourOperators)
                .First(x => x.Id == userId);

            if (user.Hotel.TourOperators.All(x => x.Id != tourOperatorId))
            {
                return this.RedirectToAction("Index");
            }

            await this.tourOperatorsService.DeleteAsync(tourOperatorId);

            return this.RedirectToAction("Index");
        }
    }
}
