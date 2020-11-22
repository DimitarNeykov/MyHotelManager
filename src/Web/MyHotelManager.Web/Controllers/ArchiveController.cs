using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using MyHotelManager.Data.Models;
using MyHotelManager.Services.Data;
using MyHotelManager.Web.ViewModels.Archive;

namespace MyHotelManager.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class ArchiveController : Controller
    {
        private readonly IReservationsService reservationsService;
        private readonly UserManager<ApplicationUser> userManager;

        public ArchiveController(IReservationsService reservationsService, UserManager<ApplicationUser> userManager)
        {
            this.reservationsService = reservationsService;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Reservation()
        {
            var user = await this.userManager.GetUserAsync(this.User);

            var reservations = this.reservationsService.GetAllDeleted<ReservationViewModel>((int)user.HotelId);
            return this.View(reservations);
        }

        public async Task<IActionResult> ReservationDetails(string id)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            var viewModel = this.reservationsService.GetDeletedById<ReservationViewModel>(id);

            if (user.HotelId != viewModel.Room.HotelId)
            {
                return this.RedirectToAction("Index");
            }

            var customPrice = 0.0M;

            if (viewModel.Room.Price * viewModel.Nights != viewModel.Price)
            {
                customPrice = viewModel.Price / viewModel.Nights;
            }

            viewModel.CustomPrice = customPrice;

            return this.View(viewModel);
        }
    }
}
