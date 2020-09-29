namespace MyHotelManager.Services.Data
{
    using System;
    using System.Threading.Tasks;

    using MyHotelManager.Data.Common.Repositories;
    using MyHotelManager.Data.Models;

    public class ReservationsService : IReservationsService
    {
        private readonly IDeletableEntityRepository<Reservation> reservationRepository;

        public ReservationsService(IDeletableEntityRepository<Reservation> reservationRepository)
        {
            this.reservationRepository = reservationRepository;
        }

        public async Task CreateAsync(int roomId, DateTime bookDate, DateTime arrivalDate, DateTime returnDate, string firstName, string lastName, string description)
        {
            var reservation = new Reservation
            {
                RoomId = roomId,
                BookDate = bookDate,
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
    }
}
