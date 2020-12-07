﻿namespace MyHotelManager.Web.Controllers
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
    using MyHotelManager.Web.ViewModels.Rooms;

    [AuthorizeRoles(new[] { GlobalConstants.ManagerRoleName, GlobalConstants.AdministratorRoleName, GlobalConstants.ReceptionistRoleName })]
    public class RoomsController : Controller
    {
        private readonly IRoomsService roomsService;
        private readonly IRoomTypesService roomTypesService;
        private readonly UserManager<ApplicationUser> userManager;

        public RoomsController(
            IRoomsService roomsService,
            IRoomTypesService roomTypesService,
            UserManager<ApplicationUser> userManager)
        {
            this.roomsService = roomsService;
            this.roomTypesService = roomTypesService;
            this.userManager = userManager;
        }

        public async Task<IActionResult> AllRooms()
        {
            var user = await this.userManager.GetUserAsync(this.User);

            var viewModel = this.roomsService.GetAll<RoomViewModel>((int)user.HotelId);

            return this.View(viewModel);
        }

        public async Task<IActionResult> GetRoomByIdJson(int roomId)
        {
            var userId = this.userManager.GetUserId(this.User);

            var user = await this.userManager
                .Users
                .Include(u => u.Hotel)
                .ThenInclude(h => h.Rooms)
                .FirstOrDefaultAsync(u => u.Id == userId);

            var viewModel = await this.roomsService.GetByIdAsync<AvailableRoomViewModel>(roomId);

            if (!user.Hotel.Rooms.Any(r => r.Id == viewModel.Id))
            {
                return null;
            }

            return this.Json(viewModel);
        }

        public IActionResult DateSearch()
        {
            return this.View();
        }

        public async Task<IActionResult> AvailableRooms(DateTime? arrivalDate, DateTime? returnDate)
        {
            if (arrivalDate == null || returnDate == null)
            {
                return null;
            }

            if (returnDate.Value.Date <= DateTime.UtcNow.ToLocalTime().Date)
            {
                return null;
            }

            var user = await this.userManager.GetUserAsync(this.User);

            var viewModel = this.roomsService
                .AvailableRooms<RoomViewModel>(
                    (int)user.HotelId,
                    (DateTime)arrivalDate,
                    (DateTime)returnDate);

            foreach (var roomViewModel in viewModel)
            {
                roomViewModel.ArrivalDate = (DateTime)arrivalDate;
                roomViewModel.ReturnDate = (DateTime)returnDate;
            }

            return this.PartialView(viewModel);
        }

        public async Task<IActionResult> AvailableRoomsWithReservationRoom(DateTime? arrivalDate, DateTime? returnDate, string reservationId)
        {
            if (arrivalDate == null || returnDate == null)
            {
                return null;
            }

            if (returnDate.Value.Date <= DateTime.UtcNow.Date)
            {
                return null;
            }

            var user = await this.userManager.GetUserAsync(this.User);

            var viewModel =
               await this.roomsService.AvailableRoomsWithReservationRoomAsync<AvailableRoomViewModel>(
                   (int)user.HotelId,
                   (DateTime)arrivalDate,
                   (DateTime)returnDate,
                   reservationId);

            return this.PartialView(viewModel);
        }

        [AuthorizeRoles(new[] { GlobalConstants.ManagerRoleName, GlobalConstants.AdministratorRoleName })]
        public IActionResult Create()
        {
            var roomTypes = this.roomTypesService.GetAll<RoomTypeDropDownViewModel>();

            var viewModel = new RoomCreateInputModel
            {
                RoomTypes = roomTypes,
            };

            return this.View(viewModel);
        }

        [HttpPost]
        [AuthorizeRoles(new[] { GlobalConstants.ManagerRoleName, GlobalConstants.AdministratorRoleName })]
        public async Task<IActionResult> Create(RoomCreateInputModel input)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            var roomTypes = this.roomTypesService.GetAll<RoomTypeDropDownViewModel>();

            if (!this.ModelState.IsValid)
            {
                input.RoomTypes = roomTypes;
                return this.View(input);
            }

            await this.roomsService.CreateAsync(
                input.Floor,
                input.Number,
                input.RoomTypeId,
                input.Price,
                input.MaxAdultCount,
                input.MaxChildCount,
                input.Description,
                (int)user.HotelId);

            return this.RedirectToAction("Index", "Home");
        }

        [AuthorizeRoles(new[] { GlobalConstants.ManagerRoleName, GlobalConstants.AdministratorRoleName })]
        public async Task<IActionResult> Delete(int roomId)
        {
            var userId = this.userManager.GetUserId(this.User);

            var user = await this.userManager
                .Users
                .Include(u => u.Hotel)
                .ThenInclude(h => h.Rooms)
                .FirstOrDefaultAsync(u => u.Id == userId);

            var room = await this.roomsService.GetByIdAsync<RoomViewModel>(roomId);

            if (!user.Hotel.Rooms.Any(r => r.Id == room.Id))
            {
                return this.RedirectToAction("AllRooms");
            }

            await this.roomsService.DeleteAsync(roomId);

            return this.RedirectToAction("AllRooms");
        }

        [AuthorizeRoles(new[] { GlobalConstants.ManagerRoleName, GlobalConstants.AdministratorRoleName })]
        public async Task<IActionResult> Update(int roomId)
        {
            var userId = this.userManager.GetUserId(this.User);
            var user = await this.userManager
                .Users
                .Include(u => u.Hotel)
                .ThenInclude(h => h.Rooms)
                .FirstOrDefaultAsync(u => u.Id == userId);

            var roomTypes = this.roomTypesService.GetAll<RoomTypeDropDownViewModel>();
            var room = await this.roomsService.GetByIdAsync<RoomUpdateViewModel>(roomId);

            if (!user.Hotel.Rooms.Any(r => r.Id == room.Id))
            {
                return this.RedirectToAction("AllRooms");
            }

            var viewModel = new RoomUpdateInputModel
            {
                Floor = room.Floor,
                Id = room.Id,
                MaxAdultCount = room.MaxAdultCount,
                MaxChildCount = room.MaxChildCount,
                Number = room.Number,
                Price = room.Price,
                Description = room.Description,
                RoomTypeId = room.RoomTypeId,
                RoomTypes = roomTypes,
            };

            return this.View(viewModel);
        }

        [HttpPost]
        [AuthorizeRoles(new[] { GlobalConstants.ManagerRoleName, GlobalConstants.AdministratorRoleName })]
        public async Task<IActionResult> Update(RoomUpdateInputModel input)
        {
            var userId = this.userManager.GetUserId(this.User);
            var user = await this.userManager
                .Users
                .Include(u => u.Hotel)
                .ThenInclude(h => h.Rooms)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (!user.Hotel.Rooms.Any(r => r.Id == input.Id))
            {
                return this.RedirectToAction("AllRooms");
            }

            if (!this.ModelState.IsValid)
            {
                var roomTypes = this.roomTypesService.GetAll<RoomTypeDropDownViewModel>();
                input.RoomTypes = roomTypes;

                return this.View(input);
            }

            await this.roomsService.UpdateAsync(
                input.Id,
                input.Floor,
                input.Number,
                input.RoomTypeId,
                input.Price,
                input.MaxAdultCount,
                input.MaxChildCount,
                input.Description);

            return this.RedirectToAction("AllRooms");
        }
    }
}
