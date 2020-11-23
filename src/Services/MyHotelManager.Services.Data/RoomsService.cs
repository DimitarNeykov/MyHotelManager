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

        public async Task CreateAsync(int floor, string number, int roomTypeId, decimal price, int maxAdultCount, int maxChildCount, string description, int hotelId)
        {
            var room = new Room
            {
                Floor = floor,
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

        public IEnumerable<T> AvailableRooms<T>(string userId, DateTime arrivalDate, DateTime returnDate)
        {
            var user = this.userManager.Users.First(x => x.Id == userId);

            var rooms = this.roomRepository
                .All()
                .Where(x => x.HotelId == user.HotelId && x.Reservations
                    .FirstOrDefault(r => returnDate > r.ArrivalDate && arrivalDate < r.ReturnDate) == null)
                .OrderBy(x => x.Number)
                .To<T>()
                .ToList();

            return rooms;
        }

        public IEnumerable<T> AvailableRoomsWithReservationRoom<T>(string userId, DateTime arrivalDate, DateTime returnDate, string reservationId)
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
                    .FirstOrDefault(r => returnDate > r.ArrivalDate && arrivalDate < r.ReturnDate) == null)
                .ToList()
                .AsQueryable()
                .To<T>()
                .ToList();

            return rooms;
        }

        public async Task Delete(int roomId)
        {
            var room = await this.roomRepository.All().FirstOrDefaultAsync(x => x.Id == roomId);

            this.roomRepository.Delete(room);

            await this.reservationRepository.SaveChangesAsync();
        }

        public async Task UpdateAsync(int roomId, int floor, string number, int roomTypeId, decimal price, int maxAdultCount, int maxChildCount, string description)
        {
            var room = this.roomRepository.All().First(r => r.Id == roomId);

            room.Floor = floor;
            room.Number = number;
            room.RoomTypeId = roomTypeId;
            room.Price = price;
            room.MaxAdultCount = maxAdultCount;
            room.MaxChildCount = maxChildCount;
            room.Description = description;

            await this.roomRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAllRoomsForCleaningToday<T>(int hotelId)
        {
            var rooms = this.roomRepository
                .All()
                .Include(x => x.Reservations)
                .Include(x => x.RoomType)
                .AsEnumerable()
                .Where(x => x.HotelId == hotelId && x.Reservations.Any(x => (DateTime.Now.Date - x.ArrivalDate.Date).Days % 3 == 0 && x.ArrivalDate.Date < DateTime.Now.Date || x.ReturnDate.Date == DateTime.Now.Date))
                .AsQueryable()
                .OrderBy(x => x.Number)
                .To<T>()
                .ToList();

            return rooms;
        }
    }
}
