namespace MyHotelManager.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using MyHotelManager.Common;
    using MyHotelManager.Data.Models;
    using MyHotelManager.Services.Data;
    using MyHotelManager.Web.Infrastructure.Attributes;
    using MyHotelManager.Web.ViewModels.Reservations;

    [AuthorizeRoles(new[] { GlobalConstants.ManagerRoleName, GlobalConstants.AdministratorRoleName, GlobalConstants.ReceptionistRoleName })]
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

        public async Task<IActionResult> Details(string id)
        {
            var userId = this.userManager.GetUserId(this.User);
            var user = await this.userManager
                .Users
                .Include(u => u.Hotel)
                .ThenInclude(h => h.Rooms)
                .FirstOrDefaultAsync(x => x.Id == userId);

            var viewModel = await this.reservationsService.GetByIdAsync<ReservationDetailsViewModel>(id);

            if (!user.Hotel.Rooms.Any(x => x.Id == viewModel.Room.Id))
            {
                return this.RedirectToAction("Manager", "Reservations");
            }

            var customPrice = 0.0M;

            if (viewModel.Room.Price * viewModel.Nights != viewModel.Price)
            {
                customPrice = viewModel.Price / viewModel.Nights;
            }

            viewModel.CustomPrice = customPrice;
            return this.View(viewModel);
        }

        public async Task<IActionResult> Index()
        {
            var user = await this.userManager.GetUserAsync(this.User);

            var viewModel = this.reservationsService.GetAll<ReservationViewModel>((int)user.HotelId);

            return this.View(viewModel);
        }

        public async Task<IActionResult> Manager()
        {
            var user = await this.userManager.GetUserAsync(this.User);

            var viewModel = this.reservationsService.GetAll<ReservationManageViewModel>((int)user.HotelId);

            return this.View(viewModel);
        }

        public async Task<IActionResult> Create(int roomId, string arrivalDate, string returnDate)
        {
            var userId = this.userManager.GetUserId(this.User);
            var user = await this.userManager
                .Users
                .Include(u => u.Hotel)
                .ThenInclude(h => h.Rooms)
                .FirstOrDefaultAsync(x => x.Id == userId);

            var room = await this.roomsService.GetByIdAsync<RoomViewModel>(roomId);

            if (!user.Hotel.Rooms.Any(x => x.Id == room.Id))
            {
                return this.RedirectToAction("Manager");
            }

            var viewModel = new ReservationCreateInputModel
            {
                Room = room,
                ArrivalDate = arrivalDate,
                ReturnDate = returnDate,
            };

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ReservationCreateInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var userId = this.userManager.GetUserId(this.User);
            var user = await this.userManager
                .Users
                .Include(u => u.Hotel)
                .ThenInclude(h => h.Rooms)
                .FirstOrDefaultAsync(x => x.Id == userId);

            var availableRooms = this.roomsService
                .AvailableRooms<RoomViewModel>(
                (int)user.HotelId,
                Convert.ToDateTime(input.ArrivalDate),
                Convert.ToDateTime(input.ReturnDate));

            if (!availableRooms.Any(x => x.Id == input.Room.Id))
            {
                return this.RedirectToAction("DateSearch", "Rooms");
            }

            if (!user.Hotel.Rooms.Any(x => x.Id == input.Room.Id))
            {
                return this.RedirectToAction("Manager");
            }

            if (input.Room.MaxAdultCount < input.AdultCount || input.AdultCount < 1 || input.Room.MaxChildCount < input.ChildCount || input.ChildCount < 0)
            {
                return this.View(input);
            }

            await this.reservationsService.CreateAsync(
                input.ReservedByGuest.PhoneNumber,
                user.Id,
                input.Room.Id,
                Convert.ToDateTime(input.ArrivalDate),
                Convert.ToDateTime(input.ReturnDate),
                input.AdultCount,
                input.ChildCount,
                input.ReservedByGuest.FirstName,
                input.ReservedByGuest.LastName,
                input.Description,
                input.AllPrice,
                input.HasBreakfast,
                input.HasLunch,
                input.HasDinner);

            return this.RedirectToAction("Manager", "Reservations");
        }

        public async Task<IActionResult> Update(string reservationId)
        {
            var userId = this.userManager.GetUserId(this.User);
            var user = await this.userManager
                .Users
                .Include(u => u.Hotel)
                .ThenInclude(h => h.Rooms)
                .FirstOrDefaultAsync(x => x.Id == userId);

            var reservation = await this.reservationsService.GetByIdAsync<ReservationUpdateViewModel>(reservationId);

            if (!user.Hotel.Rooms.Any(x => x.Id == reservation.Room.Id))
            {
                return this.RedirectToAction("Manager", "Reservations");
            }

            var customPrice = 0.0M;

            if (reservation.Room.Price * reservation.Nights != reservation.Price)
            {
                customPrice = reservation.Price / reservation.Nights;
            }

            var viewModel = new ReservationUpdateInputModel()
            {
                Id = reservation.Id,
                Room = reservation.Room,
                ReservedByGuest = new ReservedByGuestInputModel()
                {
                    FirstName = reservation.ReservedByGuest.FirstName,
                    LastName = reservation.ReservedByGuest.LastName,
                    PhoneNumber = reservation.ReservedByGuest.PhoneNumber,
                },
                ArrivalDate = reservation.ArrivalDate,
                ReturnDate = reservation.ReturnDate,
                AdultCount = reservation.AdultCount,
                ChildCount = reservation.ChildCount,
                HasBreakfast = reservation.HasBreakfast,
                HasLunch = reservation.HasLunch,
                HasDinner = reservation.HasDinner,
                AllPrice = reservation.Price,
                CustomPrice = customPrice,
            };
            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Update(ReservationUpdateInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var userId = this.userManager.GetUserId(this.User);
            var user = await this.userManager
                .Users
                .Include(u => u.Hotel)
                .ThenInclude(h => h.Rooms)
                .FirstOrDefaultAsync(x => x.Id == userId);

            if (!user.Hotel.Rooms.Any(x => x.Id == input.Room.Id))
            {
                return this.RedirectToAction("Manager");
            }

            if (input.Room.MaxAdultCount < input.AdultCount || input.AdultCount < 1 || input.Room.MaxChildCount < input.ChildCount || input.ChildCount < 0)
            {
                return this.View(input);
            }

            await this.reservationsService.UpdateAsync(
                user.Id,
                input.Id,
                input.Room.Id,
                input.ArrivalDate,
                input.ReturnDate,
                input.AdultCount,
                input.ChildCount,
                input.ReservedByGuest.FirstName,
                input.ReservedByGuest.LastName,
                input.ReservedByGuest.PhoneNumber,
                input.Description,
                input.AllPrice,
                input.HasBreakfast,
                input.HasLunch,
                input.HasDinner);

            return this.RedirectToAction("Manager");
        }

        public async Task<IActionResult> Delete(string reservationId)
        {
            var userId = this.userManager.GetUserId(this.User);
            await this.reservationsService.DeleteAsync(reservationId, userId);

            return this.RedirectToAction("Manager");
        }
    }
}
