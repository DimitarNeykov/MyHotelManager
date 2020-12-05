namespace MyHotelManager.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using MyHotelManager.Data.Common.Repositories;
    using MyHotelManager.Data.Models;
    using MyHotelManager.Services.Mapping;

    public class RoomsService : IRoomsService
    {
        private readonly IDeletableEntityRepository<Room> roomRepository;
        private readonly IDeletableEntityRepository<Reservation> reservationRepository;

        public RoomsService(
            IDeletableEntityRepository<Room> roomRepository,
            IDeletableEntityRepository<Reservation> reservationRepository)
        {
            this.roomRepository = roomRepository;
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

        public IEnumerable<T> GetAll<T>(int hotelId)
        {
            var rooms = this.roomRepository
                .All()
                .Where(x => x.HotelId == hotelId)
                .OrderBy(x => x.Number)
                .To<T>()
                .ToList();

            return rooms;
        }

        public async Task<T> GetByIdAsync<T>(int id)
        {
            var room = await this.roomRepository
                .All()
                .Where(x => x.Id == id)
                .To<T>()
                .FirstOrDefaultAsync();

            return room;
        }

        public IEnumerable<T> AvailableRooms<T>(int hotelId, DateTime arrivalDate, DateTime returnDate)
        {
            var rooms = this.roomRepository
                .All()
                .Where(x => x.HotelId == hotelId && x.Reservations
                    .FirstOrDefault(r => returnDate > r.ArrivalDate &&
                                         arrivalDate < r.ReturnDate) == null)
                .OrderBy(x => x.Number)
                .To<T>()
                .ToList();

            return rooms;
        }

        public async Task<IEnumerable<T>> AvailableRoomsWithReservationRoomAsync<T>(int hotelId, DateTime arrivalDate, DateTime returnDate, string reservationId)
        {
            var reservation = await this.reservationRepository
                .All()
                .FirstOrDefaultAsync(x => x.Id == reservationId);

            var room = this.roomRepository
                .All()
                .Include(x => x.Reservations)
                .Include(x => x.RoomType)
                .OrderBy(x => x.Number)
                .ToList();

            room.First(x => x.Id == reservation.RoomId).Reservations.Remove(reservation);

            var rooms = room
                .Where(x => x.HotelId == hotelId && x.Reservations
                    .FirstOrDefault(r => returnDate > r.ArrivalDate &&
                                         arrivalDate < r.ReturnDate) == null)
                .ToList()
                .AsQueryable()
                .To<T>()
                .ToList();

            return rooms;
        }

        public async Task DeleteAsync(int roomId)
        {
            var room = await this.roomRepository
                .All()
                .FirstOrDefaultAsync(x => x.Id == roomId);

            this.roomRepository.Delete(room);

            await this.reservationRepository.SaveChangesAsync();
        }

        public async Task UpdateAsync(int roomId, int floor, string number, int roomTypeId, decimal price, int maxAdultCount, int maxChildCount, string description)
        {
            var room = await this.roomRepository
                .All()
                .FirstOrDefaultAsync(r => r.Id == roomId);

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
                .Include(x => x.Hotel)
                .AsEnumerable()
                .Where(x => x.HotelId == hotelId &&
                            x.Reservations.Any(r =>
                                (DateTime.Now.Date - r.ArrivalDate.Date).Days % x.Hotel.CleaningPerDays == 0 &&
                                r.ArrivalDate.Date < DateTime.Now.Date ||
                                r.ReturnDate.Date == DateTime.Now.Date))
                .AsQueryable()
                .OrderBy(x => x.Number)
                .To<T>()
                .ToList();

            return rooms;
        }
    }
}
