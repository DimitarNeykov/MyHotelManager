namespace MyHotelManager.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using MyHotelManager.Common;
    using MyHotelManager.Data.Models;
    using MyHotelManager.Services.Data;
    using MyHotelManager.Web.Infrastructure.Attributes;
    using MyHotelManager.Web.ViewModels.Restaurant;

    [AuthorizeRoles(new[] { GlobalConstants.ManagerRoleName, GlobalConstants.AdministratorRoleName, GlobalConstants.ReceptionistRoleName })]
    public class RestaurantController : Controller
    {
        private readonly IReservationsService reservationsService;
        private readonly UserManager<ApplicationUser> userManager;

        public RestaurantController(
            IReservationsService reservationsService,
            UserManager<ApplicationUser> userManager)
        {
            this.reservationsService = reservationsService;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Breakfast()
        {
            var user = await this.userManager.GetUserAsync(this.User);

            var viewModel = this.reservationsService.GetActiveReservationsWithBreakfast<ReservationViewModel>((int)user.HotelId);

            this.TempData["EatName"] = "Breakfasts";

            return this.View("Restaurant", viewModel);
        }

        public async Task<IActionResult> Lunch()
        {
            var user = await this.userManager.GetUserAsync(this.User);

            var viewModel = this.reservationsService.GetActiveReservationsWithLunch<ReservationViewModel>((int)user.HotelId);

            this.TempData["EatName"] = "Lunches";

            return this.View("Restaurant", viewModel);
        }

        public async Task<IActionResult> Dinner()
        {
            var user = await this.userManager.GetUserAsync(this.User);

            var viewModel = this.reservationsService.GetActiveReservationsWithDinner<ReservationViewModel>((int)user.HotelId);

            this.TempData["EatName"] = "Dinners";

            return this.View("Restaurant", viewModel);
        }
    }
}
