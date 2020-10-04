namespace MyHotelManager.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using MyHotelManager.Data.Common.Repositories;
    using MyHotelManager.Data.Models;
    using MyHotelManager.Services.Mapping;

    public class ReservationsService : IReservationsService
    {
        private readonly IDeletableEntityRepository<Reservation> reservationRepository;
        private readonly UserManager<ApplicationUser> userManager;

        public ReservationsService(
            IDeletableEntityRepository<Reservation> reservationRepository,
            UserManager<ApplicationUser> userManager)
        {
            this.reservationRepository = reservationRepository;
            this.userManager = userManager;
        }

        public async Task CreateAsync(int roomId, DateTime arrivalDate, DateTime returnDate, string firstName, string lastName, string description)
        {
            var reservation = new Reservation
            {
                RoomId = roomId,
                BookDate = DateTime.UtcNow,
                ArrivalDate = arrivalDate,
                ReturnDate = returnDate,
                Description = description,
            };

            var guest = new Guest
            {
                FirstName = firstName,
                LastName = lastName,
            };

            var guestReservation = new GuestReservation
            {
                Reservation = reservation,
                Guest = guest,
            };

            reservation.GuestsReservations.Add(guestReservation);

            await this.reservationRepository.AddAsync(reservation);
            await this.reservationRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAll<T>(string userId)
        {
            var user = this.userManager.Users.First(x => x.Id == userId);

            var reservations = this.reservationRepository
                .All()
                .Where(x => x.Room.HotelId == user.HotelId)
                .OrderBy(x => x.ArrivalDate)
                .To<T>()
                .ToList();

            return reservations;
        }
    }
}
