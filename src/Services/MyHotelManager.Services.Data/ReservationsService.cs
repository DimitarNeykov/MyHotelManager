﻿using Microsoft.EntityFrameworkCore;

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

        public async Task CreateAsync(int roomId, DateTime arrivalDate, DateTime returnDate, int adultCount, int childCount, string firstName, string lastName, string description)
        {
            var reservation = new Reservation
            {
                RoomId = roomId,
                BookDate = DateTime.UtcNow,
                ArrivalDate = arrivalDate,
                ReturnDate = returnDate,
                AdultCount = adultCount,
                ChildCount = childCount,
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

        public async Task Delete(string reservationId)
        {
            var reservation = this.reservationRepository.All().FirstOrDefault(x => x.Id == reservationId);

            this.reservationRepository.Delete(reservation);

            await this.reservationRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAll<T>(string userId)
        {
            var user = this.userManager.Users.First(x => x.Id == userId);

            var reservations = this.reservationRepository
                .All()
                .Include(x => x.GuestsReservations)
                .ThenInclude(g => g.Guest)
                .Where(x => x.Room.HotelId == user.HotelId && x.GuestsReservations.First() != null)
                .OrderBy(x => x.ArrivalDate)
                .To<T>()
                .ToList();

            return reservations;
        }
    }
}
