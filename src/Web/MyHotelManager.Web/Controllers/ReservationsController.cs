namespace MyHotelManager.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using MyHotelManager.Data.Models;
    using MyHotelManager.Services.Data;
    using MyHotelManager.Web.ViewModels.Reservations;
    using MyHotelManager.Web.ViewModels.Rooms;

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

        [Authorize]
        public IActionResult Index()
        {
            var user = this.userManager.GetUserId(this.User);

            var viewModel = this.reservationsService.GetAll<ReservationViewModel>(user);

            return this.View(viewModel);
        }

        [Authorize]
        public IActionResult Create(int roomId, string arrivalDate, string returnDate)
        {
            var user = this.userManager.GetUserId(this.User);

            var room = this.roomsService.GetById<RoomViewModel>(roomId);

            var viewModel = new ReservationCreateInputModel
            {
                RoomId = roomId,
                ArrivalDate = arrivalDate,
                ReturnDate = returnDate,
                RoomType = room.RoomType.Name,
                RoomNumber = room.Number,
                RoomPrice = room.Price,
            };

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(ReservationCreateInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var room = this.roomsService.GetById<RoomViewModel>(input.RoomId);

            decimal allPrice = 0.0M;

            if (input.CustomPrice > 0)
            {
                allPrice = input.CustomPrice * input.Nights;
            }
            else
            {
                allPrice = room.Price * input.Nights;
            }

            await this.reservationsService.CreateAsync(
                input.RoomId,
                Convert.ToDateTime(input.ArrivalDate),
                Convert.ToDateTime(input.ReturnDate),
                input.AdultCount,
                input.ChildCount,
                input.GuestInfo.FirstName,
                input.GuestInfo.LastName,
                input.Description,
                allPrice,
                input.HasBreakfast,
                input.HasLunch,
                input.HasDinner);

            return this.RedirectToAction("Index", "Home");
        }

        [Authorize]
        public IActionResult Update(string reservationId)
        {
            var reservation = this.reservationsService.GetById<ReservationUpdateViewModel>(reservationId);

            var viewModel = reservation;

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Update(ReservationUpdateViewModel input, int roomId)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            return this.RedirectToAction("Index", "Home");
        }

        [Authorize]
        public async Task<IActionResult> Delete(string reservationId)
        {
            await this.reservationsService.Delete(reservationId);

            return this.RedirectToAction("Index");
        }
    }
}
