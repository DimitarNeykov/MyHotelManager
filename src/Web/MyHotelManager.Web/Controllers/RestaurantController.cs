using System.Threading.Tasks;
using AutoMapper.Internal;
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
            viewModel.ForAll(x => x.EatName = nameof(this.Breakfast));

            return this.View("Restaurant", viewModel);
        }

        public async Task<IActionResult> Lunch()
        {
            var user = await this.userManager.GetUserAsync(this.User);

            var viewModel = this.reservationsService.GetActiveReservationsWithLunch<ReservationViewModel>((int)user.HotelId);
            viewModel.ForAll(x => x.EatName = nameof(this.Lunch));

            return this.View("Restaurant", viewModel);
        }

        public async Task<IActionResult> Dinner()
        {
            var user = await this.userManager.GetUserAsync(this.User);

            var viewModel = this.reservationsService.GetActiveReservationsWithDinner<ReservationViewModel>((int)user.HotelId);
            viewModel.ForAll(x => x.EatName = nameof(this.Dinner));

            return this.View("Restaurant", viewModel);
        }
    }
}
