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
            if (!await this.IsValidReservation(id))
            {
                return this.RedirectToAction("Manager", "Reservations");
            }

            var viewModel = await this.reservationsService.GetByIdAsync<ReservationDetailsViewModel>(id);

            viewModel.CustomPrice = this.CalculateCustomPrice(viewModel.Room.Price, viewModel.Nights, viewModel.Price);

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
            if (!await this.IsValidRoom(DateTime.Parse(arrivalDate), DateTime.Parse(returnDate), roomId))
            {
                return this.RedirectToAction("Manager");
            }

            var room = await this.roomsService.GetByIdAsync<RoomViewModel>(roomId);

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
            if (!await this.IsValidRoom(DateTime.Parse(input.ArrivalDate), DateTime.Parse(input.ReturnDate), input.Room.Id))
            {
                return this.RedirectToAction("Manager");
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var userId = this.userManager.GetUserId(this.User);

            if (input.Room.MaxAdultCount < input.AdultCount || input.AdultCount < 1)
            {
                input.AdultCount = null;
                return this.View(input);
            }

            if (input.Room.MaxChildCount < input.ChildCount || input.ChildCount < 0)
            {
                input.ChildCount = null;
                return this.View(input);
            }

            await this.reservationsService.CreateAsync(
                input.ReservedByGuest.PhoneNumber,
                userId,
                input.Room.Id,
                Convert.ToDateTime(input.ArrivalDate),
                Convert.ToDateTime(input.ReturnDate),
                (int)input.AdultCount,
                (int)input.ChildCount,
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
            if (!await this.IsValidReservation(reservationId))
            {
                return this.NotFound();
            }

            var reservation = await this.reservationsService.GetByIdAsync<ReservationUpdateViewModel>(reservationId);

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
                CustomPrice = this.CalculateCustomPrice(reservation.Room.Price, reservation.Nights, reservation.Price),
            };
            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Update(ReservationUpdateInputModel input)
        {
            if (!await this.IsValidReservation(input.Id))
            {
                return this.NotFound();
            }

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

            if (input.Room.MaxAdultCount < input.AdultCount || input.AdultCount < 1)
            {
                input.AdultCount = null;
                return this.View(input);
            }

            if (input.Room.MaxChildCount < input.ChildCount || input.ChildCount < 0)
            {
                input.ChildCount = null;
                return this.View(input);
            }

            await this.reservationsService.UpdateAsync(
                user.Id,
                input.Id,
                input.Room.Id,
                input.ArrivalDate,
                input.ReturnDate,
                (int)input.AdultCount,
                (int)input.ChildCount,
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
            if (!await this.IsValidReservation(reservationId))
            {
                return this.NotFound();
            }

            var userId = this.userManager.GetUserId(this.User);
            await this.reservationsService.DeleteAsync(reservationId, userId);

            return this.RedirectToAction("Manager");
        }

        private async Task<bool> IsValidReservation(string reservationId)
        {
            var userId = this.userManager.GetUserId(this.User);
            var user = await this.userManager
                .Users
                .Include(u => u.Hotel)
                .ThenInclude(h => h.Rooms)
                .ThenInclude(r => r.Reservations)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (!user.Hotel.Rooms.Any(x => x.Reservations.Any(r => r.Id == reservationId)))
            {
                return false;
            }

            return true;
        }

        private async Task<bool> IsValidRoom(DateTime arrivalDate, DateTime returnDate, int roomId)
        {
            var userId = this.userManager.GetUserId(this.User);
            var user = await this.userManager
                .Users
                .Include(u => u.Hotel)
                .ThenInclude(h => h.Rooms)
                .FirstOrDefaultAsync(x => x.Id == userId);

            if (!user.Hotel.Rooms.Any(x => x.Id == roomId))
            {
                return false;
            }

            var availableRooms = this.roomsService
                .AvailableRooms<RoomViewModel>(
                    (int)user.HotelId,
                    arrivalDate,
                    returnDate);

            if (!availableRooms.Any(x => x.Id == roomId))
            {
                return false;
            }

            return true;
        }

        private decimal CalculateCustomPrice(decimal roomPrice, int nights, decimal price)
        {
            var customPrice = 0.0M;

            if (roomPrice * nights != price)
            {
                customPrice = price / nights;
            }

            return customPrice;
        }
    }
}
