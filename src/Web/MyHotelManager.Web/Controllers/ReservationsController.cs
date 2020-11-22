﻿namespace MyHotelManager.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using MyHotelManager.Data.Models;
    using MyHotelManager.Services.Data;
    using MyHotelManager.Web.ViewModels.Reservations;
    using MyHotelManager.Web.ViewModels.Rooms;

    [Authorize]
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
            var user = await this.userManager.GetUserAsync(this.User);

            var viewModel = this.reservationsService.GetById<ReservationViewModel>(id);

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

        public IActionResult Index()
        {
            var userId = this.userManager.GetUserId(this.User);

            var viewModel = this.reservationsService.GetAll<ReservationViewModel>(userId);

            return this.View(viewModel);
        }

        public IActionResult Manager()
        {
            var userId = this.userManager.GetUserId(this.User);

            var viewModel = this.reservationsService.GetAll<ReservationManageViewModel>(userId);

            return this.View(viewModel);
        }

        public async Task<IActionResult> Create(int roomId, string arrivalDate, string returnDate)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            var room = this.roomsService.GetById<RoomModel>(roomId);

            if (room.HotelId != user.HotelId)
            {
                return this.RedirectToAction("DateSearch", "Rooms");
            }

            var viewModel = new ReservationCreateInputModel
            {
                RoomId = roomId,
                ArrivalDate = arrivalDate,
                ReturnDate = returnDate,
                RoomType = room.RoomType.Name,
                RoomNumber = room.Number,
                RoomPrice = room.Price,
                MaxChildCount = room.MaxChildCount,
                MaxAdultCount = room.MaxAdultCount,
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

            var user = await this.userManager.GetUserAsync(this.User);

            var room = this.roomsService.GetById<RoomModel>(input.RoomId);

            var availableRooms = this.roomsService.AvailableRooms<AvailableRoomViewModel>(
                user.Id,
                Convert.ToDateTime(input.ArrivalDate), Convert.ToDateTime(input.ReturnDate));

            if (!availableRooms.Any(x => x.Id == room.Id))
            {
                return this.RedirectToAction("DateSearch", "Rooms");
            }

            if (room.HotelId != user.HotelId)
            {
                return this.RedirectToAction("DateSearch", "Rooms");
            }

            if (room.MaxAdultCount < input.AdultCount || input.AdultCount < 1 || room.MaxChildCount < input.ChildCount || input.ChildCount < 0)
            {
                input.RoomId = room.Id;
                input.RoomType = room.RoomType.Name;
                input.RoomNumber = room.Number;
                input.RoomPrice = room.Price;
                input.MaxChildCount = room.MaxChildCount;
                input.MaxAdultCount = room.MaxAdultCount;
                return this.View(input);
            }

            await this.reservationsService.CreateAsync(
                input.GuestInfo.PhoneNumber,
                user.Id,
                input.RoomId,
                Convert.ToDateTime(input.ArrivalDate),
                Convert.ToDateTime(input.ReturnDate),
                input.AdultCount,
                input.ChildCount,
                input.GuestInfo.FirstName,
                input.GuestInfo.LastName,
                input.Description,
                input.AllPrice,
                input.HasBreakfast,
                input.HasLunch,
                input.HasDinner);

            return this.RedirectToAction("Manager", "Reservations");
        }

        public async Task<IActionResult> Update(string reservationId)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var reservation = this.reservationsService.GetById<ReservationUpdateViewModel>(reservationId);
            var room = this.roomsService.GetById<RoomModel>(reservation.RoomId);

            if (user.HotelId != room.HotelId)
            {
                return this.RedirectToAction("Manager", "Reservations");
            }

            var customPrice = 0.0M;

            if (room.Price * reservation.Nights != reservation.Price)
            {
                customPrice = reservation.Price / reservation.Nights;
            }

            var viewModel = new ReservationUpdateInputModel
            {
                Id = reservation.Id,
                RoomId = room.Id,
                OldRoomId = room.Id,
                RoomNumber = room.Number,
                GuestFirstName = reservation.Guests.OrderBy(x => x.CreatedOn).First().FirstName,
                GuestLastName = reservation.Guests.OrderBy(x => x.CreatedOn).First().LastName,
                GuestPhoneNumber = reservation.Guests.OrderBy(x => x.CreatedOn).First().PhoneNumber,
                ArrivalDate = reservation.ArrivalDate,
                ReturnDate = reservation.ReturnDate,
                AdultCount = reservation.AdultCount,
                ChildCount = reservation.ChildCount,
                MaxAdultCount = room.MaxAdultCount,
                MaxChildCount = room.MaxChildCount,
                RoomType = room.RoomType.Name,
                HasBreakfast = reservation.HasBreakfast,
                HasLunch = reservation.HasLunch,
                HasDinner = reservation.HasDinner,
                AllPrice = reservation.Price,
                RoomPrice = room.Price,
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

            var user = await this.userManager.GetUserAsync(this.User);

            var room = this.roomsService.GetById<RoomModel>(input.RoomId);

            if (room.HotelId != user.HotelId)
            {
                return this.RedirectToAction("Manager");
            }

            if (room.MaxAdultCount < input.AdultCount || input.AdultCount < 1 || room.MaxChildCount < input.ChildCount || input.ChildCount < 0)
            {
                return this.View(input);
            }

            await this.reservationsService.UpdateAsync(
                user.Id,
                input.Id,
                input.RoomId,
                input.ArrivalDate,
                input.ReturnDate,
                input.AdultCount,
                input.ChildCount,
                input.GuestFirstName,
                input.GuestLastName,
                input.GuestPhoneNumber,
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
            await this.reservationsService.Delete(reservationId, userId);

            return this.RedirectToAction("Manager");
        }
    }
}
