﻿namespace MyHotelManager.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using MyHotelManager.Common;
    using MyHotelManager.Data.Models;
    using MyHotelManager.Services.Data;
    using MyHotelManager.Web.Infrastructure.Attributes;
    using MyHotelManager.Web.ViewModels.Archive;

    [AuthorizeRoles(new[] { GlobalConstants.ManagerRoleName, GlobalConstants.AdministratorRoleName, GlobalConstants.ReceptionistRoleName })]
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

            var viewModel = await this.reservationsService.GetDeletedByIdAsync<ReservationViewModel>(id);

            if (user.HotelId != viewModel.Room.HotelId)
            {
                return this.RedirectToAction("Reservation");
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
