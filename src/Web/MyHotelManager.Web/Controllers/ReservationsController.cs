namespace MyHotelManager.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using MyHotelManager.Data.Models;
    using MyHotelManager.Services.Data;
    using MyHotelManager.Web.ViewModels.Reservations;

    public class ReservationsController : Controller
    {
        private readonly IReservationsService reservationsService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IRoomsService roomsService;

        public ReservationsController(
            IReservationsService reservationsService,
            UserManager<ApplicationUser> userManager,
            IRoomsService roomsService)
        {
            this.reservationsService = reservationsService;
            this.userManager = userManager;
            this.roomsService = roomsService;
        }

        public IActionResult Index()
        {
            var user = this.userManager.GetUserId(this.User);

            var viewModel = this.reservationsService.GetAll<ReservationViewModel>(user);

            return this.View(viewModel);
        }

        [Authorize]
        public IActionResult Create(int roomId)
        {
            var user = this.userManager.GetUserId(this.User);

            var viewModel = new ReservationCreateInputModel
            {
                RoomId = roomId,
            };

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(ReservationCreateInputModel input, int roomId)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.reservationsService.CreateAsync(
                roomId,
                input.ArrivalDate,
                input.ReturnDate,
                input.GuestInfo.FirstName,
                input.GuestInfo.LastName,
                input.Description);

            return this.RedirectToAction("Index", "Home");
        }
    }
}
