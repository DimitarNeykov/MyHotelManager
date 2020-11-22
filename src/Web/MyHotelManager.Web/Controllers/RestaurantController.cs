using System.Threading.Tasks;
using MyHotelManager.Web.ViewModels.Restaurant;

namespace MyHotelManager.Web.Controllers
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using MyHotelManager.Data.Models;
    using MyHotelManager.Services.Data;

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

            return this.View(viewModel);
        }

        public IActionResult Lunch()
        {
            return this.View();
        }

        public IActionResult Dinner()
        {
            return this.View();
        }
    }
}
