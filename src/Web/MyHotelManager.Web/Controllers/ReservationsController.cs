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
        public IActionResult Create()
        {
            var user = this.userManager.GetUserId(this.User);

            var rooms = this.roomsService.GetAll<RoomsDropDownViewModel>(user);

            var viewModel = new ReservationCreateInputModel
            {
                Rooms = rooms,
            };
            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(ReservationCreateInputModel input)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var room = this.roomsService.GetById<Room>(input.RoomId);

            if (room.HotelId != user.SelectedHotelId)
            {
                return this.NotFound();
            }

            await this.reservationsService.CreateAsync(
                input.RoomId,
                input.ArrivalDate,
                input.ReturnDate,
                input.GuestInfo.FirstName,
                input.GuestInfo.LastName,
                input.Description);

            return this.RedirectToAction("Index", "Home");
        }
    }
}
