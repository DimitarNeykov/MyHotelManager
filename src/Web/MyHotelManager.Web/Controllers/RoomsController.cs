namespace MyHotelManager.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using MyHotelManager.Data.Models;
    using MyHotelManager.Services.Data;
    using MyHotelManager.Web.ViewModels.Rooms;

    public class RoomsController : BaseController
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

        [Authorize]
        public IActionResult AllRooms()
        {
            var userId = this.userManager.GetUserId(this.User);

            var viewModel = this.roomsService.GetAll<RoomViewModel>(userId);

            return this.View(viewModel);
        }

        [Authorize]
        public IActionResult AvailableRooms()
        {
            var viewModel = new AvailableRoomsViewModel()
            {
                From = null,
                To = null,
                Rooms = new List<RoomViewModel>(),
            };

            return this.View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public IActionResult AvailableRooms(AvailableRoomsViewModel input)
        {
            var userId = this.userManager.GetUserId(this.User);

            var rooms = this.roomsService.AvailableRooms<RoomViewModel>(userId, (DateTime)input.From, (DateTime)input.To);

            var viewModel = new AvailableRoomsViewModel
            {
                From = input.From,
                To = input.To,
                Rooms = rooms,
            };

            return this.View(viewModel);
        }

        [Authorize]
        public IActionResult Create()
        {
            var roomTypes = this.roomTypesService.GetAll<RoomTypeDropDownViewModel>();

            var viewModel = new RoomCreateInputModel
            {
                RoomTypes = roomTypes,
            };

            return this.View(viewModel);
        }

        [Authorize]
        [HttpPost]
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
                input.Number,
                input.RoomTypeId,
                input.Price,
                input.MaxAdultCount,
                input.MaxChildCount,
                input.Description,
                (int)user.HotelId);

            return this.RedirectToAction("Index", "Home");
        }
    }
}
