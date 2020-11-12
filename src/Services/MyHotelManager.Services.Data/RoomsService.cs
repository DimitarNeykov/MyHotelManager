namespace MyHotelManager.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using MyHotelManager.Data.Common.Repositories;
    using MyHotelManager.Data.Models;
    using MyHotelManager.Services.Mapping;

    public class RoomsService : IRoomsService
    {
        private readonly IDeletableEntityRepository<Room> roomRepository;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IDeletableEntityRepository<Reservation> reservationRepository;

        public RoomsService(
            IDeletableEntityRepository<Room> roomRepository,
            UserManager<ApplicationUser> userManager,
            IDeletableEntityRepository<Reservation> reservationRepository)
        {
            this.roomRepository = roomRepository;
            this.userManager = userManager;
            this.reservationRepository = reservationRepository;
        }

        public async Task CreateAsync(string number, int roomTypeId, decimal price, int maxAdultCount, int maxChildCount, string description, int hotelId)
        {
            var room = new Room
            {
                Number = number,
                RoomTypeId = roomTypeId,
                MaxAdultCount = maxAdultCount,
                MaxChildCount = maxChildCount,
                Price = price,
                Description = description,
                HotelId = hotelId,
            };

            await this.roomRepository.AddAsync(room);
            await this.roomRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAll<T>(string userId)
        {
            var user = this.userManager.Users.First(x => x.Id == userId);

            var rooms = this.roomRepository
                .All()
                .Where(x => x.HotelId == user.HotelId)
                .OrderBy(x => x.Number)
                .To<T>()
                .ToList();

            return rooms;
        }

        public T GetById<T>(int id)
        {
            var room = this.roomRepository.All().Where(x => x.Id == id).To<T>().FirstOrDefault();

            return room;
        }

        public IEnumerable<T> AvailableRooms<T>(string userId, DateTime from, DateTime to)
        {
            var user = this.userManager.Users.First(x => x.Id == userId);

            var rooms = this.roomRepository
                .All()
                .Where(x => x.HotelId == user.HotelId && x.Reservations
                .FirstOrDefault(r => from <= r.ReturnDate && from >= r.ArrivalDate ||
                                     r.ArrivalDate <= to && r.ArrivalDate >= from
                                                         && r.CancelDate == null) == null)
                .OrderBy(x => x.Number)
                .To<T>()
                .ToList();

            return rooms;
        }

        public IEnumerable<T> AvailableRoomsWithReservationRoom<T>(string userId, DateTime from, DateTime to, string reservationId)
        {
            var user = this.userManager.Users.First(x => x.Id == userId);

            var reservation = this.reservationRepository.All().FirstOrDefault(x => x.Id == reservationId);

            var room = this.roomRepository
                .All()
                .Include(x => x.Reservations)
                .Include(x => x.RoomType)
                .OrderBy(x => x.Number)
                .ToList();

            room.First(x => x.Id == reservation.RoomId).Reservations.Remove(reservation);

            var rooms = room
                .Where(x => x.HotelId == user.HotelId && x.Reservations
                    .FirstOrDefault(r => from <= r.ReturnDate && from >= r.ArrivalDate ||
                                         r.ArrivalDate <= to && r.ArrivalDate >= from
                                                             && r.CancelDate == null) == null)
                .ToList()
                .AsQueryable()
                .To<T>()
                .ToList();

            return rooms;
        }
    }
}
